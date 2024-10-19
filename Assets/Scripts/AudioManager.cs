using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    //Músicas
    public AudioClip[] clips;
    public AudioSource musicaBG;

    //Efeitos sonoros
    public AudioClip[] clipsFX;
    public AudioSource sonsFX;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (!musicaBG.isPlaying)
        {
            musicaBG.clip = GetRandom();
            musicaBG.Play();
        }
    }

    public AudioClip GetRandom()
    {
        return clips[Random.Range(0, clips.Length)];
    }

    public void SonsFXToca(int index)
    {
        sonsFX.PlayOneShot(clipsFX[index]);
    }
}
