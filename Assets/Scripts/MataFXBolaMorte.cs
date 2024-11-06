using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MataFXBolaMorte : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MataFX());
    }
    IEnumerator MataFX()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);

    }
}
