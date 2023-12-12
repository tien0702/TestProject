using UnityEngine;

public class ParallaxLayerController : MonoBehaviour
{
    [SerializeField] protected float _parallaxFactor;
    public virtual void MoveLayer(float delta)
    {
        Vector3 newPosition = transform.position;
        newPosition.x -= delta * _parallaxFactor;

        transform.position = newPosition;
    }
}
