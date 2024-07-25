using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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
    


    private int dialogueTracker, battleTriggerTracker, backgroundTriggerTracker;    

    // Start is called before the first frame update
    void Start()
    {
        dialogueTracker = 0;
        battleTriggerTracker = 0;
        backgroundTriggerTracker = 0;
        dialogueBox.text = dialogueLines[dialogueTracker];
        dialogueTracker++;
    }

    public void NextDialogue()
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
            dialogueTracker++;
        } else
        {
            Debug.Log("The End");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
