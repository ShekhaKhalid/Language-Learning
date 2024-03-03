using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WelcomeUI : MonoBehaviour
{
    public GameObject ui;
    public void HideUI()
    {
        Destroy(ui);
    }
}
