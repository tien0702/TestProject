using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TT
{
    public class PlayAnimation : IOwn, IOnEnter
    {
        StateInfo _stateInfo;
        Animator _animator;
        public virtual void OnEnter(StateController state)
        {
            if (_stateInfo.StateName != null && !_stateInfo.StateName.Equals(string.Empty))
            {
                _animator.Play(_stateInfo.AnimName);
            }
        }

        public virtual void SetOwn(object own)
        {
            if(!(own is StateController))
            {
                return;
            }

            StateController state = (StateController)own;
            _stateInfo = state.Info;
            _animator = state.GetComponentInChildren<Animator>();
        }
    }
}
