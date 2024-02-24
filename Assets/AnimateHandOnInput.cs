using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class AnimateHandOnInput : MonoBehaviour
{

    public InputActionProperty pinchAnimationAction;
    public InputActionProperty graipAnimationAction;
    public Animator animator;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float triggierValu = pinchAnimationAction.action.ReadValue<float>();
        float gripierValu = graipAnimationAction.action.ReadValue<float>();



        animator.SetFloat("Trigger", triggierValu);
        animator.SetFloat("Grip", gripierValu);

    }
}
