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
    [SerializeField] private Button pauseBtn;

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
        pontosUI = GameObject.Find("PontosUI").GetComponent<TextMeshProUGUI>();
        bolasUI = GameObject.Find("NumeroBolas").GetComponent<TextMeshProUGUI>();
        loosePainel = GameObject.Find("Loose_Painel");
        winPainel = GameObject.Find("Win_Painel");
        pausePainel = GameObject.Find("Pause_Painel");
        pauseBtn = GameObject.Find("Pause").GetComponent<Button>();
        pauseBtn.onClick.AddListener(Pause);
        LigaDesligaPainel();
    }

    public void UpdateUI()
    {
        pontosUI.text = ScoreManager.instance.moedas.ToString();
        bolasUI.text = GameManager.instance.bolasNum.ToString();    
    }

    public void WinGameUI()
    {
        winPainel.SetActive(true);
    }
    public void GameOverUI()
    {
        loosePainel.SetActive(true);
    }
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
    IEnumerator tempo()
    {
        yield return new WaitForSeconds(0.001f);
        loosePainel.SetActive(false);
        winPainel.SetActive(false);
        pausePainel.SetActive(false);
    }
}
