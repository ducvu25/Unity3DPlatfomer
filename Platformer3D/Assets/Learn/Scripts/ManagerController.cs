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

    [SerializeField] PlayerController transPlayer;
    // Xử lý điểm bắt đầu
    [SerializeField] Transform transStart;
    Vector3 pSave;

    // Xử lý âm thanh
    [SerializeField] AudioClip[] audioClips; // 0: win, 1: nhặt đồ, 2: death, 3: thất bại
    [SerializeField] GameObject goPreAudio;

    public static ManagerController instance;

    private void Awake()
    {
        instance = this;
    }

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
        pSave = transStart.position;
        transPlayer.transform.position = pSave;
        //transPlayer.GetComponent<PlayerController>().ResetState();
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
    public void AddCoin(int n)
    {
        if (goMainEndGame.activeSelf) return;
        InitAudioFx(1);
        coin += n;
        txtCoin.text = "x" + coin;
    }
    public void AddDame()
    {
        if (goMainEndGame.activeSelf) return;
        hp--;
        transHp.GetChild(hp).gameObject.SetActive(false);
        if(hp == 0)
        {
            EndGame(false);
        }
        else
        {
            transPlayer.ResetState();
            if (pSave == transStart.position) {
                transStart.GetComponent<CheckPoinStart>().ShowFx();
            }
            transPlayer.transform.position = pSave;
        }
    }
    public void AddHp()
    {
        if (goMainEndGame.activeSelf) return;
        InitAudioFx(1);
        if (hp < hpMax)
            transHp.GetChild(hp).gameObject.SetActive(true);
        hp++;
        if(hp > hpMax) hp = hpMax;
    }
    public void EndGame(bool isWin)
    {
        goMainEndGame.SetActive(true);
        InitAudioFx(isWin ? 0 : 3);
        txtShowEnd.text = isWin ? "YOU WIN!!" : "GAME OVER";
    }
    public void SavePoint(Vector3 p)
    {
        pSave = p;
    }

    public void InitAudioFx(int i, float volume = 1)
    {
        GameObject go = Instantiate(goPreAudio);
        AudioSource audioSource = go.transform.GetComponent<AudioSource>();
        audioSource.volume = volume;
        audioSource.clip = audioClips[i];
        audioSource.Play();

        Destroy(go, audioClips[i].length);
    }
}
