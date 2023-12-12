using System.Collections;
using System.Collections.Generic;
using TT;
using UnityEngine;

[System.Serializable]
public class NotOnGroundConditionInfo
{
    public string LayerName;
}

public class NotOnGroundCondition : BaseCondition, IInfo, IOnCheck, IOwn
{
    [SerializeField] NotOnGroundConditionInfo _info;
    public NotOnGroundConditionInfo Info => _info;

    Transform _groudCheck;

    [SerializeField] LayerMask _layer;

    public void SetInfo(object data)
    {
        if (data is NotOnGroundConditionInfo)
        {
            _info = (NotOnGroundConditionInfo)data;
        }
    }

    public void OnCheck()
    {
        this.SetSuitableCondition(Physics2D.OverlapCircle(_groudCheck.position, 0.1f, _layer) == null);
    }

    public void SetOwn(object own)
    {
        MonoBehaviour gameObject = own as MonoBehaviour;
        _layer = LayerMask.GetMask(_info.LayerName);
        _groudCheck = gameObject.GetComponentInChildren<Animator>().transform.Find("GroundCheck");
    }
}
