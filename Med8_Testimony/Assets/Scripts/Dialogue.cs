using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using TMPro;
using UnityEditor;
using UnityEngine.XR;


public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public string[] dialogueLines;
    public float textSpeed;
    public NPCTriggerDialogue npcTrigger;
    public Canvas dialogueCanvas;

    private int index;
    //private InputDevice controller;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dialogueText.text = string.Empty;
        //InitializeController();
    }

    /*void InitializeController()
    {
        var desiredCharacteristics = InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.HeldInHand;
        var devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(desiredCharacteristics, devices);

        if (devices.Count > 0)
            controller = devices[0];
    }*/

    // Update is called once per frame
    void Update()
    {
        /*if (!controller.isValid)
        {
            InitializeController();
            return;
        }*/

        /*if (!gameObject.activeSelf && npcTrigger != null && TryGetXRButtonDown(CommonUsages.primaryButton))
            {
                npcTrigger.TryStartDialogue();
                return;
            }*/

        /*if (TryGetXRButtonDown(CommonUsages.primaryButton))
        {
            if (dialogueText.text == dialogueLines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                dialogueText.text = dialogueLines[index];
            }
        }*/
    }

    public void DialogueInput()
    {
        if (dialogueText.text == dialogueLines[index])
        {
            NextLine();
        }
        else
        {
            StopAllCoroutines();
            dialogueText.text = dialogueLines[index];
        }
    }

    /*bool TryGetXRButtonDown(InputFeatureUsage<bool> button)
    {
        if (controller.TryGetFeatureValue(button, out bool pressed) && pressed)
            return true;
        return false;
    }*/

    public void StartDialogue()
    {
        index = 0;
        dialogueText.text = string.Empty;
        gameObject.SetActive(true);
        dialogueCanvas.gameObject.SetActive(true);
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in dialogueLines[index].ToCharArray())
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < dialogueLines.Length - 1)
        {
            index++;
            dialogueText.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
            dialogueCanvas.gameObject.SetActive(false);
        }
    }
}
