using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    [SerializeField] float shipSpeed;
    float xmin;
    float xmax;
    private float paddingNumber = 0.7f;
    public GameObject Missiles;
    public float missilesVelocity = 3f;
    public float fireTime = 2f;
    public float can = 400f;
   

    void Start()
    {
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftPoint = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightPoint = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));

        xmin = leftPoint.x + paddingNumber;
        xmax = rightPoint.x - paddingNumber;


    }
    void Fire()
    {
        GameObject shipMissiles = Instantiate(Missiles, transform.position + new Vector3(0,0.8f,0), Quaternion.identity) as GameObject;
        shipMissiles.GetComponent<Rigidbody2D>().velocity = new Vector3(0f, missilesVelocity, 0f);
    }

    void Update()
    {
        //gemimimzin x ekseni deðeri eðer -8 ile 8 arasýnda ise yeniX e ata
        float newX = Mathf.Clamp(transform.position.x, xmin, xmax);
        transform.position = new Vector3(newX,transform.position.y,transform.position.z);

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //transform.position += new Vector3(-shipSpeed*Time.deltaTime, 0, 0);
            //Vector3.left = (-1,0,0)

            transform.position += Vector3.left * shipSpeed * Time.deltaTime;

        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            //transform.position += new Vector3(shipSpeed*Time.deltaTime, 0, 0);
            transform.position += Vector3.right * shipSpeed * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            InvokeRepeating("Fire",0.00001f,fireTime);
        }
        if (Input.GetKeyUp(KeyCode.Space)){
            CancelInvoke("Fire");
        }
    }

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
}
