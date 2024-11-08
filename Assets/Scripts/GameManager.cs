using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //Bola
    [SerializeField] private GameObject bola;
    public int bolasNum = 2;
    private bool bolaMorreu = false;
    public int bolasEmCena = 0;
    private Transform pos,posSeta,canvasSeta;
    public int tiro = 0;
    public bool win;
    //public int ondeEstou;
    public bool jogoComecou;

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
        SetaPosition();
    }

    private void Start()
    {
        StartGame();
        ScoreManager.instance.GameStartScoreM();
    }

    private void Update()
    {
        ScoreManager.instance.UpdateScore();
        UIManager.instance.UpdateUI();
        NascBolas();
        if(bolasNum <= 0)
        {
            GameOver();
        }
        if (win)
        {
            WinGame();
        }
    }

    void NascBolas()
    {
        if (OndeEstou.instance.fase == 3)
        {
            if(bolasNum > 0 && bolasEmCena == 0 && Camera.main.transform.position.x <= 1.75f)
            {
                Instantiate(bola, new Vector2(pos.position.x, pos.position.y), Quaternion.identity);
                bolasEmCena += 1;
                tiro = 0;
            }
            if (bolasNum > 0 && bolasEmCena == 0)
            {
                Instantiate(bola, new Vector2(pos.position.x, pos.position.y), Quaternion.identity);
                bolasEmCena += 1;
                tiro = 0;
            }
        }

    }

    void GameOver()
    {
        UIManager.instance.GameOverUI();
        jogoComecou = false;
    }

    void WinGame()
    {
        UIManager.instance.WinGameUI();
        jogoComecou = false;
    }

    void StartGame()
    {
        jogoComecou = true;
        bolasNum = 2;
        bolasEmCena = 0;
        win = false;
        UIManager.instance.StartUI();
    }
    void Carrega(Scene cena,LoadSceneMode load)
    {
        if(OndeEstou.instance.fase != 4)
        {
            SetaPosition();
            StartGame();
        }
    }

    void SetaPosition()
    {
        pos = GameObject.Find("PosStart").GetComponent<Transform>();
        posSeta = GameObject.Find("PosSeta").GetComponent<Transform>();
        canvasSeta = GameObject.Find("CanvasSeta").GetComponent<Transform>();

        canvasSeta.position = posSeta.position;
    }
}
