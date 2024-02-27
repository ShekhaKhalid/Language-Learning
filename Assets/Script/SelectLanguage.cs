using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SelectLanguage : MonoBehaviour
{
    public TMP_Dropdown dropdown;

    void Start()
    {
        dropdown.onValueChanged.AddListener(delegate {
            OnDropdownValueChanged(dropdown);
        });
    }

    void OnDropdownValueChanged(TMP_Dropdown dropdown)
    {
        string selectedLanguage = dropdown.options[dropdown.value].text;
        PlayerPrefs.SetString("selectedLanguage", selectedLanguage);
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene("main menu");
    }
}
