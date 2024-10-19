using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Forca : MonoBehaviour
{
    private Rigidbody2D bola;
    public float force = 0;
    private Rotacao rot;
    public Image seta2Img;
    // Start is called before the first frame update
    void Start()
    {
        bola = GetComponent<Rigidbody2D>();
        rot = GetComponent<Rotacao>();
    }

    // Update is called once per frame
    void Update()
    {
        ControlaForca();
        AplicaForca();
    }

    void AplicaForca()
    {
        float x = force * Mathf.Cos(rot.zRotate * Mathf.Deg2Rad);
        float y = force * Mathf.Sin(rot.zRotate * Mathf.Deg2Rad);

        if (rot.liberaTiro)
        {
            bola.AddForce(new Vector2(x, y));
            rot.liberaTiro = false;
        }
    }

    void ControlaForca()
    {
        if (rot.liberaRot)
        {
            float moveX = Input.GetAxis("Mouse X");

            if(moveX < 0)
            {
                seta2Img.fillAmount += 0.8f * Time.deltaTime;
                force = seta2Img.fillAmount * 1000;
            }
            if(moveX > 0)
            {
                seta2Img.fillAmount -= 0.8f * Time.deltaTime;
                force = seta2Img.fillAmount * 1000;
            }
            
        }
    }
}
