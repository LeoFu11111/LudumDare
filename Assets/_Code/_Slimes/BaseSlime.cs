using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSlime : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public ContactFilter2D groundedContactFilter;
    public float jumpSpeed;
    public float slowMoveSpeed;
    public float fastMoveSpeed;
    public BaseBrain myDefaultBrain;
    public ContactFilter2D patrolContactFilter;

    private List<RaycastHit2D> _raycastHits = new();
    private BaseBrain _myCurrentBrain;
    private float _currentDirection = 1f;

    protected virtual void Start()
    {
        _myCurrentBrain = myDefaultBrain;
        _myCurrentBrain.SetSlime(this);
    }

    void Update()
    {
        _myCurrentBrain.UpdateBrain(Time.deltaTime);
        

    }

    public virtual bool GetYVelocityFromInput(out float yVelocityFromInput)
    {
        yVelocityFromInput = 0f;
        return false;
    }

    public bool IsGrounded()
    {
        int count = Physics2D.BoxCast(myRigidbody.position - Vector2.up * 0.5f, new Vector2(1f, 0.1f), 0f, Vector2.down, groundedContactFilter, _raycastHits, 0.01f);
        return count > 0;
    }

    public float GetCurrentDirection()
    {
        return _currentDirection;
    }

    public void TurnAround()
    {
        _currentDirection *= -1f;
    }

    public void ApplyXMovement(float xSpeed)
    {
        Vector2 vel = myRigidbody.velocity;
        vel.x = xSpeed;
        myRigidbody.velocity = vel;
    }

    public void ApplyYMovement(float ySpeed)
    {
        Vector2 vel = myRigidbody.velocity;
        vel.x = ySpeed;
        myRigidbody.velocity = vel;
    }
}
