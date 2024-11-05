using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManager : MonoBehaviour
{
    [System.Serializable]
    public class Level
    {
        public string levelText;
        public bool habilitado;
        public int desbloqueado;
        public bool txtAtivo;
    }

    public GameObject botao;
    public Transform localBtn;
    public List<Level> levelList;

    private void Awake()
    {
        if (GameObject.Find("UIManager"))
        {
            Destroy(GameObject.Find("UIManager"));
        }
        if ((GameObject.Find("UIManagerClone")))
        {
            Destroy(GameObject.Find("UIManagerClone"));
        }
        if ((GameObject.Find("GameManager")))
        {
            Destroy(GameObject.Find("GameManager"));
        }
        if ((GameObject.Find("GameManagerClone")))
        {
            Destroy(GameObject.Find("GameManagerClone"));
        }
    }
    void ListaAdd()
    {
        foreach(Level level in levelList)
        {
            GameObject btnNovo = Instantiate(botao) as GameObject;
            BotaoLevel btnNew = btnNovo.GetComponent<BotaoLevel>();
            btnNew.levelTxtBtn.text = level.levelText;

            if (PlayerPrefs.GetInt("Level" + btnNew.levelTxtBtn.text) == 1)
            {
                level.desbloqueado = 1;
                level.habilitado = true;
                level.txtAtivo = true;
            }

            btnNew.desbloqueadoBtn = level.desbloqueado;
            btnNew.GetComponent<Button>().interactable = level.habilitado;
            btnNew.levelTxtBtn.enabled = level.txtAtivo;

            btnNew.GetComponent<Button>().onClick.AddListener(()=>ClickLevel("Level" + btnNew.levelTxtBtn.text));

            btnNovo.transform.SetParent(localBtn, false);
        }
    }

    void ClickLevel(string level)
    {
        SceneManager.LoadScene(level);
    }

    private void Start()
    {
        //PlayerPrefs.DeleteAll();
        ListaAdd();
    }
}
