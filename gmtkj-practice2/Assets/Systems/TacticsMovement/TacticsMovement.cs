using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NSLevel;

namespace NSTacticsMovement
{
    public class TacticsMovement : MonoBehaviour
    {
        public bool turn = false;
        List<Block> selectableBlocks = new List<Block>();
        Stack<Block> path = new Stack<Block>();
        Block currentBlock;

        public bool moving = false;
        public int move = 5;
        public float jumpHeight = 2;
        public float moveSpeed = 2;
        public float jumpVelocity = 4.5f;

        //Raycasts will not detect colliders for which the raycast origin is inside the collider"
        [SerializeField]
        private float rayCastOffsetUp = 4;

        [SerializeField]
        private float rayCastDistance = 4;
        
        [SerializeField]
        private LayerMask layer;

        Vector3 velocity = new Vector3();
        Vector3 heading = new Vector3();

        float halfHeight = 0;

        bool fallingDown = false;
        bool jumpingUp = false;
        bool movingEdge = false;
        Vector3 jumpTarget;

        public Block actualTargetBlock;

        protected void Init()
        {
            
            halfHeight = GetComponent<Collider>().bounds.extents.y;

            TurnManager.AddUnit(this);
        }

        public void GetCurrentBlock()
        {
            currentBlock = GetTargetBlock(gameObject);
            currentBlock.current = true;
        }

        public Block GetTargetBlock(GameObject target)
        {
            RaycastHit hit;
            Block block = null;

            Ray ray = new Ray(target.transform.position+ new Vector3(0, rayCastOffsetUp, 0), -Vector3.up);

            if (Physics.Raycast(ray, out hit, rayCastDistance, layer))
            {
                block = hit.collider.GetComponent<BlockColliderData>().block;
            }

            return block;
        }

        public void ComputeAdjacencyLists(float jumpHeight, Block target)
        {
            foreach (Block block in NSLevel.Level.Instance.blocks)
            {
                block.FindNeighbors(jumpHeight, target);
            }
        }

        public void FindSelectableBlocks()
        {
            ComputeAdjacencyLists(jumpHeight, null);
            GetCurrentBlock();

            Queue<Block> process = new Queue<Block>();

            process.Enqueue(currentBlock);
            currentBlock.visited = true;
            //currentBlock.parent = ??  leave as null 

            while (process.Count > 0)
            {
                Block t = process.Dequeue();

                selectableBlocks.Add(t);
                t.selectable = true;

                if (t.distance < move)
                {
                    foreach (Block block in t.adjacencyList)
                    {
                        if (!block.visited)
                        {
                            block.parent = t;
                            block.visited = true;
                            block.distance = 1 + t.distance;
                            process.Enqueue(block);
                        }
                    }
                }
            }
        }

        public void MoveToBlock(Block block)
        {
            path.Clear();
            block.target = true;
            moving = true;

            Block next = block;
            while (next != null)
            {
                path.Push(next);
                next = next.parent;
            }
        }

        public void Move()
        {
            if (path.Count > 0)
            {
                Block t = path.Peek();
                Vector3 target = t.transform.position;

                //Calculate the unit's position on top of the target block
                //target.y += halfHeight + t.colliderData.collider.bounds.extents.y;

                if (Vector3.Distance(transform.position, target) >= 0.05f)
                {
                    bool jump = transform.position.y != target.y;

                    if (jump)
                    {
                        Jump(target);
                    }
                    else
                    {
                        CalculateHeading(target);
                        SetHorizotalVelocity();
                    }

                    //Locomotion
                    transform.forward = heading;
                    transform.position += velocity * Time.deltaTime;
                }
                else
                {
                    //Block center reached
                    transform.position = target;
                    path.Pop();
                }
            }
            else
            {
                RemoveSelectableBlocks();
                moving = false;

                TurnManager.EndTurn();
            }
        }

        protected void RemoveSelectableBlocks()
        {
            if (currentBlock != null)
            {
                currentBlock.current = false;
                currentBlock = null;
            }

            foreach (Block block in selectableBlocks)
            {
                block.Reset();
            }

            selectableBlocks.Clear();
        }

        void CalculateHeading(Vector3 target)
        {
            heading = target - transform.position;
            heading.Normalize();
        }

