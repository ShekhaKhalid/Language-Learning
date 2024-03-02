using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProfileInfo : MonoBehaviour
{
    [SerializeField] private TMP_InputField email;
    [SerializeField] private TMP_InputField namee;
    void Start()
    {
        namee.text = PlayerPrefs.GetString("UserName");
        email.text = PlayerPrefs.GetString("UserEmail");

    }

    
}
