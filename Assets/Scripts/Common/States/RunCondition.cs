using System.Collections;
using System.Collections.Generic;
using TT;
using UnityEngine;

[System.Serializable]
public class RunConditionInfo
{
    public string JoystickID;
}

public class RunCondition : BaseConditionBehaviour, IInfo
{
    [SerializeField] protected RunConditionInfo _info;
    public RunConditionInfo Info => _info;

    JoystickController _joystick;

    private void Start()
    {
        _joystick = JoystickController.GetJoystick(_info.JoystickID);
        _joystick.Events.RegisterEvent(JoystickController.JoystickEvent.JoyBeginDrag, JoyBeginDrag);
        _joystick.Events.RegisterEvent(JoystickController.JoystickEvent.JoyEndDrag, JoyEndDrag);
    }

    void JoyBeginDrag(JoystickController joy)
    {
        this.SetSuitableCondition(true);
    }

    void JoyEndDrag(JoystickController joy)
    {
        this.SetSuitableCondition(false);
    }

    private void OnDestroy()
    {
        _joystick.Events.UnRegisterEvent(JoystickController.JoystickEvent.JoyBeginDrag, JoyBeginDrag);
        _joystick.Events.UnRegisterEvent(JoystickController.JoystickEvent.JoyEndDrag, JoyEndDrag);
    }

    public void SetInfo(object data)
    {
        if (data is RunConditionInfo)
        {
            _info = (RunConditionInfo)data;
        }
    }
}
