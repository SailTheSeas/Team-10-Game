using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatStateMachine : MonoBehaviour
{
    public CombatState currentState;
    //public PlayerTurnStateMachine playerTurnStateMachine;
    public MenuController menuController;
    [SerializeField] private int currentPlayerIndex = 0;
    private int currentEnemyIndex = 0;
    [SerializeField] List<Item> items;
    [SerializeField] private List<PlayerCharacter> players;
    [SerializeField] private List<EnemyCharacter> enemies;

    int enemyCount = 2;
    [SerializeField] int currentEnemyTCount = 1;
    int playersCount = 4;
    [SerializeField] int currentPlayersCount = 1;



    public Camera cam;
    Transform cameraPlayerPos1;
    Transform cameraPlayerPos2;
    Transform cameraPlayerPos3;
    Transform cameraPlayerPos4;

    #region For Data Values Vars
    public GameObject P1;
    public GameObject P2;
    public GameObject P3;
    public GameObject P4;

    PlayerCharacter P1Stats;
    PlayerCharacter P2Stats;
    PlayerCharacter P3Stats;
    PlayerCharacter P4Stats;


    public GameObject E1;
    public GameObject E2;
    public GameObject E3;
    public GameObject E4;

    EnemyCharacter E1Stats;     //Overall we have 4 enemy types. Though the enemies go thepugh 3 diffrent stages. Instead of making 9 enemies, we can make other data sets to be used for the diffrent levels. So scrptable objects[SObj] that all contain data, but the level will detemine which SObj to use, creating varations of enemies without having to make other physical versions. 
    EnemyCharacter E2Stats;
    EnemyCharacter E3Stats;
    EnemyCharacter E4Stats;







    #endregion



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

    void Start() //Maybe we can use Awake here 
    {
        #region For Data Values [Gets]
        P1Stats = P1.GetComponent<PlayerCharacter>();
        P2Stats = P2.GetComponent<PlayerCharacter>();
        P3Stats = P3.GetComponent<PlayerCharacter>();
        P4Stats = P4.GetComponent<PlayerCharacter>();



        E1Stats = E1.GetComponent<EnemyCharacter>();
        E2Stats = E2.GetComponent<EnemyCharacter>();
        E3Stats = E3.GetComponent<EnemyCharacter>();
        E4Stats = E4.GetComponent<EnemyCharacter>();

        #endregion


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
                if (currentEnemyTCount < 1)
                    currentEnemyTCount = enemyCount;

                menuController.UpdateEnemyReticleTarget(currentEnemyTCount);
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                Debug.Log("A was pressed");
                currentEnemyTCount++;
                if (currentEnemyTCount > enemyCount)
                    currentEnemyTCount = 1;

                menuController.UpdateEnemyReticleTarget(currentEnemyTCount);
            }
        }

        if (currentState == CombatState.Heal)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                Debug.Log("A was pressed");
                currentPlayersCount--;
                if (currentPlayersCount < 1)
                    currentPlayersCount = playersCount;

                menuController.UpdatePlayerReticleTarget(currentPlayersCount);
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                Debug.Log("D was pressed");
                currentPlayersCount++;
                if (currentPlayersCount > playersCount)
                    currentPlayersCount = 1;

                menuController.UpdatePlayerReticleTarget(currentPlayersCount);
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
                menuController.ShowMainMenu();


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
                Debug.Log(players[currentPlayerIndex].playerMoves);
                //StartCoroutine(PersonaAttack());
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
        menuController.SetUpItems(items);
        menuController.UpdateEnemyReticleTarget(1);
        ChangeState(2);
    }


    IEnumerator Attack()
    {
        menuController.HideEnemyReticles();
        menuController.HideAllMenus();
        //Put Attack Anim here
        //Put damage Calc here

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

    IEnumerator Gaurd()
    {
        menuController.HideAllMenus();
        //Put Gaurd Anim here
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
        Debug.Log($"Healing Player: {currentPlayersCount}");
        //Put Heal Anim here
        //Put Heal Calc here

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

    IEnumerator PersonaAttacking()
    {
        menuController.HideAllMenus();
        //Put Persona Attack Anim here
        //Put Damage Calc here

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

    IEnumerator EnemyAttack()
    {
        //Put Enemy Attack Anim here
        //Randomise Party member to hit
        //Put Damage Calc here

        yield return new WaitForSeconds(2f);

        currentEnemyIndex++;
        if (currentEnemyIndex >= enemies.Count)
        {
            currentPlayerIndex = 0;
            ChangeState(2);
        }
        else
        {
            ChangeState(9);
        }
    }

    IEnumerator PlayerTurnEnd()
    {
        UpdateCameraPosition();
        menuController.HideAllMenus();
        menuController.HideEnemyReticles();
        yield return new WaitForSeconds(2f);
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


        menuController.UpdateReticlePlacement();
    }
}

