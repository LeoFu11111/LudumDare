using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputBrain : BaseBrain
{
    private bool _inputPaused;
    public bool InputPaused => _inputPaused;

    public override void SetSlime(BaseSlime mySlime)
    {
        base.SetSlime(mySlime);
        _inputPaused = false; // make sure that input is enabled whenever you enter a new slime
    }

    public override void UpdateBrain(float deltaTime)
    {
        if (_inputPaused) return;
        
        float xInput = Input.GetAxis("Horizontal");
        if (!Mathf.Approximately(xInput, 0f))
        {
            _mySlime.SetCurrentDirection(Mathf.Sign(xInput));
        }
        _mySlime.ApplyXMovement(xInput * _mySlime.fastMoveSpeed);
        if (_mySlime.GetYVelocityFromInput(out float yVelocityFromInput))
        {
            _mySlime.ApplyYMovement(yVelocityFromInput);
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            _mySlime.HandleAbilityButtonPressed();
        }
    }



    public void SetInputPaused(bool value)
    {
        _inputPaused = value;
    }

    public override bool IsControlledByPlayer()
    {
        return true;
    }
}
