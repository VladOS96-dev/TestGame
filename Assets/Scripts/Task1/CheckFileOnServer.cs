using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;
namespace Task1
{
    public class CheckFileOnServer : MonoBehaviour
    {
        [SerializeField]private string serverURL;
        [SerializeField]private Transform content;
        [SerializeField] ImageLoad prefabImageLoad;
        [SerializeField]private CheckItemVisible itemVisible;
        private List<string> jpgFiles = new List<string>();
        void Awake()
        {
            StartCoroutine(LoadAllFile());

        }
        public void GenerateItem()
        {
            for (int i = 0; i < jpgFiles.Count; i++)
            {
                ImageLoad image = Instantiate(prefabImageLoad, content);
                image.imageURL = jpgFiles[i];
                itemVisible.objectsToCheck.Add(image);
            }

        }
        private IEnumerator LoadAllFile()
        {
            using (UnityWebRequest www = UnityWebRequest.Get(serverURL))
            {
                yield return www.SendWebRequest();

                if (www.result == UnityWebRequest.Result.Success)
                {
                    string htmlText = www.downloadHandler.text;
                    string pattern = @"<a\s+(?:[^>]*?\s+)?href=([""'])(.*?)\1";
                    MatchCollection matches = Regex.Matches(htmlText, pattern);

                    foreach (Match match in matches)
                    {
                        string fileURL = "http://data.ikppbb.com" + match.Groups[2].Value;
                        if (fileURL.EndsWith(".jpg"))
                        {

                            jpgFiles.Add(fileURL);
                        }
                    }

                    GenerateItem();
                }
                else
                {
                    Debug.Log("Не удалось загрузить данные с сервера. Ошибка: " + www.error);
                }
            }
        }

    }
}