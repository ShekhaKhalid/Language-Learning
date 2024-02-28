using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private GameObject info;
    [SerializeField] private GameObject lang;
    public void LoadHomepage()
    {
        SceneManager.LoadScene("Homepage");
    }

    public void ShowLang()
    {
        info.SetActive(false);
        info.SetActive(true);

    }

    public void ShowInfo()
    {
        info.SetActive(true);
        info.SetActive(false);
    }
}
