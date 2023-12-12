using TT;
using UnityEngine;

[System.Serializable]
public class RunHandleInfo
{
    public string JoystickID;
}

public class RunHandle : IInfo, IOwn, IOnUpdate
{
    [SerializeField] RunHandleInfo _info;
    public RunHandleInfo Info => _info;

    Stat _moveStat;
    JoystickController _joystick;
    GameObject _model;
    Rigidbody2D _rb2d;

    public void OnUpdate(float deltaTime)
    {
        float directionX = _joystick.Direction.normalized.x;
        if (directionX != 0)
        {
            float dirX = (directionX < 0) ? -1 : 1;
            _rb2d.AddForce(new Vector2(dirX * _moveStat.FinalValue * deltaTime, _rb2d.velocity.y));
            
            //_rb2d.velocity = new Vector2(dirX * _moveStat.FinalValue * deltaTime, _rb2d.velocity.y);

            Vector3 curLocalScale = _model.transform.localScale;

            _model.transform.localScale = new Vector3(dirX, curLocalScale.y, curLocalScale.z);
        }
    }

    public void SetInfo(object data)
    {
        if (data is RunHandleInfo)
        {
            _info = (RunHandleInfo)data;
        }
    }

    public void SetOwn(object own)
    {
        MonoBehaviour gameObject = own as MonoBehaviour;
        StatController statController = gameObject.GetComponent<StatController>();
        _moveStat = statController.GetStatByID("SPD");

        _joystick = JoystickController.GetJoystick(_info.JoystickID);
        _model = gameObject.GetComponentInChildren<Animator>().gameObject;
        _rb2d = gameObject.GetComponentInChildren<Rigidbody2D>();
    }
}
