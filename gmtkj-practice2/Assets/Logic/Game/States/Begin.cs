﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NSGame
{
    public class Begin : GameState
    {

        public override string GetName()
        {
            return "Begin";
        }

        public override void Enter(AState from)
        {
            base.Enter(from);
            SceneManager.LoadScene("Main");
        }


        public override void Tick()
        {
            base.Tick();
        }

    }
}
