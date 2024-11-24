using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlayer : MonoBehaviour
{
    public float Speed = 5.0f;
    public float Jumb = 10.0f;
    private Rigidbody rigid;
    private bool isGrounded = true;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }
    void Update()
    {
        float moveHorizontalX = Input.GetAxis("Horizontal") * Speed;
        rigid.velocity = new Vector3(moveHorizontalX, rigid.velocity.y, 0);
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rigid.AddForce(Vector3.up * Jumb, ForceMode.Impulse);
            isGrounded = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
