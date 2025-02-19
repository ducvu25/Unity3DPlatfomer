using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeathController : MonoBehaviour
{
    [SerializeField] float speed;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, speed*Time.deltaTime, 0));
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            ManagerController.instance.AddHp();
            Destroy(gameObject);
        }
    }
}
