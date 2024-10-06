using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBrain : MonoBehaviour
{
    protected BaseSlime _mySlime;
    public BaseSlime MySlime => _mySlime;

    public virtual void SetSlime(BaseSlime mySlime)
    {
        _mySlime = mySlime;
    }
    
    public abstract void UpdateBrain(float deltaTime);

    public virtual bool IsControlledByPlayer()
    {
        return false;
    }
}
