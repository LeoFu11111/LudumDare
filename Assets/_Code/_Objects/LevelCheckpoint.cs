using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LevelCheckpoint : MonoBehaviour
{
    public enum CheckpointDirection
    {
        LeftToRight,
        RightToLeft,
    }

    public CheckpointDirection checkpointDirection;
    public GameObject leftWall;
    public GameObject rightWall;

    private GameObject _entranceWall;
    private GameObject _exitWall;
    private bool _checkpointActivated;

    private void Awake()
    {
        _entranceWall = checkpointDirection == CheckpointDirection.LeftToRight ? leftWall : rightWall;
        _exitWall = checkpointDirection == CheckpointDirection.LeftToRight ? rightWall : leftWall;

        _entranceWall.transform.localScale = new Vector3(1f, 0f, 1f);
        _exitWall.transform.localScale = new Vector3(1f, 1f, 1f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_checkpointActivated) return;
        
        if (other.TryGetComponent(out BaseSlime slime))
        {
            if (slime.IsControlledByPlayer())
            {
                // [x] force slime to become parasite and kill any resulting slimes
                slime.ForceRevertToParasite();
                
                // [x] close off entrance, open exit
                _exitWall.transform.DOScaleY(0f, 2.4f);
                _entranceWall.transform.DOScaleY(1f, 0.2f);

                // [] set as last activate checkpoint in case of death (index-based)
                GameManager.S.SaveCheckpoint(this);
            }
        }
    }

    public void RespawnFromCheckpoint(BaseSlime slime)
    {
        slime.transform.position = transform.position;
        _checkpointActivated = true;
        _entranceWall.transform.localScale = new Vector3(1f, 1f, 1f);
        _exitWall.transform.DOScaleY(0f, 2.4f);
    }
}
