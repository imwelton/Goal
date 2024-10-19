using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioClip[] clips;
    public AudioSource musicaBG;

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

        musicaBG = GetComponent<AudioSource>();
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
}
