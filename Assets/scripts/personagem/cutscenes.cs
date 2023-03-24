using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using informacoes;

public class cutscenes : MonoBehaviour
{
    bool  ramonrefeitorio = false;

    [SerializeField] charmove Charmove;
    [SerializeField] movcamera Movcamera;
    [SerializeField] Text falas;
    Combat combate;
    [SerializeField] Image fade_img;
    public bool roda_cena = true;
    [SerializeField] Animator animin;
    [SerializeField] GameObject cameraobj,interact, horda1, horda2, avsAtaque, avsAtaqueFim, avsFinca,TutoDefesa,TutoAtaque,TutoFinca, AtaqueAgora ,caixaTutorial ,caixalucas, caixaramon, caixanpc, caixapolicial, Canvafalas, inicio_prisao_trigger_1, inicio_prisao_trigger_2, inicio_prisao_trigger_3, fade;
    charinfo info;  // ------------------------ IGNYS
    public GameObject punchCollider;    //----- IGNYS
    Animator playerAnim;    //----------------- IGNYS
    int playerAnimMode, ramonInteragiu = 0; //--------------------- IGNYS
    bool endwalker = false;

    // Start is called before the first frame update
    void Start()
    {
        
        cameraobj = GameObject.Find("Main Camera");
        
        interact = GameObject.Find("interagirCanvas");
        Canvafalas = GameObject.Find("cena de falas");
        caixalucas = GameObject.Find("caixa de lucas");
        falas = GameObject.Find("caixa de fala").GetComponent<Text>();
        caixaramon = GameObject.Find("caixa de ramon");
        caixanpc = GameObject.Find("caixa de npc");
        avsAtaque = GameObject.Find("aviso de ataque");
        avsAtaque.SetActive(false);
        avsAtaqueFim = GameObject.Find("aviso de ataque true");
        avsAtaqueFim.SetActive(false);
        avsFinca = GameObject.Find("aviso de finca");
        avsFinca.SetActive(false);
        TutoDefesa = GameObject.Find("defesa");
        TutoDefesa.SetActive(false);
        TutoAtaque = GameObject.Find("ataque");
        TutoAtaque.SetActive(false);
        TutoFinca = GameObject.Find("finca");
        TutoFinca.SetActive(false);
        AtaqueAgora = GameObject.Find("ATAQUE AGORA");
        AtaqueAgora.SetActive(false);
        caixaTutorial = GameObject.Find("caixa de tutorial");
        caixaTutorial.SetActive(false);
        caixaramon.SetActive(false);
        caixapolicial = GameObject.Find("caixa de policial");
        fade = GameObject.Find("fadecutscene");
        fade_img = fade.GetComponent<Image>();
        inicio_prisao_trigger_1 = GameObject.Find("cutscene prisao inicio");
        inicio_prisao_trigger_2 = GameObject.Find("cutscene prisao inicio fim");
        inicio_prisao_trigger_3 = GameObject.Find("cutscene prisao inicio meio");
        fade_img.color = Color.clear;
        Canvafalas.SetActive(false);
        caixapolicial.SetActive(false);
        caixalucas.SetActive(false);
        fade.SetActive(false);
        caixanpc.SetActive(false);
        interact.SetActive(false);
        
        
        
        Charmove = gameObject.GetComponent<charmove>();
        Movcamera = cameraobj.GetComponent<movcamera>();
        combate = gameObject.GetComponent<Combat>();
        animin = gameObject.GetComponent<Animator>();
        info = gameObject.GetComponent<charinfo>(); //--------------------- IGNYS
        playerAnim = gameObject.GetComponent<Animator>();   //------------- IGNYS
        
    }
    private void FixedUpdate()
    {
        if(SceneManager.GetActiveScene().name == "refeitorio")
        {
            ramonrefeitorio = true;
        }
        if(SceneManager.GetActiveScene().name == "refeitorio final" && endwalker == false)
        {
            horda1 = GameObject.Find("horda1");
            horda2 = GameObject.Find("horda2");
            horda1.SetActive(false);
            horda2.SetActive(false);
            
            endwalker = true;
        }
    }

