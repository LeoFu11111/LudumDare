using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _singleton;
    public static GameManager S
    {
        get
        {
            if (_singleton == null)
            {
                _singleton = (Instantiate(Resources.Load("GameManagerPrefab") as GameObject)).GetComponent<GameManager>();
                DontDestroyOnLoad(_singleton.gameObject);
            }
            return _singleton;
        }
    }

    public AudioManager audioManager;
    
    private int _lastSavedCheckpointIndex = -1;

    public void SaveCheckpoint(LevelCheckpoint checkpoint)
    {
        LevelCheckpoint[] levelCheckpoints = FindObjectsOfType<LevelCheckpoint>();
        for (int index = 0; index < levelCheckpoints.Length; index++)
        {
            LevelCheckpoint testCheckpoint = levelCheckpoints[index];
            if (checkpoint == testCheckpoint)
            {
                _lastSavedCheckpointIndex = index;
            }
        }
    }

    public void ClearLastSavedCheckpoint()
    {
        _lastSavedCheckpointIndex = -1;
    }

    public LevelCheckpoint GetLastSavedCheckpoint()
    {
        if (_lastSavedCheckpointIndex == -1) return null;
        else
        {
            LevelCheckpoint[] levelCheckpoints = FindObjectsOfType<LevelCheckpoint>();
            return levelCheckpoints[_lastSavedCheckpointIndex];
        }
    }

    public void PlayerDies(BaseSlime slime)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
