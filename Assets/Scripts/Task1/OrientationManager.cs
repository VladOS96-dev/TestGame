using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Task1
{
    public class OrientationManager : MonoBehaviour
    {
        public bool portraitOnly;

        void Start()
        {
            if (portraitOnly)
            {
                Screen.orientation = ScreenOrientation.Portrait;
            }
            else
            {
                Screen.orientation = ScreenOrientation.AutoRotation;
                Screen.autorotateToPortrait = true;
                Screen.autorotateToLandscapeLeft = true;
                Screen.autorotateToLandscapeRight = true;
            }
        }
    }
}
