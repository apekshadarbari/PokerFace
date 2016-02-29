using UnityEngine;
using System.Collections;
using NewtonVR;

public class Trigger_trial2 : NVRInteractableItem {

			
	private BetMore triggerButton;


			public override void UseButtonUp()
			{
				base.UseButtonUp();
				
		Debug.Log ("Trigger Button press detected");
		triggerButton.EndTurn ();
				


				
			}
		
	}