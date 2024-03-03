using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadPlace : MonoBehaviour
{
    public void loadCoffe()
    {
        SceneManager.LoadScene("Coffe");
    }
    public void loadAirport()
    {
        SceneManager.LoadScene("Airport");
    }
    public void loadKaraoke()
    {
        SceneManager.LoadScene("Karaoke");
    }
}
