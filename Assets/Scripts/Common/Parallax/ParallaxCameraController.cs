using System;
using UnityEngine;

public class ParallaxCameraController : MonoBehaviour
{
    Action<float> _onCameraMove;
    public Action<float> OnCameraMove { set { _onCameraMove = value; } }
    float _oldPositionX;

    protected virtual void Start()
    {
        _oldPositionX = transform.position.x;
    }

    protected virtual void Update()
    {
        if(!transform.position.x.Equals(_oldPositionX))
        {
            if(_onCameraMove != null)
            {
                float delta = _oldPositionX - transform.position.x;
                _onCameraMove(delta);
            }

            _oldPositionX = transform.position.x;
        }
    }
}
