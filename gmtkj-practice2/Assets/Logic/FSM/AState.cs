using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AState : MonoBehaviour
{
    public NSFSM.IContext context;

    [SerializeField]
    private bool debug = false;

    private bool active;
    public bool Active { get { return active; } }

    protected static AState instance = null;

    public abstract string GetName();

    private List<NSFSM.IStateComponent> components;

    [SerializeField]
    public GameObject componentsParent;


    public void Start()
    {
        components = new List<NSFSM.IStateComponent>();

        if (componentsParent)
        {
            foreach (GameObject child in componentsParent.transform)
            {
                components.Add(child.GetComponent<NSFSM.IStateComponent>());
            }
        }

    }

    public NSFSM.IStateComponent GetStateComponent(string name)
    {
        foreach (NSFSM.IStateComponent component in components)
        {
            if (component.GetName() == name)
                return component;   
        }

        throw new Exception("State component " + name + "not found.");
    }



    public virtual void Enter(AState from)
    {
        if(debug)
        Debug.Log(GetName() + " Entered");

        foreach (NSFSM.IStateComponent component in components)
        {
            component.Enter();
        }


        active = true;
    }

    public virtual void Exit(AState to)
    {
        if(debug)
        Debug.Log(GetName() + " Exit");

        foreach (NSFSM.IStateComponent component in components)
        {
            component.Exit();
        }
        
        active = false;       
    }

    public virtual void Tick()
    {
        if(debug)
        Debug.Log(GetName() + " Tick");

        foreach (NSFSM.IStateComponent component in components)
        {
            component.Tick();
        }

    }
}


