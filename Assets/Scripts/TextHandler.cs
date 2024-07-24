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


    private int dialogueTracker;    

    // Start is called before the first frame update
    void Start()
    {
        dialogueTracker = 0;
        dialogueBox.text = dialogueLines[dialogueTracker];
        dialogueTracker++;
    }

    private void NextDialogue()
    {
        dialogueBox.text = dialogueLines[dialogueTracker];
        dialogueTracker++;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
