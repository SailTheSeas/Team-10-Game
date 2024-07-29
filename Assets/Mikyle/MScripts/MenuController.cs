using TMPro;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject statePanel;
    public GameObject mainMenuPanel;
    public GameObject itemMenuPanel;
    public GameObject personaMenuPanel;
    public GameObject mainPanel;
    public Camera cam;
    public CombatStateMachine combatStateMachine;

    TextMeshProUGUI stateTMP;

    Transform reticle1;
    Transform reticle2;

    Transform enemy1Location;
    Transform enemy2Location;

    int enemyCount = 2;
    int currentEnemyTCount = 1;


    void Start()
    {
        stateTMP = statePanel.GetComponentInChildren<TextMeshProUGUI>();
        //ShowMainMenu();
        reticle1 = mainPanel.transform.GetChild(0);
        reticle2 = mainPanel.transform.GetChild(1);

        enemy1Location = GameObject.Find("E1").transform;
        enemy2Location = GameObject.Find("E2").transform;

        Vector3 screenPos = cam.WorldToScreenPoint(enemy1Location.position);
        reticle1.position = screenPos;

        screenPos = cam.WorldToScreenPoint(enemy2Location.position);
        reticle2.position = screenPos;

        reticle1.gameObject.SetActive(false);
        reticle2.gameObject.SetActive(false);
    }

    public void UpdateReticleTarget(int newTarget)
    {
        switch (newTarget)
        {
            case 1:
                currentEnemyTCount = 1;
                reticle1.gameObject.SetActive(true);
                reticle2.gameObject.SetActive(false);
                break;
            case 2:
                currentEnemyTCount = 2;
                reticle1.gameObject.SetActive(false);
                reticle2.gameObject.SetActive(true);
                break;

        }


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
        stateTMP.text = $"State:\n {text}";
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
