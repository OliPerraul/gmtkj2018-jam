﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSGame
{
    public class Resources : MonoBehaviour
    {
        [Header("Game")]
        [Header("\n")]

        public GameObject countDown;

        private static Resources _instance;
        public static NSGame.Resources Instance {  get{ return _instance; } }


        private void Awake()
        {
            _instance = this;
        }

    }
}



