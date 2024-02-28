/*using System.Collections;
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
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SelectLanguage : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    public TMP_Dropdown dropdown2; // Declare the new dropdown

    void Start()
    {
        dropdown.onValueChanged.AddListener(delegate {
            OnDropdownValueChanged(dropdown);
        });

        // Subscribe to the onValueChanged event of the new dropdown
        dropdown2.onValueChanged.AddListener(delegate {
            OnDropdown2ValueChanged(dropdown2);
        });
    }

    void OnDropdownValueChanged(TMP_Dropdown dropdown)
    {
        string selectedLanguage = dropdown.options[dropdown.value].text;
        PlayerPrefs.SetString("selectedLanguage", selectedLanguage);
    }

    void OnDropdown2ValueChanged(TMP_Dropdown dropdown)
    {
        string selectedLanguage2 = dropdown2.options[dropdown2.value].text;
        PlayerPrefs.SetString("selectedLanguage2", selectedLanguage2);
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene("main menu");
    }
}
