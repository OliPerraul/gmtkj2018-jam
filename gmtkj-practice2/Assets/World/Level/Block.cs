﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSLevel
{
    public class Block : MonoBehaviour
    {
        public bool walkable = true;
        public bool current = false;
        public bool target = false;
        public bool selectable = false;

        [SerializeField]
        private MeshRenderer _meshRenderer;

        [SerializeField]
        public BlockColliderData colliderData;

        public List<Block> adjacencyList = new List<Block>();

        //Needed BFS (breadth first search)
        public bool visited = false;
        public Block parent = null;
        public int distance = 0;

        //For A*
        public float f = 0;
        public float g = 0;
        public float h = 0;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (current)
            {
                _meshRenderer.material.color = Color.magenta;
            }
            else if (target)
            {
                _meshRenderer.material.color = Color.green;
            }
            else if (selectable)
            {
                _meshRenderer.material.color = Color.red;
            }
            else
            {
                _meshRenderer.material.color = Color.white;
            }
        }

        public void Reset()
        {
            adjacencyList.Clear();

            current = false;
            target = false;
            selectable = false;

            visited = false;
            parent = null;
            distance = 0;

            f = g = h = 0;
        }

        public void FindNeighbors(float jumpHeight, Block target)
        {
            Reset();

            CheckBlock(Vector3.forward, jumpHeight, target);
            CheckBlock(-Vector3.forward, jumpHeight, target);
            CheckBlock(Vector3.right, jumpHeight, target);
            CheckBlock(-Vector3.right, jumpHeight, target);
        }

        public void CheckBlock(Vector3 direction, float jumpHeight, Block target)
        {
            Vector3 halfExtents = new Vector3(0.25f, (1 + jumpHeight) / 2.0f, 0.25f);
            Collider[] colliders = Physics.OverlapBox(transform.position + direction, halfExtents);

            foreach (Collider item in colliders)
            {
                BlockColliderData blockCollider = item.GetComponent<BlockColliderData>();
                if (blockCollider != null && blockCollider.block.walkable)
                {
                    RaycastHit hit;

                    if (!Physics.Raycast(blockCollider.transform.position, Vector3.up, out hit, 1) || (blockCollider == target))
                    {
                        adjacencyList.Add(blockCollider.block);
                    }
                }
            }
        }
    }

}
