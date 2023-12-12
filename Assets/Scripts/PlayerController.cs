using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using TT;
using UnityEngine;

public class PlayerController : EntityController
{
    StateMachine stateMachine;

    [SerializeField] TextMesh stateInfoName;

    protected override void Awake()
    {
        base.Awake();
        /*var data = Resources.Load<TextAsset>("Data/Player").text;

        JSONNode jsonData = JSONObject.Parse(data);

        components = ComponentUtils.GetComponentTypes(jsonData["data"]);

        foreach (var component in components)
        {
            ComponentHelper.AddComponent(this.gameObject, component);
        }*/

        StatController statController = GetComponent<StatController>();
        statController.SetStatInfo(new StatInfo() { StatID = "SPD", BaseValue = 600, MaxFinalValue = 800 });

        stateMachine = gameObject.AddComponent<StateMachine>();
        var data = Resources.Load<TextAsset>("Data/Beatrix/StateMachine").text;
        stateMachine.Init(data);
    }

    private void Start()
    {
        var states = stateMachine.States;

        foreach( var state in states )
        {
            state.Events.RegisterEvent(StateController.StateEventType.OnEnter, (StateController newState) =>
            {
                stateInfoName.text = newState.Info.StateName;
            });
        }

        InteractableController.Instance.GetComponent<FollowTargetController>().Target = this.transform;
        
    }

    protected override void OnLevelUp(int level)
    {

    }
}

public class Player : SingletonSelfBehaviour<PlayerController>
{

}