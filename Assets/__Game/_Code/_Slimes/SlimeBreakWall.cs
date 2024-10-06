using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBreakWall : BaseSlime
{
    public float dashDistance;
    public float dashDuration;
    public float freezeAfterDashDuration;
    public float cooldownAfterDash;

    private bool _dashing;
    private Vector2 _dashVelocity;
    private float _freezeAfterDashCountdown;
    private float _cooldownAfterDashCountdown;

    protected override void Update()
    {
        base.Update();

        if (_dashing)
        {
            ApplyXMovement(_dashVelocity.x);
        }
    }

    public override void TryShootHost(bool shouldKillOldSlime)
    {
        base.TryShootHost(shouldKillOldSlime);
        if (shouldKillOldSlime)
        {
            if (_nonHostSlime != null) Destroy(_nonHostSlime.gameObject);
        }
        else
        {
            ReactivateNonHostSlime();
        }

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
        
        TryShootHost(true);
    }

    public override void HandleAbilityButtonPressed()
    {
        TryDash();
    }

    private void TryDash()
    {
        if (!_dashing && _freezeAfterDashCountdown <= 0f && _cooldownAfterDashCountdown <= 0f)
        {
            StartCoroutine(_DashCoroutine());
        }
    }

    private IEnumerator _DashCoroutine()
    {
        _dashing = true;
        PlayerInputBrain playerInput = _myCurrentBrain as PlayerInputBrain;
        if (playerInput != null) playerInput.SetInputPaused(true);

        // d = rt
        // r = d/t
        _dashVelocity.x = dashDistance / dashDuration * GetCurrentDirection();
        _dashVelocity.y = 0f;
        yield return new WaitForSeconds(dashDuration);
        
        _dashing = false;
        _cooldownAfterDashCountdown = cooldownAfterDash;
        _freezeAfterDashCountdown = freezeAfterDashDuration;
        yield return new WaitForSeconds(freezeAfterDashDuration);
        _cooldownAfterDashCountdown = 0f;
        _freezeAfterDashCountdown = 0f;
        if (playerInput != null) playerInput.SetInputPaused(false);
    }

    protected override void OnCollisionStay2D(Collision2D other)
    {
        base.OnCollisionStay2D(other);

        if (_dashing)
        {
            Debug.Log($"other collider : {other.collider.name}");
            if (other.collider.TryGetComponent(out BreakableWall breakableWall))
            {
                Debug.Log("destroy it!");
                Destroy(breakableWall.gameObject);
            }
        }
    }
}
