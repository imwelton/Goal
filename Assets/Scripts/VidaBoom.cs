 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaBoom : MonoBehaviour
{
    private GameObject bombaRep;

    private void Start()
    {
        bombaRep = GameObject.Find("barril");
    }

    private void Update()
    {
        StartCoroutine(Vida());
    }

    IEnumerator Vida()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(bombaRep.gameObject);
        Destroy(this.gameObject);
    }
}
