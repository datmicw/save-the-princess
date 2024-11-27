using System;
using UnityEngine;

public class MovementPlayer : MonoBehaviour
{
    public float laneDistance = 3f;     // Khoảng cách giữa các làn đường
    public float moveSpeed = 10f;      // Tốc độ tiến lên phía trước
    public float laneSwitchSpeed = 5f; // Tốc độ đổi làn
    private int currentLane = 0;       // Làn đường hiện tại (-1, 0, 1)
    public float jumpForce = 5f;       // Lực nhảy

    private bool isGrounded = true;    // Trạng thái đang đứng trên mặt đất
    private bool isDragging = false;   // Trạng thái kéo chuột

    private Rigidbody rb;              // RigidBody của nhân vật
    private Vector2 startTouchPosition;// Vị trí bắt đầu kéo chuột
    private Vector2 currentTouchPosition; // Vị trí hiện tại kéo chuột
    public MapRun mapRun;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Lấy RigidBody của nhân vật
        if (mapRun == null) // Kiểm tra MapRun có tồn tại không
            Debug.Log("MapRun is missing!");

    }

    void Update()
    {
        HandleSwipe(); // Xử lý kéo chuột
        MovePlayer(); // Di chuyển nhân vật
    }

    void MovePlayer()
    {
        // Di chuyển nhân vật theo làn đường
        Vector3 targetPosition = new Vector3(
            currentLane * laneDistance, // Xác định vị trí theo làn đường
            rb.position.y,// Giữ nguyên độ cao
            rb.position.z);// Tiến lên phía trước

        // Chuyển động mượt mà giữa các làn đường
        Vector3 smoothedPosition = Vector3.Lerp(
            rb.position, targetPosition, // Điểm bắt đầu và kết thúc
            laneSwitchSpeed * Time.deltaTime // Tốc độ chuyển động
            );
        rb.MovePosition(smoothedPosition);
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
                    ChangeLane(1);
                    ResetSwipe();
                }
                else if (difference.x < -50 && currentLane > -1) // Sang trái
                {
                    ChangeLane(-1);
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

    void ChangeLane(int direction)
    {
        currentLane += direction;
        Debug.Log("Switched to lane: " + currentLane);
    }

    void ResetSwipe()
    {
        isDragging = false;
        startTouchPosition = Vector2.zero;
        currentTouchPosition = Vector2.zero;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Train"))
        {
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("TrainFront"))
        {
            Debug.Log("Game Over!");
            StopMapRun();
            Time.timeScale = 0; // Dừng thời gian và kết thúc game
        }
    }
    void StopMapRun()
    {
        if (mapRun != null)
            mapRun.StopMap();
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
