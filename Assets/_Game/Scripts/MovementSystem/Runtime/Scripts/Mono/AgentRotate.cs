using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentRotate: MonoBehaviour
{
    float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    Transform rotateTarget = null;
    bool canRotate = true;

    private void Awake()
    {
        StartCoroutine(IESetRotateTarget());
    }

    public void Rotate(Vector2 inputVector)
    {
        if(!canRotate) return;
        //Vector3 lookTarget = new Vector3(inputVector.x, 0, inputVector.y);
        //transform.forward = new Vector3(inputVector.x, 0, inputVector.y);
        //transform.Rotate()
        //transform.LookAt(transform.position + lookTarget);

        Vector3 direction = new Vector3(inputVector.x, 0, inputVector.y);
        direction = direction.normalized;

        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
    }

    public void SetRotateTarget(Transform target) 
    {
        rotateTarget = target;
    }

    public void ToggleRotate(bool value)
    {
        canRotate = value;
    }

    IEnumerator IESetRotateTarget()
    {
        while(true)
        {
            if(rotateTarget != null && canRotate)
                transform.LookAt(new Vector3(rotateTarget.position.x, transform.position.y, rotateTarget.position.z));

            yield return new WaitForEndOfFrame();
        }
    }
}
