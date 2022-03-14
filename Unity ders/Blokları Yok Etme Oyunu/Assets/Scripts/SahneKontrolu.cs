using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SahneKontrolu : MonoBehaviour {

	
    public void SonrakiSahne()
    {
        
        int mevcutSahneninIndeksi = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(mevcutSahneninIndeksi + 1);

    }
    public void SahneyeYonlen(string sahneIsmi)
    {
        SceneManager.LoadScene(sahneIsmi);
    }

    public void OyunSahnesineYonlen()
    {
        
        SceneManager.LoadScene(1);
    }

    public void OyundanCikis()
    {
        Application.Quit();
    }
    public void MenuSahnesineYonlen()
    {
        SceneManager.LoadScene(0);
    }
    public void BloklarinYokOlmasi()
    {
        if (Bloklar.kirilabilirBlokSayisi <= 0)
        {
            SonrakiSahne();
        }

    }

}
