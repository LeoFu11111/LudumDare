using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public PlayerCharacter tinyCreaturePrefab;
    public TinyCreatureChooser tinyCreatureChooserPrefab;
    public ContactFilter2D groundedContactFilter;
    public float jumpSpeed;
    public float moveSpeed;

    private List<RaycastHit2D> _raycastHits = new();
    void Update()
    {
        Vector2 vel = myRigidbody.velocity;
        vel.x = Input.GetAxis("Horizontal") * moveSpeed;
        
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {

            vel.y = jumpSpeed;

        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Split();
        }
        
        myRigidbody.velocity = vel;
    }

    public bool IsGrounded()
    {
        int count = Physics2D.BoxCast(myRigidbody.position - Vector2.up * 0.5f, new Vector2(1f, 0.1f), 0f, Vector2.down, groundedContactFilter, _raycastHits, 0.01f);
        return count > 0;
    }

    private void Split()
    {
        // instantiate three lil baddies
        Vector2 posOffset = Vector2.up * 2f;
        posOffset = Quaternion.AngleAxis(45f, Vector3.forward) * posOffset;
        List<PlayerCharacter> allPotentialCreatures = new();
        for (int i = 0; i < 3; i++)
        {
            PlayerCharacter potentialTinyCreature = Instantiate(tinyCreaturePrefab);
            potentialTinyCreature.transform.position = transform.position + (Vector3)posOffset;
            posOffset = Quaternion.AngleAxis(-45f, Vector3.forward) * posOffset;
            allPotentialCreatures.Add(potentialTinyCreature);
        }

        // wait for choice
        TinyCreatureChooser tinyCreatureChooser = Instantiate(tinyCreatureChooserPrefab);
        tinyCreatureChooser.SetupChoosing(allPotentialCreatures[0], allPotentialCreatures[1], allPotentialCreatures[2]);

        // deactivate this and activate that
        Destroy(gameObject);
    }
}
