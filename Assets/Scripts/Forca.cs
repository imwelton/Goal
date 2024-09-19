using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Forca : MonoBehaviour
{
    private Rigidbody2D bola;
    private float force = 1000;
    private Rotacao rot;
    // Start is called before the first frame update
    void Start()
    {
        bola = GetComponent<Rigidbody2D>();
        rot = GetComponent<Rotacao>();
    }

    // Update is called once per frame
    void Update()
    {
        AplicaForca();
    }

    void AplicaForca()
    {
        float x = force * Mathf.Cos(rot.zRotate * Mathf.Deg2Rad);
        float y = force * Mathf.Sin(rot.zRotate * Mathf.Deg2Rad);

        if (Input.GetKeyUp(KeyCode.Space))
        {
            bola.AddForce(new Vector2(x, y));
        }
    }
}
