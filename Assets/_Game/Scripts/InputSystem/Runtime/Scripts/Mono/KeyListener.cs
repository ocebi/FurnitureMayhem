using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace InputSystem
{
    public class KeyListener : MonoBehaviour
    {
        [SerializeField] KeyCode key;

        [SerializeField] UnityEvent onKeyDownEvent;
        [SerializeField] UnityEvent onKeyHoldEvent;
        [SerializeField] UnityEvent onKeyUpEvent;

        private void Update()
        {
            if(Input.GetKeyDown(key))
            {
                onKeyDownEvent?.Invoke();
            }else if (Input.GetKey(key))
            {
                onKeyHoldEvent?.Invoke();
            }else if (Input.GetKeyUp(key))
            {
                onKeyUpEvent?.Invoke();
            }
        }
    }
}