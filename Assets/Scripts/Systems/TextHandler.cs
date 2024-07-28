using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using static UnityEngine.Awaitable;
using System.IO;

public class TextHandler : MonoBehaviour
{
    [SerializeField, TextAreaAttribute] private string[] dialogueLines;

    [SerializeField] private TMP_Text dialogueBox;
    [SerializeField] private Button nextDialogue;
    
    [SerializeField] private Image imageDisplay;
    [SerializeField] private Sprite[] backgroundImage;

    [SerializeField] private int totalDialogue;
    [SerializeField] private int[] battleTriggerPoints;
    [SerializeField] private int[] backgroundTriggerPoints;

    [SerializeField] private float textStartDelay;
    [SerializeField] private float textCharacterDelay;
    [SerializeField] private string typingChar;
    [SerializeField] private bool hasTypingChar;

    private bool isTyping;
    private string textWriter;    
    private int dialogueTracker, battleTriggerTracker, backgroundTriggerTracker;    

    // Start is called before the first frame update
    void Start()
    {
        dialogueTracker = 0;
        battleTriggerTracker = 0;
        backgroundTriggerTracker = 0;
        dialogueBox.text = dialogueLines[dialogueTracker];
        textWriter = dialogueLines[dialogueTracker];
        dialogueTracker++;
        isTyping = true;
        StartCoroutine(TypeTMP());
    }

    public void NextDialogue()
    {
        if (!isTyping)
        {
            if (dialogueTracker == backgroundTriggerPoints[backgroundTriggerTracker])
            {
                imageDisplay.sprite = backgroundImage[backgroundTriggerTracker];
                backgroundTriggerTracker++;
            }

            if (dialogueTracker == battleTriggerPoints[battleTriggerTracker])
            {
                Debug.Log("Battle");
                battleTriggerTracker++;
            }

            if (dialogueTracker < totalDialogue)
            {
                dialogueBox.text = dialogueLines[dialogueTracker];
                textWriter = dialogueLines[dialogueTracker];
                dialogueTracker++;
                isTyping = true;

                StartCoroutine(TypeTMP());
            }
            else
            {
                Debug.Log("The End");
            }
        } else
        {
            StopCoroutine(TypeTMP());
            isTyping = false;
            dialogueBox.text = textWriter;
        }
    }
   
    IEnumerator TypeTMP()
    {
        if (hasTypingChar)
            dialogueBox.text = typingChar;
        else
            dialogueBox.text = "";

        yield return new WaitForSeconds(textStartDelay);

        foreach (char c in textWriter)
        {
            if (isTyping)
            {
                if (dialogueBox.text.Length > 0)
                {
                    dialogueBox.text = dialogueBox.text.Substring(0, dialogueBox.text.Length - typingChar.Length);
                }
                dialogueBox.text += c;
                dialogueBox.text += typingChar;
                yield return new WaitForSeconds(textCharacterDelay);
            }
        }

        if (typingChar != "" && isTyping)
        {
            dialogueBox.text = dialogueBox.text.Substring(0, dialogueBox.text.Length - typingChar.Length);
        }
        isTyping = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
