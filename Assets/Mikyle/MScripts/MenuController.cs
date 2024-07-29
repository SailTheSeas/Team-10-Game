using TMPro;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject statePanel;
    public GameObject mainMenuPanel;
    public GameObject itemMenuPanel;
    public GameObject personaMenuPanel;

    public CombatStateMachine combatStateMachine;

    TextMeshProUGUI stateTMP;



    void Start()
    {
        stateTMP = statePanel.GetComponentInChildren<TextMeshProUGUI>();
        //ShowMainMenu();
    }

    public void ShowMainMenu()
    {
        HideAllMenus();
        mainMenuPanel.SetActive(true);
    }

    public void ShowItemMenu()
    {
        HideAllMenus();
        itemMenuPanel.SetActive(true);
    }

    public void ShowPersonaMenu()
    {
        HideAllMenus();
        personaMenuPanel.SetActive(true);
    }

    public void HideAllMenus()
    {
        mainMenuPanel.SetActive(false);
        itemMenuPanel.SetActive(false);
        personaMenuPanel.SetActive(false);
    }

    public void OnMainMenuOptionSelected(string option)
    {
        switch (option)
        {
            case "Attack":
                // Handle attack action
                combatStateMachine.ChangeStateToResolve();
                break;
            case "Guard":
                // Handle guard action
                combatStateMachine.ChangeStateToResolve();
                break;
            case "Item":
                ShowItemMenu();
                break;
            case "Persona":
                ShowPersonaMenu();
                break;
            case "Gun":
                // Handle gun action
                combatStateMachine.ChangeStateToResolve();
                break;
        }
    }

    public void OnItemSelected(string item)
    {
        // Handle item selection
        combatStateMachine.ChangeStateToResolve();
    }

    public void OnPersonaAttackSelected(string attack)
    {
        // Handle persona attack selection
        combatStateMachine.ChangeStateToResolve();
    }

    public void UpdateStateText(string text)
    {
        stateTMP.text = $"State: {text}";
    }

    public void AttackPerformed()
    {
        UpdateStateText("attack");
    }

    public void GaurdPerformed()
    {
        UpdateStateText("gaurd");
    }


}
