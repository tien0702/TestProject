using SimpleJSON;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace TT
{
    public class StateMachine : ComponentController
    {
        #region Events
        public enum StateMachineEventType { OnChangeState };
        ObserverEvents<StateMachineEventType, StateController> _events = new ObserverEvents<StateMachineEventType, StateController>();
        public ObserverEvents<StateMachineEventType, StateController> Events => _events;
        #endregion

        Dictionary<string, StateController> _states = new Dictionary<string, StateController>();

        [SerializeField] string _currentState;
        public string CurrentStateName => _currentState;
        public StateController[] States => _states.Values.ToArray();

        protected virtual void Start()
        {
            if (_currentState != null && !_currentState.Equals(string.Empty))
            {
                _states[_currentState].OnEnter();
            }
        }

        protected virtual void Update()
        {
            string stateName = _states[_currentState].OnUpdate(Time.deltaTime);
            if (stateName.Equals(_currentState)) return;
            _states[_currentState].OnExit();
            _currentState = stateName;
            _events.Notify(StateMachineEventType.OnChangeState, _states[_currentState]);
            _states[_currentState].OnEnter();
        }

        public override bool Init(string data)
        {
            this.Clear();

            var nodeTypes = NodeUtils.GetNodeTypes(data);

            JSONNode jsonData = JSONObject.Parse(data);

            JSONArray jsonArray = jsonData["data"].AsArray;

            string[] datas = new string[jsonArray.Count];

            for(int i = 0; i < jsonArray.Count; ++i)
            {
                datas[i] = jsonArray[i]["data"].ToString();
            }

            for (int i = 0; i < nodeTypes.Length; ++i)
            {
                var component = ComponentHelper.AddComponent(this.gameObject, nodeTypes[i], OnAddComponent);
                if (component is StateController)
                {
                    var stateController = (StateController)component;
                    stateController.Init(datas[i]);
                }
            }
            return true;
        }

        protected override void OnAddComponent(object component)
        {
            if (component is StateController)
            {
                StateController state = (StateController)component;
                if (_currentState == null || _currentState.Equals(string.Empty))
                {
                    _currentState = state.Info.StateName;
                }
                if (!_states.ContainsKey(state.Info.StateName))
                {
                    _states.Add(state.Info.StateName, state);
                }
                else
                {
                    _states[state.Info.StateName] = state;
                }
            }
        }

        public StateController GetStateByName(string name)
        {
            if (!_states.ContainsKey(name))
            {
                return null;
            }

            return _states[name];
        }
    }
}