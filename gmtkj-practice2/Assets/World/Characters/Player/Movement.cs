using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NSLevel;

namespace NSPlayer
{
    public class Movement : NSTacticsMovement.TacticsMovement
    {
        // Use this for initialization
        void Awake()
        {
            Init();
        }

        // Update is called once per frame
        void Update()
        {
            Debug.DrawRay(transform.position, transform.forward);

            if (!turn)
            {
                return;
            }

            if (!moving)
            {
                FindSelectableBlocks();
                CheckMouse();
            }
            else
            {
                Move();
            }
        }

        void CheckMouse()
        {
            if (Input.GetMouseButtonUp(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.tag == "Block")
                    {
                        NSLevel.Block t = hit.collider.GetComponent<BlockColliderData>().block;

                        if (t.selectable)
                        {
                            MoveToBlock(t);
                        }
                    }
                }
            }
        }
    }

}
