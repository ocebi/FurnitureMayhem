using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace AISystem
{
    public class AIMover : MonoBehaviour
    {
        AI AIObject;
        List<Vector3> walkPoints = new List<Vector3>();
        Vector3? destination;
        int walkableMask;

        Vector3 destinationDebug;

        private void Awake()
        {
            AIObject = GetComponent<AI>();
            walkableMask = 1 << NavMesh.GetAreaFromName("Walkable");
        }

        public List<Vector3> GetWalkPoints(Vector3 destination)
        {
            destination.y = 0;
            List<Vector3> walkPoints = new List<Vector3>();
            NavMeshPath path = new NavMeshPath();
            NavMesh.CalculatePath(new Vector3(transform.position.x, 0, transform.position.z), destination, walkableMask, path);
            if (path.status != NavMeshPathStatus.PathComplete)
            {
                return walkPoints;
            }

            bool isValidLocation = IsValidLocation(destination);
            if (!isValidLocation) return walkPoints; //return empty list for error

            if (path.corners.Length > 0)
            {
                walkPoints = path.corners.ToList();
            }
            else
            {
                walkPoints.Clear();
            }
            return walkPoints;
        }

        public List<Vector3> GetClosestPath(Vector3 destination)
        {
            destination.y = 0;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(destination, out hit, 5f, walkableMask))
            {
                //Debug.Log("Closest path exists in AIMover");
                return GetWalkPoints(hit.position);
            }
            else
                return new List<Vector3>();
        }

        public virtual bool IsValidLocation(Vector3 location)
        {
            NavMeshPath path = new NavMeshPath();
            NavMesh.CalculatePath(new Vector3(transform.position.x, 0, transform.position.z), location, walkableMask, path);
            if (path.status != NavMeshPathStatus.PathComplete)
            {
                return false;
            }

            return true;
        }

        public Vector3 GetClosestPosition(Vector3 position, float distance = 50f) //Should also check if position is walkable from the map center.
        {
            position.y = 0;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(position, out hit, distance, walkableMask))
            {
                return hit.position;
            }
            else
            {
                Debug.LogError("Sample position should be used in available areas.");
                return Vector3.zero;
            }
        }

        public bool MoveToTarget(Vector3 target)
        {
            destinationDebug = target;
            if (destination == null)
            {
                walkPoints = GetWalkPoints(target);
                destination = target;
            }
            else if (target != destination.Value)
            {
                walkPoints = GetWalkPoints(target);
                destination = target;
            }
            if (walkPoints.Count == 0) //Try to find the closest position
            {
                walkPoints = GetClosestPath(target);
            }
            if (walkPoints.Count == 0)
            {
                RemoveDestination();
                return false;
            }
            else
            {
                DrawDebugLines();
                var walkpointsBegin = walkPoints[0];
                walkpointsBegin.y = transform.position.y;
                if (Vector3.Distance(transform.position, walkpointsBegin) <= 1f)
                {
                    walkPoints.RemoveAt(0);
                }
                if (walkPoints.Count > 0)
                {
                    var movePos = walkPoints[0];
                    movePos.y = transform.position.y;
                    AIObject.MoveToPosition(movePos);

                }
                else
                {
                    return false;
                }
                return true;
            }


        }

        public void StopMoving()
        {
            AIObject.ReleaseMove();
            RemoveDestination();
        }

        void RemoveDestination()
        {
            AIObject.ReleaseMove();
            destination = null;
            walkPoints.Clear();
        }

        void DrawDebugLines()
        {
            if (walkPoints.Count > 0)
            {
                Debug.DrawLine(transform.position, walkPoints[0], Color.blue);
                for (int i = 0; i < walkPoints.Count - 1; ++i)
                {
                    Debug.DrawLine(walkPoints[i], walkPoints[i + 1], Color.blue);
                }
            }
        }

    }
}