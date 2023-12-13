using System.Collections.Generic;
using UnityEngine;

namespace TT
{
    public interface IOwn
    {
        void SetOwn(object own);
    }

    public interface IOnEnter
    {
        void OnEnter(StateController state);
    }

    public interface IOnUpdate
    {
        void OnUpdate(float deltaTime);
    }

    public interface IOnExit
    {
        void OnExit();
    }

    public interface IOnCheck
    {
        void OnCheck();
    }

    [System.Serializable]
    public class StateInfo
    {
        public string StateName;
        public string AnimName;
        public string[] NextStates;


        private StateInfoHash _stateHash;
        public StateInfoHash StateHash
        {
            get
            {
                if(_stateHash == null)
                {
                    _stateHash = new StateInfoHash(this);
                }
                return _stateHash;
            }
        }
    }

    public class StateInfoHash
    {
        public int StateNameHash;
        public int AnimNameHash;
        public int[] NextStatesHash;

        public StateInfoHash(StateInfo stateInfo)
        {
            StateNameHash = Animator.StringToHash(stateInfo.StateName);
            AnimNameHash = Animator.StringToHash(stateInfo.AnimName);
            NextStatesHash = new int[stateInfo.NextStates.Length];
            for (int i = 0; i < NextStatesHash.Length; ++i)
            {
                NextStatesHash[i] = Animator.StringToHash(stateInfo.NextStates[i]);
            }
        }
    }

    public class StateController : ActionNode, IInfo
    {
        #region Events
        public enum StateEventType { OnEnter, OnUpdate, OnExit, OnClear }
        ObserverEvents<StateEventType, StateController> _events = new ObserverEvents<StateEventType, StateController>();
        public ObserverEvents<StateEventType, StateController> Events => _events;
        #endregion

        #region Interfaces
        LinkedList<IOnEnter> _onEnters = new LinkedList<IOnEnter>();
        LinkedList<IOnUpdate> _updates = new LinkedList<IOnUpdate>();
        LinkedList<IOnExit> _onExits = new LinkedList<IOnExit>();
        LinkedList<IOnCheck> _onChecks = new LinkedList<IOnCheck>();
        #endregion

        [SerializeField] protected StateInfo _info;
        public StateInfo Info => _info;

        StateMachine _stateMachine;

        protected virtual void Awake()
        {
            _stateMachine = GetComponent<StateMachine>();
        }

        protected override void OnAddComponent(object component)
        {
            base.OnAddComponent(component);
            if (component is IOnEnter)
            {
                _onEnters.AddLast((IOnEnter)component);
            }

            if (component is IOnUpdate)
            {
                _updates.AddLast((IOnUpdate)component);
            }

            if (component is IOnCheck)
            {
                _onChecks.AddLast((IOnCheck)component);
            }

            if (component is IOnExit)
            {
                _onExits.AddLast((IOnExit)component);
            }

            if (component is IOwn)
            {
                ((IOwn)component).SetOwn(this);
            }
        }

        public void SetInfo(object data)
        {
            if (data is StateInfo)
            {
                this._info = (StateInfo)data;
            }
        }

        public virtual void OnEnter()
        {
            foreach (IOnEnter onEnter in _onEnters)
            {
                onEnter.OnEnter(this);
            }

            _events.Notify(StateEventType.OnEnter, this);
            ActionHandle();
        }

        public virtual string OnUpdate(float deltaTime)
        {
            // Updates
            foreach (IOnUpdate update in _updates)
            {
                update.OnUpdate(deltaTime);
            }

            _events.Notify(StateEventType.OnUpdate, this);

            if (IsHandling()) return _info.StateName;

            // Check next states
            for (int i = 0; i < _info.NextStates.Length; i++)
            {
                StateController nextState = _stateMachine.GetStateByName(_info.NextStates[i]);
                if (nextState != null && nextState.IsSuitable())
                {
                    return _info.NextStates[i];
                }
            }

            return _info.StateName;
        }

        public virtual void OnExit()
        {
            foreach (IOnExit onExit in _onExits)
            {
                onExit.OnExit();
            }
            _events.Notify(StateEventType.OnExit, this);
        }

        protected override void Clear()
        {
            base.Clear();
            _onEnters.Clear();
            _updates.Clear();
            _onExits.Clear();
            _onChecks.Clear();

            _events.Notify(StateEventType.OnClear, this);
            _events.Clear();
        }

        public override bool IsSuitable()
        {
            foreach (IOnCheck onCheck in _onChecks)
            {
                onCheck.OnCheck();
            }
            return base.IsSuitable();
        }
    }
}
