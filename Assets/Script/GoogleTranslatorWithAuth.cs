
using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

// This class is responsible for translating text from one language to another using the Google Translation API.
public class GoogleTranslatorWithAuth : MonoBehaviour
{

    private const string APIKey = "AIzaSyDdoNXBMW6F69h0Bg4hgpBO7FKng4HJ9mQ";

    public TextMeshProUGUI translatedTextMeshPro;

    public TMP_InputField inputField;

    public Button translateButton;

    

    private void Start()
    {
   
        translateButton.onClick.AddListener(OnTranslateButtonClick);

    }


    /*    // This method is called when the translateButton is clicked.
        private void OnTranslateButtonClick()
        {
            // Get the source text from the inputField.
            string sourceText = inputField.text;

            string selectedLanguage = PlayerPrefs.GetString("selectedLanguage", "English");
            Debug.Log(selectedLanguage);

            // Check if the sourceText is not empty or null.
            if (!string.IsNullOrEmpty(sourceText))
            {

                // Call the TranslateText method to translate the sourceText from Arabic ("ar") to English ("en").
                TranslateText("en", "ar", sourceText, (success, translatedText) =>
                {
                    // If the translation is successful, update the translatedTextMeshPro with the translated text.
                    if (success)
                    {
                        // Optional: Print the translated text to the console.
                        Debug.Log(translatedText);

                        // Update the TextMeshPro component's text with the translated text.
                        translatedTextMeshPro.text = translatedText;
                    }
                });
            }
        }*/


    // This method is called when the translateButton is clicked.
    private void OnTranslateButtonClick()
    {
        // Get the source text from the inputField.
        string sourceText = inputField.text;

        // Check if the sourceText is not empty or null.
        if (!string.IsNullOrEmpty(sourceText))
        {
            // Get the selected language from PlayerPrefs.
            string selectedLanguage = PlayerPrefs.GetString("selectedLanguage", "English");

            if (selectedLanguage == "Arabic")
            {
                TranslateText("en", "ar", sourceText, (success, translatedText) =>
                {
                    // If the translation is successful, update the translatedTextMeshPro with the translated text.
                    if (success)
                    {
                        // Optional: Print the translated text to the console.
                        Debug.Log(translatedText);

                        // Update the TextMeshPro component's text with the translated text.
                        translatedTextMeshPro.text = translatedText;
                    }
                });
            }
            else if(selectedLanguage== "French")
            {
                TranslateText("en", "fr", sourceText, (success, translatedText) =>
                {
                    // If the translation is successful, update the translatedTextMeshPro with the translated text.
                    if (success)
                    {
                        // Optional: Print the translated text to the console.
                        Debug.Log(translatedText);

                        // Update the TextMeshPro component's text with the translated text.
                        translatedTextMeshPro.text = translatedText;
                    }
                });
            }
            else
            {
                TranslateText("en", "en", sourceText, (success, translatedText) =>
                {
                    // If the translation is successful, update the translatedTextMeshPro with the translated text.
                    if (success)
                    {
                        // Optional: Print the translated text to the console.
                        Debug.Log(translatedText);

                        // Update the TextMeshPro component's text with the translated text.
                        translatedTextMeshPro.text = translatedText;
                    }
                });
            }
          
        }
    }




    // This method translates the sourceText from the sourceLanguage to the targetLanguage.
    public void TranslateText(string sourceLanguage, string targetLanguage, string sourceText, Action<bool, string> callback)
    {
        // Start the coroutine TranslateTextRoutine to handle the translation process.
        StartCoroutine(TranslateTextRoutine(sourceLanguage, targetLanguage, sourceText, callback));
    }

    // This coroutine sends a POST request to the Google Translation API to translate the sourceText.
    private IEnumerator TranslateTextRoutine(string sourceLanguage, string targetLanguage, string sourceText, Action<bool, string> callback)
    {
        // Create a list of form data containing the necessary parameters for the translation request.
        var formData = new List<IMultipartFormSection>
        {
            new MultipartFormDataSection("Content-Type", "application/json; charset=utf-8"),
            new MultipartFormDataSection("source", sourceLanguage),
            new MultipartFormDataSection("target", targetLanguage),
            new MultipartFormDataSection("format", "text"),
            new MultipartFormDataSection("q", sourceText)
        };

        // Create the URI for the translation request.
        var uri = $"https://translation.googleapis.com/language/translate/v2?key={APIKey}";

        // Create a UnityWebRequest with the specified URI and form data.
        var webRequest = UnityWebRequest.Post(uri, formData);

        // Send the web request and wait for the response.
        yield return webRequest.SendWebRequest();

        // Check if there was an HTTP error or a network error.
        if (webRequest.isHttpError || webRequest.isNetworkError)
        {
            // Log the error and invoke the callback with a success flag of false and an empty string.
            Debug.LogError(webRequest.error);
            callback.Invoke(false, string.Empty);

            // Exit the coroutine.
            yield break;
        }

        // Parse the JSON response from the Google Translation API.
        var parsedTexts = JSONNode.Parse(webRequest.downloadHandler.text);

        // Get the translated text from the parsed JSON.
        var translatedText = parsedTexts["data"]["translations"][0]["translatedText"];

        // Invoke the callback with a success flag of true and the translated text.
        callback.Invoke(true, translatedText);
    }
}


