using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Task2_1
{
    public class CoinController : MonoBehaviour
    {
        [SerializeField] private float speedRotateX = 0;
        [SerializeField] private float speedRotateY = 0;
        [SerializeField] private float speedRotateZ = 0;
        private Renderer renderer;
        private void Start()
        {
            renderer = GetComponentInChildren<Renderer>();
        }
        void Update()
        {
            transform.Rotate(speedRotateX, speedRotateY, speedRotateZ);
        }
        private void OnMouseDown()
        {
            float red = Random.Range(0f, 1f);
            float green = Random.Range(0f, 1f);
            float blue = Random.Range(0f, 1f);


            Color randomColor = new Color(red, green, blue);


            renderer.material.color = randomColor;
        }
    }
}