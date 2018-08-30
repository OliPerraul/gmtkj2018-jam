﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSInventory
{

    public class Grid : MonoBehaviour
    {

        Inventory inventory;

        [SerializeField]
        private int slotCount = 16;

        private GridItem[] gridItems;


        // Use this for initialization
        void Start()
        {
            inventory = (Inventory)Game.instance.fsm.Top.GetStateComponent("Inventory");
            inventory.onInventoryUpdated.AddListener(Refresh);

            gridItems = new GridItem[slotCount];
            InitializeGrid();

        }

        void InitializeGrid()
        {
            for (int i = 0; i < slotCount; i++)
            {
                GameObject gobj = Instantiate(Game.instance.resources.inventory.gridItem, transform);
                gridItems[i] = gobj.GetComponent<GridItem>();
                gridItems[i].index = i;
            }
        }

        private void Refresh()
        {
            for (int i = 0; i < slotCount; i++)
            {
                gridItems[i].item = inventory.items[i];

            }
        }

    }
}
