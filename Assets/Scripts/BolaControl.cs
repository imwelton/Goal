using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class BolaControl : MonoBehaviour
{
    private Image setaCinzaImg, setaVerdeImg;
    public GameObject setaGO;
    public float zRotate;
    public bool liberaRot = false;
    public bool liberaTiro = false;

    private Rigidbody2D bola;
    public float force = 0;

    //paredes
    private Transform paredeLE, paredeLD;

    private void Awake()
    {
        setaCinzaImg = GameObject.Find("SetaCinza").GetComponent<Image>();
        setaVerdeImg = setaCinzaImg.transform.GetChild(0).GetComponent<Image>();
        setaCinzaImg.enabled = false;
        setaVerdeImg.enabled = false;
        paredeLD = GameObject.Find("ParedeLD").GetComponent<Transform>();
        paredeLE = GameObject.Find("ParedeLE").GetComponent<Transform>();
    }

    private void Start()
    {
        //força
        bola = GetComponent<Rigidbody2D>();
    }

    private void Update()   
    {
        RotacaoSeta();
        InputDeRotacao();
        LimitaRotacao();

        //força
        ControlaForca();
        AplicaForca();

        //paredes
        Paredes();
    }

    private void RotacaoSeta()
    {
        setaCinzaImg.rectTransform.eulerAngles = new Vector3(0, 0, zRotate);
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
                setaVerdeImg.fillAmount += 0.8f * Time.deltaTime;
                force = setaVerdeImg.fillAmount * 1000;
            }
            if (moveX > 0)
            {
                setaVerdeImg.fillAmount -= 0.8f * Time.deltaTime;
                force = setaVerdeImg.fillAmount * 1000;
            }

        }
    }
    void BolaDinamica()
    {
        gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
    }

    void Paredes()
    {
        if(gameObject.transform.position.x > paredeLD.position.x || gameObject.transform.position.x < paredeLE.position.x)
        {
            Destroy(gameObject);
            GameManager.instance.bolasEmCena -= 1;
            GameManager.instance.bolasNum -= 1;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("morte"))
        {
            Destroy(gameObject);
            GameManager.instance.bolasEmCena -= 1;
            GameManager.instance.bolasNum -= 1;
        }
        if (collision.CompareTag("win"))
        {
            GameManager.instance.win = true;
        }
    }
    private void OnMouseDown()
    {
        if(GameManager.instance.tiro == 0)
        {
            setaCinzaImg.enabled = true;
            setaVerdeImg.enabled = true;
            liberaRot = true;
        }
    }
    private void OnMouseUp()
    {
        liberaRot = false;
        setaCinzaImg.enabled = false;
        setaVerdeImg.enabled = false;
        if (GameManager.instance.tiro == 0 && force > 0)
        {
            liberaTiro = true;
            setaVerdeImg.fillAmount = 0;
            AudioManager.instance.SonsFXToca(1);
            GameManager.instance.tiro = 1;
        }
    }
}
