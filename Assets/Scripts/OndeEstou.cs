using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OndeEstou : MonoBehaviour
{
    public static OndeEstou instance;
    public int fase;

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

    void VerificaFase(Scene cena,LoadSceneMode modo)
    {
        fase = SceneManager.GetActiveScene().buildIndex;
    }
}
