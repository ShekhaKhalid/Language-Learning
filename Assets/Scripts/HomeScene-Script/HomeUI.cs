using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeUI : MonoBehaviour
{
    [SerializeField] private GameObject profile;
    [SerializeField] private GameObject home;
    [SerializeField] private GameObject settings;

    [SerializeField] private GameObject page1;
    [SerializeField] private GameObject page2;

   public void ShowHomepage()
    {
        home.SetActive(true);
        profile.SetActive(false);
        //settings.SetActive(false);
    }

    public void ShowProfilePage()
    {
        home.SetActive(false);
        profile.SetActive(true);
        //settings.SetActive(false);
    }


    public void ShowSettings()
    {
        home.SetActive(false);
        profile.SetActive(false);
        //settings.SetActive(true);
    }

    public void ShowPage1()
    {
        page1.SetActive(true);
        page2.SetActive(false);
    }

    public void ShowPage2()
    {
        page1.SetActive(false);
        page2.SetActive(true);
    }


    public void LoadAirport()
    {
        SceneManager.LoadScene("Airport");
    }

    public void LoadCoffee()
    {
        SceneManager.LoadScene("Coffe");
    }
}
