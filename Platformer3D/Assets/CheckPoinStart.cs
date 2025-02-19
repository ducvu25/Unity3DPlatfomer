using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoinStart : MonoBehaviour
{
    [SerializeField] GameObject goFx;
    // Start is called before the first frame update
    void Start()
    {
        ShowFx();
    }

    public void ShowFx()
    {
        goFx.SetActive(true);
        Invoke("HideFx", 2);
    }
    void HideFx() {
        goFx.SetActive(false);
    }
}
