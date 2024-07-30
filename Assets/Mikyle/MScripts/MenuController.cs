using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public GameObject statePanel;
    public GameObject mainMenuPanel;
    public GameObject itemMenuPanel;
    GameObject itemButton1;
    GameObject itemButton2;
    GameObject itemButton3;
    GameObject itemButton4;
    List<Item> menuItems;
    public GameObject personaMenuPanel;
    GameObject personaButton1;
    GameObject personaButton2;
    GameObject personaButton3;
    GameObject personaButton4;
    List<PersonaMove> personaMoveItems;
    public GameObject mainPanel;
    public Camera cam;
    public CombatStateMachine combatStateMachine;

    TextMeshProUGUI stateTMP;

    Transform enemyReticle1;
    Transform enemyReticle2;
    Transform playerReticle1;
    Transform playerReticle2;
    Transform playerReticle3;
    Transform playerReticle4;

    Transform enemy1Location;
    Transform enemy2Location;
    Transform cameraPos1;
    Transform cameraPos2;

    int enemyCount = 2;
    int currentEnemyTCount = 1;
    int playerCount = 4;
    int currentPlayerCount = 1;




    void Start()
    {
        stateTMP = statePanel.GetComponentInChildren<TextMeshProUGUI>();

        //Reticle Setups
        enemyReticle1 = mainPanel.transform.GetChild(0);
        enemyReticle2 = mainPanel.transform.GetChild(1);
        playerReticle1 = mainPanel.transform.GetChild(2);
        playerReticle2 = mainPanel.transform.GetChild(3);
        playerReticle3 = mainPanel.transform.GetChild(4);
        playerReticle4 = mainPanel.transform.GetChild(5);

        enemy1Location = GameObject.Find("E1").transform;
        enemy2Location = GameObject.Find("E2").transform;


        Vector3 screenPos = cam.WorldToScreenPoint(enemy1Location.position);
        enemyReticle1.position = screenPos;

        screenPos = cam.WorldToScreenPoint(enemy2Location.position);
        enemyReticle2.position = screenPos;

        enemyReticle1.gameObject.SetActive(false);
        enemyReticle2.gameObject.SetActive(false);

        //Camera Handling
        cameraPos1 = GameObject.Find("CameraFirstPos").transform;
        cameraPos2 = GameObject.Find("CameraSecondPos").transform;

        //Item Button Setups
        itemButton1 = itemMenuPanel.transform.GetChild(1).gameObject;
        itemButton2 = itemMenuPanel.transform.GetChild(2).gameObject;
        itemButton3 = itemMenuPanel.transform.GetChild(3).gameObject;
        itemButton4 = itemMenuPanel.transform.GetChild(4).gameObject;

        //Persona Button Setups
        personaButton1 = personaMenuPanel.transform.GetChild(1).gameObject;
        personaButton2 = personaMenuPanel.transform.GetChild(2).gameObject;
        personaButton3 = personaMenuPanel.transform.GetChild(3).gameObject;
        personaButton4 = personaMenuPanel.transform.GetChild(4).gameObject;

    }

    public void UpdateEnemyReticleTarget(int newTarget)
    {
        switch (newTarget)
        {
            case 1:
                currentEnemyTCount = 1;
                enemyReticle1.gameObject.SetActive(true);
                enemyReticle2.gameObject.SetActive(false);
                break;
            case 2:
                currentEnemyTCount = 2;
                enemyReticle1.gameObject.SetActive(false);
                enemyReticle2.gameObject.SetActive(true);
                break;

        }
    }

    public void UpdatePlayerReticleTarget(int newTarget)
    {
        switch (newTarget)
        {
            case 1:
                currentPlayerCount = 1;
                playerReticle1.gameObject.SetActive(true);
                playerReticle2.gameObject.SetActive(false);
                playerReticle3.gameObject.SetActive(false);
                playerReticle4.gameObject.SetActive(false);
                break;
            case 2:
                currentPlayerCount = 2;
                playerReticle1.gameObject.SetActive(false);
                playerReticle2.gameObject.SetActive(true);
                playerReticle3.gameObject.SetActive(false);
                playerReticle4.gameObject.SetActive(false);
                break;
            case 3:
                currentPlayerCount = 3;
                playerReticle1.gameObject.SetActive(false);
                playerReticle2.gameObject.SetActive(false);
                playerReticle3.gameObject.SetActive(true);
                playerReticle4.gameObject.SetActive(false);
                break;
            case 4:
                currentPlayerCount = 4;
                playerReticle1.gameObject.SetActive(false);
                playerReticle2.gameObject.SetActive(false);
                playerReticle3.gameObject.SetActive(false);
                playerReticle4.gameObject.SetActive(true);
                break;

        }
    }

    public void HideEnemyReticles()
    {
        enemyReticle1.gameObject.SetActive(false);
        enemyReticle2.gameObject.SetActive(false);
    }

    public void HidePlayerReticles()
    {
        playerReticle1.gameObject.SetActive(false);
        playerReticle2.gameObject.SetActive(false);
        playerReticle3.gameObject.SetActive(false);
        playerReticle4.gameObject.SetActive(false);
    }

    public void ShowMainMenu()
    {
        //cam.transform.position = cameraPos1.position;
        //cam.transform.rotation = cameraPos1.rotation;
        combatStateMachine.UpdateCameraPosition();

        HideAllMenus();
        HidePlayerReticles();
        UpdateEnemyReticleTarget(currentEnemyTCount);
        mainMenuPanel.SetActive(true);
        //combatStateMachine.ChangeState(2);
    }

    public void ShowItemMenu()
    {
        combatStateMachine.ChangeState(5);
        HideAllMenus();
        HideEnemyReticles();
        itemMenuPanel.SetActive(true);

        //CoRoutine to Shift Camera
        cam.transform.position = cameraPos2.position;
        cam.transform.rotation = cameraPos2.rotation;

        //Show Player Reticle
        UpdatePlayerReticleTarget(currentPlayerCount);
    }

    public void ShowPersonaMenu()
    {
        combatStateMachine.ChangeState(6);
        HideAllMenus();
        personaMenuPanel.SetActive(true);
    }

    public void HideAllMenus()
    {
        mainMenuPanel.SetActive(false);
        itemMenuPanel.SetActive(false);
        personaMenuPanel.SetActive(false);
    }

    public void SetUpItems(List<Item> items)
    {
        menuItems = items;
        itemButton1.GetComponentInChildren<TextMeshProUGUI>().text = menuItems[0].itemName;
        itemButton2.GetComponentInChildren<TextMeshProUGUI>().text = menuItems[1].itemName;
        itemButton3.GetComponentInChildren<TextMeshProUGUI>().text = menuItems[2].itemName;
        itemButton4.GetComponentInChildren<TextMeshProUGUI>().text = menuItems[3].itemName;
    }

    public void SetUpPersonaMoves(List<PersonaMove> moves)
    {
        personaMoveItems = moves;
        personaButton1.GetComponentInChildren<TextMeshProUGUI>().text = personaMoveItems[0].moveName;
        personaButton2.GetComponentInChildren<TextMeshProUGUI>().text = personaMoveItems[1].moveName;
        personaButton3.GetComponentInChildren<TextMeshProUGUI>().text = personaMoveItems[2].moveName;

        if (personaMoveItems[3].moveName == "na")
        {
            personaButton4.SetActive(false);
        }
        else
        {
            personaButton4.SetActive(true);
            personaButton4.GetComponentInChildren<TextMeshProUGUI>().text = personaMoveItems[3].moveName;
        }
    }



    //public void OnMainMenuOptionSelected(string option)
    //{
    //    switch (option)
    //    {
    //        case "Attack":
    //            // Handle attack action
    //            combatStateMachine.ChangeStateToResolve();
    //            break;
    //        case "Guard":
    //            // Handle guard action
    //            combatStateMachine.ChangeStateToResolve();
    //            break;
    //        case "Item":
    //            ShowItemMenu();
    //            break;
    //        case "Persona":
    //            ShowPersonaMenu();
    //            break;
    //        case "Gun":
    //            // Handle gun action
    //            combatStateMachine.ChangeStateToResolve();
    //            break;
    //    }
    //}

    //public void OnItemSelected(string item)
    //{
    //    // Handle item selection
    //    combatStateMachine.ChangeStateToResolve();
    //}

    //public void OnPersonaAttackSelected(string attack)
    //{
    //    // Handle persona attack selection
    //    combatStateMachine.ChangeStateToResolve();
    //}

    public void UpdateStateText(string text)
    {
        stateTMP.text = $"{text}";
        //stateTMP.text = $"State:\n {text}";
    }

    public void AttackPerformed()
    {
        UpdateStateText("attack");
    }

    public void GaurdPerformed()
    {
        UpdateStateText("gaurd");
    }


    public void UpdateReticlePlacement()
    {
        Vector3 screenPos = cam.WorldToScreenPoint(enemy1Location.position);
        enemyReticle1.position = screenPos;

        screenPos = cam.WorldToScreenPoint(enemy2Location.position);
        enemyReticle2.position = screenPos;
    }


    public void UpdateMenuItems()
    {
        if (menuItems[0].isUsed)
        {
            itemButton1.GetComponent<Button>().interactable = false;
        }
        else
        {
            itemButton1.GetComponent<Button>().interactable = true;

        }

        if (menuItems[1].isUsed)
        {
            itemButton2.GetComponent<Button>().interactable = false;
        }
        else
        {
            itemButton2.GetComponent<Button>().interactable = true;

        }

        if (menuItems[2].isUsed)
        {
            itemButton3.GetComponent<Button>().interactable = false;
        }
        else
        {
            itemButton3.GetComponent<Button>().interactable = true;

        }

        if (menuItems[3].isUsed)
        {
            itemButton4.GetComponent<Button>().interactable = false;
        }
        else
        {
            itemButton4.GetComponent<Button>().interactable = true;

        }
    }

    public void I1ButtonPressed()
    {
        menuItems[0].isUsed = true;
        UpdateMenuItems();
        combatStateMachine.ChangeState(13);
    }

    public void I2ButtonPressed()
    {
        menuItems[1].isUsed = true;
        UpdateMenuItems();
        combatStateMachine.ChangeState(13);
    }

    public void I3ButtonPressed()
    {
        menuItems[2].isUsed = true;
        UpdateMenuItems();
        combatStateMachine.ChangeState(13);
    }

    public void I4ButtonPressed()
    {
        menuItems[3].isUsed = true;
        UpdateMenuItems();
        combatStateMachine.ChangeState(13);
    }


}
