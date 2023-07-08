using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Task2_2
{
    public class CameraFollow : MonoBehaviour
    {
        public float moveSmothness;
        public float rotSmothness;
        public Vector3 moveOffset;
        public Vector3 rotOffset;
        public Transform carTarget;


        private void FixedUpdate()
        {
            FollowTarget();
        }
        void FollowTarget()
        {
            HandleMovement();
            HandleRotation();
        }
        void HandleRotation()
        {
            var direction = carTarget.position - transform.position;
            var rotation = new Quaternion();
            rotation = Quaternion.LookRotation(direction+rotOffset,Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation,rotation,rotSmothness*Time.deltaTime);
        }
        void HandleMovement()
        {
            Vector3 targetPos = new Vector3();
            targetPos = carTarget.TransformPoint(moveOffset);
            transform.position = Vector3.Lerp(transform.position,targetPos,moveSmothness*Time.deltaTime);
        }
    }
}