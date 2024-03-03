
using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using ArabicSupport;


public class GoogleTranslatorWithAuth : MonoBehaviour
{

    private const string APIKey = "AIzaSyDdoNXBMW6F69h0Bg4hgpBO7FKng4HJ9mQ";

    public TextMeshProUGUI translatedTextMeshPro;

    public TMP_InputField inputField;

    public Button translateButton;

    public Text ArabicText;

    public TextMeshProUGUI KoreanText;


    /// Mother language Flag
    [SerializeField]
    private RawImage ar_motherFlag;
    [SerializeField]
    private RawImage fr_motherFlag;
    [SerializeField]
    private RawImage en_motherFlag;
    [SerializeField]
    private RawImage ko_motherFlag;

    /// Learn language Flag
    [SerializeField]
    private RawImage fr_LearnFlag;
    [SerializeField]
    private RawImage en_LearnFlag;


    string selectedLanguage;
    string selectedLanguage2;

    public Canvas Keyboard;
    private void Start()
    {

        selectedLanguage = PlayerPrefs.GetString("selectedLanguage", "English");
        selectedLanguage2 = PlayerPrefs.GetString("selectedLanguage2", "English");

        translateButton.onClick.AddListener(OnTranslateButtonClick);

        flagsChang();


    }

    private void flagsChang()
    {

        string targetLanguage;
        switch (selectedLanguage2)
        {
            case "English":
                targetLanguage = "en";
                en_motherFlag.gameObject.SetActive(true);
                break;
            case "French":
                targetLanguage = "fr";

                fr_motherFlag.gameObject.SetActive(true);
                break;
            case "Arabic":
                targetLanguage = "ar";

                ar_motherFlag.gameObject.SetActive(true);

                break;
            case "Korean":
                targetLanguage = "ko";
                ko_motherFlag.gameObject.SetActive(true);
                break;
            default:
                targetLanguage = "en";
                en_motherFlag.gameObject.SetActive(true);
                break;
        }


        string sourceLanguage;
        switch (selectedLanguage)
        {
            case "English":
                sourceLanguage = "en";
                en_LearnFlag.gameObject.SetActive(true);
                break;
            case "French":
                sourceLanguage = "fr";
                fr_LearnFlag.gameObject.SetActive(true);
                break;
            default:
                sourceLanguage = "en";
                en_LearnFlag.gameObject.SetActive(true);
                break;
        }


        print("1" + targetLanguage);
        print("2" + sourceLanguage);
    }

    // This method is called when the translateButton is clicked.
    private void OnTranslateButtonClick()
    {

        string sourceText = inputField.text;

        if (!string.IsNullOrEmpty(sourceText))
        {
            // Get the selected language from PlayerPrefs.

            Debug.Log(selectedLanguage);


            print("Mother" + selectedLanguage2);
            print("LeRN" + selectedLanguage);
            string targetLanguage;
            switch (selectedLanguage2)
            {
                case "English":
                    targetLanguage = "en";
                    translatedTextMeshPro.gameObject.SetActive(true);

                    break;
                case "French":
                    targetLanguage = "fr";
                    translatedTextMeshPro.gameObject.SetActive(true);

                    break;
                case "Arabic":
                    targetLanguage = "ar";
                    ArabicText.gameObject.SetActive(true);


                    break;
                case "Korean":
                    targetLanguage = "ko";
                    KoreanText.gameObject.SetActive(true);

                    break;
                default:
                    targetLanguage = "en";
                    en_motherFlag.gameObject.SetActive(true);
                    break;
            }


            string sourceLanguage;
            switch (selectedLanguage)
            {
                case "English":
                    sourceLanguage = "en";

                    break;
                case "French":
                    sourceLanguage = "fr";
                    break;
                default:
                    sourceLanguage = "en";

                    break;
            }


            print("1" + targetLanguage);
            print("2" + sourceLanguage);



            TranslateText(sourceLanguage, targetLanguage, sourceText, (success, translatedText) =>
            {

                if (success)
                {
                    Debug.Log(translatedText);
                    switch (selectedLanguage2)
                    {
                        case "Korean":
                            KoreanText.text = translatedText;

                            break;
                        case "Arabic":
                            ArabicText.text = ArabicFixer.Fix(translatedText);
                            break;
                        default:
                            translatedTextMeshPro.text = translatedText;
                            break;
                    }
                }
            });
        }


        Keyboard.gameObject.SetActive(false);
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









