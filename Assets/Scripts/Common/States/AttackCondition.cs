using System.Collections;
using System.Collections.Generic;
using TT;
using UnityEngine;


[System.Serializable]
public class AttackConditionInfo
{
    public float Cooldown;
    public string KeyName;
}

public class AttackCondition : BaseCondition, IInfo, IResetCondition, IOnCheck
{
    [SerializeField] AttackConditionInfo _info;
    public AttackConditionInfo Info => _info;

    [SerializeField] float _cooldown;

    public void SetInfo(object data)
    {
        if(data is AttackConditionInfo)
        {
            _info = (AttackConditionInfo)data;
            _cooldown = 0;
        }
    }

    private void Update()
    {
        if(_cooldown <= 0) { return; }
        _cooldown -= Time.deltaTime;
    }

    public void ResetCondition()
    {
        _cooldown = _info.Cooldown;
        this.SetSuitableCondition(false);
        LeanTween.delayedCall(_info.Cooldown, () => { 

        });
    }

    public void OnCheck()
    {
        if (_cooldown <= 0 && Input.GetKeyDown(KeyCode.Space))
        {
            this.SetSuitableCondition(true);
        }
    }
}
