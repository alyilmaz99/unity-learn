using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OyunTopuKontrolu : MonoBehaviour {

    public oyunBariKontrolu oyunBari;
    private bool oyunBasladiMi;
    private Vector3 TopIleBarArasindakiMesafe;
	// Use this for initialization
	void Start () {
        TopIleBarArasindakiMesafe = this.transform.position - oyunBari.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(!oyunBasladiMi)
        {
            this.transform.position = oyunBari.transform.position + TopIleBarArasindakiMesafe;
            if (Input.GetMouseButtonDown(0))
            {
                oyunBasladiMi = true;
                this.GetComponent<Rigidbody2D>().velocity = new Vector3(3f, 9f, 0f);
            }
        }
        
	}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 ufakSapma = new Vector2(Random.Range(0f, 0.3f), Random.Range(0f, 0.3f));

        if (oyunBasladiMi)
        {
            GetComponent<AudioSource>().Play();
            GetComponent<Rigidbody2D>().velocity += ufakSapma;
        }
       
    }
}
