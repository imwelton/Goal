using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [System.Serializable]
    public class Level
    {
        public string levelText;
        public bool habilitado;
        public int desbloqueado;
    }

    public GameObject botao;
    public Transform localBtn;
    public List<Level> levelList;

    void ListaAdd()
    {
        foreach(Level level in levelList)
        {
            GameObject btnNovo = Instantiate(botao) as GameObject;
            btnNovo.transform.SetParent(localBtn, false);
        }
    }

    private void Start()
    {
        ListaAdd();
    }
}
