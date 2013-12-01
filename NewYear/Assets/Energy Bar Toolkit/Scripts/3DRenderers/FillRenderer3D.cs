/*
* Energy Bar Toolkit by Mad Pixel Machine
* http://www.madpixelmachine.com
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace EnergyBarToolkit {
 
[ExecuteInEditMode]
[RequireComponent(typeof(EnergyBar))]
public class FillRenderer3D : EnergyBarBase {

    // ===========================================================
    // Constants
    // ===========================================================
    
    // how much depth values will be reserved for single energy bar
    private const int DepthSpace = 32;

    // ===========================================================
    // Fields
    // ===========================================================
    
    
    //
    // textures
    //
    public Texture2D textureBar;
    
    //
    // appearance
    //
    public ColorType textureBarColorType;
    public Color textureBarColor = Color.white;
    public Gradient textureBarGradient;
    
    public GrowDirection growDirection = GrowDirection.LeftToRight;
    
    public float radialOffset;
    public float radialLength = 1;
    
    // label
    public MadFont labelFont;
    public float labelScale = 32;
    MadText labelSprite;
    
    //
    // effect
    //
    
    // burn effect
    public bool effectBurn = false;                 // bar draining will display 'burn' effect
    public Texture2D effectBurnTextureBar;
    public Color effectBurnTextureBarColor = Color.red;
    private float burnDisplayValue;
    
    // blink effect
    public bool effectBlink = false;
    public float effectBlinkValue = 0.2f;
    public float effectBlinkRatePerSecond = 1f;
    public Color effectBlinkColor = new Color(1, 1, 1, 0);
    
    //
    // others
    //
    
    int lastRebuildHash;
    bool dirty;
    
    MadSprite spriteBar;
    MadSprite spriteBurnBar;
    
    // ===========================================================
    // Properties
    // ===========================================================
    
    float _burnDisplayValue;
    float ValueFBurn {
        get {
            EnergyBarCommons.SmoothDisplayValue(
                    ref _burnDisplayValue, ValueF2, effectSmoothChangeSpeed);
            _burnDisplayValue = Mathf.Max(_burnDisplayValue, ValueF2);
            return _burnDisplayValue;
        }
    }
    
    float _actualDisplayValue;
    float ValueF2 {
    
        get {
        
            if (effectBurn) {
                if (effectSmoothChange) {
                    // in burn mode smooth primary bar only when it's increasing
                    if (ValueF > _actualDisplayValue) {
                        EnergyBarCommons.SmoothDisplayValue(ref _actualDisplayValue, ValueF, effectSmoothChangeSpeed);
                    } else {
                        _actualDisplayValue = energyBar.ValueF;
                    }
                } else {
                    _actualDisplayValue = energyBar.ValueF;
                }
                
            } else {
                if (effectSmoothChange) {
                    EnergyBarCommons.SmoothDisplayValue(ref _actualDisplayValue, ValueF, effectSmoothChangeSpeed);
                } else {
                    _actualDisplayValue = energyBar.ValueF;
                }
            }
            
            return _actualDisplayValue;
        }
    }
    
    bool Blink {
        get; set;
    }
    
    // return current bar color based on color settings and effect
    float _effectBlinkAccum;
    Color BarColor {
        get {
            Color outColor = Color.white;
            
            if (growDirection == EnergyBarBase.GrowDirection.ColorChange) {
                outColor = textureBarGradient.Evaluate(energyBar.ValueF);
            } else {
                switch (textureBarColorType) {
                    case ColorType.Solid:
                        outColor = textureBarColor;
                        break;
                    case ColorType.Gradient:
                        outColor = textureBarGradient.Evaluate(energyBar.ValueF);
                        break;
                    default:
                        MadDebug.Assert(false, "Unkwnown option: " + textureBarColorType);
                        break;
                }
            }
            
            if (Blink) {
                outColor = effectBlinkColor;
            }
            
            return PremultiplyAlpha(outColor);
        }
    }
    
    Color BurnColor {
        get {
            Color outColor = effectBurnTextureBarColor;
            if (Blink) {
                outColor = new Color(0, 0, 0, 0);
            }
            
            return outColor;
        }
    }
                    
    // ===========================================================
    // Methods for/from SuperClass/Interfaces
    // ===========================================================
    
    public override Rect TexturesRect {
        get {
            if (spriteBar != null) {
                return spriteBar.GetBounds();
            } else {
                return new Rect();
            }
        }
    }

    // ===========================================================
    // Methods
    // ===========================================================
    
#if UNITY_EDITOR
    void OnDrawGizmos() {
    
        // Draw the gizmo
        Gizmos.matrix = transform.localToWorldMatrix;
        
        Gizmos.color = (UnityEditor.Selection.activeGameObject == gameObject)
            ? Color.green : new Color(1, 1, 1, 0.2f);
            
        var childSprites = MadTransform.FindChildren<MadSprite>(transform);
        Bounds totalBounds = new Bounds(Vector3.zero, Vector3.zero);
        bool totalBoundsSet = false;
        
        foreach (var sprite in childSprites) {
            Rect boundsRect = sprite.GetBounds();
            Bounds bounds = new Bounds(boundsRect.center, new Vector2(boundsRect.width, boundsRect.height));
            
            if (!totalBoundsSet) {
                totalBounds = bounds;
                totalBoundsSet = true;
            } else {
                totalBounds.Encapsulate(bounds);
            }
        }
        
            
        Gizmos.DrawWireCube(totalBounds.center, totalBounds.size);

        // Make the widget selectable
        Gizmos.color = Color.clear;
        Gizmos.DrawCube(totalBounds.center,
            new Vector3(totalBounds.size.x, totalBounds.size.y, 0.01f * (guiDepth + 1)));
    }
#endif

    void Update() {
        if (effectBlink) {
            Blink = EnergyBarCommons.Blink(
                ValueF, effectBlinkValue, effectBlinkRatePerSecond, ref _effectBlinkAccum);
        } else {
            Blink = false;
        }
    
        if (RebuildNeeded()) {
            Rebuild();
        }
        
        UpdateBar();
        UpdateLabel();
        
        if (spriteBar != null) {
            spriteBar.tint = BarColor;
        }
    }
    
    void UpdateBar() {
        if (effectBurn && spriteBurnBar != null) {
            spriteBurnBar.tint = BurnColor;
            spriteBurnBar.fillValue = ValueFBurn;
        }
        
        if (spriteBar != null) {
            spriteBar.tint = BarColor;
            spriteBar.fillValue = ValueF2;
        }
    }
    
    void UpdateLabel() {
        if (labelSprite == null) {
            return;
        }
        
        labelSprite.scale = labelScale;
        labelSprite.text = LabelFormatResolve(labelFormat);
        
        labelSprite.transform.localPosition = LabelPositionPixels;
        
        labelSprite.tint = labelColor;
    }
    
    void LateUpdate() {
        if (anchorObject != null) {
            transform.position = anchorObject.transform.position;
        }
    }
    
    bool RebuildNeeded() {
        if (dirty) {
            dirty = false;
            return true;
        }
        
        var hash = new MadHashCode();
        hash.Add(MadObject.TableHash(texturesBackground));
        hash.Add(textureBar);
        hash.Add(MadObject.TableHash(texturesForeground));
        hash.Add(guiDepth);
        hash.Add(growDirection);
        hash.Add(effectBurn);
        hash.Add(labelEnabled);
        hash.Add(labelFont);
    
        int hashNumber = hash.GetHashCode();
        
        if (hashNumber != lastRebuildHash) {
            lastRebuildHash = hashNumber;
            return true;
        } else {
            return false;
        }
    }
    
    void Rebuild() {
        labelSprite = null;
    
        var children = MadTransform.FindChildren<MadSprite>(transform);
        foreach (var child in children) {
            DestroyImmediate(child.gameObject);
        }
        
        int nextDepth = BuildTextures(texturesBackground, "bg_", guiDepth * DepthSpace);
        
        if (textureBar != null) {
        
            if (effectBurn) {
                spriteBurnBar = MadTransform.CreateChild<MadSprite>(transform, "bar_effect_burn");
#if !MAD_DEBUG
                spriteBurnBar.gameObject.hideFlags = HideFlags.HideInHierarchy;
#endif
                spriteBurnBar.guiDepth = nextDepth++;
                spriteBurnBar.texture = textureBar;
                
                spriteBurnBar.fillType = ToFillType(growDirection);
            }
        
            spriteBar = MadTransform.CreateChild<MadSprite>(transform, "bar");
#if !MAD_DEBUG
            spriteBar.gameObject.hideFlags = HideFlags.HideInHierarchy;
#endif
            spriteBar.guiDepth = nextDepth++;
            spriteBar.texture = textureBar;
            
            spriteBar.fillType = ToFillType(growDirection);
        }
        
        nextDepth = BuildTextures(texturesForeground, "fg_", nextDepth);
        
        // label
        if (labelEnabled && labelFont != null) {
            labelSprite = MadTransform.CreateChild<MadText>(transform, "label");
            labelSprite.font = labelFont;
            labelSprite.guiDepth = nextDepth++;
            
#if !MAD_DEBUG
                labelSprite.gameObject.hideFlags = HideFlags.HideInHierarchy;
#endif
        }
    }
    
    MadSprite.FillType ToFillType(GrowDirection growDirection) {
        switch (growDirection) {
            case GrowDirection.LeftToRight:
                return MadSprite.FillType.LeftToRight;
            case GrowDirection.RightToLeft:
                return MadSprite.FillType.RightToLeft;
            case GrowDirection.TopToBottom:
                return MadSprite.FillType.TopToBottom;
            case GrowDirection.BottomToTop:
                return MadSprite.FillType.BottomToTop;
            case GrowDirection.ExpandHorizontal:
                return MadSprite.FillType.ExpandHorizontal;
            case GrowDirection.ExpandVertical:
                return MadSprite.FillType.ExpandVertical;
            case GrowDirection.RadialCW:
                return MadSprite.FillType.RadialCW;
            case GrowDirection.RadialCCW:
                return MadSprite.FillType.RadialCCW;
            case GrowDirection.ColorChange:
                return MadSprite.FillType.None;
            default:
                MadDebug.Assert(false, "Unkwnown grow direction: " + growDirection);
                return MadSprite.FillType.None;
        }
    }
    
    int BuildTextures(Tex[] textures, string prefix, int startDepth) {
        
        int counter = 0;
        foreach (var texture in textures) {
            if (texture.texture == null) {
                continue;
            }
        
            string name = string.Format("{0}{1:D2}", prefix, counter + 1);
            var sprite = MadTransform.CreateChild<MadSprite>(transform, name);
#if !MAD_DEBUG
            sprite.gameObject.hideFlags = HideFlags.HideInHierarchy;
#endif
            
            sprite.guiDepth = startDepth + counter;
            sprite.texture = texture.texture;
            sprite.tint = texture.color;
            
            counter++;
        }
        
        return startDepth + counter;
    }
    
    // ===========================================================
    // Static Methods
    // ===========================================================

    // ===========================================================
    // Inner and Anonymous Classes
    // ===========================================================

}

} // namespace