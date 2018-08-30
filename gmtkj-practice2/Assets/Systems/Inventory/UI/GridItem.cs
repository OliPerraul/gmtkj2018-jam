using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;


namespace NSInventory
{

    public class GridItem : Button, IPointerClickHandler
    {

        public int index = 0;

        public Item item;
        public UnityEvent unityEvent;

        //Used as ref when ovj is drag
        private HoverItem hoverItem;

        private Image itemImage;

        Inventory inventory;


        // Use this for initialization
        public void Start()
        {
            inventory = (Inventory)Game.instance.fsm.Top.GetStateComponent("Inventory");


        }

        public void Update()
        {
            if (item != null)
                item.icon = itemImage.sprite;
            else
                itemImage.sprite = null;
        }


        public override void OnPointerDown(PointerEventData eventData)
        {
            if (item != null)
            {
                GameObject gobj = Instantiate(Game.instance.resources.inventory.hoverItem);

                gobj.transform.position = transform.position;

                hoverItem = gobj.GetComponent<HoverItem>(); ;
                hoverItem.item = item;

                unityEvent.AddListener(hoverItem.OnMouseReleased);

                item = null;
                inventory.RemoveItem(index);
            }
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            if (hoverItem != null)
            {
                unityEvent.Invoke();

                if (!hoverItem.successful)
                {
                    inventory.AddItem(hoverItem.item, index);
                }

                Destroy(hoverItem.gameObject);
                hoverItem = null;
            }
        }

    }
}
