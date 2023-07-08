using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
namespace Task1
{
    public class ImageLoad : MonoBehaviour
    {
        [SerializeField]private Image image;
        public string imageURL;
        [SerializeField] private int loadScene = 2;
        public bool isVisible { get; set; }
        public bool isLoad { get; set; }
        public RectTransform rectTransform { get; set; }
        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }
        public void LoadImage()
        {
            StartCoroutine(Load());
            isLoad = true;
        }
        private IEnumerator Load()
        {

            using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(imageURL))
            {

                yield return www.SendWebRequest();

                if (www.result == UnityWebRequest.Result.Success)
                {

                    Texture2D texture = ((DownloadHandlerTexture)www.downloadHandler).texture;

                    image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
                    GlobalEvent.OnFinishLoadItem();
                }
                else
                {
                    Debug.Log("�� ������� ��������� �����������. ������: " + www.error);
                }
            }
        }


        public void Click()
        {
            GlobalEvent.OnLoadItem(image.sprite);
            GlobalEvent.InvokeOpenLoad(loadScene);
        }


    }
}
