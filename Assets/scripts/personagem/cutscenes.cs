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
        falas.text = "Não tenho o dia todo para esperar a donzela!";
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
        falas.text = "Sinto que vou ficar doente só de está aqui";
        yield return new WaitForSeconds(3);
        caixapolicial.SetActive(true);
        caixalucas.SetActive(false);
        falas.text = "Agora que você já conhece a sua cela, desça logo!";
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
        GameObject inimigo = GameObject.Find("poliça");
        
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

        // Inserir falas pre tutorial aí.
        falas.text = "Olha só isso, mais um verme chegou";
        falas.enabled = true;
        yield return new WaitForSeconds(3);
        falas.text = "Sabe qual a melhor forma de lidar com vermes?";
        yield return new WaitForSeconds(3);
        falas.text = "exterminando eles antes que se espalhem";
        yield return new WaitForSeconds(3);

        // Início tutorial
        //combate.enabled = true;        
        Canvafalas.SetActive(false);
        caixanpc.SetActive(false);
        fade.SetActive(false);
        // Inimigo anda até você e entra posição de combate.
        punchCollider.SetActive(true);
        enemy.enabled = true;
        enemy.Combat("Start", 0, false);
        enemy.Combat("Set Life", 9999999, false);
        enemy.Combat("Set Damage", 47, false);
        yield return new WaitForSeconds(0.4f);
        yield return new WaitUntil(() => !enemy.move);
        yield return new WaitForSeconds(2f);

        // Você entra em pose de combate.
        combate.enabled = true;
        combate.input = false;
        info.combat = true;
        yield return new WaitForSeconds(0.1f);
        combate.input = false;

        yield return new WaitForSeconds(1.5f);
        Canvafalas.SetActive(true);
        // Aparece alerta de preparação do inimigo.
        enemy.Combat("Spawn Alert", 1, false);
        caixaTutorial.SetActive(true);
        avsAtaque.SetActive(true); 

        // O tutorial da preparação aparece. ==========================================
        Debug.Log("Aperte E para continuar.");
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));
        avsAtaque.SetActive(false);

        // Inimigo puxa o braço.
        enemy.Combat("Change Aux", 0, true);
        enemy.Combat("Attack", 1, false);
        yield return new WaitForSeconds(0.2f);
        enemy.Combat("Manual Animation", 0, true);

        // Tutorial preparação ataque. ==========================================
        avsAtaqueFim.SetActive(true);
        yield return new WaitForSeconds(3.5f);
        avsAtaqueFim.SetActive(false);

        // Espera você defender.
        TutoDefesa.SetActive(true);
        Debug.Log("Aperte Botão Direito Mouse para continuar.");
        combate.inputKey = 2;
        combate.input = true;
        yield return new WaitUntil(() => Input.GetButtonDown("Fire2"));
        combate.input = false;

        // Quando você defende ele ataca.
        enemy.Combat("Manual Animation", 0, false);
        enemy.Combat("Attack", 0, true);
        TutoDefesa.SetActive(false);
        yield return new WaitForSeconds(1f);

        // Fala(???????????)

        // Tutorial de ataque. ==========================================
        TutoAtaque.SetActive(true);
        Debug.Log("Aperte Botão Esquerdo Mouse para continuar.");
        enemy.Combat("Manual Defense", 0, true);
        combate.inputKey = 1;
        combate.input = true;
        yield return new WaitUntil(() => Input.GetButtonDown("Fire1"));
        combate.input = false;
        TutoAtaque.SetActive(false);
        yield return new WaitForSeconds(1f);




        // Você ataca.

        // Fala?

        // Quando você atacar novamente ele ataca antes e dá crítico.
        /*yield return new WaitUntil(() => Input.GetButtonDown("Fire1"));
        yield return new WaitUntil(() => Input.GetButtonDown("Fire1"));
        yield return new WaitUntil(() => playerAnimMode == 2);
        enemy.Combat("Attack", 1, false);
        yield return new WaitForSeconds(0.2f);
        enemy.Combat("Attack", 1, true);*/

        // Tutorial de crítico. ==========================================
        //Debug.Log("Aperte E para continuar.");
        //yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));

        // Ele usa finta.
        enemy.Combat("Faint", 0, false);
        combate.inputKey = 2;
        combate.input = true;
        yield return new WaitForSeconds(3f);
        combate.input = false;


        // Após a finta você recebe o tutorial. ==========================================
        avsFinca.SetActive(true);
        Debug.Log("Aperte E para continuar.");
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));
        avsFinca.SetActive(false);

        // Fala easter egg caso evite o golpe?

        // Você recebe tutorial de como fazer finta. ==========================================
        TutoFinca.SetActive(true);
        Debug.Log("Aperte Espaço para continuar.");
        combate.inputKey = 3;
        combate.input = true;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        combate.input = false;
        TutoFinca.SetActive(false);
        enemy.Combat("Defend", 0, false);

        // Você realiza finta e acerta.
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
        falas.text = "Se você já consegue ficar de pé, já pode voltar para a sua cela.";
        yield return new WaitForSeconds(4.5f);
        falas.text = "Não escutou a sirene não?";
        yield return new WaitForSeconds(3);
        falas.text = "Ela quem indica quando ratos iguais a você tem que voltar para os seus ninhos!";
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
            falas.text = "Você é o carinha de ontem...";
            yield return new WaitForSeconds(3);
            falas.text = "Já está melhor?";
            yield return new WaitForSeconds(3);
            caixaramon.SetActive(false);
            caixalucas.SetActive(true);
            falas.text = "Desculpa, nos conhecemos?";
            yield return new WaitForSeconds(3);
            caixaramon.SetActive(true);
            caixalucas.SetActive(false);
            falas.text = "ahh...";
            yield return new WaitForSeconds(2); 
            falas.text = "Você não deve se lembrar de min, afinal, você estava desacordado...";
            yield return new WaitForSeconds(3);
            falas.text = "Eu sou o Ramon, o cara que lambeu sua feridas ontem.";
            yield return new WaitForSeconds(3);
            falas.text = "Digo... cuidou das sua feridas.";
            yield return new WaitForSeconds(3);
            caixaramon.SetActive(false);
            caixalucas.SetActive(true);
            falas.text = "É... valeu?";
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
            falas.text = "Então...eu tenho que ir agora no refeitorio ver se eu consigo comer algo.";
            yield return new WaitForSeconds(3);
            caixaramon.SetActive(true);
            caixalucas.SetActive(false);
            falas.text = "Cuidado, hehehe.";
            yield return new WaitForSeconds(3);
            falas.text = "Espero não ter que lamber as suas feridas tão novamente tão cedo";
            yield return new WaitForSeconds(3);
            caixaramon.SetActive(false);
            caixalucas.SetActive(true);
            falas.text = "Cuidado com o que?";
            yield return new WaitForSeconds(3);
            caixaramon.SetActive(true);
            caixalucas.SetActive(false);
            falas.text = "Ah, você descobrirá...";
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
            falas.text = "Você é o carinha de ontem...";
            yield return new WaitForSeconds(3);
            falas.text = "Já está melhor?";
            yield return new WaitForSeconds(3);
            caixaramon.SetActive(false);
            caixalucas.SetActive(true);
            falas.text = "Desculpa, nos conhecemos?";
            yield return new WaitForSeconds(3);
            caixaramon.SetActive(true);
            caixalucas.SetActive(false);
            falas.text = "ahh...";
            yield return new WaitForSeconds(2);
            falas.text = "Você não deve se lembrar de min, afinal, você estava desacordado...";
            yield return new WaitForSeconds(3);
            falas.text = "Eu sou o Ramon, o cara que lambeu sua feridas ontem.";
            yield return new WaitForSeconds(3);
            falas.text = "Digo... cuidou das sua feridas.";
            yield return new WaitForSeconds(3);
            caixaramon.SetActive(false);
            caixalucas.SetActive(true);
            falas.text = "É... valeu?";
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
            falas.text = "Então... o que veio fazer aqui?";
            yield return new WaitForSeconds(3);            
            falas.text = "Veio só agradece a minha boa vontade em não te deixar morrer alí?";
            yield return new WaitForSeconds(3);
            caixaramon.SetActive(false);
            caixalucas.SetActive(true);            
            falas.text = "Sim...";
            yield return new WaitForSeconds(2);
            caixaramon.SetActive(true);
            caixalucas.SetActive(false);
            falas.text = "Tudo que eu fiz foi por beneficio próprio.";
            yield return new WaitForSeconds(3);
            caixaramon.SetActive(false);
            caixalucas.SetActive(true);
            falas.text = "Não entendi.";
            yield return new WaitForSeconds(3);
            caixaramon.SetActive(true);
            caixalucas.SetActive(false);
            falas.text = "É bom ter algum tipo de diversão por aqui.";
            yield return new WaitForSeconds(3);            
            falas.text = "E a minha nova diversão...";
            yield return new WaitForSeconds(3);
            falas.text = "É Você!!!";
            yield return new WaitForSeconds(3);
            falas.text = "Acho que será divertido observar você.";
            yield return new WaitForSeconds(3);
            falas.text = "E para garantir que você dure o maximo de tempo para poder me entreter.";
            yield return new WaitForSeconds(3);
            falas.text = "Eu vou te ajudar um pouco, mas só um pouquinho.";
            yield return new WaitForSeconds(3);
            falas.text = "Então vê se não se acostuma.";
            yield return new WaitForSeconds(3);
            caixaramon.SetActive(false);
            caixalucas.SetActive(true);
            falas.text = "Certo, mas que tipo de ajuda você vai me dar?";
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
            falas.text = "Você sabia que tinha uma gangue dominando o refeitorio?";
            yield return new WaitForSeconds(3);
            caixaramon.SetActive(true);
            caixalucas.SetActive(false);
            falas.text = "sim";
            yield return new WaitForSeconds(1.5f);
            caixaramon.SetActive(false);
            caixalucas.SetActive(true);
            falas.text = "Por que não me avisou antes.";
            yield return new WaitForSeconds(3);
            caixaramon.SetActive(true);
            caixalucas.SetActive(false);
            falas.text = "Informação custa caro, e além do mais, qual seria a graça se eu tivesse avisado antes";
            yield return new WaitForSeconds(3);
            caixaramon.SetActive(false);
            caixalucas.SetActive(true);
            falas.text = "Certo...";
            yield return new WaitForSeconds(1.5f);
            caixaramon.SetActive(false);
            caixalucas.SetActive(true);
            falas.text = "E como vocês fazem para comer por aqui?";
            yield return new WaitForSeconds(3);
            caixaramon.SetActive(true);
            caixalucas.SetActive(false);
            falas.text = "Bem... alguns fazem acordos e acabam se juntando a essa gangue, outros invadem em busca de restos";
            yield return new WaitForSeconds(4);            
            falas.text = "E tem pessoas iguais a mim, que comem o que a prisão oferece.";
            yield return new WaitForSeconds(3);
            caixaramon.SetActive(false);
            caixalucas.SetActive(true);
            falas.text = "O que a prisão oferece?";
            yield return new WaitForSeconds(3);
            caixaramon.SetActive(true);
            caixalucas.SetActive(false);
            falas.text = "Sim, baratas, ratos, inse-";
            yield return new WaitForSeconds(2);
            caixaramon.SetActive(false);
            caixalucas.SetActive(true);
            falas.text = "Chega, eu já entendi.";
            yield return new WaitForSeconds(3);            
            falas.text = "Suas iguarias não se encaixam no meu cardapio não.";
            yield return new WaitForSeconds(3);
            falas.text = "Prefiro continuar me arriscando com o refeitorio.";
            yield return new WaitForSeconds(3);
            caixaramon.SetActive(true);
            caixalucas.SetActive(false);
            falas.text = "Você tem coragem garoto, ou loucura...";
            yield return new WaitForSeconds(3);
            falas.text = "Ainda não decidi ainda.";
            yield return new WaitForSeconds(3);
            falas.text = "Mas eu aprecio isso que você tem.";
            yield return new WaitForSeconds(3);
            falas.text = "Aprecio tanto que eu decidir que irei te ajudar.";
            yield return new WaitForSeconds(3);
            falas.text = "Quero ver o quanto você sobrevive.";
            yield return new WaitForSeconds(3);
            falas.text = "Venha aqui uma vez por dia, eu irei te dar um negocio interessante.";
            yield return new WaitForSeconds(3);
            falas.text = "Mas só vou te dar 1 por dia viu, depois que você pegar uma vez, não adianta implorar.";
            yield return new WaitForSeconds(3);
            caixaramon.SetActive(false);
            caixalucas.SetActive(true);
            falas.text = "Certo... Eu agradeço.";
            yield return new WaitForSeconds(3);
            caixaramon.SetActive(true);
            caixalucas.SetActive(false);
            falas.text = "Eu que agradeço, vai ser divertido ver o quanto você dura";
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
            falas.text = "Volte amanhã.";
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
            falas.text = "Pega aquele remédio alí no armario de remédios, vai ser logo o primeiro.";
            yield return new WaitForSeconds(3);
            caixaramon.SetActive(false);
            caixalucas.SetActive(true);
            falas.text = "E esse remédio faz o que?";
            yield return new WaitForSeconds(3);
            caixaramon.SetActive(true);
            caixalucas.SetActive(false);
            falas.text = "Ele melhora o seu sistema imunologico quando você toma.";
            yield return new WaitForSeconds(3);
            falas.text = "Teoricamente...";
            yield return new WaitForSeconds(3);
            falas.text = "Agora que você já pegou o que tinha que pegar...";
            yield return new WaitForSeconds(3);
            falas.text = "Vaza, só venha aqui agora amanhã.";
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
            falas.text = "O remédio está pronto e já está no armario de remédios.";
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
            falas.text = "O remédio está pronto e já está no armario de remédios.";
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
            falas.text = "O remédio está pronto e já está no armario de remédios.";
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

   String = Comando.      (Obrigatório)
   Int = Inteiro auxiliar.
   Bool = Boleano auxiliar.

 = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
                                     Comandos
 - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

 "Attack" - Ataca o inimigo de forma padrão.
      (Bool = Pular a animação de puxar o braço) --- Opcional (Int = Tempo do ataque em mílissegundos)--- Opcional
 "Defend" - Defende no momento atual de forma padrão.
      ---Sem auxiliar---
 "Faint" - Realiza uma finta.
      (Bool = Não atacar depois do faint) --- Opcional (Int = Tempo do finta em mílissegundos)--- Opcional
 "Manual Defense" - Desliga a IA de defesa e ativa a forma manual.
      {
          Int 1 = Defende todos os golpes Ignorando recarga de defesa.
          Int 2 = Não defende nenhum golpe.
      }
      {Bool = ligado ou desligado}
 "Manual Animation" - Altera a forma como a animação funciona. A animação apenas muda com este comando.
      {Bool = ligado ou desligado}    (Int 1-4 = estágio da animação)--- Opcional
 "Spawn Alert" - Spawna o alerta de combate apenas. Usa "Change Aux" para avançar.
      {
          Int 1 = Spawna o alerta de preparação e é deletado ao avançar;
          Int 2 = Spawna o alerta de ataque apenas e é deletado ao avançar;
          Int 3 = Spawna o alerta de finta apenas e é deletado ao avançar;
          Int 4 = Spawna o alerta de preparação, avança para o de ataque e depois é deletado;
          Int 5 = Spawna o alerta de preparação, avança para o de finta e depois é deletado;
      }
 "Change Aux" - Altera um valor booleano no código de combate. (Caso um código use ele como trigger, automaticamente voltará a ser falso após ser usado)
      {Bool = ligado ou desligado.}
 "Set Life" - Altera o valor de vida. (Padrão 100)
      {Int = Novo valor da vida}
 "Set Damage" - Altera o valor de dano. (Padrão 20)
      {Int = Novo valor da vida}
 "Start" - Liga o modo manual. (OBRIGATÓRIO PARA USAR OS OUTROS)
      ---Sem auxiliar---
 "Quit" - Finaliza o modo manual. (Desfaz as alterações exceto de vida e dano)
      ---Sem auxiliar---


    --- Comandos sem necessidade de auxiliares use Int = 0 e Bool = false ---
= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
*/