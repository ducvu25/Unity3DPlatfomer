using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudController : MonoBehaviour
{
    [SerializeField] List<Transform> lTarget;
    [SerializeField] float speed;
    [SerializeField] int indexNext;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, lTarget[indexNext].position, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, lTarget[indexNext].position) < 0.2f)
        {
            indexNext = (indexNext + 1)%lTarget.Count;
        }
        // n = 3, 0, 1, 2, 3
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.transform.parent = transform;
        }
    }
}
