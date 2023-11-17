using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AISystem
{
    public class Sensor : MonoBehaviour
    {
        // protected Team agentTeam;
        int walkableMask;
        protected virtual void Awake()
        {
            // agentTeam = GetComponent<Team>();
            walkableMask = 1 << NavMesh.GetAreaFromName("Walkable");
        }

        public virtual Transform IsObjectInRange(float range, LayerMask lm)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, range, lm);
            if (hitColliders.Length > 0)
            {
                //float distance = range;
                float distance = range;
                Transform chosenObject = null;
                foreach (Collider c in hitColliders)
                {
                    if (!GameObject.ReferenceEquals(c.gameObject, gameObject))
                    {
                        // Team hitTeam = c.GetComponent<Team>();
                        // if (hitTeam && hitTeam.ID != 0 && hitTeam.ID == agentTeam.ID) continue;
                        if(!ConditionsMatch(c.gameObject)) continue;

                        var currentPosition = transform.position;
                        currentPosition.y = 0;
                        var targetPosition = c.transform.position;
                        targetPosition.y = 0;
                        float localDistance = Vector3.Distance(currentPosition, targetPosition);
                        if (localDistance < distance)
                        {
                            NavMeshPath path = new NavMeshPath();
                            //NavMesh.CalculatePath(GetClosestPosition(currentPosition, 3f), GetClosestPosition(targetPosition, 3f), walkableMask, path);
                            //NavMesh.CalculatePath(currentPosition, targetPosition, walkableMask, path);
                            NavMesh.CalculatePath(currentPosition, targetPosition, NavMesh.AllAreas, path);
                            if (path.status != NavMeshPathStatus.PathComplete) continue; // && !ClosestPointExists(c.transform.position, 5f)) continue;

                            //if (!ClosestPointExists(c.transform.position, 5f)) continue;
                            distance = localDistance;
                            chosenObject = c.transform;
                        }
                    }
                }

                if (chosenObject == null) return null;
                else return chosenObject;

            }
            return null;
        }

        protected virtual bool ConditionsMatch(GameObject targetObject)
        {
            return true;
        }

        public bool ClosestPointExists(Vector3 destination, float distance)
        {
            NavMeshHit hit;
            destination.y = 0;
            if (NavMesh.SamplePosition(destination, out hit, distance, walkableMask))
                return true;
            return false;
        }

        public Vector3 GetClosestPosition(Vector3 position, float distance = 50f) //Should also check if position is walkable from the map center.
        {
            position.y = 0;
            NavMeshHit hit;
            position.y = 0;
            if (NavMesh.SamplePosition(position, out hit, distance, NavMesh.AllAreas))
            {
                return hit.position;
            }
            else
            {
                Debug.LogError("Sample position should be used in available areas.");
                return Vector3.zero;
            }
        }
    }
}