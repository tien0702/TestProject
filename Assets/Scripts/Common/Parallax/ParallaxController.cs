using Unity.VisualScripting;
using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    ParallaxLayerController[] _parallaxLayers;
    ParallaxCameraController _parallaxCamera;

    protected virtual void Awake()
    {
        _parallaxLayers = GetComponentsInChildren<ParallaxLayerController>();
        _parallaxCamera = Camera.main.GetComponent<ParallaxCameraController>();
        if(_parallaxCamera == null)
        {
            _parallaxCamera = Camera.main.AddComponent<ParallaxCameraController>();
        }
        _parallaxCamera.OnCameraMove = OnCameraMove;
    }

    protected virtual void OnCameraMove(float delta)
    {
        foreach(ParallaxLayerController layer in _parallaxLayers)
        {
            layer.MoveLayer(delta);
        }
    }
}
