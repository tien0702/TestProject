using System;
using System.Collections;
using System.Collections.Generic;
using TT;
using UnityEngine;

[System.Serializable]
public class AttackHandleInfo
{
    public float TimeAction;
}

public class AttackHandle : BaseHandle, IHandle, IInfo
{
    [SerializeField] AttackHandleInfo _info;
    public AttackHandleInfo Info => _info;

    public void SetInfo(object data)
    {
        if (data is AttackHandleInfo)
        {
            _info = (AttackHandleInfo)data;
        }
    }

    public override void Handle()
    {
        LeanTween.delayedCall(_info.TimeAction, () =>
        {
            if (_onEndHandle != null)
            {
                _onEndHandle(this);
            }
        });
    }
}
