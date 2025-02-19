using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public bool isEndGame;
    bool isSave = false;
    private void Start()
    {
        if (!isEndGame)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            transform.GetChild(2).gameObject.SetActive(true);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!isSave && other.transform.CompareTag("Player"))
        {
            if (isEndGame)
            {
                GameManager.instance.EndGame();
            }
            else
            {
                GameManager.instance.SavePoint(transform);
                transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(1).gameObject.SetActive(true);
                isSave = true;
            }
        }
    }
}
