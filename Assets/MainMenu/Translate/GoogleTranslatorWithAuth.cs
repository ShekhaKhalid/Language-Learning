/*using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;
using UnityEngine.Networking;

public class GoogleTranslatorWithAuth : MonoBehaviour
{
    private const string APIKey = "AIzaSyDdoNXBMW6F69h0Bg4hgpBO7FKng4HJ9mQ";

    private void Awake()
    {
        DoExample();
    }

 
    private void DoExample()
    {
        TranslateText("en", "ko", "I'm a real gangster.", (success, translatedText) =>
        {
            if (success) Debug.Log(translatedText);
        });

        TranslateText("ko", "en", "?? ?? ????.", (success, translatedText) =>
        {
            if (success) Debug.Log(translatedText);
        });
    }

    public void TranslateText(string sourceLanguage, string targetLanguage, string sourceText, Action<bool, string> callback)
    {
        StartCoroutine(TranslateTextRoutine(sourceLanguage, targetLanguage, sourceText, callback));
    }

    private IEnumerator TranslateTextRoutine(string sourceLanguage, string targetLanguage, string sourceText, Action<bool, string> callback)
    {
        var formData = new List<IMultipartFormSection>
        {
            new MultipartFormDataSection("Content-Type", "application/json; charset=utf-8"),
            new MultipartFormDataSection("source", sourceLanguage),
            new MultipartFormDataSection("target", targetLanguage),
            new MultipartFormDataSection("format", "text"),
            new MultipartFormDataSection("q", sourceText)
        };

        var uri = $"https://translation.googleapis.com/language/translate/v2?key={APIKey}";

        var webRequest = UnityWebRequest.Post(uri, formData);

        yield return webRequest.SendWebRequest();

        if (webRequest.isHttpError || webRequest.isNetworkError)
        {
            Debug.LogError(webRequest.error);
            callback.Invoke(false, string.Empty);

            yield break;
        }

        var parsedTexts = JSONNode.Parse(webRequest.downloadHandler.text);
        var translatedText = parsedTexts["data"]["translations"][0]["translatedText"];

        callback.Invoke(true, translatedText);
    }
}


*/


using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

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

    private void OnTranslateButtonClick()
    {
        string sourceText = inputField.text;
        if (!string.IsNullOrEmpty(sourceText))
        {
            TranslateText("ar", "en", sourceText, (success, translatedText) =>
            {
                if (success)
                {
                    Debug.Log(translatedText); // Optional: Print the translated text to the console
                    translatedTextMeshPro.text = translatedText; // Update the TextMeshPro component's text
                }
            });
        }
    }

    public void TranslateText(string sourceLanguage, string targetLanguage, string sourceText, Action<bool, string> callback)
    {
        StartCoroutine(TranslateTextRoutine(sourceLanguage, targetLanguage, sourceText, callback));
    }

    private IEnumerator TranslateTextRoutine(string sourceLanguage, string targetLanguage, string sourceText, Action<bool, string> callback)
    {
        var formData = new List<IMultipartFormSection>
        {
            new MultipartFormDataSection("Content-Type", "application/json; charset=utf-8"),
            new MultipartFormDataSection("source", sourceLanguage),
            new MultipartFormDataSection("target", targetLanguage),
            new MultipartFormDataSection("format", "text"),
            new MultipartFormDataSection("q", sourceText)
        };

        var uri = $"https://translation.googleapis.com/language/translate/v2?key={APIKey}";

        var webRequest = UnityWebRequest.Post(uri, formData);

        yield return webRequest.SendWebRequest();

        if (webRequest.isHttpError || webRequest.isNetworkError)
        {
            Debug.LogError(webRequest.error);
            callback.Invoke(false, string.Empty);

            yield break;
        }

        var parsedTexts = JSONNode.Parse(webRequest.downloadHandler.text);
        var translatedText = parsedTexts["data"]["translations"][0]["translatedText"];

        callback.Invoke(true, translatedText);
    }
}


