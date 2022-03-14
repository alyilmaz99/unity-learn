using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OyunMekanigi : MonoBehaviour
{
    [SerializeField] int max;
    [SerializeField] int min;
    [SerializeField] int tahmin;
    [SerializeField] TextMeshProUGUI tahminMetni;

    // Start is called before the first frame update
    void Start()
    {
        OyununBaslangici();
    }

    public void OyununBaslangici()
    {
        max = max + 1;
        SonrakiTahmin();

    }

    public void Arttir()
    {
        min = tahmin;
        SonrakiTahmin();
    }
    public void Azalt()
    {
        max = tahmin;
        SonrakiTahmin();
    }

    public void SonrakiTahmin()
    {
        //tahmin = (min + max) / 2;
        tahmin = Random.Range(min, max);
        tahminMetni.text = tahmin.ToString();
    }
}
