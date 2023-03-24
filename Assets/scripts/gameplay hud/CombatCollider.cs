using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CombatCollider : MonoBehaviour
{
    // VARIABLES
    public int state = 0;       // Represents triggered state. (0 = not triggered | 1 = triggered with enemy | 2 = triggered with player )  
    public Enemy_Combat enemy;  // Enemy code for punchCollider.
    public Collider2D ene;      // Enemy/Player collider.
    public GameObject enem;     // Enemy/Player GameObject.

    // AUXILIAR
    Collider2D coll;            // Collision of this GameObject.
    Collider2D Player;          // Collider of the player.

    // START -----------------------------------------------------------------------------------
    private void Start()
    {
        coll = GetComponent<Collider2D>();                  // Get collider from this object.
    }
    //__________________________________________________________________________________________/

    // ON TRIGGER ------------------------------------------------------------------------------
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("enemy"))                      // If it's an enemy...
        {
            state = 1;                                       // State is colliding with enemy.
            enemy = other.GetComponent<Enemy_Combat>();      // Get enemy combat script.
            ene = other;                                     // Get collider for enemy.
        }
        enem = other.GetComponent<GameObject>();            // Get collider for... Anything...?
        if (other.CompareTag("Player"))                     // If is's the player...
        {
            state = 2;                                       // State is colliding with player.
            Player = other;                                  // Get collider for player.
        }
    }
    //__________________________________________________________________________________________/

    // OFF TRIGGER -------------------------------------------------------------------------------------
    public void OnTriggerExit2D(Collider2D other)
    {
        if (state == 1 && ene != null)          // If is touching enemy and collision isn't null...
        {
            if (!coll.IsTouching(ene))           // If it's still colliding...
            {
                state = 0;                        // State is not colliding.
                ene = null;                       // clean collision
            }
        }

        // IN TEST
        if (state == 2 && ene != null)          // If is touching player and collision isn't null...
        {
            if (!coll.IsTouching(ene))           // If it's still colliding...
            {
                state = 0;                        // State is not colliding.
                ene = null;                       // clean collision
            }
        }
    }
    //__________________________________________________________________________________________________/

    // UPDATE ----------------------------------------------------------------------------------------------------------
    private void Update()
    {
        if (state == 1 && ene == null)                      // If state says it's triggered but it's not...
        {
            state = 0;                                       // State is not triggered.
        }
        if (state == 2 && !coll.IsTouching(Player))         // If state says it's triggered by player nut it's not...
        {
            state = 0;                                       // State is not triggered.
        }
    }
    //__________________________________________________________________________________________________________________/
}

