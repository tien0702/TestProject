using TT;
using UnityEngine;

[System.Serializable]
public class JoystickConditionInfo
{
    public string JoystickID;
    public bool IsControl;
}

public class JoystickCondition : BaseCondition, IInfo, IOnCheck
{
    [SerializeField] JoystickConditionInfo _info;
    JoystickController _joystick;

    public void SetInfo(object data)
    {
        if (data is JoystickConditionInfo)
        {
            _info = (JoystickConditionInfo)data;
        }
    }

    public void OnCheck()
    {
        _joystick = JoystickController.GetJoystick(_info.JoystickID);
        if (!_info.IsControl)
        {
            this.SetSuitableCondition(_joystick.Direction.Equals(Vector2.zero));
        }
        else
        {
            this.SetSuitableCondition(!_joystick.Direction.Equals(Vector2.zero));
        }
    }
}
