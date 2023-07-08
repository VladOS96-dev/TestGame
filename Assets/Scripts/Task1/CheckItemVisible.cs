using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Task1
{
    public class CheckItemVisible : MonoBehaviour
    {
        [SerializeField]private ScrollRect scrollRect;
        [SerializeField]private RectTransform maskRect;
        public List<ImageLoad> objectsToCheck { get; set; } = new List<ImageLoad>();
        public static bool load { get; set; } = false;

        public void Scroll()
        {
            int count = 0;
            foreach (var item in objectsToCheck)
            {

                bool isObjectVisible = RectTransformUtility.RectangleContainsScreenPoint(maskRect, item.rectTransform.position);

                if (isObjectVisible)
                {
                    item.isVisible = true;
                    if (!item.isLoad)
                    {
                        count++;
                        item.LoadImage();
                    }
                }
                else
                {
                    item.isVisible = false;
                }
            }
            if (LoadManager.isLoad && !load)
            {
                load = true;
                GlobalEvent.InvokeLoadItems(count);
            }
        }

    }
}