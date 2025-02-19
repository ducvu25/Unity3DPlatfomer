using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Transform transStart;
    [SerializeField] Transform transLimit;

    [Header("\n----UI-----")]
    [SerializeField] Transform transHp;
    [SerializeField] TextMeshProUGUI txtCoin;
    [SerializeField] Button btnRestart;
    int hp;
    int coin = 0;

    [Header("\n---End-----")]
    [SerializeField] GameObject goMainEnd;
    [SerializeField] TextMeshProUGUI txtTitle;
    [SerializeField] TextMeshProUGUI txtYourScore;
    [SerializeField] Button btnEndReset;
    [SerializeField] Button btnQuit;


    public static GameManager instance;
    AudioManager audioManager;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ResetPlayer(0));
        audioManager = GetComponent<AudioManager>();
        hp = transHp.childCount;
        txtCoin.text = "x" + coin;

        btnRestart.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(0);
        });

        goMainEnd.SetActive(false);
        btnEndReset.onClick.AddListener(() => {
            SceneManager.LoadScene(0);
        });
        btnQuit.onClick.AddListener(() => {
            Application.Quit();
        });
    }
    private void Update()
    {
        if(player.position.y < transLimit.position.y)
        {
            AddDame();
        }
    }
    IEnumerator ResetPlayer(float delay = 0)
    {
        yield return new WaitForSeconds(delay);
        player.GetComponent<Player>().SetAnimation(0);
        transStart.GetChild(1).gameObject.SetActive(true);
        player.position = transStart.position;

        yield return new WaitForSeconds(1);
        transStart.GetChild(1).gameObject.SetActive(false);
    }
    public void SavePoint(Transform p)
    {
        transStart = p;
    }
    public void AddDame()
    {
        if (player.GetComponent<Player>().state == 3) return;
        audioManager.PlayAudio(0);
        hp--;
        transHp.GetChild(hp).gameObject.SetActive(false);
        player.GetComponent<Player>().SetAnimation(3);
        if (hp == 0)
        {
            EndGame();
        }
        else
        {
            StartCoroutine(ResetPlayer(1));
        }
    }
    public void AddCoin()
    {
        audioManager.PlayAudio(1);
        coin++;
        txtCoin.text = "x" + coin;
    }
    public void AddHp()
    {
        audioManager.PlayAudio(1);
        if (hp < transHp.childCount)
        {
            transHp.GetChild(hp).gameObject.SetActive(true);
            hp++;
        }
    }
    public void EndGame()
    {
        player.GetComponent<Player>().enabled = false;
        goMainEnd.gameObject.SetActive(true);
        txtTitle.text = hp > 0 ? "YOU WIN" : "YOU LOSS";
        audioManager.PlayAudio(hp > 0 ? 3 : 2);
        txtYourScore.text = "YOUR SCORE: " + coin;
    }
}
