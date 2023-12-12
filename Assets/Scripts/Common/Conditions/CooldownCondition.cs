using System;
using TT;
using UnityEngine;

[System.Serializable]
public class CooldownInfo
{
    public float CooldownTime;
}

public class CooldownCondition : BaseCondition, IResetCondition, IInfo
{
    [SerializeField] CooldownInfo _info;

    public void SetInfo(object data)
    {
        if (data is CooldownInfo)
        {
            _info = (CooldownInfo)data;
        }
    }

    public void ResetCondition()
    {
        this.SetSuitableCondition(false);
    }

    public void Handle()
    {
        LeanTween.delayedCall(_info.CooldownTime, () => {
            this.SetSuitableCondition(true);
        });
    }
}
