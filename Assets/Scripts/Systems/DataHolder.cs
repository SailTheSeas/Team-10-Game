using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataHolder : MonoBehaviour
{
    private int currentDialogue, currentCombat, currentBackground;
    private static DataHolder instance = null;
    // Start is called before the first frame update
    void Start()
    {
        currentDialogue = 0;
        currentCombat = 0;
        currentBackground = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {

            Debug.Log(currentCombat);
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            return;
        }
        Destroy(this.gameObject);
    }

    public void ResetData()
    {
        currentDialogue = 0;
        currentCombat = 0;
        currentBackground = 0;
    }

    public int GetCurrentDialogue()
    {
        return currentDialogue;
    }

    public int GetCurrentCombat()
    {
        return currentCombat;
    }

    public int GetCurrentBackground()
    {
        return currentBackground;
    }

    public void SetCurrentDialogue(int newCurrentDialogue)
    {
        currentDialogue = newCurrentDialogue;
    }

    public void SetCurrentCombat(int newCurrentCombat)
    {
        currentCombat = newCurrentCombat;
    }

    public void SetCurrentBackground(int newCurrentBackground)
    {
        currentBackground = newCurrentBackground;
    }
}
