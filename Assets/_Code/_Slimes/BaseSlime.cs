using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSlime : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public Collider2D mainCollider;
    public ContactFilter2D groundedContactFilter;
    public float jumpSpeed;
    public float slowMoveSpeed;
    public float fastMoveSpeed;
    public BaseBrain myDefaultBrain;
    public ContactFilter2D patrolContactFilter;

    [Header("Host Shooting")] public float hostShootSpeed;
    
    private List<RaycastHit2D> _raycastHits = new();
    protected BaseBrain _myCurrentBrain;
    private float _currentDirection = 1f;
    private BaseSlime _currentHost;

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
        vel.y = ySpeed;
        myRigidbody.velocity = vel;
    }

    public void SetBrain(BaseBrain newBrain)
    {
        _myCurrentBrain = newBrain;
        newBrain.SetSlime(this);
    }

    public void ResetToDefaultBrain()
    {
        SetBrain(myDefaultBrain);
    }

    public virtual bool CanShootHost()
    {
        return false;
    }

    public virtual void TryShootHost()
    {
        Debug.Log("trying to shoot host");
        StartCoroutine(_TemporaryCollisionIgnoreCoroutine(mainCollider, _currentHost.mainCollider));
        _currentHost.transform.position = transform.position;
        _currentHost.gameObject.SetActive(true);
        _currentHost.ApplyYMovement(hostShootSpeed);
        ReleaseControlToHostSlime();

    }

    private IEnumerator _TemporaryCollisionIgnoreCoroutine(Collider2D colliderA, Collider2D colliderB)
    {
        Physics2D.IgnoreCollision(colliderA, colliderB);
        yield return new WaitForSeconds(0.4f);
        if (colliderA != null && colliderB != null)
        {
            Physics2D.IgnoreCollision(colliderA, colliderB, false);
        }
    }

    public void SetHost(BaseSlime newHost)
    {
        _currentHost = newHost;
    }

    public void ClearHost()
    {
        SetHost(null);
    }

    public void TakeOverOtherSlime(BaseSlime otherSlime)
    {
        Debug.Log($"{name} taking over {otherSlime.name}");
        BaseBrain tempBrain = _myCurrentBrain;
        ResetToDefaultBrain();
        otherSlime.SetBrain(tempBrain);
        otherSlime.SetHost(this);
    }

    public void ReleaseControlToHostSlime()
    {
        Debug.Log($"{name} releasing control to {_currentHost.name}");
        _currentHost.SetBrain(_myCurrentBrain);
        ResetToDefaultBrain();
        ClearHost();
    }
}
