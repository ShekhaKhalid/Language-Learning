using UnityEngine;
using System.ComponentModel;
using UnityEngine.UI;
using System;
using TMPro;

public class Example : MonoBehaviour {

    bool triggerResultEmail= false;
    bool resultEmailSucess;

 
    public TMP_InputField UserName;
    public TMP_InputField UserEmail;
    

  

    public void sendEmail()
    {
        PlayerPrefs.SetString("UserName", UserName.text);
        PlayerPrefs.SetString("UserEmail", UserEmail.text);
        SimpleEmailSender.emailSettings.UserName = UserName.text.Trim();
        SimpleEmailSender.emailSettings.UserEmail = UserEmail.text.Trim();

        SimpleEmailSender.Send(UserEmail.text, UserName.text,SendCompletedCallback);
    }

    private void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
    {
        if (e.Cancelled || e.Error != null)
        {
            print("Email not sent: " + e.Error.ToString());

            resultEmailSucess = false;
            triggerResultEmail = true;
        }
        else
        {
            print("Email successfully sent.");

            resultEmailSucess = true;
            triggerResultEmail = true;
        }
    }
}
