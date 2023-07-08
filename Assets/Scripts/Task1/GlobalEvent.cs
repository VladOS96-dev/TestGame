using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace Task1
{
    public static class GlobalEvent
    {
        public static Action<int> OnOpenLoad;
        public static Action OnFinishLoadItem;
        public static Action<int> OnStartLoadItems;
        public static Action<Sprite> OnLoadItem;
        public static void InvokeOpenLoad(int numberScene)
        {
            OnOpenLoad?.Invoke(numberScene);
        }
        public static void InvokeFinishLoadItem()
        {
            OnFinishLoadItem?.Invoke();
        }
        public static void InvokeLoadItem(Sprite sprite)
        {
            OnLoadItem?.Invoke(sprite);
        }
        public static void InvokeLoadItems(int countItemsLoad)
        {
            OnStartLoadItems?.Invoke(countItemsLoad);
        }
    }
}
