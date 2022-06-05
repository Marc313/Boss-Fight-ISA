using System;
using System.Collections.Generic;
using UnityEngine;

public class FSM : MonoBehaviour
{
    private Dictionary<Type, State> states = new Dictionary<Type, State>();
    private State currentState;

    private void Awake()
    {
        State[] stateComponents = GetComponentsInChildren<State>();     // Place the states UNDER this script.
        for (int i = 0; i < stateComponents.Length; i++)
        {
            State thisState = stateComponents[i];
            thisState.InitializeFSM(this);

            states.Add(stateComponents[i].GetType(), stateComponents[i]);
        }
        currentState = states[typeof(ChaseStateBoss)];
    }

    /*private void Update()
    {
        Debug.Log(currentState.ToString());
    }*/

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
    protected SwordEnemyAI enemyAI;
    protected BossAI bossAI;

    public void InitializeFSM(FSM fsm)
    {
        this.fsm = fsm;
        enemyAI = GetComponent<SwordEnemyAI>();
        bossAI = GetComponentInParent<BossAI>();
    }

    public abstract void onUpdate();
    public abstract void onEnter();
    public abstract void onExit();
}
