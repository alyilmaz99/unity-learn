using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public GameObject missiles;
    public float mermiHizi = 8f;
    public float can = 100f;
    public float saniyeBasinaMermi = 0.6f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        MissiledController carpanMermi = collision.gameObject.GetComponent<MissiledController>();
        if (carpanMermi)
        {
            carpanMermi.CarptigindaYokOl();
            can -= carpanMermi.ZararVerme();
            if(can <= 0)
            {
               
                Destroy(gameObject);
            }

        }
    }

    void Start()
    {
        
    }

    void Update()
    {

        float atmaOlasiligi = Time.deltaTime *saniyeBasinaMermi ; 
        if(Random.value < atmaOlasiligi)
        {
            Fire();
        }

       
    }

    void Fire()
    {
        Vector3 baslangicPozisyonu = transform.position + new Vector3(0, -0.8f, 0);

        GameObject dusmaninMermisi = Instantiate(missiles, baslangicPozisyonu, Quaternion.identity) as GameObject;
        dusmaninMermisi.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -mermiHizi);
    }
}
