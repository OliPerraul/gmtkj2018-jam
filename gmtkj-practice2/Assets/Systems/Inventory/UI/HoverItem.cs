using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace NSInventory
{

    public class HoverItem : MonoBehaviour
    {
        public Item item;

        [HideInInspector]
        public bool successful = false;


        // Update is called once per frame
        void Update()
        {
            if (item != null)
                GetComponent<SpriteRenderer>().sprite = item.icon;

            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);

            //Create tile here
        }



        //On Object dropped
        public void OnMouseReleased()
        {

            ////flag if so
            //if (Game.instance.money >= item.itemCost)
            //{
            //    Game.instance.money -= item.itemCost;

            //    successful = true;

            //    Vector3Int gridPos = World.instance.GetGridPos(transform);

            //    World.instance.tilemapInterests.SetTile(gridPos, item.tile);

            //    Vector3 pos = World.instance.GetWorldPos(gridPos);

            //    Trap t = GameResources.Create(pos, item.trap).GetComponent<Trap>();

            //    t.gridPos = gridPos;
            //}
        }

    }
}



