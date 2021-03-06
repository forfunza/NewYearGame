﻿using UnityEngine;
using System.Collections;
using EnergyBarToolkit;

public class GameEvent : Photon.MonoBehaviour
{

		public GameObject play1, play2;
		public GameObject hpbar, hpbar2;
		// Use this for initialization
		void Start ()
		{


				EnergyBarRenderer eg = gameObject.GetComponent<EnergyBarRenderer> ();
	

				if (PhotonNetwork.isMasterClient) {
			hpbar = PhotonNetwork.Instantiate ("HpBar",
			                          play1.transform.position, Quaternion.identity, 0);

						if (PhotonNetwork.player.customProperties ["Character"].ToString ().Equals ("Boy")) {
								PhotonNetwork.Instantiate ("First Person Boy Controller",
				                          play1.transform.position, Quaternion.identity, 0);
						} else {
								PhotonNetwork.Instantiate ("First Person Girl Controller",
				                          play1.transform.position, Quaternion.identity, 0);
						}

				} else {
						print (EnergyBarBase.GrowDirection.RightToLeft);

			hpbar2 = PhotonNetwork.Instantiate ("HpBar2",
			                          play2.transform.position, play2.transform.rotation, 0);
						if (PhotonNetwork.player.customProperties ["Character"].ToString ().Equals ("Boy")) {
								PhotonNetwork.Instantiate ("Second Boy Person Controller",
				                          play2.transform.position, play2.transform.rotation, 0);
						} else {
								PhotonNetwork.Instantiate ("Second Girl Person Controller",
				                          play2.transform.position, play2.transform.rotation, 0);
						}

				}

		}
		

//		 Update is called once per frame
		void Update ()
		{
	
		}
}
