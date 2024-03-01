using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectsInLayer : MonoBehaviour
{

    public LayerMask EN;
    public LayerMask FR;


    void Start()
    {

        string selectedLanguageAI = PlayerPrefs.GetString("LearningLang", "English");
        Debug.Log(selectedLanguageAI);
        // Assign the target language based on the selected language.

        switch (selectedLanguageAI)
        {
            case "English":
                DestroyFN();
                break;
            case "French":
                DestroyEN();
                break;
            default:
                DestroyFN(); // Default to English if language is not recognized.
                Debug.Log("default");
                break;
        }
    }


    void DestroyEN()
    {
        // Find all objects with CapsuleCollider components in the scene
        CapsuleCollider[] characters = FindObjectsOfType<CapsuleCollider>();

        for (int i = 0; i < characters.Length; i++)
        {
            // Check if the collider's game object is in the target layer
            if (((1 << characters[i].gameObject.layer) & EN.value) != 0)
            {
                Destroy(characters[i].gameObject);
            }
        }
        Debug.Log("DestroyEN");
    }

    void DestroyFN()
    {
        // Find all objects with CapsuleCollider components in the scene
        CapsuleCollider[] characters = FindObjectsOfType<CapsuleCollider>();
        for (int i = 0; i < characters.Length; i++)
        {
            // Check if the collider's game object is in the target layer
            if (((1 << characters[i].gameObject.layer) & FR.value) != 0)
            {
                Destroy(characters[i].gameObject);
            }
        }
        Debug.Log("DestroyFN");
    }
}
