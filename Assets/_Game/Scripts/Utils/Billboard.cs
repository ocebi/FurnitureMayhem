using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Camera m_Camera;

    private void Start()
    {
        m_Camera = Camera.main;
    }
    
    void LateUpdate()
    {
        transform.LookAt(transform.position + m_Camera.transform.rotation * Vector3.forward,
            m_Camera.transform.rotation * Vector3.up);
    }
}