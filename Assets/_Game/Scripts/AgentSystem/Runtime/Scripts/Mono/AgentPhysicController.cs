using UnityEngine;
using UnityEngine.AI;

public class AgentPhysicController
{
    Rigidbody rb;
    Collider collider;
    public AgentPhysicController(GameObject owner)
    {
        rb = owner.AddComponent<Rigidbody>();
        collider = owner.GetComponent<Collider>();
        //navMeshObstacle = owner.AddComponent<NavMeshObstacle>();
        SetRigidbody();
        SetCollider(true);
        //SetNavMeshObstacle();
    }

    void SetRigidbody()
    {
        if (rb == null) return;

        Debug.Log("Inside set rb");
        rb.mass = 2; //4
        rb.drag = 10; //5
        rb.angularDrag = 20f; //5
        rb.useGravity = true;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        // set rb settings
    }

    public void SetCollider(bool value)
    {
        collider.enabled = value;
    }
}