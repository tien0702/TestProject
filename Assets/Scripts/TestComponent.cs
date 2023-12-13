using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using TT;

public interface IAction
{
    void PerformAction();
}

public class A : IAction
{
    public void PerformAction()
    {
        Debug.Log("Action performed by class A");
    }
}

public class B : A
{

}

public class ConditionA : Component
{
    public void OnUpdate(float deltaTime)
    {
        Debug.Log("call update");
    }
}

public class TestComponent : MonoBehaviour
{
    public ParticleSystem _particleSystem;
    public void Play()
    {
        _particleSystem.Play();
    }
}
