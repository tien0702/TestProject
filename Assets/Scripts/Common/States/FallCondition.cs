using System.Collections;
using System.Collections.Generic;
using TT;
using UnityEngine;

[System.Serializable]
public class FallConditionInfo
{
    public int Val;
}

public class FallCondition : BaseCondition, IOnCheck, IInfo, IOwn
{
    Rigidbody2D _rb2d;

    public void OnCheck()
    {
        if(_rb2d.velocity.y < -0.1f)
        {
            this.SetSuitableCondition(true);
        }
        else
        {
            this.SetSuitableCondition(false);
        }
    }

    public void SetInfo(object data)
    {

    }

    public void SetOwn(object own)
    {
        MonoBehaviour obj = own as MonoBehaviour;
        _rb2d = obj.GetComponentInChildren<Rigidbody2D>();
    }
}
