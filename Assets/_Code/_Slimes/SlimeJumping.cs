using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeJumping : BaseSlime
{
    public SlimeBreakWall slimeBreakWallPrefab;
    
    public override bool GetYVelocityFromInput(out float yVelocityFromInput)
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            yVelocityFromInput = jumpSpeed;
            return true;
        }
        else
        {       
            yVelocityFromInput = 0f;
            return false;
        }
    }

    public override bool CanShootHost()
    {
        return true;
    }
    
    protected override void OnCollisionEnter2D(Collision2D other)
    {
        base.OnCollisionEnter2D(other);
        
        if (_myCurrentBrain.IsControlledByPlayer())
        {
            if (other.gameObject.TryGetComponent(out SlimeJumping slimeJumping))
            {
                SlimeBreakWall newBigSlime = Instantiate(slimeBreakWallPrefab);
                newBigSlime.SetNonHostSlime(slimeJumping);
                newBigSlime.transform.position = (transform.position + other.transform.position) / 2f;
                TakeOverOtherSlime(newBigSlime);
                gameObject.SetActive(false);
                slimeJumping.gameObject.SetActive(false);
            }
        }
    }
}
