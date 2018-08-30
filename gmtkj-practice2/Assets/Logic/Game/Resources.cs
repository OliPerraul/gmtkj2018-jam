using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSGame
{
    public class Resources : MonoBehaviour
    {

        public static Resources instance;
        public GameObject countDown;

        [Header("Other Resources")]
        public NSInventory.Resources inventory;


        private void Start()
        {
            instance = this;
        }

    }
}



