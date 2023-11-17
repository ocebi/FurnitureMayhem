using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AgentSystem
{
    public class AgentMovement : MonoBehaviour
    {
        private Rigidbody _rb;

        private Rigidbody rb
        {
            get
            {
                if (_rb == null)
                    _rb = GetComponent<Rigidbody>();
                return _rb;
            }
        }
        float moveSpeed = 7.5f;

        [SerializeField, ReadOnly]
        Vector3 movementVector = Vector3.zero;
        bool canMove = true;

        public void MoveCharacter(Vector2 inputVector)
        {
            movementVector = new Vector3(inputVector.x, 0, inputVector.y);
        }

        private void FixedUpdate()
        {
            if (movementVector == Vector3.zero)
                return;
            if(!canMove) 
                return;

            Vector3 direction = new Vector3(movementVector.x, 0, movementVector.z);

            Vector3 targetVelocity = direction;
            targetVelocity *= moveSpeed;
            Vector3 velocity = rb.velocity;

            float maxVelocityChange = moveSpeed * 3;

            Vector3 velocityChange = (targetVelocity - velocity);
            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);

            //rb.AddForce(velocityChange, ForceMode.Impulse);
            //rb.MovePosition(transform.position + movementVector * moveSpeed * 0.001f);
            rb.AddForce(velocityChange * Time.deltaTime * 75, ForceMode.Impulse);
        }

        public float GetVelocity()
        {
            return rb.velocity.magnitude;
        }

        public void ToggleMovement(bool value)
        {
            canMove = value;
        }

        public void SetMoveSpeed(float speed)
        {
            moveSpeed = speed;
        }
    }

}