        void SetHorizotalVelocity()
        {
            velocity = heading * moveSpeed;
        }

        void Jump(Vector3 target)
        {
            if (fallingDown)
            {
                FallDownward(target);
            }
            else if (jumpingUp)
            {
                JumpUpward(target);
            }
            else if (movingEdge)
            {
                MoveToEdge();
            }
            else
            {
                PrepareJump(target);
            }
        }

        void PrepareJump(Vector3 target)
        {
            float targetY = target.y;
            target.y = transform.position.y;

            CalculateHeading(target);

            if (transform.position.y > targetY)
            {
                fallingDown = false;
                jumpingUp = false;
                movingEdge = true;

                jumpTarget = transform.position + (target - transform.position) / 2.0f;
            }
            else
            {
                fallingDown = false;
                jumpingUp = true;
                movingEdge = false;

                velocity = heading * moveSpeed / 3.0f;

                float difference = targetY - transform.position.y;

                velocity.y = jumpVelocity * (0.5f + difference / 2.0f);
            }
        }

        void FallDownward(Vector3 target)
        {
            velocity += Physics.gravity * Time.deltaTime;

            if (transform.position.y <= target.y)
            {
                fallingDown = false;
                jumpingUp = false;
                movingEdge = false;

                Vector3 p = transform.position;
                p.y = target.y;
                transform.position = p;

                velocity = new Vector3();
            }
        }

        void JumpUpward(Vector3 target)
        {
            velocity += Physics.gravity * Time.deltaTime;

            if (transform.position.y > target.y)
            {
                jumpingUp = false;
                fallingDown = true;
            }
        }

        void MoveToEdge()
        {
            if (Vector3.Distance(transform.position, jumpTarget) >= 0.05f)
            {
                SetHorizotalVelocity();
            }
            else
            {
                movingEdge = false;
                fallingDown = true;

                velocity /= 5.0f;
                velocity.y = 1.5f;
            }
        }

        protected Block FindLowestF(List<Block> list)
        {
            Block lowest = list[0];

            foreach (Block t in list)
            {
                if (t.f < lowest.f)
                {
                    lowest = t;
                }
            }

            list.Remove(lowest);

            return lowest;
        }

        protected Block FindEndBlock(Block t)
        {
            Stack<Block> tempPath = new Stack<Block>();

            Block next = t.parent;
            while (next != null)
            {
                tempPath.Push(next);
                next = next.parent;
            }

            if (tempPath.Count <= move)
            {
                return t.parent;
            }

            Block endBlock = null;
            for (int i = 0; i <= move; i++)
            {
                endBlock = tempPath.Pop();
            }

            return endBlock;
        }

        protected void FindPath(Block target)
        {
            ComputeAdjacencyLists(jumpHeight, target);
            GetCurrentBlock();

            List<Block> openList = new List<Block>();
            List<Block> closedList = new List<Block>();

            openList.Add(currentBlock);
            //currentBlock.parent = ??
            currentBlock.h = Vector3.Distance(currentBlock.transform.position, target.transform.position);
            currentBlock.f = currentBlock.h;

            while (openList.Count > 0)
            {
                Block t = FindLowestF(openList);

                closedList.Add(t);

                if (t == target)
                {
                    actualTargetBlock = FindEndBlock(t);
                    MoveToBlock(actualTargetBlock);
                    return;
                }

                foreach (Block block in t.adjacencyList)
                {
                    if (closedList.Contains(block))
                    {
                        //Do nothing, already processed
                    }
                    else if (openList.Contains(block))
                    {
                        float tempG = t.g + Vector3.Distance(block.transform.position, t.transform.position);

                        if (tempG < block.g)
                        {
                            block.parent = t;

                            block.g = tempG;
                            block.f = block.g + block.h;
                        }
                    }
                    else
                    {
                        block.parent = t;

                        block.g = t.g + Vector3.Distance(block.transform.position, t.transform.position);
                        block.h = Vector3.Distance(block.transform.position, target.transform.position);
                        block.f = block.g + block.h;

                        openList.Add(block);
                    }
                }
            }

            //todo - what do you do if there is no path to the target block?
            Debug.Log("Path not found");
        }

        public void BeginTurn()
        {
            turn = true;
        }

        public void EndTurn()
        {
            turn = false;
        }
    }
}