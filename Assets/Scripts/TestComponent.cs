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
    private void Awake()
    {
        Assembly assembly = Assembly.Load("Assembly-CSharp");
        NodeType componentType = new NodeType();
        componentType.Type = assembly.GetType("ConditionA");


        object c = (object)Activator.CreateInstance(componentType.Type);

        if (c is Component)
        {
            Debug.Log("is component");
        }

        /*if(c is IOnUpdate)
        {
            Debug.Log("State update");
            ((IOnUpdate)c).OnUpdate(0);
        }*/

        if(c is ICondition)
        {
            Debug.Log("condition");
        }

        /*B b = (B)Activator.CreateInstance(type);
        if(b is IAction)
        {
            Debug.Log("i action");
            var action = (IAction)b;
            action.PerformAction();
        }*/
    }
}
