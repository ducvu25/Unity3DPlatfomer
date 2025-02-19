using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGround : MonoBehaviour
{
    [SerializeField] Transform[] items;
    [SerializeField] float speed;
    [SerializeField] float timeStop = 1;
    float _timeStop = 0;
    bool isMoveTop;
    // Start is called before the first frame update
    void Start()
    {
        isMoveTop = Random.value > 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if(_timeStop > 0)
        {
            _timeStop -= Time.deltaTime;
            return;
        }
        int index = isMoveTop ? 0 : 1;
        float d = Vector3.Distance(transform.position, items[index].position);
        if(d < 0.1f)
        {
            isMoveTop = !isMoveTop;
            _timeStop = timeStop;
        }
        transform.position = Vector3.MoveTowards(transform.position, items[index].position, speed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (transform.childCount == 0 && other.transform.CompareTag("Player"))
        {
            other.transform.parent = transform;
            other.transform.localScale = Vector3.one;
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.transform.CompareTag("Player"))
        {
            other.transform.parent = null;
            other.transform.localScale = Vector3.one;
        }
    }

}
