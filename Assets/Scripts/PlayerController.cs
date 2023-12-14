using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using TT;
using UnityEngine;

public class PlayerController : EntityController
{
    StateMachine stateMachine;

    [SerializeField] TextMesh stateInfoName;

    public TestComponent prefab;

    Transform pool;

    protected override void Awake()
    {
        base.Awake();

        StatController statController = GetComponent<StatController>();
        statController.SetStatInfo(new StatInfo() { StatID = "SPD", BaseValue = 600, MaxFinalValue = 800 });

        stateMachine = gameObject.AddComponent<StateMachine>();
        var data = Resources.Load<TextAsset>("Data/Beatrix/StateMachine").text;
        stateMachine.Init(data);
    }

    private void Start()
    {
        var states = stateMachine.States;

        foreach (var state in states)
        {
            state.Events.RegisterEvent(StateController.StateEventType.OnEnter, (StateController newState) =>
            {
                stateInfoName.text = newState.Info.StateName;
            });
        }

        InteractableController.Instance.GetComponent<FollowTargetController>().Target = this.transform;

        pool = ObjectPool.Instance.CreatePool<TestComponent>("Test Pool", prefab, 10);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            var obj = ObjectPool.Instance.GetObject<TestComponent>("Test Pool");
        }
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            ObjectPool.Instance.DestroyPool("Test Pool");
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ObjectPool.Instance.AddMoreObject("Test Pool", 3);
        }
    }

    protected override void OnLevelUp(int level)
    {

    }
}

public class Player : SingletonSelfBehaviour<PlayerController>
{

}