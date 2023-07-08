using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
namespace Task1
{
    public class LoadManager : MonoBehaviour
    {
        [SerializeField] private int sceneId;
        [SerializeField] private TextMeshProUGUI percentLoadText;
        [SerializeField] private Slider loadBar;
        [SerializeField] private float timeLoad;
        public Sprite currImage { get; private set; }
        private CanvasGroup canvasGroup;
        private float time = 0;
        private float deltaPercentLoad;
        private float percentLoad = 0;
        private bool isStop = false;
        public static LoadManager instance { get; private set; }
        private int currCountItem = 0;
        private int countItems = 0;
        public static bool isLoad;
        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this);
                InitEvent();
            }
            else
            {
                Destroy(gameObject);
            }

        }
        private void InitEvent()
        {
            GlobalEvent.OnOpenLoad += OpenLoad;
            GlobalEvent.OnFinishLoadItem += OnLoad;
            GlobalEvent.OnStartLoadItems += OnCountItems;
            GlobalEvent.OnLoadItem += OnLoadItem;
        }
        public void OnLoadItem(Sprite sprite)
        {
            currImage = sprite;
        }
        public void OnLoad()
        {
            currCountItem++;
        }
        public void OnCountItems(int countItems)
        {
            currCountItem = 0;
            this.countItems = countItems;
        }
        public void OpenLoad(int numberScene)
        {
            isLoad = true;
            isStop = false;
            sceneId = numberScene;
            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = true;
            currCountItem = 0;
            countItems = 0;
            time = timeLoad;
            percentLoad = 0;
            loadBar.value = (int)percentLoad;
            percentLoadText.text = $"{(int)percentLoad}%";
            deltaPercentLoad = (Time.fixedDeltaTime * 100) / timeLoad;
            CheckItemVisible.load = false;
            SceneManager.LoadScene(sceneId);
        }
        void FixedUpdate()
        {
            if (isStop)
                return;
            time -= Time.fixedDeltaTime;
            if (time > 0)
            {
                percentLoad += deltaPercentLoad;
                loadBar.value = (int)percentLoad;
                percentLoadText.text = $"{(int)percentLoad}%";
            }
            else if (!isStop && currCountItem == countItems)
            {
                isStop = true;
                isLoad = false;
                canvasGroup.alpha = 0;
                canvasGroup.blocksRaycasts = false;
            }
        }
        private void OnDestroy()
        {
            GlobalEvent.OnOpenLoad -= OpenLoad;
            GlobalEvent.OnFinishLoadItem -= OnLoad;
            GlobalEvent.OnStartLoadItems -= OnCountItems;
            GlobalEvent.OnLoadItem -= OnLoadItem;
        }
    }
}