using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTargetController : MonoBehaviour
{
    [SerializeField] protected Transform _target;
    [SerializeField] protected bool _convertToScreen;

    public Transform Target
    {
        get { return _target; }
        set
        {
            _target = value;
            this.enabled = (_target != null);
        }
    }

    protected virtual void Awake()
    {
        this.enabled = (_target != null);
    }

    protected virtual void Update()
    {
        if (_convertToScreen)
        {
            transform.position = Camera.main.WorldToScreenPoint(_target.position);
        }
        else
        {
            transform.position = _target.position;
        }
    }
}
