using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeJumping : BaseSlime
{
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
}
