using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class OyunYoneticisi : MonoBehaviour
{

    [SerializeField] Text oyunHikayesiYazisi;
    [SerializeField] Durum baslangicDurumu;

    Durum guncelDurum;
    // Start is called before the first frame update
    void Start()
    {
        guncelDurum = baslangicDurumu;
        oyunHikayesiYazisi.text = baslangicDurumu.DurumHikayesi();
    }

    // Update is called once per frame
    void Update()
    {
        var sonrakiDurum = guncelDurum.sonrakiDurumlariAl();

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            guncelDurum = sonrakiDurum[0];
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)){
            guncelDurum = sonrakiDurum[1];
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            guncelDurum = sonrakiDurum[2];
        }

        oyunHikayesiYazisi.text = guncelDurum.DurumHikayesi();
    }
}
