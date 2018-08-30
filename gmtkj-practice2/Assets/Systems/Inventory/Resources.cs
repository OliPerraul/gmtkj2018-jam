using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSInventory
{
    public class Resources : MonoBehaviour
    {
        public GameObject gridItem;
        public GameObject hoverItem;

        public Item[] baseItems;
        public Dictionary<string, Item> items;

        private void Start()
        {
            // Must have at least one base item
            ItemFactory.Initialize(baseItems);

        }

    }
}



