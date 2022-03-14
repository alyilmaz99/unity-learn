using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oyunBariKontrolu : MonoBehaviour {

	public bool otomatikOynama = false;
	private OyunTopuKontrolu oyundakiTop;


	// Use this for initialization
	void Start () {
		oyundakiTop = GameObject.FindObjectOfType<OyunTopuKontrolu>();	
	}
	
	// Update is called once per frame
	void Update () {
        if (otomatikOynama)
        {
			otomatikHareket();
        }
        else
        {
			faremizleHareket();
		}

		
	}

	void faremizleHareket()
    {
		Vector3 oyunBariKonumu = new Vector3(0f, this.transform.position.y, 0f);
		float mouseKonumX = Input.mousePosition.x / Screen.width * 16;
		oyunBariKonumu.x = Mathf.Clamp(mouseKonumX, 1f, 15f);
		this.transform.position = oyunBariKonumu;
	}
	void otomatikHareket()
    {
		Vector3 oyunBariKonumu = new Vector3(0f, this.transform.position.y, 0f);
		Vector2 topunKonumu = oyundakiTop.transform.position;
		oyunBariKonumu.x = Mathf.Clamp(topunKonumu.x, 1f, 15f);
		this.transform.position = oyunBariKonumu;
	}
}
