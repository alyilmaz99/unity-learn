using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float jumpPower;
    //burda myspeedx deðiþkeni oluþturduk hýz deðerini klavyeden almak için
    private float mySpeedX;
    //oyunda hýz deðerini almak için deðiþken oluþturduk
    [SerializeField] float speed;
    //Rigidboy2d deðiþkeni oluþturduk, sürekli tekrara girmemesi için
    private Rigidbody2D myBody;
    //vectör3 deðiþkeni oluþturduk, oyundakiþ local scale deðerini tutmak için
    private Vector3 defaultLocalScale;
    //Zemin üzerinde mi deðil mi bunun için bool deðer oluþturuyoruz
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
        //mybody deðiþkenine Rigidbody2d componentlerini atadýk
        myBody = GetComponent<Rigidbody2D>();
        //defaultlocalscale deðiþkenine transform.localScale kodu ile transform bölümünü scale deðerlerini atadýk, transform u direkt çaðýrma sebebimiz ,yaygýn olduðu için herhangi bir oluþturmaya gerek duymuyoruz
        defaultLocalScale = transform.localScale;
        myAnimator = GetComponent<Animator>();
        arrowNumberText.text = arrowNumber.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        //Karaktere hýzz gönderiyoruz . animatörden durumunu deðiþtirmek için
        myAnimator.SetFloat("Speed",Mathf.Abs(mySpeedX));


        //burda myspeedx deðiþkenine klavyeden girilen input deðerini aldýk
        //Debug.Log(Input.GetAxis("Horizontal"));
        mySpeedX = Input.GetAxis("Horizontal");
        //burda mybodye rigidboy deðerlerini atamýþtýk, o yüzden velocitysine ulaþýp ona yeni deðer atadýk, bu deðer Vector2 cinsinden oldu
        myBody.velocity = new Vector2(mySpeedX * speed, myBody.velocity.y);

        #region playerýn saðp ve sol hareket yönüne göre yüzünün dönmesi
        // myspeedX deðerini sorguladýk ve karakterin yüzünü saða veya sola çevirdik
        if (mySpeedX > 0)
        {
            transform.localScale = new Vector3(defaultLocalScale.x, defaultLocalScale.y, defaultLocalScale.z);
        }
        else if (mySpeedX < 0)
        {
            transform.localScale = new Vector3(-defaultLocalScale.x, defaultLocalScale.y, defaultLocalScale.z);
        }
        #endregion


        #region playerýn zýplamasýnýn kontrolü
        //Karakteri zýplattýk yukardaki kodu y ekseni için kullandýk
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (onGround == true)
            {
                // Debug.Log("Tuþa basýldý");
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

        #region playerýn ok atmasýnýn kontrolü
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
