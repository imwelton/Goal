using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoedasControl : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("bola")) return;

        else
        {
            ScoreManager.instance.ColetaMoedas(10);
            AudioManager.instance.SonsFXToca(0);
            Destroy(gameObject);
        }
    }
}
