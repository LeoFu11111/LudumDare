using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TinyCreatureChooser : MonoBehaviour
{
    private PlayerCharacter _leftCreature;
    private PlayerCharacter _upCreature;
    private PlayerCharacter _rightCreature;
    private List<PlayerCharacter> _allPotentialCreatures = new();
    private bool _choosing;
    
    public void SetupChoosing(PlayerCharacter leftCreature, PlayerCharacter upCreature, PlayerCharacter rightCreature)
    {
        _choosing = true;
        _leftCreature = leftCreature;
        _upCreature = upCreature;
        _rightCreature = rightCreature;
        _allPotentialCreatures.Add(leftCreature);
        _allPotentialCreatures.Add(upCreature);
        _allPotentialCreatures.Add(rightCreature);
    }

    void Update()
    {
        if (_choosing)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                ChooseCreature(_leftCreature);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                ChooseCreature(_upCreature);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                ChooseCreature(_rightCreature);
            }
        }
    }

    private void ChooseCreature(PlayerCharacter chosenCreature)
    {
        _choosing = false;
        foreach (PlayerCharacter potentialCreature in _allPotentialCreatures)
        {
            if (potentialCreature != chosenCreature)
            {
                Destroy(potentialCreature.gameObject);
            }
        }

        chosenCreature.myRigidbody.gravityScale = 1f;
        chosenCreature.enabled = true;
    }
}
