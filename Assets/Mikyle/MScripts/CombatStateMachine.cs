using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatStateMachine : MonoBehaviour
{
    [Header("Admin")]
    public CombatState currentState;
    //public PlayerTurnStateMachine playerTurnStateMachine;
    public MenuController menuController;
    [SerializeField] private int currentPlayerIndex = 0;
    [SerializeField] private int currentEnemyIndex = 0;
    [SerializeField] List<Item> items;
    [SerializeField] private List<PlayerCharacter> players;
    [SerializeField] private List<EnemyCharacter> enemies;

    [Header("Enemy")]
    int enemyCount = 2;
    [SerializeField] int currentEnemyTCount = 0;
    int playersCount = 4;
    [SerializeField] int currentPlayersTCount = 0;


    [Header("Camera")]
    public Camera cam;
    Transform cameraPlayerPos1;
    Transform cameraPlayerPos2;
    Transform cameraPlayerPos3;
    Transform cameraPlayerPos4;
    [SerializeField] Transform playerLight;

    public List<PersonaMove> CurrentMove = new List<PersonaMove>(1);

    public enum CombatState
    {
        CombatStart,
        PlayerTurn,
        Attack,
        Gaurd,
        Heal,
        PersonaAttack,
        GunAttack,
        PlayerTurnEnd,
        EnemyTurn,
        HoldUp,
        AllOutAttack,
        CombatEnd,
        Healing,
        PersonaAttacking
    }

    void Start()
    {
        CurrentMove[0] = players[currentPlayerIndex].playerMoves[0];

        //ChangeState(1);
        HandleState();
    }

    void Update()
    {
        //HandleState();


        if (currentState == CombatState.PlayerTurn || currentState == CombatState.PersonaAttack)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                Debug.Log("D was pressed");
                currentEnemyTCount--;
                if (currentEnemyTCount < 0)
                    currentEnemyTCount = enemyCount - 1;

                menuController.UpdateEnemyReticleTarget(currentEnemyTCount);
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                Debug.Log("A was pressed");
                currentEnemyTCount++;
                if (currentEnemyTCount >= enemyCount)
                    currentEnemyTCount = 0;

                menuController.UpdateEnemyReticleTarget(currentEnemyTCount);
            }
        }

        if (currentState == CombatState.Heal)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                Debug.Log("A was pressed");
                currentPlayersTCount--;
                if (currentPlayersTCount < 0)
                    currentPlayersTCount = playersCount - 1;

                menuController.UpdatePlayerReticleTarget(currentPlayersTCount);
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                Debug.Log("D was pressed");
                currentPlayersTCount++;
                if (currentPlayersTCount >= playersCount)
                    currentPlayersTCount = 0;

                menuController.UpdatePlayerReticleTarget(currentPlayersTCount);
            }
        }
    }

    void HandleState()
    {
        switch (currentState)
        {
            case CombatState.CombatStart:
                //Debug.Log("Ping0");
                menuController.UpdateStateText("SetUp");
                StartCoroutine(SetupCombat());
                break;

            case CombatState.PlayerTurn:
                menuController.UpdateStateText($"{players[currentPlayerIndex].characterName} is Aiming");
                players[currentPlayerIndex].playerAnim.SetInteger("CallPersona", 0);
                menuController.ShowMainMenu();
                playerLight.position = new Vector3(players[currentPlayerIndex].transform.position.x, players[currentPlayerIndex].transform.position.y + 3, players[currentPlayerIndex].transform.position.z);

                if (currentPlayerIndex < players.Count)
                {
                    PlayerCharacter currentPlayer = players[currentPlayerIndex];
                    UpdateCameraPosition();
                    //playerTurnStateMachine.StartPlayerTurn(currentPlayer);
                }
                else
                {
                    //ChangeState(CombatState.EnemyTurn);
                }
                break;

            case CombatState.Attack:
                menuController.UpdateStateText($"{players[currentPlayerIndex].characterName} is Attacking");
                StartCoroutine(Attack());

                break;

            case CombatState.Heal:
                menuController.UpdateStateText($"Will {players[currentPlayerIndex].characterName} Heal?");
                break;

            case CombatState.Gaurd:
                menuController.UpdateStateText($"{players[currentPlayerIndex].characterName} is Gaurding");
                StartCoroutine(Gaurd());
                break;

            case CombatState.PersonaAttack:
                menuController.UpdateStateText($"{players[currentPlayerIndex].characterName} summons their Persona!");
                menuController.SetUpPersonaMoves(players[currentPlayerIndex].playerMoves);
                //Debug.Log(players[currentPlayerIndex].playerMoves);
                //StartCoroutine(PersonaAttack());

                //Player Summons Persona Animation
                players[currentPlayerIndex].playerAnim.SetInteger("CallPersona", 1);

                break;

            case CombatState.GunAttack:
                menuController.UpdateStateText($"{players[currentPlayerIndex].characterName} - GunAttck");
                //StartCoroutine(Attack());
                break;

            case CombatState.PlayerTurnEnd:
                menuController.UpdateStateText("PTurns over");
                StartCoroutine(PlayerTurnEnd());
                break;

            case CombatState.EnemyTurn:
                // Handle enemy turns here
                menuController.UpdateStateText($"E{currentEnemyIndex + 1} - Attacking");
                StartCoroutine(EnemyAttack());

                //ChangeState(5);
                break;

            case CombatState.AllOutAttack:
                PerformAllOutAttack();
                break;

            case CombatState.CombatEnd:
                EndCombat();
                break;

            case CombatState.Healing:
                menuController.UpdateStateText($"{players[currentPlayerIndex].characterName} is Healing");
                StartCoroutine(Healing());
                break;

            case CombatState.PersonaAttacking:
                menuController.UpdateStateText($"{players[currentPlayerIndex].characterName} uses Persona Attack!");
                StartCoroutine(PersonaAttacking());
                break;
        }
    }

    public void ChangeState(int stateNum)
    {
        switch (stateNum)
        {
            case 1:
                currentState = CombatState.CombatStart;
                break;
            case 2:
                currentState = CombatState.PlayerTurn;
                break;
            case 3:
                currentState = CombatState.Attack;
                break;
            case 4:
                currentState = CombatState.Gaurd;
                break;
            case 5:
                currentState = CombatState.Heal;
                break;
            case 6:
                currentState = CombatState.PersonaAttack;
                break;
            case 7:
                currentState = CombatState.GunAttack;
                break;
            case 8:
                currentState = CombatState.PlayerTurnEnd;
                break;
            case 9:
                currentState = CombatState.EnemyTurn;
                break;
            case 13:
                currentState = CombatState.Healing;
                break;
            case 14:
                currentState = CombatState.PersonaAttacking;
                break;
            default:
                break;
        }


        HandleState();
    }

    //public void ChangeStateToResolve()
    //{
    //    //currentState = CombatState.ResolveAction;
    //    HandleState();
    //}

    IEnumerator SetupCombat()
    {
        currentPlayerIndex = 0;
        currentEnemyIndex = 0;

        //Camera Handling
        cameraPlayerPos1 = GameObject.Find("CameraFirstPos").transform;
        cameraPlayerPos2 = GameObject.Find("CameraThirdPos").transform;
        cameraPlayerPos3 = GameObject.Find("CameraForthPos").transform;
        cameraPlayerPos4 = GameObject.Find("CameraFifthPos").transform;

        yield return new WaitForSeconds(2f);
        playerLight.gameObject.SetActive(true);
        menuController.SetUpItems(items);
        menuController.UpdateEnemyReticleTarget(0);
        ChangeState(2);
    }


    IEnumerator Attack()
    {
        menuController.HideEnemyReticles();
        menuController.HideAllMenus();

        //do the attack animation
        players[currentPlayerIndex].playerAnim.SetInteger("BasicAttack", 1);

        //Put damage Calc here

        enemies[currentEnemyTCount].enemyHealth -= players[currentPlayerIndex].playerPhysAttack;


        yield return new WaitForSeconds(2f);

        //return to idle animation
        players[currentPlayerIndex].playerAnim.SetInteger("BasicAttack", 0);

        currentPlayerIndex++;
        if (currentPlayerIndex >= players.Count)
        {
            currentPlayerIndex = 0;
            ChangeState(8);
        }
        else
        {
            ChangeState(2);
        }

    }

    IEnumerator Gaurd()
    {
        menuController.HideAllMenus();

        //do guard animation
        players[currentPlayerIndex].playerAnim.SetInteger("Guard", 1);
        players[currentPlayerIndex].isGuarding = true;
        //Put Gaurd Calc here

        yield return new WaitForSeconds(2f);

        currentPlayerIndex++;
        if (currentPlayerIndex >= players.Count)
        {

            currentPlayerIndex = 0;
            ChangeState(8);
        }
        else
        {
            ChangeState(2);
        }

    }

    IEnumerator Healing()
    {
        menuController.HideAllMenus();
        Debug.Log($"Healing Player: {currentPlayersTCount}");

        //Do heal animation
        players[currentPlayerIndex].playerAnim.SetInteger("Heal", 1);

        //Put Heal Calc here

        yield return new WaitForSeconds(2f);

        //Return to Idle Animation
        players[currentPlayerIndex].playerAnim.SetInteger("Heal", 0);

        currentPlayerIndex++;
        if (currentPlayerIndex >= players.Count)
        {
            currentPlayerIndex = 0;
            ChangeState(8);
        }
        else
        {
            ChangeState(2);
        }

    }

    IEnumerator PersonaAttacking()
    {
        menuController.HideAllMenus();
        //Put Persona Attack Anim here
        //Put Damage Calc here

        enemies[currentEnemyTCount].enemyHealth -= CurrentMove[0].damageValue;


        //=--------------------

        yield return new WaitForSeconds(2f);

        //Revert to Idle Animation
        players[currentPlayerIndex].playerAnim.SetInteger("CallPersona", 0);

        currentPlayerIndex++;
        if (currentPlayerIndex >= players.Count)
        {
            currentPlayerIndex = 0;
            ChangeState(8);
        }
        else
        {
            ChangeState(2);
        }


    }

    IEnumerator EnemyAttack()
    {
        playerLight.GetComponent<Light>().color = Color.red;
        playerLight.position = new Vector3(enemies[currentEnemyIndex].transform.position.x, enemies[currentEnemyIndex].transform.position.y + 3, enemies[currentEnemyIndex].transform.position.z);
        yield return new WaitForSeconds(1f);
        //Put Enemy Attack Anim here
        //Do enemy attack animation
        enemies[currentEnemyIndex].enemyAnim.SetInteger("EnemyAttack", 1);

        //Randomise Party member to hit
        //Put Damage Calc here

        //---------------

        yield return new WaitForSeconds(2f);

        //Return to Enemy Idle Animation
        enemies[currentEnemyIndex].enemyAnim.SetInteger("EnemyAttack", 0);

        currentEnemyIndex++;
        if (currentEnemyIndex >= enemies.Count)
        {
            for (int i = 0; i <= playersCount - 1; i++)
            {

                players[i].isGuarding = false;
                //playerLight.gameObject.SetActive(true);
                playerLight.GetComponent<Light>().color = Color.green;
                players[i].playerAnim.SetInteger("Guard", 0);


            }


            currentEnemyIndex = 0;
            ChangeState(2);
        }
        else
        {
            ChangeState(9);
        }
    }

    IEnumerator PlayerTurnEnd()
    {
        playerLight.GetComponent<Light>().color = Color.red;
        playerLight.position = new Vector3(enemies[currentEnemyIndex].transform.position.x, enemies[currentEnemyIndex].transform.position.y + 3, enemies[currentEnemyIndex].transform.position.z);
        UpdateCameraPosition();
        menuController.HideAllMenus();
        menuController.HideEnemyReticles();
        menuController.HidePlayerReticles();
        //playerLight.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        ChangeState(9);
    }

    void PerformAllOutAttack()
    {
        // Perform all-out attack
        ChangeState(3);
    }

    void EndCombat()
    {
        // End combat
    }

    public void UpdateCameraPosition()
    {
        if (currentPlayerIndex == 0)
        {
            cam.transform.position = cameraPlayerPos1.position;
            cam.transform.rotation = cameraPlayerPos1.rotation;
        }

        if (currentPlayerIndex == 1)
        {
            cam.transform.position = cameraPlayerPos2.position;
            cam.transform.rotation = cameraPlayerPos2.rotation;
        }

        if (currentPlayerIndex == 2)
        {
            cam.transform.position = cameraPlayerPos3.position;
            cam.transform.rotation = cameraPlayerPos3.rotation;
        }

        if (currentPlayerIndex == 3)
        {
            cam.transform.position = cameraPlayerPos4.position;
            cam.transform.rotation = cameraPlayerPos4.rotation;
        }


        //menuController.UpdateReticlePlacement();
    }

    public void PButton1()
    {
       
       CurrentMove[0] = players[currentPlayerIndex].playerMoves[0];
         ChangeState(14); 


    }

    public void PButton2()
    {
        CurrentMove[0] = players[currentPlayerIndex].playerMoves[1];
         ChangeState(14); 


    }

    public void PButton3()
    {
        CurrentMove[0] = players[currentPlayerIndex].playerMoves[2];
        ChangeState(14); 


    }

    public void PButton4()
    {
        CurrentMove[0] = players[currentPlayerIndex].playerMoves[3];
  
         ChangeState(14); 


    }













}

