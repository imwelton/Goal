using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using DG.Tweening;


public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    private TextMeshProUGUI pontosUI,bolasUI;
    [SerializeField] private GameObject loosePainel,winPainel,pausePainel;
    [SerializeField] private Button pauseBtn,pauseBtn_Return;
    [SerializeField] private Button btnNovamente, btnMenuFases; //lose
    [SerializeField] private Button btnLevelWin,btnNovamenteWin,btnAvancaWin;    //win
    [SerializeField] private Image visaoCam;
    public int moedasNumAntes, moedasNumDepois, resultado;

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
        PegaDados();
    }

    void Carrega(Scene cena, LoadSceneMode modo)
    {
        if(OndeEstou.instance.fase != 4 || OndeEstou.instance.fase != 5 || OndeEstou.instance.fase != 6)
        {
            PegaDados();
        }
    }

    void PegaDados()
    {
        //Elementos da UI
        if (GameObject.Find("PontosUI").GetComponent<TextMeshProUGUI>())
        {
            pontosUI = GameObject.Find("PontosUI").GetComponent<TextMeshProUGUI>();
        }

        if(GameObject.Find("NumeroBolas").GetComponent<TextMeshProUGUI>())
        {
            bolasUI = GameObject.Find("NumeroBolas").GetComponent<TextMeshProUGUI>();
        }

        if(bolasUI = GameObject.Find("NumeroBolas").GetComponent<TextMeshProUGUI>())
        {
            bolasUI = GameObject.Find("NumeroBolas").GetComponent<TextMeshProUGUI>();
        }

        //Paineis
        if (GameObject.Find("Loose_Painel"))
        {
            loosePainel = GameObject.Find("Loose_Painel");
        }

        if (GameObject.Find("Win_Painel"))
        {
            winPainel = GameObject.Find("Win_Painel");
        }

        if (GameObject.Find("Pause_Painel"))
        {
            pausePainel = GameObject.Find("Pause_Painel");
        }

        //Botões de pause
        if (GameObject.Find("BtnNovamenteLose").GetComponent<Button>())
        {
            btnNovamente = GameObject.Find("BtnNovamenteLose").GetComponent<Button>();
        }
        if (GameObject.Find("BtnMenuFasesLose").GetComponent<Button>())
        {
            btnMenuFases = GameObject.Find("BtnMenuFasesLose").GetComponent<Button>();
        }

        //Botões de lose
        if (GameObject.Find("Pause").GetComponent<Button>())
        {
            pauseBtn = GameObject.Find("Pause").GetComponent<Button>();
        }
        if (GameObject.Find("Pause_Return").GetComponent<Button>())
        {
            pauseBtn_Return = GameObject.Find("Pause_Return").GetComponent<Button>();
        }

        //Botões de win
        if (GameObject.Find("BtnMenuFasesWin").GetComponent<Button>())
        {
            btnLevelWin = GameObject.Find("BtnMenuFasesWin").GetComponent<Button>();
        }
        if (GameObject.Find("BtnNovamenteWin").GetComponent<Button>())
        {
            btnNovamenteWin = GameObject.Find("BtnNovamenteWin").GetComponent<Button>();
        }
        if (GameObject.Find("AvancarWin").GetComponent<Button>())
        {
            btnAvancaWin = GameObject.Find("AvancarWin").GetComponent<Button>();
        }
        
        //Eventos pause
        pauseBtn.onClick.AddListener(Pause);
        pauseBtn_Return.onClick.AddListener(PauseReturn);
        //Eventos you lose
        btnNovamente.onClick.AddListener(JogarNovamente);
        btnMenuFases.onClick.AddListener(Levels);
        //Eventos you win
        btnLevelWin.onClick.AddListener(Levels);
        btnNovamenteWin.onClick.AddListener(JogarNovamente);
        btnAvancaWin.onClick.AddListener(ProximaFase);
        //Visão da câmera
        visaoCam = GameObject.Find("CameraVision").GetComponent<Image>();

        moedasNumAntes = PlayerPrefs.GetInt("moedasSave");
    }
    IEnumerator FadeCamVision()
    {
        visaoCam.DOFade(1, 0);
        yield return new WaitForSeconds(0.001f);
        visaoCam.DOFade(0, 0.2f);
    }
    public void UseFadeCamVision()
    {
        StartCoroutine(FadeCamVision());
    }

    public void StartUI()
    {
        LigaDesligaPainel();
    }
    public void UpdateUI()
    {
        pontosUI.text = ScoreManager.instance.moedas.ToString();
        bolasUI.text = GameManager.instance.bolasNum.ToString();
        moedasNumDepois = ScoreManager.instance.moedas;
    }

    public void WinGameUI()
    {
        if (loosePainel.activeSelf) return;
        winPainel.SetActive(true);
    }
    public void GameOverUI()
    {
        if (winPainel.activeSelf) return;
        loosePainel.SetActive(true);
    }
    //
    void LigaDesligaPainel()
    {
        StartCoroutine(tempo());
    }
    void Pause()
    {
        if (winPainel.activeSelf || loosePainel.activeSelf) return;
        Debug.Log("Pausing");
        pausePainel.SetActive(true);
        pausePainel.GetComponent<Animator>().Play("MoveUI_Pause");
        Time.timeScale = 0;
    }

    void PauseReturn()
    {
        pausePainel.GetComponent<Animator>().Play("MoveUI_PauseR");
        Time.timeScale = 1;
        StartCoroutine(EsperaPause());
    }

    IEnumerator EsperaPause()
    {
        yield return new WaitForSeconds(0.8f);

        pausePainel.SetActive(false);
    }
    IEnumerator tempo()
    {
        if (visaoCam == null) yield break;
        visaoCam.enabled = true;
        visaoCam.DOFade(1, 0);

        yield return new WaitForSeconds(0.1f);
        loosePainel.SetActive(false);
        winPainel.SetActive(false);
        pausePainel.SetActive(false);

        yield return new WaitForSeconds(0.1f);
        visaoCam.DOFade(0, 0.5f).OnComplete(() =>
        {
            visaoCam.enabled = false;
        });
    }

    void JogarNovamente()
    {
        Debug.Log("Jogar novamente");
        if (!GameManager.instance.win)
        {
            SceneManager.LoadScene(OndeEstou.instance.fase);
            resultado = moedasNumDepois - moedasNumAntes;
            ScoreManager.instance.PerdeMoedas(resultado);
            resultado = 0;
        }
        else
        {
            SceneManager.LoadScene(OndeEstou.instance.fase);
            resultado = 0;
        }
    }

    void Levels()
    {
        Debug.Log("Voltar ao Levels");
        if (!GameManager.instance.win)
        {
            resultado = moedasNumDepois - moedasNumAntes;
            ScoreManager.instance.PerdeMoedas(resultado);
            resultado = 0;
            SceneManager.LoadScene(4);
        }
        else
        {
            resultado = 0;
            SceneManager.LoadScene(4);
        }
    }

    void ProximaFase()
    {
        if (GameManager.instance.win)
        {
            int temp = OndeEstou.instance.fase + 1;
            SceneManager.LoadScene(temp);
        }
    }

    public bool PainelWinOn()
    {
        if (winPainel.activeSelf)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void DesativarPaineis()
    {
        winPainel.SetActive(false);
        loosePainel.SetActive(false);
        pausePainel.SetActive(false);
    }
}
