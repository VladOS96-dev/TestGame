using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace Task1
{
    public class View : MonoBehaviour
    {
        [SerializeField] private int numberScene;
        [SerializeField] private Image imgIcon;
        private void Start()
        {
            imgIcon.sprite =LoadManager.instance.currImage;
        }
        public void BackButton()
        {
            GlobalEvent.InvokeOpenLoad(numberScene);
        }
    }
}