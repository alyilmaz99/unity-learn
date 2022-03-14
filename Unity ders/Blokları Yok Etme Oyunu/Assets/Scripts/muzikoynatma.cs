using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class muzikoynatma : MonoBehaviour {

    
    static muzikoynatma TekMuzikOynaticisi = null;
    /*
    [SerializeField] int x = 0;
    static int y = 0;   // Geçici bellekte tutulan y değerine bakıyor.
    //public int x = 0;
    */


    //https://docs.unity3d.com/Manual/ExecutionOrder.html
    void Awake()
    {
        Debug.Log("Awake matotu çalıştı.");
        
        if (TekMuzikOynaticisi != null) //muzik oynaticisi varsa
        {
            Debug.Log(GetInstanceID());
            Destroy(gameObject);
        }
        else  //muzik oynatıcısı yoksa
        {
           
            TekMuzikOynaticisi = this;
            Debug.Log(TekMuzikOynaticisi.GetInstanceID());

            GameObject.DontDestroyOnLoad(gameObject);

        }
    }

    // Use this for initialization
    void Start () {
        Debug.Log("Start Fonksiyonu çalıştı.");


        /*
        x++; //x = x+1;   x +=1;
        y++; //y= y + 1;    y+=1;
        Debug.Log("Statik Değiken: "+ y);
        Debug.Log("Normal Değişken: " + x);
        */



    }

    
    // Update is called once per frame
    void Update () {
        
	}
}
