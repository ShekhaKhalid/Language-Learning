using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour
{
    public Canvas MainMene;
    public Canvas translete;

    private void Start()
    {
        MainMene.gameObject.SetActive(false);
        translete.gameObject.SetActive(false);
    }
    public void ShowMenu()
    {
        MainMene.gameObject.SetActive(true);
    }

    public void HideMenu()
    {
        MainMene.gameObject.SetActive(false);
    }

    public void Home()
    {
       
        SceneManager.LoadScene("static cov");
    }

    public void showTranslete()
    {
        MainMene.gameObject.SetActive(false);
        translete.gameObject.SetActive(true);
    }

    public void hideTranslete()
    {
      
        translete.gameObject.SetActive(false);
        MainMene.gameObject.SetActive(true);
    }


}
