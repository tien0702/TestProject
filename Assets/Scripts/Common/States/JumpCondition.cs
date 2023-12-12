using System.Collections;
using System.Collections.Generic;
using TT;
using UnityEngine;

[System.Serializable]
public class JumpConditionInfo
{
    public string KeyName;
}

public class JumpCondition : BaseCondition, IInfo, IOnCheck
{
    [SerializeField] JumpConditionInfo _info;
    public JumpConditionInfo Info => _info;

    public void SetInfo(object data)
    {
        if (data is JumpConditionInfo)
        {
            _info = (JumpConditionInfo)data;
        }
    }

    void IOnCheck.OnCheck()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            this.SetSuitableCondition(true);
        }
        else
        {
            this.SetSuitableCondition(false);
        }
    }
}