    // Update is called once per frame
    void Update()
    {if(SceneManager.GetActiveScene().name != "armazem")
        {
            playerAnimMode = playerAnim.GetInteger("Combat_Anim");
        }
       
        /*if (Input.GetKeyDown(KeyCode.P))
        {
            StopAllCoroutines();
            Charmove.enabled = true;
            Movcamera.enabled = true;
            combate.enabled = true;
            falas.enabled = false;
            Destroy(inicio_prisao_trigger_1);
            //Destroy(inicio_prisao_trigger_2);
            Destroy(inicio_prisao_trigger_3);

        }*/
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "fim_trigger")
        {
            horda1.SetActive(true);

            Destroy(GameObject.Find("fim_trigger"));


        }
        if (collision.gameObject.name == "fim_trigger_2" )
        {
            StartCoroutine(InTheEnd());
            Destroy(GameObject.Find("fim_trigger_2"));

        }
        if (collision.gameObject.name == "cutscene prisao inicio" && roda_cena == true)
        {
            combate.enabled = false;
            animin.SetBool("movimento", false);
            Charmove.enabled = false;
            Movcamera.enabled = false;
            StartCoroutine(inicioprisao());
            Canvafalas.SetActive(true);
            caixapolicial.SetActive(true);

        }
        if (collision.gameObject.name == "cutscene prisao inicio fim" && roda_cena == true)
        {
            combate.enabled = false;
            animin.SetBool("movimento", false);
            Charmove.enabled = false;
            Movcamera.enabled = false;
            StartCoroutine(inicioprisaofim());
            Canvafalas.SetActive(true);
            caixapolicial.SetActive(true);

        }
        if (collision.gameObject.name == "cutscene prisao inicio meio" && roda_cena == true)
        {
            combate.enabled = false;
            animin.SetBool("movimento", false);
            Charmove.enabled = false;
            Movcamera.enabled = false;
            fade.SetActive(true);
            Canvafalas.SetActive(true);
            StartCoroutine(inicioprisaomeio());
            

        }
        if (collision.gameObject.name == "ramon_prototipo" && roda_cena == true)
        {
            combate.enabled = false;
            animin.SetBool("movimento", false);
            Charmove.enabled = false;
            Movcamera.enabled = false;            
            Canvafalas.SetActive(true);
            StartCoroutine(Ramon());
            Collider2D ramoncorpo = GameObject.Find("ramon_prototipo").GetComponent<Collider2D>();
            ramoncorpo.enabled = false;


        }
    }
    IEnumerator inicioprisao()
    {
        falas.enabled = true;
        falas.text = "Ei seu merda, anda logo com isso porra!!!";
        yield return new WaitForSeconds(3);
        falas.text = "N�o tenho o dia todo para esperar a donzela!";
        yield return new WaitForSeconds(3);
        falas.enabled = false;
        Canvafalas.SetActive(false);
        caixapolicial.SetActive(false);
        Charmove.enabled = true;
        Movcamera.enabled = true;
        combate.enabled = true;
        Destroy(inicio_prisao_trigger_1);




    }
    
    IEnumerator inicioprisaomeio()
    {

       while(fade_img.color != Color.black)
        {
            fade_img.color = Color.Lerp(fade_img.color, Color.black, 10 * Time.deltaTime);
            yield return null;

        }
        animin.SetLayerWeight(1, 1);
        animin.SetLayerWeight(0, 0);
        
        
        while (fade_img.color != Color.clear)
        {
            fade_img.color = Color.Lerp(fade_img.color, Color.clear, 10 * Time.deltaTime);
            yield return null;
        }
        
        yield return new WaitForSeconds(0.2f);
        Charmove.enabled = true;
        Movcamera.enabled = true;
        combate.enabled = true;
        Canvafalas.SetActive(false);
        fade.SetActive(false);
        Destroy(inicio_prisao_trigger_3);

    }
    IEnumerator inicioprisaofim()
    {
        falas.enabled = true;
        falas.text = "Agora sim";
        yield return new WaitForSeconds(3);
        falas.text = "Bem vindo ao inferno Garoto!";
        yield return new WaitForSeconds(3);
        falas.enabled = false;
        Canvafalas.SetActive(false);
        caixapolicial.SetActive(false);
        Charmove.enabled = true;
        Movcamera.enabled = true;
        combate.enabled = true;
        Destroy(inicio_prisao_trigger_2);
        fade_img.color = Color.black;
        yield return new WaitUntil(()=> SceneManager.GetActiveScene().name == "cadeia principal");

       combate.enabled = false;
       animin.SetBool("movimento", false);
       Charmove.enabled = false;
       Movcamera.enabled = false;
       Canvafalas.SetActive(true);
       caixalucas.SetActive(true);
       fade.SetActive(true);

        while (fade_img.color != Color.clear)
       {
           fade_img.color = Color.Lerp(fade_img.color, Color.clear, 20 * Time.deltaTime);
            yield return null;

        }
        falas.enabled = true;
        falas.text = "Cof! Cof!";
        yield return new WaitForSeconds(3);
        falas.text = "Que lugar podre";
        yield return new WaitForSeconds(3);
        falas.text = "Sinto que vou ficar doente s� de est� aqui";
        yield return new WaitForSeconds(3);
        caixapolicial.SetActive(true);
        caixalucas.SetActive(false);
        falas.text = "Agora que voc� j� conhece a sua cela, des�a logo!";
        yield return new WaitForSeconds(3);
        falas.text = "Esta na hora do banho de sol!";
        yield return new WaitForSeconds(3);
        falas.enabled = false;
        //combate.enabled = true;
        
        Charmove.enabled = true;
        Movcamera.enabled = true;
        Canvafalas.SetActive(false);
        caixapolicial.SetActive(false);
        fade_img.color = Color.black;
        fade.SetActive(false);
        

        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == "patio cadeia");
        //-------------------------------------------------------------------------------------------------------------------------|
        charinfo desativatempo = gameObject.GetComponent<charinfo>();
        desativatempo.enabled = false;
        //-------------------------------------------------------------------------------------------------------------------------|
        animin.SetBool("movimento", false);
        Charmove.enabled = false;
        Movcamera.enabled = false;
        Canvafalas.SetActive(true);
        caixanpc.SetActive(true);
        fade.SetActive(true);
        GameObject inimigo = GameObject.Find("poli�a");
        
        Animator inimigo_animin = inimigo.GetComponent<Animator>();
        Enemy_Combat enemy = inimigo.GetComponent<Enemy_Combat>();     // Pegar componente do combate do npc.
        inimigo.GetComponent<policial>().enabled = false;
        enemy.enabled = false;
        inimigo_animin.SetBool("Move", false);

        while (fade_img.color != Color.clear)
        {
            fade_img.color = Color.Lerp(fade_img.color, Color.clear, 20 * Time.deltaTime);
            yield return null;

        }

        // Inserir falas pre tutorial a�.
        falas.text = "Olha s� isso, mais um verme chegou";
        falas.enabled = true;
        yield return new WaitForSeconds(3);
        falas.text = "Sabe qual a melhor forma de lidar com vermes?";
        yield return new WaitForSeconds(3);
        falas.text = "exterminando eles antes que se espalhem";
        yield return new WaitForSeconds(3);

        // In�cio tutorial
        //combate.enabled = true;        
        Canvafalas.SetActive(false);
        caixanpc.SetActive(false);
        fade.SetActive(false);
        // Inimigo anda at� voc� e entra posi��o de combate.
        punchCollider.SetActive(true);
        enemy.enabled = true;
        enemy.Combat("Start", 0, false);
        enemy.Combat("Set Life", 9999999, false);
        enemy.Combat("Set Damage", 47, false);
        yield return new WaitForSeconds(0.4f);
        yield return new WaitUntil(() => !enemy.move);
        yield return new WaitForSeconds(2f);

        // Voc� entra em pose de combate.
        combate.enabled = true;
        combate.input = false;
        info.combat = true;
        yield return new WaitForSeconds(0.1f);
        combate.input = false;

        yield return new WaitForSeconds(1.5f);
        Canvafalas.SetActive(true);
        // Aparece alerta de prepara��o do inimigo.
        enemy.Combat("Spawn Alert", 1, false);
        caixaTutorial.SetActive(true);
        avsAtaque.SetActive(true); 

        // O tutorial da prepara��o aparece. ==========================================
        Debug.Log("Aperte E para continuar.");
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));
        avsAtaque.SetActive(false);

        // Inimigo puxa o bra�o.
        enemy.Combat("Change Aux", 0, true);
        enemy.Combat("Attack", 1, false);
        yield return new WaitForSeconds(0.2f);
        enemy.Combat("Manual Animation", 0, true);

        // Tutorial prepara��o ataque. ==========================================
        avsAtaqueFim.SetActive(true);
        yield return new WaitForSeconds(3.5f);
        avsAtaqueFim.SetActive(false);

        // Espera voc� defender.
        TutoDefesa.SetActive(true);
        Debug.Log("Aperte Bot�o Direito Mouse para continuar.");
        combate.inputKey = 2;
        combate.input = true;
        yield return new WaitUntil(() => Input.GetButtonDown("Fire2"));
        combate.input = false;

        // Quando voc� defende ele ataca.
        enemy.Combat("Manual Animation", 0, false);
        enemy.Combat("Attack", 0, true);
        TutoDefesa.SetActive(false);
        yield return new WaitForSeconds(1f);

        // Fala(???????????)

        // Tutorial de ataque. ==========================================
        TutoAtaque.SetActive(true);
        Debug.Log("Aperte Bot�o Esquerdo Mouse para continuar.");
        enemy.Combat("Manual Defense", 0, true);
        combate.inputKey = 1;
        combate.input = true;
        yield return new WaitUntil(() => Input.GetButtonDown("Fire1"));
        combate.input = false;
        TutoAtaque.SetActive(false);
        yield return new WaitForSeconds(1f);




        // Voc� ataca.

        // Fala?

        // Quando voc� atacar novamente ele ataca antes e d� cr�tico.
        /*yield return new WaitUntil(() => Input.GetButtonDown("Fire1"));
        yield return new WaitUntil(() => Input.GetButtonDown("Fire1"));
        yield return new WaitUntil(() => playerAnimMode == 2);
        enemy.Combat("Attack", 1, false);
        yield return new WaitForSeconds(0.2f);
        enemy.Combat("Attack", 1, true);*/

        // Tutorial de cr�tico. ==========================================
        //Debug.Log("Aperte E para continuar.");
        //yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));

        // Ele usa finta.
        enemy.Combat("Faint", 0, false);
        combate.inputKey = 2;
        combate.input = true;
        yield return new WaitForSeconds(3f);
        combate.input = false;


        // Ap�s a finta voc� recebe o tutorial. ==========================================
        avsFinca.SetActive(true);
        Debug.Log("Aperte E para continuar.");
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));
        avsFinca.SetActive(false);

        // Fala easter egg caso evite o golpe?

        // Voc� recebe tutorial de como fazer finta. ==========================================
        TutoFinca.SetActive(true);
        Debug.Log("Aperte Espa�o para continuar.");
        combate.inputKey = 3;
        combate.input = true;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        combate.input = false;
        TutoFinca.SetActive(false);
        enemy.Combat("Defend", 0, false);

        // Voc� realiza finta e acerta.
        yield return new WaitForSeconds(0.3f);
        enemy.Combat("Manual Defense", 2, true);
        AtaqueAgora.SetActive(true);
        combate.inputKey = 1;
        combate.input = true;
        yield return new WaitUntil(() => Input.GetButtonDown("Fire1"));
        combate.input = false;
        combate.inputKey = 0;
        AtaqueAgora.SetActive(false);
        caixaTutorial.SetActive(false);
        falas.enabled = false;


        // Fim do tutorial. Fala?
        yield return new WaitForSeconds(1f);
        enemy.Combat("Manual Defense", 1, true);
        Debug.Log("Fim do Tutorial");

        // Hit fim.
        yield return new WaitForSeconds(1f);
        enemy.Combat("Manual Animation", 2, true);
        yield return new WaitForSeconds(0.3f);
        enemy.Combat("Manual Animation", 3, true);
        yield return new WaitForSeconds(0.2f);

        Debug.Log("FADE OUT");
        //inimigo.SetActive(false);
         fade_img.color = Color.clear;
        fade.SetActive(true);
        while (fade_img.color != Color.black)
        {
            fade_img.color = Color.Lerp(fade_img.color, Color.black, 20 * Time.deltaTime);
            yield return new WaitForSeconds(0.02f);

        }
        
        animin.SetBool("movimento", false);
        transform.position = new Vector3(5.97f, -1.34f, 0);
        transform.eulerAngles = new Vector3(0, 180, 0);
        
        SceneManager.LoadScene(5);
        animin.SetBool("Combat", false);
        Charmove.enabled = false;
        Movcamera.enabled = false;
        caixapolicial.SetActive(true);
        falas.enabled = true;
        falas.text = "Ei seu merdinha!";
        animin.SetBool("movimento", false);
        
        charinfo.dia_tempo = 110;
        while (fade_img.color != Color.clear)
        {
            fade_img.color = Color.Lerp(fade_img.color, Color.clear, 20 * Time.deltaTime);
            yield return null;

        }
        desativatempo.enabled = true;
        charinfo.fome -= 35;
        Charmove.enabled = false;
        Movcamera.enabled = false;
        yield return new WaitForSeconds(1);
        falas.text = "Se voc� j� consegue ficar de p�, j� pode voltar para a sua cela.";
        yield return new WaitForSeconds(4.5f);
        falas.text = "N�o escutou a sirene n�o?";
        yield return new WaitForSeconds(3);
        falas.text = "Ela quem indica quando ratos iguais a voc� tem que voltar para os seus ninhos!";
        yield return new WaitForSeconds(5);
        Charmove.enabled = true;
        Movcamera.enabled = true;
        combate.enabled = true;
        Canvafalas.SetActive(false);
       
    }
    IEnumerator Ramon()
    {
        if(charinfo.dia_passagem == 1 && ramonInteragiu == 0 && ramonrefeitorio == false)
        {
            caixaramon.SetActive(true);
            caixapolicial.SetActive(false);
            animin.SetBool("movimento", false);
            Charmove.enabled = false;
            Movcamera.enabled = false;
            Canvafalas.SetActive(true);
            falas.enabled = true;
            falas.text = "Voc� � o carinha de ontem...";
            yield return new WaitForSeconds(3);
            falas.text = "J� est� melhor?";
            yield return new WaitForSeconds(3);
            caixaramon.SetActive(false);
            caixalucas.SetActive(true);
            falas.text = "Desculpa, nos conhecemos?";
            yield return new WaitForSeconds(3);
            caixaramon.SetActive(true);
            caixalucas.SetActive(false);
            falas.text = "ahh...";
            yield return new WaitForSeconds(2); 
            falas.text = "Voc� n�o deve se lembrar de min, afinal, voc� estava desacordado...";
            yield return new WaitForSeconds(3);
            falas.text = "Eu sou o Ramon, o cara que lambeu sua feridas ontem.";
            yield return new WaitForSeconds(3);
            falas.text = "Digo... cuidou das sua feridas.";
            yield return new WaitForSeconds(3);
            caixaramon.SetActive(false);
            caixalucas.SetActive(true);
            falas.text = "�... valeu?";
            yield return new WaitForSeconds(3);
            falas.text = "Por sinal, eu sou o Lucas.";
            yield return new WaitForSeconds(3);
            caixaramon.SetActive(true);
            caixalucas.SetActive(false);
            falas.text = "...";
            yield return new WaitForSeconds(1.5f);
            caixaramon.SetActive(false);
            caixalucas.SetActive(true);
            yield return new WaitForSeconds(1.5f);
            falas.text = "Ent�o...eu tenho que ir agora no refeitorio ver se eu consigo comer algo.";
            yield return new WaitForSeconds(3);
            caixaramon.SetActive(true);
            caixalucas.SetActive(false);
            falas.text = "Cuidado, hehehe.";
            yield return new WaitForSeconds(3);
            falas.text = "Espero n�o ter que lamber as suas feridas t�o novamente t�o cedo";
            yield return new WaitForSeconds(3);
            caixaramon.SetActive(false);
            caixalucas.SetActive(true);
            falas.text = "Cuidado com o que?";
            yield return new WaitForSeconds(3);
            caixaramon.SetActive(true);
            caixalucas.SetActive(false);
            falas.text = "Ah, voc� descobrir�...";
            yield return new WaitForSeconds(3);
            ramonInteragiu ++;
            Charmove.enabled = true;
            Movcamera.enabled = true;
            combate.enabled = true;
            falas.enabled = false;
            caixaramon.SetActive(false);
            caixalucas.SetActive(false);
            Canvafalas.SetActive(false);
            StartCoroutine(resetRamon1());
            StopCoroutine(Ramon());
        }
        if (charinfo.dia_passagem == 1 && ramonInteragiu == 0 && ramonrefeitorio == true)
        {
            caixaramon.SetActive(true);
            caixapolicial.SetActive(false);
            animin.SetBool("movimento", false);
            Charmove.enabled = false;
            Movcamera.enabled = false;
            Canvafalas.SetActive(true);
            falas.enabled = true;
            falas.text = "Voc� � o carinha de ontem...";
            yield return new WaitForSeconds(3);
            falas.text = "J� est� melhor?";
            yield return new WaitForSeconds(3);
            caixaramon.SetActive(false);
            caixalucas.SetActive(true);
            falas.text = "Desculpa, nos conhecemos?";
            yield return new WaitForSeconds(3);
            caixaramon.SetActive(true);
            caixalucas.SetActive(false);
            falas.text = "ahh...";
            yield return new WaitForSeconds(2);
            falas.text = "Voc� n�o deve se lembrar de min, afinal, voc� estava desacordado...";
            yield return new WaitForSeconds(3);
            falas.text = "Eu sou o Ramon, o cara que lambeu sua feridas ontem.";
            yield return new WaitForSeconds(3);
            falas.text = "Digo... cuidou das sua feridas.";
            yield return new WaitForSeconds(3);
            caixaramon.SetActive(false);
            caixalucas.SetActive(true);
            falas.text = "�... valeu?";
            yield return new WaitForSeconds(3);
            falas.text = "Por sinal, eu sou o Lucas.";
            yield return new WaitForSeconds(3);
            caixaramon.SetActive(true);
            caixalucas.SetActive(false);
            falas.text = "...";
            yield return new WaitForSeconds(1.5f);
            caixaramon.SetActive(false);
            caixalucas.SetActive(true);
            yield return new WaitForSeconds(1.5f);
            caixaramon.SetActive(true);
            caixalucas.SetActive(false);
            falas.text = "Ent�o... o que veio fazer aqui?";
            yield return new WaitForSeconds(3);            
            falas.text = "Veio s� agradece a minha boa vontade em n�o te deixar morrer al�?";
            yield return new WaitForSeconds(3);
            caixaramon.SetActive(false);
            caixalucas.SetActive(true);            
            falas.text = "Sim...";
            yield return new WaitForSeconds(2);
            caixaramon.SetActive(true);
            caixalucas.SetActive(false);
            falas.text = "Tudo que eu fiz foi por beneficio pr�prio.";
            yield return new WaitForSeconds(3);
            caixaramon.SetActive(false);
            caixalucas.SetActive(true);
            falas.text = "N�o entendi.";
            yield return new WaitForSeconds(3);
            caixaramon.SetActive(true);
            caixalucas.SetActive(false);
            falas.text = "� bom ter algum tipo de divers�o por aqui.";
            yield return new WaitForSeconds(3);            
            falas.text = "E a minha nova divers�o...";
            yield return new WaitForSeconds(3);
            falas.text = "� Voc�!!!";
            yield return new WaitForSeconds(3);
            falas.text = "Acho que ser� divertido observar voc�.";
            yield return new WaitForSeconds(3);
            falas.text = "E para garantir que voc� dure o maximo de tempo para poder me entreter.";
            yield return new WaitForSeconds(3);
            falas.text = "Eu vou te ajudar um pouco, mas s� um pouquinho.";
            yield return new WaitForSeconds(3);
            falas.text = "Ent�o v� se n�o se acostuma.";
            yield return new WaitForSeconds(3);
            caixaramon.SetActive(false);
            caixalucas.SetActive(true);
            falas.text = "Certo, mas que tipo de ajuda voc� vai me dar?";
            yield return new WaitForSeconds(3);
            caixaramon.SetActive(true);
            caixalucas.SetActive(false);
            falas.text = "Porra, se quer rir, tem que fazer rir.";
            yield return new WaitForSeconds(3);
            falas.text = "Volte aqui depois que ai eu irei te mostrar.";
            yield return new WaitForSeconds(3);
            ramonInteragiu +=2;
            Charmove.enabled = true;
            Movcamera.enabled = true;
            combate.enabled = true;
            falas.enabled = false;
            caixaramon.SetActive(false);
            caixalucas.SetActive(false);
            Canvafalas.SetActive(false);
            StartCoroutine(resetRamon1());
            StopCoroutine(Ramon());
        }
        if (charinfo.dia_passagem == 1  && ramonrefeitorio == true && ramonInteragiu == 1)
        {
            caixapolicial.SetActive(false);
            caixaramon.SetActive(true);
            animin.SetBool("movimento", false);
            Charmove.enabled = false;
            Movcamera.enabled = false;
            Canvafalas.SetActive(true);
            falas.enabled = true;
            falas.text = "Espero que tenha conseguido o que queria.";
            yield return new WaitForSeconds(3);            
            caixaramon.SetActive(false);
            caixalucas.SetActive(true);
            falas.text = "Voc� sabia que tinha uma gangue dominando o refeitorio?";
            yield return new WaitForSeconds(3);
            caixaramon.SetActive(true);
            caixalucas.SetActive(false);
            falas.text = "sim";
            yield return new WaitForSeconds(1.5f);
            caixaramon.SetActive(false);
            caixalucas.SetActive(true);
            falas.text = "Por que n�o me avisou antes.";
            yield return new WaitForSeconds(3);
            caixaramon.SetActive(true);
            caixalucas.SetActive(false);
            falas.text = "Informa��o custa caro, e al�m do mais, qual seria a gra�a se eu tivesse avisado antes";
            yield return new WaitForSeconds(3);
            caixaramon.SetActive(false);
            caixalucas.SetActive(true);
            falas.text = "Certo...";
            yield return new WaitForSeconds(1.5f);
            caixaramon.SetActive(false);
            caixalucas.SetActive(true);
            falas.text = "E como voc�s fazem para comer por aqui?";
            yield return new WaitForSeconds(3);
            caixaramon.SetActive(true);
            caixalucas.SetActive(false);
            falas.text = "Bem... alguns fazem acordos e acabam se juntando a essa gangue, outros invadem em busca de restos";
            yield return new WaitForSeconds(4);            
            falas.text = "E tem pessoas iguais a mim, que comem o que a pris�o oferece.";
            yield return new WaitForSeconds(3);
            caixaramon.SetActive(false);
            caixalucas.SetActive(true);
            falas.text = "O que a pris�o oferece?";
            yield return new WaitForSeconds(3);
            caixaramon.SetActive(true);
            caixalucas.SetActive(false);
            falas.text = "Sim, baratas, ratos, inse-";
            yield return new WaitForSeconds(2);
            caixaramon.SetActive(false);
            caixalucas.SetActive(true);
            falas.text = "Chega, eu j� entendi.";
            yield return new WaitForSeconds(3);            
            falas.text = "Suas iguarias n�o se encaixam no meu cardapio n�o.";
            yield return new WaitForSeconds(3);
            falas.text = "Prefiro continuar me arriscando com o refeitorio.";
            yield return new WaitForSeconds(3);
            caixaramon.SetActive(true);
            caixalucas.SetActive(false);
            falas.text = "Voc� tem coragem garoto, ou loucura...";
            yield return new WaitForSeconds(3);
            falas.text = "Ainda n�o decidi ainda.";
            yield return new WaitForSeconds(3);
            falas.text = "Mas eu aprecio isso que voc� tem.";
            yield return new WaitForSeconds(3);
            falas.text = "Aprecio tanto que eu decidir que irei te ajudar.";
            yield return new WaitForSeconds(3);
            falas.text = "Quero ver o quanto voc� sobrevive.";
            yield return new WaitForSeconds(3);
            falas.text = "Venha aqui uma vez por dia, eu irei te dar um negocio interessante.";
            yield return new WaitForSeconds(3);
            falas.text = "Mas s� vou te dar 1 por dia viu, depois que voc� pegar uma vez, n�o adianta implorar.";
            yield return new WaitForSeconds(3);
            caixaramon.SetActive(false);
            caixalucas.SetActive(true);
            falas.text = "Certo... Eu agrade�o.";
            yield return new WaitForSeconds(3);
            caixaramon.SetActive(true);
            caixalucas.SetActive(false);
            falas.text = "Eu que agrade�o, vai ser divertido ver o quanto voc� dura";
            yield return new WaitForSeconds(3);
            ramonInteragiu ++;
            Charmove.enabled = true;
            Movcamera.enabled = true;
            combate.enabled = true;
            falas.enabled = false;
            caixaramon.SetActive(false);
            caixalucas.SetActive(false);
            Canvafalas.SetActive(false);
            StartCoroutine(resetRamon1());
            StopCoroutine(Ramon());

        }
        if(charinfo.dia_passagem == 1 && ramonInteragiu >= 2)
        {
            caixapolicial.SetActive(false);
            caixaramon.SetActive(true);
            animin.SetBool("movimento", false);
            Charmove.enabled = false;
            Movcamera.enabled = false;
            Canvafalas.SetActive(true);
            falas.enabled = true;
            falas.text = "Eu ainda estou preparando as coisas.";
            yield return new WaitForSeconds(3);
            falas.text = "Volte amanh�.";
            yield return new WaitForSeconds(3);
            Charmove.enabled = true;
            Movcamera.enabled = true;
            combate.enabled = true;
            falas.enabled = false;
            caixaramon.SetActive(false);
            caixalucas.SetActive(false);
            Canvafalas.SetActive(false);
            StartCoroutine(resetRamon1());
            StopCoroutine(Ramon());
        }
        if (charinfo.dia_passagem == 2 && ramonInteragiu == 0)
        {
            caixapolicial.SetActive(false);
            caixaramon.SetActive(true);
            animin.SetBool("movimento", false);
            Charmove.enabled = false;
            Movcamera.enabled = false;
            Canvafalas.SetActive(true);
            falas.enabled = true;
            falas.text = "Aqui oh, um negocio novo que eu fiz.";
            yield return new WaitForSeconds(3);
            falas.text = "Pega aquele rem�dio al� no armario de rem�dios, vai ser logo o primeiro.";
            yield return new WaitForSeconds(3);
            caixaramon.SetActive(false);
            caixalucas.SetActive(true);
            falas.text = "E esse rem�dio faz o que?";
            yield return new WaitForSeconds(3);
            caixaramon.SetActive(true);
            caixalucas.SetActive(false);
            falas.text = "Ele melhora o seu sistema imunologico quando voc� toma.";
            yield return new WaitForSeconds(3);
            falas.text = "Teoricamente...";
            yield return new WaitForSeconds(3);
            falas.text = "Agora que voc� j� pegou o que tinha que pegar...";
            yield return new WaitForSeconds(3);
            falas.text = "Vaza, s� venha aqui agora amanh�.";
            yield return new WaitForSeconds(3);
            Charmove.enabled = true;
            Movcamera.enabled = true;
            combate.enabled = true;
            falas.enabled = false;
            caixaramon.SetActive(false);
            caixalucas.SetActive(false);
            Canvafalas.SetActive(false);
            ramonInteragiu++;
            StartCoroutine(resetRamon2());
            StopCoroutine(Ramon());
        }
        if (charinfo.dia_passagem == 3 && ramonInteragiu == 0)
        {
            caixapolicial.SetActive(false);
            caixaramon.SetActive(true);
            animin.SetBool("movimento", false);
            Charmove.enabled = false;
            Movcamera.enabled = false;
            Canvafalas.SetActive(true);
            falas.enabled = true;
            falas.text = "O rem�dio est� pronto e j� est� no armario de rem�dios.";
            yield return new WaitForSeconds(3);
            falas.text = "Pega e vaza.";
            yield return new WaitForSeconds(3);
            
            Charmove.enabled = true;
            Movcamera.enabled = true;
            combate.enabled = true;
            falas.enabled = false;
            caixaramon.SetActive(false);
            caixalucas.SetActive(false);
            Canvafalas.SetActive(false);
            ramonInteragiu++;
            StartCoroutine(resetRamon3());
            StopCoroutine(Ramon());
        }
        if (charinfo.dia_passagem == 4 && ramonInteragiu == 0)
        {
            caixapolicial.SetActive(false);
            caixaramon.SetActive(true);
            animin.SetBool("movimento", false);
            Charmove.enabled = false;
            Movcamera.enabled = false;
            Canvafalas.SetActive(true);
            falas.enabled = true;
            falas.text = "O rem�dio est� pronto e j� est� no armario de rem�dios.";
            yield return new WaitForSeconds(3);
            falas.text = "Pega e vaza.";
            yield return new WaitForSeconds(3);

            Charmove.enabled = true;
            Movcamera.enabled = true;
            combate.enabled = true;
            falas.enabled = false;
            caixaramon.SetActive(false);
            caixalucas.SetActive(false);
            Canvafalas.SetActive(false);
            ramonInteragiu++;
            StartCoroutine(resetRamon4());
            StopCoroutine(Ramon());
        }
        if (charinfo.dia_passagem == 5 && ramonInteragiu == 0)
        {
            caixapolicial.SetActive(false);
            caixaramon.SetActive(true);
            animin.SetBool("movimento", false);
            Charmove.enabled = false;
            Movcamera.enabled = false;
            Canvafalas.SetActive(true);
            falas.enabled = true;
            falas.text = "O rem�dio est� pronto e j� est� no armario de rem�dios.";
            yield return new WaitForSeconds(3);
            falas.text = "Pega e vaza.";
            yield return new WaitForSeconds(3);

            Charmove.enabled = true;
            Movcamera.enabled = true;
            combate.enabled = true;
            falas.enabled = false;
            caixaramon.SetActive(false);
            caixalucas.SetActive(false);
            Canvafalas.SetActive(false);
            ramonInteragiu++;            
            StopCoroutine(Ramon());
        }
        Charmove.enabled = true;
        Movcamera.enabled = true;
        combate.enabled = true;
        falas.enabled = false;
        caixaramon.SetActive(false);
        caixalucas.SetActive(false);
        Canvafalas.SetActive(false);
        yield return null;
        
    }
    IEnumerator resetRamon1()
    {
        yield return new WaitUntil(()=> charinfo.dia_passagem == 2);
        ramonInteragiu = 0;
    }
    IEnumerator resetRamon2()
    {
        yield return new WaitUntil(() => charinfo.dia_passagem == 3);
        ramonInteragiu = 0;
    }
    IEnumerator resetRamon3()
    {
        yield return new WaitUntil(() => charinfo.dia_passagem == 4);
        ramonInteragiu = 0;
    }
    IEnumerator resetRamon4()
    {
        yield return new WaitUntil(() => charinfo.dia_passagem == 5);
        ramonInteragiu = 0;
    }
    IEnumerator InTheEnd()
    {

        caixapolicial.SetActive(false);
        falas.enabled = false;
        animin.SetBool("movimento", false);
        Charmove.enabled = false;
        Movcamera.enabled = false;
        Canvafalas.SetActive(true);
        fade.SetActive(true);
        fade_img.color = Color.clear;
        
        cameraobj.transform.position = new Vector3(cameraobj.transform.position.x, -1.63f, cameraobj.transform.position.z);
        while (cameraobj.transform.position.x != 32.21f)
        {
            cameraobj.transform.position = Vector3.MoveTowards(cameraobj.transform.position, new Vector3(32.21f, cameraobj.transform.position.y, cameraobj.transform.position.z), 10 * Time.deltaTime);
                yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        horda2.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        while (cameraobj.transform.position.x != 11.37f)
        {
            cameraobj.transform.position = Vector3.MoveTowards(cameraobj.transform.position, new Vector3(11.37f, cameraobj.transform.position.y, cameraobj.transform.position.z), 6 * Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(1.5f);
        while (cameraobj.transform.position.x != 21.5f)
        {
            cameraobj.transform.position = Vector3.MoveTowards(cameraobj.transform.position, new Vector3(21.5f, cameraobj.transform.position.y, cameraobj.transform.position.z), 6 * Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        Camera cam_controle = cameraobj.GetComponent<Camera>();
        while (fade_img.color != Color.black)
        {
            fade_img.color = Color.Lerp(fade_img.color, Color.black, 20 * Time.deltaTime);
            cam_controle.orthographicSize -= 0.01f;
            yield return null;

        }
        GameObject indestrutivel = GameObject.Find("indestrutiveis");
        SceneManager.MoveGameObjectToScene(indestrutivel, SceneManager.GetActiveScene());
        SceneManager.LoadScene(11);




    }
}

/* 
==========================|  CONTROLE MODO MANUAL:  |==========================

          Uso:
                 enemy.Combat(String, Int, Bool)

   String = Comando.      (Obrigat�rio)
   Int = Inteiro auxiliar.
   Bool = Boleano auxiliar.

 = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
                                     Comandos
 - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

 "Attack" - Ataca o inimigo de forma padr�o.
      (Bool = Pular a anima��o de puxar o bra�o) --- Opcional (Int = Tempo do ataque em m�lissegundos)--- Opcional
 "Defend" - Defende no momento atual de forma padr�o.
      ---Sem auxiliar---
 "Faint" - Realiza uma finta.
      (Bool = N�o atacar depois do faint) --- Opcional (Int = Tempo do finta em m�lissegundos)--- Opcional
 "Manual Defense" - Desliga a IA de defesa e ativa a forma manual.
      {
          Int 1 = Defende todos os golpes Ignorando recarga de defesa.
          Int 2 = N�o defende nenhum golpe.
      }
      {Bool = ligado ou desligado}
 "Manual Animation" - Altera a forma como a anima��o funciona. A anima��o apenas muda com este comando.
      {Bool = ligado ou desligado}    (Int 1-4 = est�gio da anima��o)--- Opcional
 "Spawn Alert" - Spawna o alerta de combate apenas. Usa "Change Aux" para avan�ar.
      {
          Int 1 = Spawna o alerta de prepara��o e � deletado ao avan�ar;
          Int 2 = Spawna o alerta de ataque apenas e � deletado ao avan�ar;
          Int 3 = Spawna o alerta de finta apenas e � deletado ao avan�ar;
          Int 4 = Spawna o alerta de prepara��o, avan�a para o de ataque e depois � deletado;
          Int 5 = Spawna o alerta de prepara��o, avan�a para o de finta e depois � deletado;
      }
 "Change Aux" - Altera um valor booleano no c�digo de combate. (Caso um c�digo use ele como trigger, automaticamente voltar� a ser falso ap�s ser usado)
      {Bool = ligado ou desligado.}
 "Set Life" - Altera o valor de vida. (Padr�o 100)
      {Int = Novo valor da vida}
 "Set Damage" - Altera o valor de dano. (Padr�o 20)
      {Int = Novo valor da vida}
 "Start" - Liga o modo manual. (OBRIGAT�RIO PARA USAR OS OUTROS)
      ---Sem auxiliar---
 "Quit" - Finaliza o modo manual. (Desfaz as altera��es exceto de vida e dano)
      ---Sem auxiliar---


    --- Comandos sem necessidade de auxiliares use Int = 0 e Bool = false ---
= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
*/