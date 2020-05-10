using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transitions : MonoBehaviour
{

    public enum ProcessState
    {
        noBattle,
        playerTurn,
        enemyTurn,
        dialogue,
        attackSub,
        skillSub,
        itemSub,
        dialogueChoice
    }

    public enum Command
    {
        startBattle,
        attackSelect,
        skillSelect,
        itemSelect,
        back,
        playerChoice,
        enemyChoice,
        waitForPlayer,
        waitForEnemy,
        exitBattle,
        enterConvo,
        waitForChoice,
        makeChoice,
        exitConvo
    }

    public class Process
    {
        class StateTransition
        {
            readonly ProcessState CurrentState;
            readonly Command Command;

            public StateTransition(ProcessState currentState, Command command)
            {
                CurrentState = currentState;
                Command = command;
            }

            public override int GetHashCode()
            {
                return 17 + 31 * CurrentState.GetHashCode() + 31 * Command.GetHashCode();
            }

            public override bool Equals(object obj)
            {
                StateTransition other = obj as StateTransition;
                return other != null && this.CurrentState == other.CurrentState && this.Command == other.Command;
            }
        }

        Dictionary<StateTransition, ProcessState> transitions;
        public ProcessState CurrentState { get; private set; }

        public Process()
        {
            CurrentState = ProcessState.noBattle;
            transitions = new Dictionary<StateTransition, ProcessState>
            {
                { new StateTransition(ProcessState.noBattle, Command.startBattle), ProcessState.playerTurn },
                { new StateTransition(ProcessState.playerTurn, Command.attackSelect), ProcessState.attackSub },
                { new StateTransition(ProcessState.playerTurn, Command.skillSelect), ProcessState.skillSub },
                { new StateTransition(ProcessState.playerTurn, Command.itemSelect), ProcessState.itemSub },
                { new StateTransition(ProcessState.playerTurn, Command.exitBattle), ProcessState.noBattle },
                { new StateTransition(ProcessState.attackSub, Command.playerChoice), ProcessState.dialogue },
                { new StateTransition(ProcessState.skillSub, Command.playerChoice), ProcessState.dialogue },
                { new StateTransition(ProcessState.itemSub, Command.playerChoice), ProcessState.dialogue },
                { new StateTransition(ProcessState.attackSub, Command.back), ProcessState.playerTurn },
                { new StateTransition(ProcessState.skillSub, Command.back), ProcessState.playerTurn },
                { new StateTransition(ProcessState.itemSub, Command.back), ProcessState.playerTurn },
                { new StateTransition(ProcessState.dialogue, Command.waitForEnemy), ProcessState.enemyTurn },
                { new StateTransition(ProcessState.dialogue, Command.waitForPlayer), ProcessState.playerTurn },
                { new StateTransition(ProcessState.dialogue, Command.exitBattle), ProcessState.noBattle },
                { new StateTransition(ProcessState.enemyTurn, Command.enemyChoice), ProcessState.dialogue },
                { new StateTransition(ProcessState.noBattle, Command.enterConvo), ProcessState.dialogue },
                { new StateTransition(ProcessState.dialogue, Command.waitForChoice), ProcessState.dialogueChoice },
                { new StateTransition(ProcessState.dialogueChoice, Command.makeChoice), ProcessState.dialogue }
            };
        }

        public ProcessState GetNext(Command command)
        {
            StateTransition transition = new StateTransition(CurrentState, command);
            ProcessState nextState;
            if (!transitions.TryGetValue(transition, out nextState))
            {
                throw new System.Exception("Invalid transition: " + CurrentState + " -> " + command);
            }
            return nextState;
        }

        public ProcessState MoveNext(Command command)
        {
            CurrentState = GetNext(command);
            return CurrentState;
        }

    }
}
