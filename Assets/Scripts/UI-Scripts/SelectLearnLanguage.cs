using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectLearnLanguage : MonoBehaviour
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
        string selectedLanguageAI = dropdown.options[dropdown.value].text;
        PlayerPrefs.SetString("LearningLang", selectedLanguageAI);
        print(selectedLanguageAI);
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene("AItest");
    }
}




