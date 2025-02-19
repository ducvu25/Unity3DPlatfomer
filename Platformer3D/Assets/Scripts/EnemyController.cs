using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Vector3 pO;
    [SerializeField] float radius;
    [SerializeField] float speed;
    [SerializeField] float timeIdle;

    Vector3 target;
    float _timeIdle;
    bool isInit = false;

    Rigidbody rb;
    Animator animator;
    string[] nameState = { "Idle", "Run"};
    int state = 0;
    // Start is called before the first frame update
    void Start()
    {
        pO = transform.position;
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        _timeIdle = timeIdle * Random.RandomRange(0.8f, 1.2f);
        SetAnimation(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (_timeIdle > 0) {
            _timeIdle -= Time.deltaTime;
        }
        else
        {
            if (!isInit)
            {
                target = pO + new Vector3(Random.RandomRange(-1.0f, 1.0f), 0, Random.RandomRange(-1.0f, 1.0f))*radius;
                isInit = true;
                SetAnimation(1);
            }
            else
            {
                float d = Vector3.Distance(transform.position, target);
                if (d < 0.1f)
                {
                    SetAnimation(0);
                    _timeIdle = timeIdle * Random.RandomRange(0.8f, 1.2f);
                    isInit = false;
                    rb.velocity = Vector3.zero;
                }
                else
                {
                    Vector3 dir = target - transform.position;
                    float angle = Mathf.Atan2(dir.x, dir.z)*Mathf.Rad2Deg;
                    transform.rotation = Quaternion.Euler(0, angle, 0);
                    rb.velocity = new Vector3(dir.normalized.x, 0, dir.normalized.z)*speed;
                }
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
    public void SetAnimation(int i)
    {
        animator.SetBool(nameState[state], false);
        state = i;
        animator.SetBool(nameState[state], true);
    }
}
