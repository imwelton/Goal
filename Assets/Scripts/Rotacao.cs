using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rotacao : MonoBehaviour
{
    [SerializeField] private Transform posStart;
    [SerializeField] private Image setaImg;
    public float zRotate;
    private void Start()
    {
        PosicionaBola();
        PosicionaSeta();
    }

    private void Update()
    {
        RotacaoSeta();
        InputDeRotacao();
    }
    private void PosicionaSeta()
    {
        setaImg.rectTransform.position = posStart.position;
    }

    private void PosicionaBola()
    {
        gameObject.transform.position = posStart.position;
    }

    private void RotacaoSeta()
    {
        setaImg.rectTransform.eulerAngles = new Vector3(0, 0, zRotate);
    }

    private void InputDeRotacao()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            zRotate += 2.5f;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            zRotate -= 2.5f;
        }
    }
}
