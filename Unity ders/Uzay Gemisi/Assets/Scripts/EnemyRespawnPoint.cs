using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawnPoint : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float width;
    public float height;

    private bool RightMove = true;
    private float speed = 5f;
    private float xmax;
    private float xmin;

    void Start() 
    {
        float objectCameraDistance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftPoint = Camera.main.ViewportToWorldPoint(new Vector3(0,0,objectCameraDistance));
        Vector3 rightPoint = Camera.main.ViewportToWorldPoint(new Vector3(1,0,objectCameraDistance));
        xmax = rightPoint.x;
        xmin = leftPoint.x;


        foreach(Transform cocuk in transform)
        {
            GameObject enemy = Instantiate(enemyPrefab, cocuk.transform.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = cocuk;
        }

      
    }
    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position,new Vector3(width,height));
    }


    void Update()
    {
        if (RightMove)
        {
            //transform.position += new Vector3(speed * Time.deltaTime, 0);
            transform.position += speed*Vector3.right * Time.deltaTime;
        }
        else
        {
            transform.position += new Vector3(-speed * Time.deltaTime, 0);
        }

        float rightLimit = transform.position.x +width /2;
        float leftLimit = transform.position.x - width/2;

        if(rightLimit > xmax)
        {
            RightMove = false;
        }else if(leftLimit < xmin)
        {
            RightMove = true;
        }
    }
}
