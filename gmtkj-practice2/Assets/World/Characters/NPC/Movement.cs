using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NSLevel;
using NSTacticsMovement;

namespace NSNPC
{

    public class Movement : TacticsMovement
    {
        GameObject target;

        // Use this for initialization
        void Start()
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
                FindNearestTarget();
                CalculatePath();
                FindSelectableBlocks();
                actualTargetBlock.target = true;
            }
            else
            {
                Move();
            }
        }

        void CalculatePath()
        {
            Block targetBlock = GetTargetBlock(target);
            FindPath(targetBlock);
        }

        void FindNearestTarget()
        {
            GameObject[] targets = GameObject.FindGameObjectsWithTag("Player");

            GameObject nearest = null;
            float distance = Mathf.Infinity;

            foreach (GameObject obj in targets)
            {
                float d = Vector3.Distance(transform.position, obj.transform.position);

                if (d < distance)
                {
                    distance = d;
                    nearest = obj;
                }
            }

            target = nearest;
        }
    }

}