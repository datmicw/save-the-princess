using System;
using UnityEngine;

public class MovementPlayer : MonoBehaviour
{
    public float laneDistance = 3f; // Khoảng cách giữa các làn đường
    public float moveSpeed = 10f;  // Tốc độ di chuyển
    private int currentLane = 0;   // Làn đường hiện tại (-1, 0, 1)
    private Rigidbody rb;          // RigidBody của nhân vật
    public float jumpForce = 5f;   // Lực nhảy
    private bool isDragging = false; // Trạng thái kéo chuột
    private bool isGrounded = true;  // Trạng thái đang đứng trên mặt đất

    private Vector2 startTouchPosition; // Vị trí bắt đầu kéo chuột
    private Vector2 currentTouchPosition; // Vị trí hiện tại kéo chuột

    private Vector3 targetPosition; // Vị trí mục tiêu

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Lấy RigidBody của nhân vật
        targetPosition = transform.position; // Thiết lập vị trí ban đầu
    }

    void Update()
    {
        HandleSwipe();

        // Di chuyển nhân vật đến vị trí mục tiêu
        Vector3 desiredPosition = new Vector3(
            currentLane * laneDistance, // Xác định vị trí theo làn đường
            rb.position.y,             // Giữ nguyên độ cao
            rb.position.z);            // Tiến lên phía trước
        rb.MovePosition(Vector3.MoveTowards(rb.position, desiredPosition, moveSpeed * Time.deltaTime));
    }

    void HandleSwipe()
    {
        if (Input.GetMouseButtonDown(0)) // Khi nhấn chuột
        {
            isDragging = true;
            startTouchPosition = Input.mousePosition; // Lấy vị trí bắt đầu kéo
        }
        else if (Input.GetMouseButton(0) && isDragging) // Khi kéo chuột
        {
            currentTouchPosition = Input.mousePosition; // Cập nhật vị trí kéo hiện tại
            Vector2 difference = currentTouchPosition - startTouchPosition;

            // Đổi làn nếu kéo ngang
            if (Mathf.Abs(difference.x) > Mathf.Abs(difference.y)) // Kéo ngang
            {
                if (difference.x > 50 && currentLane < 1) // Sang phải
                {
                    currentLane++;
                    ResetSwipe();
                }
                else if (difference.x < -50 && currentLane > -1) // Sang trái
                {
                    currentLane--;
                    ResetSwipe();
                }
            }
            // Nhảy nếu kéo lên
            else if (difference.y > 30) // Kéo lên
            {
                Jump();
                ResetSwipe();
            }
        }
        else if (Input.GetMouseButtonUp(0)) // Khi nhả chuột
        {
            ResetSwipe();
        }
    }

    void ResetSwipe()
    {
        isDragging = false;
        startTouchPosition = Vector2.zero;
        currentTouchPosition = Vector2.zero;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Nếu va chạm với mặt đất hoặc tàu
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Train"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        // Khi rời khỏi mặt đất hoặc tàu
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Train"))
        {
            isGrounded = false;
        }
    }

    void Jump()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
        else
        {
            Debug.Log("Cannot Jump: Not Grounded");
        }
    }
}
