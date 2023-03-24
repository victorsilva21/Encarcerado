using informacoes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy_Combat : MonoBehaviour
{
    // REQUIREMENTS
    public Animator anim;                   // Enemy animation.
    public charinfo info;                   // Get players info.
    public GameObject player;               // Player GameObject.
    public policial cop;                    // Cop script.
    public GameObject Glimit;               // GameObject of Limits.
    public GameObject GoffLimits;           // GameObject of Off the limits.
    public Collider2D col;                  // Collider of the enemy.
    public GameObject objReady;             // GameObject of the combat Alert.
    public Transform readyPos;              // Transform of the Combat Alert.
    public SpriteRenderer Enemy_visual;     // Renderer of this enemy.

    // AUXILIAR REQUIREMENTS
    Combat com;                             // Script of the player combat module.
    CombatCollider limit;                   // Script of the collider Limit.
    CombatCollider offLimits;               // Script of the collider Off Limits.

    // Variables
    public float life = 60f;                // Life value.
    public bool combat = false;             // Is the player in combat?
    public int stage = 0;                   // Current animation.
    public float defenseReloadSpeed = 1f;   // Time take to reload defense. (DRS in code)
    public float damage = 17f;              // Damage value.
    public float mediumTimeAttack = 1.07f;  // Medium attack time.
    public float attackChance = 73f;        // Chance of attacking per attack time.
    public float faintChance = 18f;         // Chance of fainting per attack.
    public float defenseChance = 53f;       // Chance of defending the attack.
    public float faintFallChance = 73f;     // Chance of falling in the faint.
    public float waitTime = 0.4f;           // Time between combat alert spawn and action.

    // Internal Auxiliar Variables
    private bool hit = false;               // If hitted.
    private bool defend = false;            // If defending.
    private bool defenseStatus = true;      // Is defensing available?
    private float DRS;                      // Defense Reload Speed but abbreviated. (Because why not?)
    private bool defended = false;          // If the attack was defended?
    private bool faint = false;             // If fainted.
    private bool critic = false;            // A hit now is critic?
    private bool run = false;               // Is IA running?             
    private float timeAtk;                  // Maximum time attack (mediumTimeAttack * 2)
    private bool afterFaint = false;        // Should attack after faint?
    private bool playerDefend;              // Is player defending?
    private int ready = 0;                  // Is this going to attack or faint?
    public bool move = true;               // Is this enemy moving?
    private bool death = false;             // Is the player dead?
    private bool deathAvail = true;         // Is death available?
    private bool manualControl;             // Is this on manual control?
    private bool manualAux;                 // Auxiliar manual control bool.
    private bool manualDef;                 // Active manual defense state.
    private bool manualAnim;                // Active manual animation transition.
    private float manualTime;               // Manual WaitTime.


    // COMPONENTS AND ADJUSTMENTS --------------------------------------------------------------------------------------
    private void Start()
    {
        Enemy_visual = this.gameObject.GetComponent<SpriteRenderer>();  // Get the renderer of this enemy.
        player = GameObject.Find("char");                               // Find the player.
        info = player.GetComponent<charinfo>();                         // Get info component.
        com = player.GetComponent<Combat>();                            // Get player Combat component.
        limit = Glimit.GetComponent<CombatCollider>();                  // Get the script from the collider Limit.
        offLimits = GoffLimits.GetComponent<CombatCollider>();          // Get the scrupt from the collider Off Limits.
        timeAtk = mediumTimeAttack * 2;                                 // Adjust timeAtk.
        manualTime = waitTime;
    }
    //__________________________________________________________________________________________________________________/

    // UPDATES AND IA ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    void Update()
    {
        // COMBAT STARTER ------------------------------------------------------------------------------
        if (info.combat && !combat && !manualControl)       // if combat haven't started yet...
        {
            Enemy_visual.sortingOrder = 3;                   // Change visual layer of the enemy.
            cop.enabled = false;                             // Disable patrol script.
            DRS = defenseReloadSpeed;                        // Abreviatting defenseReloadSpeed to DRS.
            stage = 0;                                       // Set animation to start pose.
            defend = false;                                  // Setting defend to standard.
            hit = false;                                     // Setting hit to standard.
            defenseStatus = true;                            // Setting defenseStatus to standard.
            defended = false;                                // Setting defended to standard.
            run = false;                                     // Setting run to standard.
            combat = true;                                   // Enabling combat.
        }
        //______________________________________________________________________________________________/

        /*MANUAL INPUTS ---------------------------------------
        /  if (Input.GetKeyDown(KeyCode.Z))        // Attack.
            { Attack("1"); }
            if (Input.GetKeyDown(KeyCode.X))        // Defend.
            { Defend(); }
            if (Input.GetKeyDown(KeyCode.C))        // Faint.
            { Faint(); }
        //____________________________________________________*/

        // IA ----------------------------------------------------------------------------------
        if (!com.isDefend)                              // If player is not defending...
        {
            playerDefend = false;                        // Set player to not defending.
        }
        else                                            // If it is defending...
        {
            playerDefend = true;                         // Set it to defending.
        }
        if (limit.state == 2 && !move && !death)        // If in front of player and not moving...
        {
            if (!run && !manualControl)                  // Is the IA code has stopped...
            {
                StartCoroutine("Timing");                 // Start the IA code again.
            }
            if (afterFaint && playerDefend)              // If player felt in faint...
            {
                StopCoroutine("Timing");                  // Stop first step of IA.
                StartCoroutine("FaintAttack");            // Start second half of faint attack.
                run = false;                              // END OF IA CYCLE //
            }
        }
        //______________________________________________________________________________________/

        // MOVEMENT --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        if (combat)                                                                                                                                 // If in combat...
        {
            // FLIP
            if (player.transform.position.x > this.transform.position.x)                                                                             // If enemy is in the left side of the player...
            {
                transform.eulerAngles = new Vector3(0, 0, 0);                                                                                         // Turn to the right.
            }
            else                                                                                                                                     // If enemy is on left side...
            {
                transform.eulerAngles = new Vector3(0, 180, 0);                                                                                       // Turn to the left.
            }

            // WALK
            if (offLimits.state > 0)                                                                                                                 // If there's something inside of offLimits... (Too close)
            {
                if (offLimits.ene != null && offLimits.state == 1)                                                                                    // If it's an enemy...
                {
                    if (transform.right.x * transform.position.x - offLimits.ene.transform.right.x * offLimits.ene.transform.position.x < 0)           // If this is behind the other enemy...
                    {
                        transform.Translate(-1f * Time.deltaTime, 0, 0);                                                                                // Goes back.
                        move = true;                                                                                                                    // It's moving.
                    }
                    else                                                                                                                               // If it isn't behind the other enemy... (This is ahead)
                    {
                        if (limit.state == 0)                                                                                                           // If there isn't nothing ahead...
                        {
                            transform.Translate(2f * Time.deltaTime, 0, 0);                                                                              // Goes forward.
                            move = true;                                                                                                                 // It's moving.
                        }
                    }
                }
                else                                                                                                                                   // If it's not an enemy... (It's a player)
                {
                    transform.Translate(-1f * Time.deltaTime, 0, 0);                                                                                    // Goes back.
                    move = true;                                                                                                                        // It's moving.
                }
            }
            else                                                                                                                                     // If there isn't something inside of offLimits...
            {
                if (limit.state == 0)                                                                                                                 // If there is nothing ahead...
                {
                    transform.Translate(2f * Time.deltaTime, 0, 0);                                                                                    // Goes forward.
                    move = true;                                                                                                                       // It's moving.
                }
                else                                                                                                                                  // If there's something ahead...
                {
                    move = false;                                                                                                                      // It's not moving.
                }
            }
        }
        //______________________________________________________________________________________________________________________________________________________________________________________________________/

        // ANIMATION UPDATE --------------------------------------------------------------------
        anim.SetBool("Combat", combat);                 // Enter in combat animations.
        if (!move)                                      // If not moving...
        {
            anim.SetBool("Move", false);                 // Do not use moving animation.
            anim.SetInteger("Combat_Anim", stage);       // Set the  right combat animation.
        }
        else                                            // If it's moving...
        {
            anim.SetBool("Move", true);                  // Use moving animation.
        }
        //______________________________________________________________________________________/

        // DEATH -------------------------------------------------------------------------------
        if (life <= 0 && stage == 1)            // If life is over and it's on idle pose...
        {
            col.enabled = false;                 // disable collisior.
            death = true;                        // Turn off attacks.
            Enemy_visual.enabled = false;        // Hide sprite.
            if (deathAvail)                      // If destroying this object is available...
            {
                Destroy(this.gameObject);         // Destroy this object.
            }
        }
        //______________________________________________________________________________________/

        // COMBAT FINISHER -------------------------------------------------------------------------
        if (!info.combat && combat && !manualControl)       // If the combat have finished...
        {
            Enemy_visual.sortingOrder = 1;                   // Put this on correct visual layer.
            cop.enabled = true;                              // Enable patrol script.
            combat = false;                                  // Disable combat.
            move = true;                                     // It's moving.
            run = false;                                     // IA it's not running.
            StopAllCoroutines();                             // Stop all IA routines.
        }
        //__________________________________________________________________________________________/
    }
    //______________________________________________________________________________________________________________________________________________________________________________________________________________/

    // IA STAGE 1 --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    IEnumerator Timing()
    {
        run = true;                                                                                                                                                                             // IA it's running.

        yield return new WaitForSeconds((Random.Range(0f, timeAtk) + Random.Range(0f, timeAtk) + Random.Range(0f, timeAtk) + Random.Range(0f, timeAtk) + Random.Range(0f, timeAtk)) / 5);           // Wait for some time. . .

        afterFaint = false;                                                                                                                                                                     // Faint reaction time over.
        if (Random.Range(0, 100) < attackChance)                                                                                                                                                // If random is smaller than attackChance.
        {
            if (Random.Range(0, 100) < faintChance)                                                                                                                                              // If random is smaller than faintChance.
            {
                ready = 2;                                                                                                                                                                        // Ready to faint.
                StartCoroutine("Timing2");                                                                                                                                                        // Start IA stage 2.
            }
            else                                                                                                                                                                                 // If random is smaller than faintChance.
            {
                ready = 1;                                                                                                                                                                        // Ready to attack.
                StartCoroutine("Timing2");                                                                                                                                                        // Start IA stage 2.
            }
        }
        else                                                                                                                                                                                    // If random is bigger than attackChance.
        {
            run = false;                                                                                                                                                                         // END OF IA CYCLE //
        }

    }
    //______________________________________________________________________________________________________________________________________________________________________________________________________________________________________/

    // IA STAGE 2 ------------------------------------------------------------------------------------------------------------------------------
    IEnumerator Timing2()
    {
        deathAvail = false;                                                                 // Disable death. (prevent leaving alert behind)
        GameObject obj = Instantiate(objReady, readyPos.position, readyPos.rotation);       // Spawn combat alert.
        Animator objAnim = obj.GetComponent<Animator>();                                    // Get combat alert animator component.

        if (manualControl)                                                                  // If manual mode...
        {
            yield return new WaitForSeconds(manualTime);                                         // Wait for some time . . .
            manualTime = waitTime;
        }
        else                                                                                // If not in manual mode...
        {
            yield return new WaitForSeconds(waitTime);                                           // Wait for some time . . .
        }

        if (ready == 1)                                                                     // If it's ready to attack...
        {
            objAnim.SetInteger("status", 1);                                                 // Combat alert animation to attack.
            Attack("1");                                                                     // Attack.
            run = false;                                                                     // END OF IA CYCLE //
        }
        else                                                                                // If it's ready to faint...
        {
            objAnim.SetInteger("status", 2);                                                 // Combat alert animation to faint.
            Attack("1");                                                                     // Attack.
            faint = true;                                                                    // But faint.
            run = false;                                                                     // END OF IA CYCLE //
        }

        yield return new WaitUntil(() => stage == 1);                                           // Wait for idle animation. . .

        Destroy(obj);                                                                       // Destroy combat alert.
        deathAvail = true;                                                                  // Enable death.
    }
    //__________________________________________________________________________________________________________________________________________/

    // MANUAL CODE CONTROL ------------------------------------------
    public void Combat(string command, int value, bool aux)
    {
        if(command == "Start")                          // START MANUAL MODE ===========================
        {
            manualControl = true;
            if (deathAvail)
            {
                StopAllCoroutines();
            }
            defenseStatus = true;
            combat = true;
        }
        if (manualControl)
        {
            if (command == "Attack")                         // ATTACK COMMAND ==============================
            {
                if(value > 0)
                {
                    manualTime = value / 1000;
                }
                if (!aux)
                {
                    ready = 1;
                    StartCoroutine("Timing2");
                }
                else
                {
                    Attack("2");
                }
            }
            if (command == "Defend")                        // DEFENSE COMMAND =============================
            {
                Defend();
            }
            if (command == "Faint")                         // FAINT COMMAND ===============================
            {
                ready = 2;
                StartCoroutine("Timing2");
                if (value > 0)
                {
                    manualTime = value / 1000;
                }
            }
            if (command == "Manual Defense")                  // CHANGE DEFENSE MODE COMMAND =================
            {
                if(value == 1)
                {
                    manualDef = aux;
                    if (aux)
                    {
                        defenseStatus = false;
                    }
                }
                if (value == 2)
                {
                    manualDef = aux;
                    if (aux)
                    {
                    defenseStatus = false;
                    }
                }
            }
            if (command == "Manual Animation")
            {
                manualAnim = aux;
                if (value > 0 && value <= 4)
                {
                    stage = value;
                }
            }
            if (command == "Spawn Alert")                   // SPAWN ALERT COMMAND =========================
            {
                StartCoroutine(SpawnAlert(value));      //
            }
            if (command == "Change Aux")                     // CHANGE AUXILIAR BOOL ========================
            {
                manualAux = aux;
            }
            if (command == "Set Life")                       // CHANGE LIFE VALUE ===========================
            {
                life = value;
            }
            if (command == "Set Damage")                     // CHANGE DAMAGE VALUE =========================
            {
                damage = value;
            }
            if (command == "Quit")                           // STOP MANUAL MODE ============================
            {

                manualDef = false;
                manualAux = false;
                manualControl = false;
            }
        }
        else
        {
            Debug.Log("Você precisa ligar o modo manual antes");
        }
    }
    //___________________________________________________________________________________/

    // MANUAL SPAWN ALERT ---
    IEnumerator SpawnAlert(int mode)
    {
        if(mode > 0 && mode < 6)
        {
            GameObject obj = Instantiate(objReady, readyPos.position, readyPos.rotation);       // Spawn combat alert.
            Animator objAnim = obj.GetComponent<Animator>();                                    // Get combat alert animator component.
            if(mode == 1)
            {
                yield return new WaitUntil(() => manualAux);
                manualAux = false;
                Debug.Log("Aviso: O booleano retornou para falso.");
                Destroy(obj);
            }
            if (mode == 2)
            {
                objAnim.SetInteger("status", 1);
                yield return new WaitUntil(() => manualAux);
                manualAux = false;
                Debug.Log("Aviso: O booleano retornou para falso.");
                Destroy(obj);
            }
            if (mode == 3)
            {
                objAnim.SetInteger("status", 2);
                yield return new WaitUntil(() => manualAux);
                manualAux = false;
                Debug.Log("Aviso: O booleano retornou para falso.");
                Destroy(obj);
            }
            if (mode == 4)
            {
                yield return new WaitUntil(() => manualAux);
                manualAux = false;
                Debug.Log("Aviso: O booleano retornou para falso.");
                objAnim.SetInteger("status", 1);
                yield return new WaitUntil(() => manualAux);
                manualAux = false;
                Debug.Log("Aviso: O booleano retornou para falso.");
                Destroy(obj);
            }
            if (mode == 5)
            {
                yield return new WaitUntil(() => manualAux);
                manualAux = false;
                Debug.Log("Aviso: O booleano retornou para falso.");
                objAnim.SetInteger("status", 2);
                yield return new WaitUntil(() => manualAux);
                manualAux = false;
                Debug.Log("Aviso: O booleano retornou para falso.");
                Destroy(obj);
            }

        }
        else
        {
            Debug.Log("User Error: Modo manual SpawnAlert() precisa ter um valor inteiro entre 1-5.");
        }
    }
    //_______________________/

    // IA FAINT FALL ---------------------------------------------------------------------------------------
    public void Fainted()
    {
        if (Random.Range(0f, 100f) < faintFallChance || manualDef && defenseStatus)      // If enemy fell on faint...
        {
            StopCoroutine("Timing");                                     // Stop IA stage 1.
            if (!manualAnim)                                             // If manual animation mode off...
            {
                stage = 4;                                                // Set animation to defense.
            }
            if (!manualDef)
            {
            defenseStatus = false;                                       // Turn defese off.
            }
            StartCoroutine("ReloadDefense");                             // Start to reload defense.
            run = false;                                                 // END OF IA CYCLE
        }
    }
    //______________________________________________________________________________________________________/

    // IA CHECK AFTER FAINT --------------------------------------------------------
    IEnumerator FaintAttack()
    {
        if (manualControl)
        {
            yield return new WaitUntil(() => playerDefend || !afterFaint);
            if (afterFaint)
            {
                yield return new WaitForSeconds(0.5f);          // Wait for some time. . .

                Attack("1");                                // Attack.
            }
            else
            {
                yield return null;
            }
        }
        else
        {
        yield return new WaitForSeconds(0.5f);          // Wait for some time. . .

        Attack("1");                                // Attack.
        }
    }
    //______________________________________________________________________________/

    // IDLE ------------------------------------------------------------------------------------
    void Idle(string Event)
    {
        if (!move)                                      // If not moving...
        {
            hit = false;                                 // Turn hitted off.
            defend = false;                              // Turn defense off.
            if (!manualAnim)                             // If manual animation mode off...
            {
                stage = 1;                                // Set idle animation.
            }
            if (Event == "Defend")                       // If after defense...
            {
                defend = false;                           // Stop defending.

                if (!defended)                            // If it defended nothing...
                {
                    if (!manualDef)                        // Iff manual mode off...
                    {
                        defenseStatus = false;              // Disable defense.
                        StartCoroutine("ReloadDefense");    // Reload defense.
                    }
                }
                defended = false;                         // Reset defended.
            }
        }
    }
    //__________________________________________________________________________________________/

    // ATTACK ----------------------------------------------------------------------------------------------
    void Attack(string Event)
    {
        if (stage <= 1 && Event == "1")                         // If Idle...
        {
            if (!manualAnim)                                     // If manual animation mode off.
            {
                stage = 2;                                        // Anticipation animation.
            }
            critic = true;                                       // Enable critic damage.
        }
        else                                                    // If not ide...
        {
            if (!hit && !defend && Event == "2" && !manualAnim)  // If called by anticipation animation...
            {
                critic = false;                                   // Disable critic damage.
                if (!faint)                                       // If not fainting...
                {
                    if (!manualAnim)                                // If manual animation mode off...
                    {
                        stage = 3;                                  // attack animation.
                    }
                    if (!death)                                    // If not dead...
                    {
                        com.Hit(damage, transform.right.x);         // Inflict damage.
                    }
                }
                else                                               // If fainting...
                {
                    if (!manualAnim)                                // If manual animation mode off...
                    {
                    stage = 1;                                       // Idle animation
                    }
                    faint = false;                                  // Faint off.
                    afterFaint = true;                              // Start watching for defense
                }
            }
        }
    }
    //______________________________________________________________________________________________________/

    // DEFEND ----------------------------------------------------------------------
    void Defend()
    {
        if (defenseStatus)         // If you can defend...
        {
            defend = true;                       // Defend.
            stage = 4;                           // defend animation.
        }
    }
    //______________________________________________________________________________/

    // DEFENSE RELOAD TIMER ------------------------------------------------------------------------
    IEnumerator ReloadDefense()
    {
        yield return new WaitForSeconds(DRS);               // Wait some time. . .

        if (!manualDef)                                     // Disable automatic reload in manual.
        {
        defenseStatus = true;                                // Enable defense again.
        }
    }
    //______________________________________________________________________________________________/

    /* Faint Code ----------------------------------------------
    void Faint()
    {
        if (stage == 2)         // If in animation stage...
        {
            faint = true;        // Enable faint.
            stage = 1;           //Go back to idle animation.
        }
    }
    //__________________________________________________________*/

    // HIT ---------------------------------------------------------------------------------------------------------------------------------
    public void Hit(float hitDamage)
    {
        if (Random.Range(0, 100) < defenseChance && defenseStatus || manualDef && defenseStatus)        // If random is smaller than defense chance and this can defend...
        {
            Defend();                                                    // Defend.
            defended = true;                                             // This defended.
        }
        else                                                            // If can't defend...
        {
            if (!defend)                                                 // If it didn't defended...
            {
                hit = true;                                               // This got hitted.
                if (!manualAnim)                                           // If manual animation mode off...
                {
                    stage = 5;                                             // Hitted animation.
                }
                if (critic)                                               // If critic damage enabled...
                {
                    life -= hitDamage * 1.73f;                             // Deal critic damage.
                }
                else                                                      // If critic damage disable...
                {
                    life -= hitDamage;                                     // Deal normal damage.
                }
            }
            else                                                         // If it defended...
            {
                defended = true;                                          // Defended.
            }
        }
    }
    //______________________________________________________________________________________________________________________________________/
}
