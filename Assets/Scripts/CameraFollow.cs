using UnityEngine;

namespace Camera
{
    public class CameraFollow : MonoBehaviour
    {
        public Transform target;       // Target object to follow
        public Vector3 offset;         // Offset from the target
        public float smoothSpeed = 0.125f; // Smoothness of movement

        // Update is called once per frame
        void LateUpdate()
        {
            if (target != null) // kiem tra target co ton tai khong
            {
                Vector3 desiredPosition = CalculateDesiredPosition(); // Tinh toan vi tri mong muon
                SmoothMoveToTarget(desiredPosition); // Di chuyen camera den vi tri mong muon
            }
        }
        // Calculate the desired position of the camera
        private Vector3 CalculateDesiredPosition()
        {
            return target.position + offset; // Tinh toan vi tri
        }

        private void SmoothMoveToTarget(Vector3 desiredPosition) // Di chuyen camera den vi tri mong muon
        {
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.LookAt(target);
        }
    }
}
