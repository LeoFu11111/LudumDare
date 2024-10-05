using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingBrain : BaseBrain
{
    private List<RaycastHit2D> _hits = new();
    
    public override void UpdateBrain(float deltaTime)
    {
        int count = Physics2D.Raycast(transform.position, Vector2.right * _mySlime.GetCurrentDirection(), _mySlime.patrolContactFilter, _hits, 0.6f);
        if (count > 0)
        {
            _mySlime.TurnAround();
        }
        
        _mySlime.ApplyXMovement(_mySlime.GetCurrentDirection() * _mySlime.slowMoveSpeed);
    }
}
