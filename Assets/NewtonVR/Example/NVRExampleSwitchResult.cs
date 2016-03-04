using System.Collections;
using UnityEngine;

namespace NewtonVR.Example
{
    public class NVRExampleSwitchResult : MonoBehaviour
    {
        public NVRSwitch Switch;

        private Light spotlight;

        private void Awake()
        {
            spotlight = this.GetComponent<Light>();
        }

        private void Update()
        {
            spotlight.enabled = Switch.CurrentState;
        }
    }
}