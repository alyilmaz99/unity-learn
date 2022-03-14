using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float jumpPower;
    //burda myspeedx de�i�keni olu�turduk h�z de�erini klavyeden almak i�in
    private float mySpeedX;
    //oyunda h�z de�erini almak i�in de�i�ken olu�turduk
    [SerializeField] float speed;
    //Rigidboy2d de�i�keni olu�turduk, s�rekli tekrara girmemesi i�in
    private Rigidbody2D myBody;
    //vect�r3 de�i�keni olu�turduk, oyundaki� local scale de�erini tutmak i�in
    private Vector3 defaultLocalScale;
    //Zemin �zerinde mi de�il mi bunun i�in bool de�er olu�turuyoruz
    public bool onGround;
    private bool canDoubleJump;
    [SerializeField] GameObject arrow;
    [SerializeField] bool attacked;
    [SerializeField] float currentAtackTimer;
    [SerializeField] float defaultAttackTimer;
    private Animator myAnimator;
    [SerializeField] int arrowNumber;
    [SerializeField] Text arrowNumberText;
    [SerializeField] AudioClip dieMusic;
    [SerializeField] GameObject winPanel, losePanel;

    void Start()
    {
        attacked = false;
        //mybody de�i�kenine Rigidbody2d componentlerini atad�k
        myBody = GetComponent<Rigidbody2D>();
        //defaultlocalscale de�i�kenine transform.localScale kodu ile transform b�l�m�n� scale de�erlerini atad�k, transform u direkt �a��rma sebebimiz ,yayg�n oldu�u i�in herhangi bir olu�turmaya gerek duymuyoruz
        defaultLocalScale = transform.localScale;
        myAnimator = GetComponent<Animator>();
        arrowNumberText.text = arrowNumber.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        //Karaktere h�zz g�nderiyoruz . animat�rden durumunu de�i�tirmek i�in
        myAnimator.SetFloat("Speed",Mathf.Abs(mySpeedX));


        //burda myspeedx de�i�kenine klavyeden girilen input de�erini ald�k
        //Debug.Log(Input.GetAxis("Horizontal"));
        mySpeedX = Input.GetAxis("Horizontal");
        //burda mybodye rigidboy de�erlerini atam��t�k, o y�zden velocitysine ula��p ona yeni de�er atad�k, bu de�er Vector2 cinsinden oldu
        myBody.velocity = new Vector2(mySpeedX * speed, myBody.velocity.y);

        #region player�n sa�p ve sol hareket y�n�ne g�re y�z�n�n d�nmesi
        // myspeedX de�erini sorgulad�k ve karakterin y�z�n� sa�a veya sola �evirdik
        if (mySpeedX > 0)
        {
            transform.localScale = new Vector3(defaultLocalScale.x, defaultLocalScale.y, defaultLocalScale.z);
        }
        else if (mySpeedX < 0)
        {
            transform.localScale = new Vector3(-defaultLocalScale.x, defaultLocalScale.y, defaultLocalScale.z);
        }
        #endregion


        #region player�n z�plamas�n�n kontrol�
        //Karakteri z�platt�k yukardaki kodu y ekseni i�in kulland�k
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (onGround == true)
            {
                // Debug.Log("Tu�a bas�ld�");
                myBody.velocity = new Vector2(myBody.velocity.x, jumpPower);
                canDoubleJump = true;
                myAnimator.SetTrigger("Jump");
            }
            else
            {
                if(canDoubleJump == true)
                {
                    myBody.velocity = new Vector2(myBody.velocity.x, jumpPower);
                    canDoubleJump = false;
                }
            }
        }
        #endregion

        #region player�n ok atmas�n�n kontrol�
        if (Input.GetMouseButtonDown(0) && arrowNumber > 0)
        {
            if(attacked == false)
            {
                attacked = true;
                myAnimator.SetTrigger("Attack");
                Invoke("Fire", 0.5f);
            }
            
        }
        #endregion
        if(attacked == true)
        {
            currentAtackTimer -= Time.deltaTime;
        }
        else
        {
            currentAtackTimer = defaultAttackTimer;
        }

        if(currentAtackTimer <= 0)
        {
            attacked = false;
        }
    }
    void Fire()
    {
        

        if (transform.localScale.x > 0)
        {
            GameObject okumuz = Instantiate(arrow, transform.localPosition + new Vector3(1, 0, 0), Quaternion.identity);
            okumuz.transform.parent = GameObject.Find("Arrows").transform;
            okumuz.GetComponent<Rigidbody2D>().velocity = new Vector2(5f, 0f);
        }
        else
        {
            GameObject okumuz = Instantiate(arrow, transform.localPosition + new Vector3(-1, 0, 0), Quaternion.identity);
            Vector3 okumuzScale = okumuz.transform.localScale;
            okumuz.transform.localScale = new Vector3(-okumuzScale.x, okumuzScale.y, okumuzScale.z);
            okumuz.GetComponent<Rigidbody2D>().velocity = new Vector2(-5f, 0f);
        }

        arrowNumber-- ;
        arrowNumberText.text = arrowNumber.ToString();
    }
    IEnumerator Wait(bool win)
    {
        yield return new WaitForSecondsRealtime(1f);
        Time.timeScale = 0;
        if (win == true)
        {
            winPanel.SetActive(true);
        }
        else
        {
            losePanel.SetActive(true);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            GetComponent<TimeControl>().enabled = false;
            Die();
        }
        else if (collision.gameObject.CompareTag("Finish"))
        {
            Destroy(collision.gameObject);
            /*winPanel.active = true;
            Time.timeScale = 0;*/
            StartCoroutine(Wait(true));

        }
    }

    public void Die()
    {
        GameObject.Find("Sound Controller").GetComponent<AudioSource>().clip = null;
        GameObject.Find("Sound Controller").GetComponent<AudioSource>().PlayOneShot(dieMusic);
        myAnimator.SetTrigger("Die");
        myAnimator.SetFloat("Speed",0);
        myBody.constraints = RigidbodyConstraints2D.FreezeAll;
        enabled = false;
        // losePanel.active = true;
        // Time.timeScale = 0;

        StartCoroutine(Wait(false));
    }
}
