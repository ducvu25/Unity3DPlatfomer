using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] bool isFollow = true;
    Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.position;

        if (!isFollow)
        {
            transform.parent = player.GetChild(1);
            transform.position = player.GetChild(1).position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isFollow)
        {
            transform.position = player.position + offset;
            //transform.LookAt(player.position);
        }
    }
}
