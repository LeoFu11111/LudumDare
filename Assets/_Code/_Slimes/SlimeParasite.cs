using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeParasite : BaseSlime
{
    public PlayerInputBrain playerInputBrainPrefab;
    
    protected override void Start()
    {
        if (myDefaultBrain == null)
        {
            myDefaultBrain = Instantiate(playerInputBrainPrefab);
        }
        base.Start();
    }

    protected override void OnCollisionEnter2D(Collision2D other)
    {
        base.OnCollisionEnter2D(other);

        if (other.gameObject.TryGetComponent(out SlimeJumping slimeJumping))
        {
            TakeOverOtherSlime(slimeJumping);
            gameObject.SetActive(false);
        }
    }
    
    protected override void HandleSpikes(Spikes spikes)
    {
        base.HandleSpikes(spikes);
        
        Debug.Log("uh oh! player died!");
    }
}
