using informacoes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    // REQUIREMENTS
    public Animator anim;                   // Animator component.
    public charinfo info;                   // Info component.
    public charmove move;                   // Movement component.
    public GameObject punchCollider;        // Collider child component.

    // INTERNAL REQUIREMENTS
    CombatCollider col;                     // Combat collider script.
    //GameObject mobs;                      // What the f#ck is this.

    // VARIABLES
    public bool combat = false;             // Is the player in combat?
    public int stage = 0;                   // Current animation.
    public float defenseReloadSpeed = 1f;   // Time take to reload defense. (DRS in code)
    public float damage = 33f;              // Damage inflicted by the player.
    public float staminaCost = 20f;         // Cost of stamina. (Punch = Full Value | Faint = Half | Defense = Quarter)

    // INTERNAL AUXILIAR VARIABLES
    private bool hit = false;               // If hitted.
    private bool defend = false;            // If defending.
    private bool defenseStatus = true;      // Is defensing available?
    public float defense = 1f;              // Defense animation auxiliar.
    private float life;                     // Internal life value.
    private float DRS;                      // Defense Reload Speed but abbreviated. (Because why not?)
    private bool defended = false;          // If the attack was defended?
    public bool faint = false;              // If fainted.
    private bool critic = false;            // A hit now is critic?
    public bool isDefend;                   // Tell IA if you defended.
    public bool input = true;               // Can the script receive inputs.
    public int inputKey = 0;


    //Codigo de vitu_____________________________________________________//
    private void Start()
    {
        punchCollider = GameObject.Find("PunchCollider");
        punchCollider.SetActive(false);
    }
    //___________________________________________________________________//

    // START COMBAT --------------------------------------------------------------------------------------------------------    
    void CombatStart()
    {
        DRS = defenseReloadSpeed;                               // Transform DefenseReloadSpeed to DRS to short things up.
        life = charinfo.vida;                                   // Updates internal variable life.
        stage = 0;                                              // Set animation stage to start.
        combat = true;                                          // Updates internal variable combat.
        move.enabled = false;                                   // Disable movement.
        anim.speed = 1f;                                        // Set animation speed to default.
        col = punchCollider.GetComponent<CombatCollider>();     // Get punch collider
        input = true;
        inputKey = 0;
        
    }
    //______________________________________________________________________________________________________________________/

    
    // INPUTS AND UPDATES ------------------------------------------------------------------------------------------------------------------------------
    void Update()
    {
        // COMBAT STATE UPDATE -------------------------------------------------------------------------------------------------------------------------
        if (info.combat && !combat)                                                                 // If combat haven't started yet...
        {
            punchCollider.SetActive(true);//ativar o punchcolider quando o combate começar           // INVASION DETECTED!!! PROCEED WITH CAUTION!!!
            CombatStart();                                                                           // Start combat.
        }
        if(!info.combat && combat)                                                                  // If combat has already finished...
        {
            FinishCombat();                                                                          // End combat.
            punchCollider.SetActive(false);//desativar o punchcolider quando o combate acabar        // INVASION DETECTED!!! PROCEED WITH CAUTION!!!
        }
        //______________________________________________________________________________________________________________________________________________/

        // INPUT RECEIVER ------------------------------------------------------------------
        if (input)
        {
            if (Input.GetButtonDown("Fire1") && (inputKey == 0 || inputKey == 1))                       // Attack.
                { Attack("1"); }
            if (Input.GetButtonDown("Fire2") && (inputKey == 0 || inputKey == 2))                       // Defend.
                { Defend(); }
            if (Input.GetButtonDown("Jump") && (inputKey == 0 || inputKey == 3))                        // Faint.
                { Faint(); }
            if (Input.GetKeyDown(KeyCode.A) && combat == true && inputKey == 0)  
                { transform.eulerAngles = new Vector3(0, 180, 0); } // Walk to the left.
            if (Input.GetKeyDown(KeyCode.D) && combat == true && inputKey == 0)
                { transform.eulerAngles = new Vector3(0, 0, 0); }   // Walk to the right.
        }
        //__________________________________________________________________________________/

        // ANIMATION UPDATE --------------------------------------------------------
        anim.SetBool("Combat", combat);         // Enter combat animation state.
        anim.SetInteger("Combat_Anim", stage);  // Set right combat animation.
        //__________________________________________________________________________/

        // LIFE UPDATE -----------------------------------------
        if (combat)
        {
            charinfo.vida = life;   // Update life in charinfo.
        }
        //______________________________________________________/

        // Finish Battle ---------------------------------------------------------------------------------------------------
        GameObject[] gameObjects;                                   // Set array for enemies.
        gameObjects = GameObject.FindGameObjectsWithTag("enemy");   // Get enemies in the array.
        if (gameObjects.Length == 0)                                // If the array is empty. (There is no more enemies)
        {
            info.detectado = false;                                  // Change info for detected.
            info.combat = false;                                     // Change info for combat.
        }
        //__________________________________________________________________________________________________________________/
    }
    //__________________________________________________________________________________________________________________________________________________/

    // IDDLE -----------------------------------------------------------------------
    void Idle(string Event)
    {
        critic = false;                             // Remove critic damage.
        stage = 1;                                  // Set animation to idle.
        hit = false;                                // Remove hit status.
        if (Event == "Defend")                      // If after defense...
        {
            defend = false;                          // Stop defending
            isDefend = false;                        // Also stop defending.
            if (!defended)                           // If it defended nothing...
            {
                defenseStatus = false;                // Disable defense.
                StartCoroutine("ReloadDefense");      // Reload defense.
            }
            defended = false;                        // Reset defended
        }
    }
    //______________________________________________________________________________/

    // ATTACK ------------------------------------------------------------------------------------------------------------------------------
    void Attack(string Event)
    {
        if (charinfo.stamina > staminaCost || faint)                                    // If you have enough stamina or are fainting...
        {
            if (stage <= 1 && Event == "1")                                              // If idle...
            {
                stage = 2;                                                                // Anticipation animation.
                critic = true;                                                            // Enable critic.
            }
            else                                                                         // If not idle...
            {
                if (!hit && !defend && Event == "2")                                      // If called by anticipation animation...
                {
                    critic = false;                                                        // Disable critic.
                    if (!faint)                                                            // If you did not faint...
                    {
                        charinfo.stamina -= staminaCost;                                    // Remove stamina.
                        stage = 3;                                                          // Attack animation.
                        if (col.enemy.GetComponent<Enemy_Combat>() != null)                 // If there's a enemy to attack...
                        {
                            if(col.state != 0)                                               // If you are facing the enemy...
                            {
                                col.enemy.GetComponent<Enemy_Combat>().Hit(damage);           // Deal the damage.
                            }
                        }
                    }
                    else                                                                   // If you did faint...
                    {
                        faint = false;                                                      // Disable faint.
                        stage = 1;                                                          // Return to default position.
                    }
                }
            }
        }
    }
    //______________________________________________________________________________________________________________________________________/

    // DEFEND ----------------------------------------------------------------------------------------------
    void Defend()
    {
        if (defenseStatus && !hit && charinfo.stamina > staminaCost/2)              // If you can defend...
        {
            charinfo.stamina -= staminaCost / 4;                                     // Consume stamina.
            defend = true;                                                           // Defend.
            isDefend = true;                                                         // Also defend.
            stage = 4;                                                               // defend animation.
        }
    }
    //______________________________________________________________________________________________________/

    // DEFENSE RELOAD TIME -------------------------------------------------------------
    IEnumerator ReloadDefense()
    {
        defense = 0f;                                   // Turn animation on.
        for (int i = 0; i != 24 ; i++)                  // Wait some time...
        {
            defense += 1f/24;                            // Increase defense animation.
            yield return new WaitForSeconds(DRS/24);     // Return.
        }
        defense = 1;                                    // Turn animation off.
        defenseStatus = true;                           // Enable defense again.
    }
    //__________________________________________________________________________________/

    // FAINT -----------------------------------------------------------------------------------------------------------------------------------
    void Faint()
    {
        if(stage <= 2 && charinfo.stamina > staminaCost/2 && !faint)        // If animation is idle or attack 1 and you have enough stamina...
        {
            col.enemy.GetComponent<Enemy_Combat>().Fainted();                // Warn enemy of the faint.
            charinfo.stamina -= staminaCost/4;                               // Consume stamina.
            faint = true;                                                    //
            if(stage != 2)                                                   // If you are not attacking...
            {
            Attack("1");                                                      // Atack to faint.
            }
        }
    }
    //__________________________________________________________________________________________________________________________________________/

    // HIT ---------------------------------------------------------------------------------
    public void Hit(float hitDamage, float direction)
    {   
        if(direction == transform.right.x)          // If your back is against oponent...
        {
            critic = true;                           // Critic damage on.
        }
        if (!defend)                                // If you are not defending 
        {
            hit = true;                              // Hitted.
            stage = 5;                               // Hit animation.
            if (critic)                              // If is critic damage...
            {
                life -= hitDamage*1.73f;              // Deal critic damage.
            }
            else                                     // if not critical damage...
            {
                life -= hitDamage;                    // Deal normal damage.
            }
        }
        else                                        // If defending...
        {
            defended = true;                         // You defended.
            charinfo.stamina += staminaCost / 2;     // Restore stamina.
        }
    }
    //______________________________________________________________________________________/

    // FINISH COMBAT -------------------------------
    void FinishCombat()
    {
        combat = false;         // Disable combat.
        charinfo.vida = life;   // Update life.
        move.enabled = true;    // Enable movement.
    }
    //______________________________________________/
}
