using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissiledController : MonoBehaviour
{
    public float hitPower = 10f;

    public void CarptigindaYokOl()
    {
        Destroy(gameObject);
    }

    public float ZararVerme()
    {
        return hitPower;
    }
}
