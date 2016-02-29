using UnityEngine;
using System.Collections;
using NewtonVR;

public class Trial_change_color : NVRHand {
	
			public bool CurrentState = true;
			public bool LastState = true;
			private bool FixedState = true;

			public Transform OnButton;
			public Renderer OnButtonRenderer;


			private Rigidbody Rigidbody;
			private float ForceMagic = 100f;

			private void Awake()
			{
				Rigidbody = this.GetComponent<Rigidbody>();
				SetColor (CurrentState);

			}

		
			private void Update()
			{
				LastState = CurrentState;
				CurrentState = FixedState;
			}

			private void SetColor(bool forState)
			{
				FixedState = forState;
				if (FixedState == true)
				{
					this.transform.localEulerAngles = Vector3.zero;
					OnButtonRenderer.material.color = Color.red;
					
				}
				else
				{
					this.transform.localEulerAngles = new Vector3(0, 0, -15);
					OnButtonRenderer.material.color = Color.white;
					
				}

				
			}
		}
	
