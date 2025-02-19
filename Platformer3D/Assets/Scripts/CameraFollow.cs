using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float speedSmoth;

    Vector3 offset;
    public bool isFl = true;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - target.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFl)
        {
            Vector3 t = offset + target.position;
            transform.position = t;// new Vector3(t.x, transform.position.y, t.z);
            transform.LookAt(target);
        }
    }
    public void SetValue(bool value)
    {
        isFl = value;
        if (isFl)
        {
            transform.parent = null;
        }
        else
        {
            transform.parent = target;
            transform.localPosition = Vector3.zero;
        }
    }
}
