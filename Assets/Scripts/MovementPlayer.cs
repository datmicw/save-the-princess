using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlayer : MonoBehaviour
{
    public float laneDistance = 3f; // Khoảng cách giữa các làn đường
    public float moveSpeed = 10f;  // Tốc độ di chuyển
    private int currentLane = 0; // Lane giữa (làn đường -1, 0, 1)
    private Rigidbody rb; // RigidBody của nhân vật
    public float JumpForce = 5f; // Lực nhảy
    private bool isDragging = false; // Trạng thái kéo chuột
    public bool isGrounded = true; // Trạng thái đang đứng trên mặt đất


    private Vector2 startTouchPosition; // Vị trí bắt đầu kéo chuột
    private Vector2 currentTouchPosition; // Vị trí hiện tại kéo chuột
    private Vector3 targetPosition; // Vị trí mục tiêu

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Lấy RigidBody của nhân vật
        targetPosition = transform.position; // Vị trí mục tiêu ban đầu
    }

    void Update()
    {
        HandleSwipe();

        // Cập nhật vị trí nhân vật
        targetPosition = new Vector3(
            currentLane * laneDistance, // Làn đường
            transform.position.y, // Vị trí y không đổi
            transform.position.z); // Vị trí mục tiêu
        transform.position = Vector3.Lerp(
            transform.position, // Vị trí hiện tại
            targetPosition, // Vị trí mục tiêu
            moveSpeed * Time.deltaTime); // Di chuyển nhân vật đến vị trí mục tiêu
    }

    void HandleSwipe()
    {
        if (Input.GetMouseButtonDown(0)) // Khi nhấn chuột
        {
            isDragging = true; // Bắt đầu kéo chuột
            startTouchPosition = Input.mousePosition; // Lấy vị trí bắt đầu kéo chuột
        }
        else if (Input.GetMouseButton(0) && isDragging) // Khi kéo chuột
        {
            currentTouchPosition = Input.mousePosition; // Lấy vị trí hiện tại kéo chuột
            Vector2 difference = currentTouchPosition - startTouchPosition; // Khoảng cách giữa vị trí hiện tại và vị trí bắt đầu kéo chuột

            // Kéo theo chiều ngang để đổi làn
            if (Mathf.Abs(difference.x) > Mathf.Abs(difference.y)) // Kéo theo chiều ngang
            {
                if (difference.x > 50 && currentLane < 1) // Kéo sang phải
                {
                    currentLane++;
                    ResetSwipe();
                }
                else if (difference.x < -50 && currentLane > -1) // Kéo sang trái
                {
                    currentLane--; // Đổi làn
                    ResetSwipe(); // Reset trạng thái kéo chuột
                }
            }
            // Kéo lên để nhảy
            else if (difference.y > 50) // Chỉ quan tâm kéo lên
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

    void ResetSwipe() // Reset trạng thái kéo chuột
    {
        isDragging = false;
        startTouchPosition = Vector2.zero; // Reset vị trí bắt đầu kéo chuột
        currentTouchPosition = Vector2.zero; // Reset vị trí hiện tại kéo chuột
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }
    void Jump()
    {
       if(isGrounded == true)
        {
            rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
        else
        {
            Debug.Log("Can not Jumb");
        }
    }
}
