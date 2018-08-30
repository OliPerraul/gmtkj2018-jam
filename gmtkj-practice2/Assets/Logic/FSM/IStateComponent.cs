using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSFSM
{
    public interface IStateComponent
    {
        void Tick();
        void Enter();
        void Exit();

        string GetName();


    }
}