using UnityEngine;

public class PlayerTurnStateMachine : MonoBehaviour
{
    public PlayerTurnState currentState;
    //private PlayerCharacter currentPlayer;
    private CombatStateMachine combatStateMachine;

    public enum PlayerTurnState
    {
        Pause,
        ChooseAction,
        Attacking,
        SelectingEnemy,
        SelectingAlly,
        UsingItem,
        UsingPersona,
        UsingGun
    }


    void Update()
    {
        //HandleState();
    }

    //public void StartPlayerTurn(PlayerCharacter player)
    //{
    //    currentPlayer = player;
    //    ChangeState(PlayerTurnState.ChooseAction);
    //}

    //void HandleState()
    //{
    //    switch (currentState)
    //    {
    //        case PlayerTurnState.Pause:

    //            break;

    //        case PlayerTurnState.ChooseAction:
    //            // Show action menu
    //            break;

    //        case PlayerTurnState.Attacking:
    //            // Handle attacking logic
    //            combatStateMachine.ChangeStateToResolve();
    //            break;

    //        case PlayerTurnState.SelectingEnemy:
    //            // Handle enemy selection logic
    //            break;

    //        case PlayerTurnState.SelectingAlly:
    //            // Handle ally selection logic
    //            break;

    //        case PlayerTurnState.UsingItem:
    //            // Handle item usage logic
    //            combatStateMachine.ChangeStateToResolve();
    //            break;

    //        case PlayerTurnState.UsingPersona:
    //            // Handle persona usage logic
    //            combatStateMachine.ChangeStateToResolve();
    //            break;

    //        case PlayerTurnState.UsingGun:
    //            // Handle gun usage logic
    //            combatStateMachine.ChangeStateToResolve();
    //            break;
    //    }
    //}

    void ChangeState(PlayerTurnState newState)
    {
        currentState = newState;
        //HandleState();
    }
}

