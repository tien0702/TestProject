using System.Collections;
using System.Collections.Generic;
using TT;
using UnityEngine;

[System.Serializable]
public class FallHandleInfo
{
    public float MaxFallSpeed;
}
public class FallHandle : BaseHandle, IInfo, IOwn, IOnUpdate
{
    Rigidbody2D _rb2d;
    [SerializeField] FallHandleInfo _info;
    public FallHandleInfo Info => _info;

    public override void Handle()
    {
        EndHandle();
    }

    public void OnUpdate(float deltaTime)
    {
        if (_rb2d.velocity.y < _info.MaxFallSpeed)
        {
            _rb2d.velocity = new Vector2(_rb2d.velocity.x, _info.MaxFallSpeed);
        }
    }

    public void SetInfo(object data)
    {
        if(data is FallHandleInfo)
        {
            _info = (FallHandleInfo)data;
        }
    }

    public void SetOwn(object own)
    {
        MonoBehaviour behaviour = own as MonoBehaviour;
        _rb2d = behaviour.GetComponentInChildren<Rigidbody2D>();
    }
}
