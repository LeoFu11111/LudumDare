using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCamera : MonoBehaviour
{
    private PlayerInputBrain _playerInputBrain;
    
    void LateUpdate()
    {
        if (_playerInputBrain == null)
        {
            _playerInputBrain = FindObjectOfType<PlayerInputBrain>();
        }

        if (_playerInputBrain != null && _playerInputBrain.MySlime != null)
        {
            Vector2 targetPos = transform.position;
            targetPos = Vector2.Lerp(targetPos, _playerInputBrain.MySlime.transform.position, Time.deltaTime * 10f);
            transform.position = new Vector3(targetPos.x, targetPos.y, transform.position.z);
        }
    }
}
