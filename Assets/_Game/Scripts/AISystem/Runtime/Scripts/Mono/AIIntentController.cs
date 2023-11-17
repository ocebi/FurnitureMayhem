using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AISystem
{
    public class AIIntentController : MonoBehaviour
    {
        AI ai;
        Vector3? movePosition;
        Rigidbody rb;

        public void Initialize()
        {
            ai = GetComponent<AI>();
            rb = GetComponent<Rigidbody>();

            StartCoroutine(IEAIMovement());
        }

        public void SendMovePosition(Vector3 currentPos, Vector3 targetPos)
        {
            movePosition = targetPos;
            // if (!pv) return;
            // pv.RPC(nameof(ReceiveMovePosition), RpcTarget.AllBufferedViaServer, transform.position, targetPos);

        }

        public void SendStopMove()
        {
            movePosition = transform.position;
            // if (!pv) return;
            // pv.RPC(nameof(ReceiveStopMove), RpcTarget.AllBufferedViaServer);
        }

        // [PunRPC]
        // public void ReceiveMovePosition(Vector3 currentPos, Vector3 targetPos)
        // {
        //     if (Vector3.Distance(transform.position, currentPos) > TeleportDistanceIfGreater)
        //     {
        //         transform.position = currentPos;
        //     }

        //     movePosition = targetPos;
        // }

        // [PunRPC]
        // public void ReceiveStopMove()
        // {
        //     movePosition = transform.position;
        // }

        IEnumerator IEAIMovement()
        {
            while (ai.AIMover) //&& !GameStateManager.Instance.IsGameEndedGlobal)
            {
                if(movePosition != null)
                {
                    if (Vector3.Distance(transform.position, movePosition.Value) > 1)
                        ai.AIMover.MoveToTarget(movePosition.Value);
                    else
                    {
                        movePosition = null;
                        ai.AIMover.StopMoving();
                        rb.velocity = Vector3.zero;
                    }
                }

                yield return null;
            }

            ai.AIMover.StopMoving();
            rb.velocity = Vector3.zero;
        }
    }
}