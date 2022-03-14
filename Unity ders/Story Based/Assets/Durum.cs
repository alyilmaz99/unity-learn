using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName ="Durum")]

public class Durum : ScriptableObject
{
    [TextArea(12, 10)] [SerializeField] string hikayeMetni;

    [SerializeField] Durum[] sonrakiDurum;

    public string DurumHikayesi()
    {
        return hikayeMetni;
    }

    public Durum[] sonrakiDurumlariAl()
    {
        return sonrakiDurum;
    }
}
