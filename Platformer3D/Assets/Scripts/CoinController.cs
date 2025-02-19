using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    [SerializeField] int typeItem;

    private void Start()
    {
        transform.GetChild(typeItem).gameObject.SetActive(true);
        transform.GetChild((typeItem + 1)%2).gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            if(typeItem == 0)
                GameManager.instance.AddCoin();
            else
                GameManager.instance.AddHp();
            Destroy(gameObject);
        }
    }
}
