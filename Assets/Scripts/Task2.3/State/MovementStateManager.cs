using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementStateManager : MonoBehaviour
{
    public float moveSpeed = 3;
    [HideInInspector] public Vector3 dir;
    private float hInput, vInput;
    private CharacterController controller;
    [SerializeField] private float groundYOffset;
    [SerializeField] private LayerMask groundMask;
    private Vector3 spherePos;
    [SerializeField] private float gravity = -9.81f;
    private Vector3 velocity;
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        GetDirectionAndMove();
        Gravity();
    }
    private void GetDirectionAndMove()
    {
       
        vInput = Input.GetAxis("Vertical");
        hInput = Input.GetAxis("Horizontal");
        dir = transform.forward * vInput + transform.right * hInput;
        controller.Move(dir*moveSpeed*Time.deltaTime);
    }
    bool isGrounded()
    {

        spherePos = new Vector3(transform.position.x,transform.position.y-groundYOffset,transform.position.z);
        if (Physics.CheckSphere(spherePos, controller.radius - 0.05f, groundMask)) return true;
        return false;
    }
    void Gravity()
    {
        if (!isGrounded()) velocity.y += gravity * Time.deltaTime;
        else if (velocity.y < 0) velocity.y = -2;
        controller.Move(velocity * Time.deltaTime);
    }

}
