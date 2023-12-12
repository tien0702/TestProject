using System.Collections;
using System.Collections.Generic;
using TT;
using UnityEngine;

[System.Serializable]
public class FromStateInfo
{
    public string[] StateNames;
}

public class FromStateCondition : BaseConditionBehaviour, IInfo
{
    [SerializeField] FromStateInfo info;
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
        if (data is FromStateInfo) info = (FromStateInfo)data;
    }

    void OnChangeState(StateController newState)
    {
        foreach (string stateName in info.StateNames)
        {
            if (stateName.Equals(machine.CurrentStateName))
            {
                this.SetSuitableCondition(true);
                return;
            }
        }

        this.SetSuitableCondition(false);
    }
}
