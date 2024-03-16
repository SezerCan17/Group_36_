using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    private Rigidbody rb;
    public GameObject mom;
    public float moveSpeed;
    public float rotateSpeed;
    public float minRot;
    public float maxRot;
    float xCurRot;
    float mv;

    private Animator _controller;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
        _controller = GetComponent<Animator>();

        xCurRot = 50;
        mv = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");

        xCurRot += -y * rotateSpeed;
        xCurRot = Mathf.Clamp(xCurRot, minRot, maxRot);

        transform.eulerAngles = new Vector3(xCurRot, transform.eulerAngles.y + (x * rotateSpeed), 0.0f);

        //Movement
        Vector3 forward = mom.transform.forward;
        forward.y = 0.0f;
        forward.Normalize();

        Vector3 right = mom.transform.right;

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        Vector3 dir = forward * moveZ + right * moveX;

        dir.Normalize();

        dir *= moveSpeed * Time.deltaTime;

        transform.position += dir;

        //Move Faster

        if (Input.GetKey(KeyCode.LeftShift))
        {

            moveSpeed = 6;
        }
        else
        {
            moveSpeed = mv;
        }

        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) 
        {
            _controller.SetFloat("Speed", 1);
        }
        else
        {
            _controller.SetFloat("Speed", 0);
        }

    }
}
