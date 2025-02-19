using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ManagerController : MonoBehaviour
{
    [SerializeField] int hpMax;
    int hp;
    int coin;

    [SerializeField] Transform transHp;
    [SerializeField] TextMeshProUGUI txtCoin;


    [SerializeField] GameObject goMainEndGame;
    [SerializeField] TextMeshProUGUI txtShowEnd;
    [SerializeField] Button btnReplay;
    [SerializeField] Button btnQuit;

    // Start is called before the first frame update
    void Start()
    {
        hp = hpMax;
        coin = 0;

        txtCoin.text = "x" + coin;

        btnReplay.onClick.AddListener(() =>
        {
            int indexScene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(indexScene);
        });

        btnQuit.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            AddDame();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            AddHp();
        }
    }
    void AddCoin(int n)
    {
        if (goMainEndGame.activeSelf) return;

        coin += n;
        txtCoin.text = "x" + coin;
    }
    void AddDame()
    {
        if (goMainEndGame.activeSelf) return;

        hp--;
        transHp.GetChild(hp).gameObject.SetActive(false);
        if(hp == 0)
        {
            EndGame(true);
        }
    }
    void AddHp()
    {
        if (goMainEndGame.activeSelf) return;

        if (hp < hpMax)
            transHp.GetChild(hp).gameObject.SetActive(true);
        hp++;
        if(hp > hpMax) hp = hpMax;
    }
    void EndGame(bool isWin)
    {
        goMainEndGame.SetActive(true);
        txtShowEnd.text = isWin ? "YOU WIN!!" : "GAME OVER";
    }
}
