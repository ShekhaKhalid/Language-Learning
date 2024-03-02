using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using Oculus.Voice;
using Meta.WitAi.Json;
using System.Text.RegularExpressions;
using UnityEngine.Windows;
using Convai.Scripts.Utils;

public class DialogManager : MonoBehaviour
{
    [SerializeField] private GameObject dialogueParent;
    [SerializeField] private TMP_Text dialogueText;
    

    [SerializeField] private float typingSpeed = 0.05f;
    [SerializeField] private float turnSpeed = 2f;

    private List<dialogString> dialogueList;

    //there is a player part to not make him move

    private int currentDialogueIndex = 0;

    [Header("Sound")]
    [SerializeField] private Button option1Listen;
    [SerializeField] private Button option1Speak;
    [SerializeField] private Button option1Stop;
    [SerializeField] private TMP_Text option1Text;
    [SerializeField] private TMP_Text textSpoken;

    [SerializeField] private AudioClip correct;
    [SerializeField] private AudioClip wrong;
    public AppVoiceExperience voiceRecognizer;
    public AudioSource audioSource;


    private string currentAnswerOption;
    [SerializeField] private Animator anim;
    public ConvaiLipSync lips;
   private void Start()
    {
        dialogueParent.SetActive(false);
        voiceRecognizer.VoiceEvents.OnPartialResponse.AddListener(HandleSpeak);
       
    }
    public void DialogueStart(List<dialogString> textToprint, Transform NPC)
    {
        dialogueParent.SetActive(true);
       

        dialogueList = textToprint;
        currentDialogueIndex = 0;

        //DisableButtons();

        StartCoroutine(PrintDialogue());
    }

    private void DisableButtons()
    {
       

        option1Listen.interactable = false;
        option1Speak.interactable =false;
        option1Stop.interactable = false;
        

    }


    private bool spoken=false;  
    private IEnumerator PrintDialogue()
    {
        while(currentDialogueIndex < dialogueList.Count)
        {
            dialogString line = dialogueList[currentDialogueIndex];
            print(currentDialogueIndex);
            line.startDialogueEvent?.Invoke();

            if (line.isQuestion)
            {
                audioSource.PlayOneShot(line.clip);
                yield return StartCoroutine(TypeText(line.text));
                anim.SetBool("Talk", false);
                lips.StopLipSync();


                option1Text.text = line.answerOption1;
                currentAnswerOption = option1Text.text;
    RemoveListenSelectedListener();

                option1Listen.onClick.AddListener(() => HandleListenSelected(line.answerClip));
                option1Speak.onClick.AddListener(() => StartRecord());
                option1Stop.onClick.AddListener(() => StopRecording(currentAnswerOption));
                print(currentDialogueIndex+"o"+line.answerClip);
                print("!" + spoken);
               
                yield return new WaitUntil(() => spoken);
            }
            else
            {
                
                yield return StartCoroutine(TypeText(line.text));
                audioSource.PlayOneShot(line.clip);
            }

            line.endDialogueEvent?.Invoke();
            spoken = false;
            currentDialogueIndex++;
        }
        print("done11111");
        DialogueStop();
    }

    

    private void HandleListenSelected(AudioClip audio)
    {
        //DisableButtons();

        audioSource.PlayOneShot(audio);
    }

    private IEnumerator TypeText(string text)
    {
        dialogueText.text = "";

        foreach (char letter in text.ToCharArray())
        {
            dialogueText.text += letter;
            anim.SetBool("Talk", true);
            lips.LipSyncCharacter();
            yield return new WaitForSeconds(typingSpeed);
        }
        if (!dialogueList[currentDialogueIndex].isQuestion)
        {
            //yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        }
        if (dialogueList[currentDialogueIndex].isEnd)
            DialogueStop();
        //currentDialogueIndex++;
    }

    public void DialogueStop()
    {
        StopAllCoroutines();
        dialogueText.text = "";
        dialogueParent.SetActive(false);
    }


    //WIT AI

    public void StartRecord()
    {
        
        voiceRecognizer.Activate();
    }

    public void StopRecording(string word)
    {
        voiceRecognizer.Deactivate();
        if (checkSpeak(spokenWord, word))
        {
            spoken = true;
            audioSource.PlayOneShot(correct);
            print("!!" + spoken);
        }
        else
            audioSource.PlayOneShot(wrong);
    }

    private string spokenWord;

    public void HandleSpeak(WitResponseNode responseNode)
    {
         spokenWord = responseNode["text"].Value.ToString().ToLower();

        textSpoken.text = spokenWord;
        
        
        
    }

    public bool checkSpeak(string wordSpoken, string word)
    {
        word = word.ToLower();
        wordSpoken = wordSpoken.ToLower();  
        string pattern = "[^a-zA-Z0-9]";

        // Replace special characters with an empty string
        //string wordNoSpecial = Regex.Replace(word, pattern, "");
        //string wordSpokenNoSpecial = Regex.Replace(wordSpoken, pattern, "");

        print(word + " 11" + wordSpoken);
        if(word == wordSpoken)
        {
            return true;
        }
       
        return false;
    }
    private void RemoveListenSelectedListener()
    {
        option1Listen.onClick.RemoveAllListeners();
    }
}
