﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OyununBasarisizSonlanmasi : MonoBehaviour {

    private SahneKontrolu yonetici;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        yonetici = GameObject.FindObjectOfType<SahneKontrolu>();
        yonetici.SahneyeYonlen("Kaybetme");
    }
}
