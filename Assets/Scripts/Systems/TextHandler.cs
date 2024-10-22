using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using static UnityEngine.Awaitable;
using System.IO;
using UnityEngine.SceneManagement;

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

    [SerializeField] private GameObject enemyDisp;

    [SerializeField] private float textStartDelay;
    [SerializeField] private float textCharacterDelay;
    [SerializeField] private string typingChar;
    [SerializeField] private bool hasTypingChar;

    [SerializeField] private Animator menuAnimator;


    private DataHolder DH;
    private bool isTyping, startBattle, canClick;
    private string textWriter;    
    private int dialogueTracker, battleTriggerTracker, backgroundTriggerTracker;    

    // Start is called before the first frame update
    void Start()
    {
        DH = FindAnyObjectByType<DataHolder>();
        if (DH == null)
        {
            Debug.Log(null);
            dialogueTracker = 0;
            battleTriggerTracker = 0;
            backgroundTriggerTracker = 0;
        } else
        {
            dialogueTracker = DH.GetCurrentDialogue();
            battleTriggerTracker = DH.GetCurrentCombat();
            backgroundTriggerTracker = DH.GetCurrentBackground();            
        }
        imageDisplay.sprite = backgroundImage[backgroundTriggerTracker];
        dialogueBox.text = dialogueLines[dialogueTracker];
        textWriter = dialogueLines[dialogueTracker];
        dialogueTracker++;
        backgroundTriggerTracker++;
        canClick = true;
        isTyping = true;
        startBattle = false;
        StartCoroutine(TypeTMP());
    }

    public void NextDialogue()
    {
        if (canClick)
        {
            if (!isTyping)
            {
                if (dialogueTracker == backgroundTriggerPoints[backgroundTriggerTracker])
                {
                    imageDisplay.sprite = backgroundImage[backgroundTriggerTracker];
                    if (DH != null)
                        DH.SetCurrentBackground(backgroundTriggerTracker);
                    backgroundTriggerTracker++;

                }

                if (dialogueTracker == (battleTriggerPoints[battleTriggerTracker] - 1))
                    enemyDisp.SetActive(true);

                if (dialogueTracker == battleTriggerPoints[battleTriggerTracker])
                {
                    startBattle = true;

                    battleTriggerTracker++;
                    if (DH != null)
                        DH.SetCurrentCombat(battleTriggerTracker);
                }

                if (dialogueTracker < totalDialogue)
                {
                    if (startBattle)
                    {
                        canClick = false;
                        startBattle = false;
                        enemyDisp.SetActive(false);
                        /*SceneManager.LoadScene("CombatScene");*/
                        menuAnimator.SetInteger("ChangeScene", 1);
                        isTyping = false;
                        dialogueTracker++;
                    }
                    else
                    {
                        dialogueBox.text = dialogueLines[dialogueTracker];
                        textWriter = dialogueLines[dialogueTracker];
                        dialogueTracker++;
                        isTyping = true;
                        StartCoroutine(TypeTMP());
                    }


                }
                else
                {

                    Debug.Log("The End");
                    Application.Quit();
                }
            }
            else
            {
                StopCoroutine(TypeTMP());
                if (DH != null)
                    DH.SetCurrentDialogue(dialogueTracker);
                isTyping = false;
                dialogueBox.text = textWriter;
            }
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
        if (DH != null)
            DH.SetCurrentDialogue(dialogueTracker);
        isTyping = false;
        
    }
}
