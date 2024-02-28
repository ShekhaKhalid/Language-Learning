using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogTrigger : MonoBehaviour
{
    [SerializeField] private List<dialogString> dialogStrings = new List<dialogString>();
    [SerializeField] private Transform NPCTrasform;

    private bool hasSpoken = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !hasSpoken)
        {
            other.gameObject.GetComponent<DialogManager>().DialogueStart(dialogStrings, NPCTrasform);
            hasSpoken = true;   
        }
    }


    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("Player") )
    //    {
    //        other.gameObject.GetComponent<DialogManager>().DialogueStop();
            
    //    }
    //}
}


[System.Serializable]

public class dialogString
{
    public string text;
    public bool isEnd;
    public AudioClip clip; //what you will hear from the AI

    [Header("Branch")]
    public bool isQuestion;
    public string answerOption1;
    public AudioClip answerClip;


    [Header("Triggered Events")]
    public UnityEvent startDialogueEvent;
    public UnityEvent endDialogueEvent;
}
