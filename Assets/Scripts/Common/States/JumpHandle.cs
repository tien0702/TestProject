using System;
using System.Collections;
using System.Collections.Generic;
using TT;
using UnityEngine;

[System.Serializable]
public class JumpHandleInfo
{
    public float JumpForce;
    public string JoystickID;
}

public class JumpHandle : BaseHandle, IInfo, IOwn
{
    [SerializeField] JumpHandleInfo _info;
    public JumpHandleInfo Info => _info;

    Rigidbody2D _rb2d;
    JoystickController _joystick;

    public void SetInfo(object data)
    {
        if (data is JumpHandleInfo)
        {
            _info = (JumpHandleInfo)data;
        }
    }

    public override void Handle()
    {
        _rb2d.AddForce(Vector2.up * _info.JumpForce, ForceMode2D.Impulse);

        LeanTween.delayedCall(0.2f, () =>
        {
            if (_onEndHandle != null)
            {
                _onEndHandle(this);
            }
        });
    }

    public void SetOwn(object own)
    {
        MonoBehaviour gameObject = own as MonoBehaviour;
        _rb2d = gameObject.GetComponentInChildren<Rigidbody2D>();
        _joystick = JoystickController.GetJoystick(_info.JoystickID);
    }
}
