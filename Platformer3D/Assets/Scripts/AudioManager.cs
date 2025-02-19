using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] GameObject goPre;
    [SerializeField] List<AudioClip> clipList; // 0: hit, 1: coin, 2: loss, 3 win
    public void PlayAudio(int i)
    {
        GameObject go = Instantiate(goPre);
        AudioSource audioSource = go.GetComponent<AudioSource>();
        audioSource.clip = clipList[i];
        audioSource.Play();
        Destroy(go, clipList[i].length);
    }
}
