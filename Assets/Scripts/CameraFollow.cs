using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;       // Đối tượng mà camera sẽ theo dõi
    public Vector3 offset;         // Vị trí cố định của camera so với nhân vật
    public float smoothSpeed = 0.125f; // Tốc độ di chuyển mượt mà

    void LateUpdate()
    {
        // Target là nhân vật mà camera sẽ theo dõi
        if (target != null)
        {
            // Vị trí camera (phía sau nhân vật)
            Vector3 desiredPosition = target.position + offset;

            // Di chuyển camera mượt mà
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Đảm bảo camera luôn nhìn về phía nhân vật
            transform.LookAt(target);
        }
    }
}
