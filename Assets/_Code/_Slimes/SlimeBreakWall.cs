using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBreakWall : BaseSlime
{
    public override void TryShootHost()
    {
        base.TryShootHost();
        ReactivateNonHostSlime();
        Destroy(gameObject);
    }

    public void SetNonHostSlime(BaseSlime nonHostSlime)
    {
        _nonHostSlime = nonHostSlime;
    }

    private void ReactivateNonHostSlime()
    {
        _nonHostSlime.transform.position = transform.position;
        _nonHostSlime.gameObject.SetActive(true);
    }

    protected override void HandleSpikes(Spikes spikes)
    {
        base.HandleSpikes(spikes);
        
        TryShootHost();
    }
}
