using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUI : MonoBehaviour
{
    [SerializeField] private GameObject logo;
    [SerializeField] private GameObject background;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip welcome;
    [SerializeField] private AudioClip music;


    [SerializeField] private GameObject userInfoUI;
    [SerializeField] private GameObject langInfoUI;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
        StartCoroutine(logoAnimation());
    }

    private IEnumerator logoAnimation()
    {
        audioSource.PlayOneShot(welcome);
        yield return new WaitForSeconds(2.7f); // Wait for 3 seconds

       Destroy(logo);
       background.SetActive(true);
        audioSource.PlayOneShot(music); 
    }

    public void HideUserInfo()
    {
        userInfoUI.SetActive(false);
        langInfoUI.SetActive(true);
    }
}
