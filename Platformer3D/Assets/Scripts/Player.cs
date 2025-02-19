using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float jumpForce;

    Rigidbody rb;
    int valueFacing = 0;
    float[] angleFacing = { 0, -90, 180, 90 };

    Animator animator;
    string[] nameState = { "Idle", "Run", "Jump", "Death" };
    public int state = 0; // 0: idle, 1: run, 2: jump, 3: deadth
    bool isGround = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        SetAnimation(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (state == 3) return;
        Vector3 v = Vector3.zero;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            v.x = -1;
            v.z = 0;
            valueFacing = 1;
        }

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            v.x = 1;
            v.z = 0;
            valueFacing = 3;
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            v.x = 0;
            v.z = 1;
            valueFacing = 0;
        }
        if(Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            v.x = 0;
            v.z = -1;
            valueFacing = 2;
        }
        Rotation();
        rb.velocity = v * speed + new Vector3(0, rb.velocity.y, 0) ;
        if (isGround)
        {
            if (rb.velocity.x != 0 || rb.velocity.z != 0) {
                SetAnimation(1);
            }
            else
            {
                SetAnimation(0);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGround){
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z) ;
        }
    }
    void Rotation()
    {
        // Tạo một biến mới cho góc xoay
        Vector3 currentRotation = transform.eulerAngles;

        // Gán giá trị mới cho góc y
        currentRotation.y = angleFacing[valueFacing];

        // Cập nhật lại góc xoay
        transform.eulerAngles = currentRotation;
    }
    public void SetAnimation(int i)
    {
        animator.SetBool(nameState[state], false);
        state = i;
        animator.SetBool(nameState[state], true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (state == 3) return;
        isGround = true;
        SetAnimation(0);
    }
    private void OnCollisionExit(Collision collision)
    {
        if (state == 3) return;
        isGround = false;
        SetAnimation(2);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(state != 3 && other.transform.CompareTag("Wood"))
        {
            GameManager.instance.AddDame();
        }
    }
}
