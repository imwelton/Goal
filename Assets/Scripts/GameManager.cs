using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
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

    private void Start()
    {
        ScoreManager.instance.GameStartScoreM();
    }

    private void Update()
    {
        ScoreManager.instance.UpdateScore();
        UIManager.instance.UpdateUI();
    }
}
