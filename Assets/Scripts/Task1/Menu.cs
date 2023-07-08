using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Task1
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] private int numberScene = 1;

        public void OpenProgressBar()
        {
            GlobalEvent.InvokeOpenLoad(numberScene);
        }
    }
}
