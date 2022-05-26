using System;
using System.Collections.Generic;
using UnityEngine;

public class FSM : MonoBehaviour
{
    private Dictionary<Type, State> states = new Dictionary<Type, State>();
    private State currentState;

    private void Awake()
    {
        State[] stateComponents = GetComponents<State>();
        for (int i = 0; i < stateComponents.Length; i++)
        {
            State thisState = stateComponents[i];
            thisState.InitializeFSM(this);

            states.Add(stateComponents[i].GetType(), stateComponents[i]);
        }
        currentState = states[typeof(IdleState)];
    }

    public void onUpdate()
    {
        currentState?.onUpdate();
    }

    public void SwitchState(Type stateType)
    {
        if(states.ContainsKey(stateType))
        {
            currentState?.onExit();
            State newState = states[stateType];
            currentState = newState;
            currentState?.onEnter();
        }
    }

    public State GetCurrentState() { return currentState; }
}

public abstract class State : MonoBehaviour
{
    protected FSM fsm;
    protected EnemyAIFSM enemyAI;

    public void InitializeFSM(FSM fsm)
    {
        this.fsm = fsm;
        enemyAI = GetComponent<EnemyAIFSM>();
    }

    public abstract void onUpdate();
    public abstract void onEnter();
    public abstract void onExit();

    public abstract int getNumber();
}
