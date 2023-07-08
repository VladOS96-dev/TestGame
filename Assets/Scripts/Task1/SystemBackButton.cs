using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Task1
{
    public class SystemBackButton : MonoBehaviour
    {


        void Update()
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                if (Input.GetKey(KeyCode.Escape))
                {

                    LoadPrevScene();
                }
            }
            else if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
                {

                    if (Input.GetTouch(0).position.x < Screen.width * 0.2f)
                    {

                        LoadPrevScene();
                    }

                }
            }
        }
        private void LoadPrevScene()
        {
            if (SceneManager.GetActiveScene().buildIndex != 0)
            {
                GlobalEvent.InvokeOpenLoad(SceneManager.GetActiveScene().buildIndex - 1);
            }
        }
    }
}
