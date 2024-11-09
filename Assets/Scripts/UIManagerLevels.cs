using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManagerLevels : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moedasLevel;

    private void Start()
    {
        moedasLevel.text = PlayerPrefs.GetInt("moedasSave").ToString();
    }
}
