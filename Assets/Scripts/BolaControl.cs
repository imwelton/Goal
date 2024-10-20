using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class BolaControl : MonoBehaviour
{
    [SerializeField] private Transform posStart,posSeta;
    [SerializeField] private Image setaImg;
    public GameObject setaGO;
    public float zRotate;
    public bool liberaRot = false;
    public bool liberaTiro = false;

    private Rigidbody2D bola;
    public float force = 0;
    public Image seta2Img;

    private void Awake()
    {
        setaImg = GameObject.Find("SetaCinza").GetComponent<Image>();
        setaGO = GameObject.Find("CanvasSeta");
        setaGO.SetActive(false);
    }

    private void Start()
    {
        posStart = GameObject.Find("PosStart").GetComponent<Transform>();
        posSeta = GameObject.Find("PosSeta").GetComponent<Transform>();
        PosicionaBola();

        //for�a

        seta2Img = setaImg.gameObject.transform.GetChild(0).GetComponent<Image>();
        bola = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        RotacaoSeta();
        InputDeRotacao();
        LimitaRotacao();
        PosicionaSeta();

        //for�a
        ControlaForca();
        AplicaForca();
    }

    private void PosicionaSeta()
    {
        setaImg.rectTransform.position = posSeta.position;
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


            if (zRotate < 90)
            {
                if (moveY > 0)
                {
                    zRotate += 2.5f;
                }
            }
            if (zRotate > 0)
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
        if (zRotate > 90)
        {
            zRotate = 90;
        }
        else if (zRotate < 0)
        {
            zRotate = 0;
        }
    }
    void AplicaForca()
    {
        float x = force * Mathf.Cos(zRotate * Mathf.Deg2Rad);
        float y = force * Mathf.Sin(zRotate * Mathf.Deg2Rad);

        if (liberaTiro)
        {
            bola.AddForce(new Vector2(x, y));
            liberaTiro = false;
        }
    }

    void ControlaForca()
    {
        if (liberaRot)
        {
            float moveX = Input.GetAxis("Mouse X");

            if (moveX < 0)
            {
                seta2Img.fillAmount += 0.8f * Time.deltaTime;
                force = seta2Img.fillAmount * 1000;
            }
            if (moveX > 0)
            {
                seta2Img.fillAmount -= 0.8f * Time.deltaTime;
                force = seta2Img.fillAmount * 1000;
            }

        }
    }
    void BolaDinamica()
    {
        gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
    }
    private void OnMouseDown()
    {
        if(GameManager.instance.tiro == 0)
        {
            liberaRot = true;
            setaGO.SetActive(true);
        }
    }
    private void OnMouseUp()
    {
        liberaRot = false;
        setaGO.SetActive(false);
        if (GameManager.instance.tiro == 0 && force > 0)
        {
            liberaTiro = true;
            AudioManager.instance.SonsFXToca(1);
            GameManager.instance.tiro = 1;
        }
    }
}
