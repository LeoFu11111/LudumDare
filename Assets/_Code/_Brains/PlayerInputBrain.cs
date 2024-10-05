using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputBrain : BaseBrain
{
    public override void UpdateBrain(float deltaTime)
    {
        _mySlime.ApplyXMovement(Input.GetAxis("Horizontal") * _mySlime.fastMoveSpeed);
        if (_mySlime.GetYVelocityFromInput(out float yVelocityFromInput))
        {
            _mySlime.ApplyYMovement(yVelocityFromInput);
        }
    }
}
