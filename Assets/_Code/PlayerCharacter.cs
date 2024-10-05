using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public ContactFilter2D groundedContactFilter;
    public float jumpSpeed;
    public float moveSpeed;

    private List<RaycastHit2D> _raycastHits = new();
    void Update()
    {
        Vector2 vel = myRigidbody.velocity;
        vel.x = Input.GetAxis("Horizontal") * moveSpeed;
        
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {

            vel.y = jumpSpeed;

        }
        
        myRigidbody.velocity = vel;
    }

    public bool IsGrounded()
    {
        int count = Physics2D.BoxCast(myRigidbody.position - Vector2.up * 0.5f, new Vector2(1f, 0.1f), 0f, Vector2.down, groundedContactFilter, _raycastHits, 0.01f);
        return count > 0;
    }
}
