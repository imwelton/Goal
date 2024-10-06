using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Salvar : MonoBehaviour
{
    public TextMeshProUGUI txt;
    public TMP_InputField caixaTxt;
    private string teste;

    private void Start()
    {
        txt.text = PlayerPrefs.GetString("pontos");
    }
    public void SalvarFloat()
    {
        teste = caixaTxt.text;
        PlayerPrefs.SetString("pontos", teste);
    }
}
