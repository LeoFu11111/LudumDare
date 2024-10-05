using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.TryGetComponent(out SlimeBreakWall slimeBreakWall))
        {
            Destroy(gameObject);
        }
    }
}
