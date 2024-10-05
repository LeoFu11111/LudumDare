using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCheckpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out BaseSlime slime))
        {
            if (slime.IsControlledByPlayer())
            {
                // [] force slime to become parasite and kill any resulting slimes
                
                // [] close off entrance, open exit
                
                // [] set as last activate checkpoint in case of death (index-based)
            }
        }
    }
}
