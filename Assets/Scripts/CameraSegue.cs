using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSegue : MonoBehaviour
{
    [SerializeField] private Transform objE, objD, bola;
    private float t = 1;
    void Update()
    {
        if (GameManager.instance.jogoComecou)
        {
            if(transform.position.x != objE.position.x)
            {
                t *= Time.deltaTime;
                transform.position = new Vector3(Mathf.SmoothStep(objE.position.x,Camera.main.transform.position.x,t),transform.position.y,transform.position.z);
            }
            if(bola == null && GameManager.instance.bolasEmCena > 0)
            {
                bola = GameObject.Find("png_bola(Clone)").GetComponent<Transform>();
            }else if (GameManager.instance.bolasEmCena > 0)
            {
                Vector3 posCam = transform.position;
                posCam.x = bola.position.x;
                posCam.x = Mathf.Clamp(posCam.x, objE.position.x, objD.position.x);
                transform.position = posCam;
            }
        }
    }
}
