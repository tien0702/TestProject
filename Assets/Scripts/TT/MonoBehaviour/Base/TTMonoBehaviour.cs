using System;
using UnityEngine;

namespace TT
{
    public class TTMonoBehaviour : MonoBehaviour
    {
        public float time = 0.05f;
        private int id;
        public LeanTweenType leanTweenType;

        public virtual void MoveToPosition(Vector3 posTarget, Action callbackOnComplete = null)
        {
            LeanTween.move(gameObject, posTarget, time).setEase(leanTweenType).setOnComplete(callbackOnComplete);
        }

        public virtual void MoveToPositionUpdate(Vector3 posMoveTo, Action callbackOnComplete = null)
        {
            LeanTween.cancel(id);
            id = LeanTween.move(gameObject, posMoveTo, time).setEase(leanTweenType).setOnComplete(callbackOnComplete).id;
        }

        public virtual void MoveToLocalPosition(Vector3 posTarget, Action callbackOnComplete = null)
        {
            LeanTween.moveLocal(gameObject, posTarget, time).setEase(leanTweenType).setOnComplete(callbackOnComplete);
        }

        public virtual void MoveToLocalPositionUpdate(Vector3 posMoveTo, Action callbackOnComplete = null)
        {
            LeanTween.cancel(id);
            id = LeanTween.moveLocal(gameObject, posMoveTo, time).setEase(leanTweenType).setOnComplete(callbackOnComplete).id;
        }

        public virtual void ScalceTo(Vector3 targetValue, Action callbackOnComplete = null)
        {
            LeanTween.scale(gameObject, targetValue, time).setEase(leanTweenType).setOnComplete(callbackOnComplete);
        }

        public virtual void ScalceUpdate(Vector3 targetValue, Action callbackOnComplete = null)
        {
            LeanTween.cancel(id);
            id = LeanTween.scale(gameObject, targetValue, time).setEase(leanTweenType).setOnComplete(callbackOnComplete).id;
        }

        public virtual void RotateTo(Vector3 angleTarget, Action callbackOnComplete = null)
        {
            LeanTween.cancel(id);
            id = LeanTween.rotate(gameObject, angleTarget, time).setEase(leanTweenType).setOnComplete(callbackOnComplete).id;
        }

        public virtual void UpdateValue(float from, float to, Action<float> callbackOnUpdate = null, Action callbackOnComplete = null)
        {
            LeanTween.cancel(id);
            id = LeanTween.value(gameObject, callbackOnUpdate, from, to, time).setEase(leanTweenType).setOnComplete(callbackOnComplete).id;
        }

        public virtual void UpdateValue(Vector2 from, Vector2 to, Action<Vector2> callbackOnUpdate = null, Action callbackOnComplete = null)
        {
            LeanTween.cancel(id);
            id = LeanTween.value(gameObject, callbackOnUpdate, from, to, time).setEase(leanTweenType).setOnComplete(callbackOnComplete).id;
        }

        public virtual void UpdateValue(Color from, Color to, Action<Color> callbackOnUpdate = null, Action callbackOnComplete = null)
        {
            LeanTween.cancel(id);
            id = LeanTween.value(gameObject, callbackOnUpdate, from, to, time).setEase(leanTweenType).setOnComplete(callbackOnComplete).id;
        }
    }
}
