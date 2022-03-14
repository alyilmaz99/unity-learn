using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bloklar : MonoBehaviour {

    public GameObject efekt;
    public static int kirilabilirBlokSayisi = 0;
    bool KirilabilirMi;
    public int vurulmaSayisi;
    private SahneKontrolu sahneYoneticisi;
    public Sprite[] blokGorunumleri;
	// Use this for initialization
	void Start () {
        KirilabilirMi = (this.tag == "kirilabilir");
        if (KirilabilirMi)
        {
            kirilabilirBlokSayisi++;
        }
        vurulmaSayisi = 0;
        sahneYoneticisi = GameObject.FindObjectOfType<SahneKontrolu>();
        
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        

        if (KirilabilirMi)
        {
            GetComponent<AudioSource>().Play();
            vurulmaKontrolu();
        }
        
    }

    public void vurulmaKontrolu()
    {
         int can;
        can = blokGorunumleri.Length + 1;
        vurulmaSayisi++;
        //vurulmaSayisi = vurulmaSayisi + 1;
        //vurulmaSayisi += 1;
        if (vurulmaSayisi >= can)
        {
            kirilabilirBlokSayisi--;
           GameObject efektimiz = Instantiate(efekt,gameObject.transform.position,Quaternion.identity) as GameObject;
            efektimiz.GetComponent<ParticleSystem>().startColor = gameObject.GetComponent<SpriteRenderer>().color;
            Destroy(gameObject);
            sahneYoneticisi.BloklarinYokOlmasi();
            
        }
        else
        {
            BlokGoruntusunuDegistir();
        }

    }

    public void BlokGoruntusunuDegistir()
    {
        //vurulma sayisi = 1
        this.GetComponent<SpriteRenderer>().sprite = blokGorunumleri[vurulmaSayisi - 1];

    }


    public void SonrakiSahne()
    {
        sahneYoneticisi.SonrakiSahne();
    }

   
    
}
