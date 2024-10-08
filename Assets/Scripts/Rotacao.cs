using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rotacao : MonoBehaviour
{
    [SerializeField] private Transform posStart;
    [SerializeField] private Image setaImg;
    public float zRotate;
    public bool liberaRot = false;
    public bool liberaTiro = false;

    private void Start()
    {
        PosicionaBola();
        PosicionaSeta();
    }

    private void Update()
    {
        RotacaoSeta();
        InputDeRotacao();
        LimitaRotacao();
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
        if (liberaRot)
        {
            float moveY = Input.GetAxis("Mouse Y");


            if(zRotate < 90)
            {
                if (moveY > 0)
                {
                    zRotate += 2.5f;
                }
            }
            if(zRotate > 0)
            {
                if (moveY < 0)
                {
                    zRotate -= 2.5f;
                }
            }
        }
    }

    void LimitaRotacao()
    {
        if(zRotate > 90)
        {
            zRotate = 90;
        }else if(zRotate < 0)
        {
            zRotate = 0;
        }
    }

    private void OnMouseDown()
    {
        liberaRot = true;

    }

    private void OnMouseUp()
    {
        liberaRot = false;
        liberaTiro = true;
    }
}
