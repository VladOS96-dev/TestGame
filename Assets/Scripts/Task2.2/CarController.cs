using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace Task2_2
{
    public class CarController : MonoBehaviour
    {
        public enum ControlMode 
        {
        Keyboard,
        Touch
        }
        public enum Axel
        { 
        Front,
        Rear
        }
        [Serializable]
        public struct Wheel
        {
            public GameObject wheelModel;
            public WheelCollider wheelCollider;
            public TrailRenderer wheelEffect;
            public ParticleSystem smokeParticle;
            public Axel axel;

        }
        [SerializeField] private float maxAccelearation = 30.0f;
        [SerializeField] private float breakAccelearation = 50.0f;
        [SerializeField] private float turnSensitivity = 1f;
        [SerializeField] private float maxSteerAngle = 30f;
        [SerializeField] private float groundForce = 10f;
        [SerializeField] private List<Wheel> wheels;
        [SerializeField] private ControlMode controlMode;
        [SerializeField] private LayerMask mask;
        private float moveInput;
        private float steerInput;
        private Rigidbody carRb;
        private bool isBrake;
        [SerializeField] private Vector3 _centerOfMass;
        private void Start()
        {
            carRb = GetComponent<Rigidbody>();
            carRb.centerOfMass = _centerOfMass;
        }
        void GetInputs()
        {
            if (controlMode == ControlMode.Keyboard)
            {
                moveInput = Input.GetAxis("Vertical");
                steerInput = Input.GetAxis("Horizontal");
            }
        }
        public void MoveInput(float input)
        {
            moveInput = input;
        }
        public void RotateInput(float input)
        {
            steerInput = input;
        }
        public void BrakeInput(bool input)
        {
            isBrake = input;
        }
        private void FixedUpdate()
        {
            Downforce();
        }
        private void Update()
        {
            GetInputs();
            AnimateWheels();
            Brake();
            WheelEffects();
        }
        private void LateUpdate()
        {
            Move();
            Steer();
        }
        private void Downforce()
        {
            RaycastHit hit;
            if (Physics.OverlapBox(transform.position, Vector3.one*1.5f, transform.rotation,mask).Length==0)
            {

                float appliedForce = carRb.mass * groundForce ;
                carRb.AddForce(-transform.up * appliedForce);
 
            }
        }
        private void Brake()
        {
            if (Input.GetKey(KeyCode.Space)|| isBrake)
            {
                foreach (var wheel in wheels)
                {
                    wheel.wheelCollider.brakeTorque = 300 * breakAccelearation * Time.deltaTime;
                }
            }
            else
            {
                foreach (var wheel in wheels)
                {
                    wheel.wheelCollider.brakeTorque = 0;
                }
            }
        }
        private void Move()
        {
            foreach (var wheel in wheels)
            {
                wheel.wheelCollider.motorTorque = moveInput * maxAccelearation*600 * Time.deltaTime;
            }
           
        }
       
        private void Steer()
        {
            foreach (var wheel in wheels)
            {
                if (wheel.axel == Axel.Front)
                {
                    var _steerAngle = steerInput * turnSensitivity * maxSteerAngle;
                    wheel.wheelCollider.steerAngle = Mathf.Lerp(wheel.wheelCollider.steerAngle,_steerAngle,0.6f);
                }
            }
        }
        private void AnimateWheels()
        {
            foreach (var wheel in wheels)
            {
                Quaternion rot;
                Vector3 pos;
                wheel.wheelCollider.GetWorldPose(out pos,out rot);
                wheel.wheelModel.transform.position = pos;
                wheel.wheelModel.transform.rotation = rot;
            }
        }
        private void WheelEffects()
        {
            foreach (var wheel in wheels)
            {
                if ((Input.GetKey(KeyCode.Space)|| isBrake) &&wheel.axel==Axel.Rear&&wheel.wheelCollider.isGrounded&&carRb.velocity.magnitude>=10)
                {
                    wheel.wheelEffect.emitting = true;
                    wheel.smokeParticle.Play();
                }
                else
                {
                    wheel.wheelEffect.emitting = false;
                }
            }
        }
    }
    
}