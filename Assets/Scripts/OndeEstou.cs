using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OndeEstou : MonoBehaviour
{
    public static OndeEstou instance;
    public int fase = -1;
    [SerializeField] private GameObject UIManagerGO, GameManagerGO;

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

        SceneManager.sceneLoaded += VerificaFase;
    }

    void VerificaFase(Scene cena,LoadSceneMode modo)
    {
        fase = SceneManager.GetActiveScene().buildIndex;
        if(fase != 4 && fase != 5 && fase != 6)
        {
            Instantiate(UIManagerGO);
            Instantiate(GameManagerGO);
        }
    }
}
