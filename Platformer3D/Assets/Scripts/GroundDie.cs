using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDie : MonoBehaviour
{
    [SerializeField] float timeDelay = 1;
    [SerializeField] bool isDes = false;
    private void OnCollisionEnter(Collision collision)
    {
        if(!isDes && collision.transform.CompareTag("Player"))
        {
            isDes = true;
            Invoke("HitPlayer", timeDelay);
        }
    }
    void HitPlayer()
    {
        transform.GetComponent<Rigidbody>().isKinematic = false;
    }
}
