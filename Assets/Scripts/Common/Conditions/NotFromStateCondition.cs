using System.Collections;
using System.Collections.Generic;
using TT;
using UnityEngine;

[System.Serializable]
public class NotFromStateInfo
{
    public string[] StateNames;
}

public class NotFromStateCondition : BaseConditionBehaviour, IInfo
{
    [SerializeField] NotFromStateInfo info;
    StateMachine machine;

    private void Start()
    {
        machine = this.GetComponent<StateMachine>();
        machine.Events.RegisterEvent(StateMachine.StateMachineEventType.OnChangeState, OnChangeState);
    }

    private void OnDestroy()
    {
        if (machine == null) return;
        machine.Events.UnRegisterEvent(StateMachine.StateMachineEventType.OnChangeState, OnChangeState);
    }

    public void SetInfo(object data)
    {
        if (data is NotFromStateInfo)
        {
            info = (NotFromStateInfo)data;

            OnChangeState(null);
        }
    }

    void OnChangeState(StateController newState)
    {
        foreach (string stateName in info.StateNames)
        {
            if (stateName.Equals(machine.CurrentStateName))
            {
                this.SetSuitableCondition(false);
                return;
            }
        }

        this.SetSuitableCondition(true);
    }
}
