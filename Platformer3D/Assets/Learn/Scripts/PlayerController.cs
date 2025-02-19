using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float jumpFroce;
    [SerializeField] int numberJump;
    int _numberJump;
    bool isGround;

    Rigidbody rb;
    Animator animator;

    int valueFace = 0;
    float[] angleFace = { 0, 90, 180, -90 };

    int state = 0;
    string[] nameState = { "Idle", "Run", "Jump", "Death" };

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //animator = transform.GetChild(0).GetComponent<Animator>();  
        animator = GetComponentInChildren<Animator>();

        _numberJump = numberJump;
        SetState(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (state == 3) return;

        float moveX = 0;
        float moveY = 0;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveX = -1;
            moveY = 0;
            valueFace = 3;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            valueFace = 1;
            moveX = 1;
            moveY = 0;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            valueFace = 0;
            moveX = 0;
            moveY = 1;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            moveX = 0;
            moveY = -1;
            valueFace = 2;
        }
        if (Input.GetKeyDown(KeyCode.Space)) {
            Jump(); 
        }
        Rotation();
        rb.velocity = new Vector3(moveX, 0, moveY)*speed + new Vector3(0, rb.velocity.y, 0);
        if (isGround) {
            if(moveX != 0 || moveY != 0)
                SetState(1);
            else
                SetState(0);
        }
    }
    void Jump()
    {
        if (_numberJump > 0)
        {
            _numberJump--;
            rb.velocity = new Vector3(rb.velocity.x, jumpFroce, rb.velocity.z);
        }
    }
    void Rotation()
    {
        Vector3 anglePre = transform.eulerAngles;

        anglePre.y = angleFace[valueFace];

        transform.eulerAngles = anglePre;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("VatCan"))
        {
            SetState(3);
            ManagerController.instance.InitAudioFx(2);
            Invoke("Die", 2);
        }
        else
        {
            isGround = true;
            _numberJump = numberJump;
            SetState(0);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Finish"))
        {
            state = 3;
            ManagerController.instance.EndGame(true);
        }else if (other.transform.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            ManagerController.instance.AddCoin(1);
        }
    }
    void Die()
    {
        ManagerController.instance.AddDame();
    }
    private void OnCollisionExit(Collision collision) {
        if (state == 3) return;

        SetState(2);
        isGround = false;
    }
    void SetState(int s)
    {
        animator.SetBool(nameState[state], false);
        state = s;
        animator.SetBool(nameState[state], true);
    }
    public void ResetState()
    {
        SetState(0);
    }
}
