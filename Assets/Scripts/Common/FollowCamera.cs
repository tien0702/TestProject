using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : FollowTargetController
{
    [SerializeField] Vector3 _offset;

    protected override void Awake()
    {
        base.Awake();
        _target = Camera.main.transform;
    }

    protected override void Update()
    {
        if (_convertToScreen)
        {
            transform.position = Camera.main.WorldToScreenPoint(_target.position + _offset);
        }
        else
        {
            transform.position = _target.position + _offset;
        }
    }
}
