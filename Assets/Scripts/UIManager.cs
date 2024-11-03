using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    private TextMeshProUGUI pontosUI,bolasUI;
    [SerializeField] private GameObject loosePainel,winPainel,pausePainel;
    [SerializeField] private Button pauseBtn,pauseBtn_Return;
    [SerializeField] private Button btnNovamente, btnMenuFases; //loose
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
    }

    void Carrega(Scene cena, LoadSceneMode modo)
    {
        if(OndeEstou.instance.fase != 4)
        {
            pontosUI = GameObject.Find("PontosUI").GetComponent<TextMeshProUGUI>();
            bolasUI = GameObject.Find("NumeroBolas").GetComponent<TextMeshProUGUI>();
            loosePainel = GameObject.Find("Loose_Painel");
            winPainel = GameObject.Find("Win_Painel");
            pausePainel = GameObject.Find("Pause_Painel");
            btnNovamente = GameObject.Find("BtnNovamenteLoose").GetComponent<Button>();
            btnMenuFases = GameObject.Find("BtnMenuFasesLoose").GetComponent<Button>();
            pauseBtn = GameObject.Find("Pause").GetComponent<Button>();
            pauseBtn_Return = GameObject.Find("Pause_Return").GetComponent<Button>();

            pauseBtn.onClick.AddListener(Pause);
            pauseBtn_Return.onClick.AddListener(PauseReturn);

            //you loose
            btnNovamente.onClick.AddListener(JogarNovamente);
            btnMenuFases.onClick.AddListener(Levels);
            moedasNumAntes = PlayerPrefs.GetInt("moedasSave");

        }
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
        winPainel.SetActive(true);
    }
    public void GameOverUI()
    {
        loosePainel.SetActive(true);
    }
    //
    void LigaDesligaPainel()
    {
        StartCoroutine(tempo());
    }
    void Pause()
    {
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
        yield return new WaitForSeconds(0.001f);
        loosePainel.SetActive(false);
        winPainel.SetActive(false);
        pausePainel.SetActive(false);
    }

    void JogarNovamente()
    {
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
}
