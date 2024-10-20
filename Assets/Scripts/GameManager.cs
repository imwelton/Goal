using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //Bola
    [SerializeField] private GameObject bola;
    private int bolasNum = 2;
    private bool bolaMorreu = false;
    private int bolasEmCena = 0;
    private Transform pos;
    public int tiro = 0;

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

        SceneManager.sceneLoaded += Carrega;
    }

    private void Start()
    {
        ScoreManager.instance.GameStartScoreM();
    }

    private void Update()
    {
        ScoreManager.instance.UpdateScore();
        UIManager.instance.UpdateUI();
        NascBolas();
    }

    void NascBolas()
    {
        if(bolasNum > 0 && bolasEmCena == 0)
        {
            Instantiate(bola, new Vector2(pos.position.x, pos.position.y), Quaternion.identity);
            bolasEmCena += 1;
            tiro = 0;
        }
    }

    void Carrega(Scene cena,LoadSceneMode load)
    {
        pos = GameObject.Find("PosStart").GetComponent<Transform>();
    }
}
