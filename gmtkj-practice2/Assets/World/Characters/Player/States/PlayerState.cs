using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerState : AState
{
    public Player Context
    {
        get { return (Player)context; }
    }

    public override string GetName()
    {
        throw new NotImplementedException();
    }

}

