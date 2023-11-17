using UnityEngine;

public class CameraRelativeDirection : MonoBehaviour
{
    static Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }

    public static Vector2 AlignToCamera(Vector2 inputVector)
    {
        Vector3 camForward = cam.transform.forward;
        camForward.y = 0;
        camForward = camForward.normalized;
        Vector3 camRight = cam.transform.right;
        camRight.y = 0;
        camRight = camRight.normalized;
        Vector3 returnVector = camForward * inputVector.y + camRight * inputVector.x;
        return new Vector2(returnVector.x, returnVector.z);
    }
}