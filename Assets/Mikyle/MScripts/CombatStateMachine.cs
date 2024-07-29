using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatStateMachine : MonoBehaviour
{
    public CombatState currentState;
    public PlayerTurnStateMachine playerTurnStateMachine;
    public MenuController menuController;
    private int currentPlayerIndex = 0;
    private int currentEnemyIndex = 0;
    [SerializeField] private List<PlayerCharacter> players;
    [SerializeField] private List<EnemyCharacter> enemies;

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
        //ChooseAction,
        //ResolveAction,
        EnemyTurn,
        HoldUp,
        AllOutAttack,
        CombatEnd
    }

    void Start()
    {
        //ChangeState(1);
        HandleState();
    }

    void Update()
    {
        //HandleState();
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
                menuController.UpdateStateText($"P{currentPlayerIndex + 1} - Aiming");
                menuController.ShowMainMenu();

                if (currentPlayerIndex < players.Count)
                {
                    PlayerCharacter currentPlayer = players[currentPlayerIndex];
                    //playerTurnStateMachine.StartPlayerTurn(currentPlayer);
                }
                else
                {
                    //ChangeState(CombatState.EnemyTurn);
                }
                break;

            case CombatState.Attack:
                menuController.UpdateStateText($"P{currentPlayerIndex + 1} - Attacking");
                StartCoroutine(Attack());

                break;

            case CombatState.Heal:
                menuController.UpdateStateText($"P{currentPlayerIndex + 1} - Healing");
                StartCoroutine(Heal());
                break;

            case CombatState.Gaurd:
                menuController.UpdateStateText($"P{currentPlayerIndex + 1} - Gaurding");
                StartCoroutine(Gaurd());
                break;

            case CombatState.PersonaAttack:
                menuController.UpdateStateText($"P{currentPlayerIndex + 1} - PersAttck");
                StartCoroutine(PersonaAttack());
                break;

            case CombatState.GunAttack:
                menuController.UpdateStateText($"P{currentPlayerIndex + 1} - GunAttck");
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
            default:
                break;
        }


        HandleState();
    }

    public void ChangeStateToResolve()
    {
        //currentState = CombatState.ResolveAction;
        HandleState();
    }

    IEnumerator SetupCombat()
    {
        currentPlayerIndex = 0;
        currentEnemyIndex = 0;

        yield return new WaitForSeconds(2f);
        ChangeState(2);
    }


    IEnumerator Attack()
    {
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

    IEnumerator Heal()
    {
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

    IEnumerator PersonaAttack()
    {
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
        menuController.HideAllMenus();
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
}

