using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class cena_delegacia : MonoBehaviour
{
    Camera cam_view;
    GameObject posicao;
    Image fade_img;
    GameObject cena_1, cena_2, cena_3_1, cena_3_2, caixa_policia, caixa_lucas, caixa_gabriel, texto_obj;
    Text texto_txt;
    bool cutscene_iniciou = false;
    // Start is called before the first frame update
    void Start()
    {
        cena_1 = GameObject.Find("cenario delegacia");
        cena_2 = GameObject.Find("CUTSCENE 1 PRONTO");
        cena_3_1 = GameObject.Find("CUTSCENE 2 FACES NORMAIS");
        cena_3_2 = GameObject.Find("CUTSCENE 2 FACES TRISTES");
        caixa_lucas = GameObject.Find("lucas_caixa");
        caixa_policia = GameObject.Find("policia_caixa");
        texto_obj = GameObject.Find("caixa de fala");
        caixa_gabriel = GameObject.Find("gabriel_caixa");
        texto_txt = texto_obj.GetComponent<Text>();
        caixa_lucas.SetActive(false);
        caixa_policia.SetActive(false);
        caixa_gabriel.SetActive(false);
        cena_2.SetActive(false);
        cena_3_1.SetActive(false);
        cena_3_2.SetActive(false);
        texto_obj.SetActive(false);
        fade_img = GameObject.Find("fade").GetComponent<Image>();
        
        posicao = GameObject.Find("local");
        cam_view = gameObject.GetComponent<Camera>();
        StartCoroutine("cutscene_inicio");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && cutscene_iniciou == true)
        {
            SceneManager.LoadScene(7);
        }
    }
    IEnumerator cutscene_inicio()
    {
        while (fade_img.color != Color.clear)
        {
            fade_img.color = Color.Lerp(fade_img.color, Color.clear, 20 * Time.deltaTime);
            yield return new WaitForSeconds(0.01f);

        }
        yield return new WaitForSeconds(1);
        cutscene_iniciou = true;


        while (  gameObject.transform.position != posicao.transform.position || cam_view.orthographicSize > 1.5f)
        {if(cam_view.orthographicSize > 1.5f)
            {
                cam_view.orthographicSize -= 0.005f;
            }
        if(gameObject.transform.position != posicao.transform.position)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(posicao.transform.position.x, posicao.transform.position.y, -10), 4 * Time.deltaTime);
            }
            
            
            yield return new WaitForSeconds(0.002f);
        }
        while (fade_img.color != Color.black)
        {
            fade_img.color = Color.Lerp(fade_img.color, Color.black, 20 * Time.deltaTime);
            yield return new WaitForSeconds(0.01f);

        }
        cena_2.SetActive(true);
        cena_1.SetActive(false);
        transform.position = new Vector3(0, 0, -10);
        cam_view.orthographicSize = 5;

        while (fade_img.color != Color.clear)
        {
            fade_img.color = Color.Lerp(fade_img.color, Color.clear, 20 * Time.deltaTime);
            yield return new WaitForSeconds(0.01f);

        }
        caixa_policia.SetActive(true);        
        texto_obj.SetActive(true);
        texto_txt.text = "Seu nome � Lucas, certo?";
        yield return new WaitForSeconds(4);
        caixa_policia.SetActive(false);
        caixa_lucas.SetActive(true);
        texto_txt.text = "* Sons de choro *";
        yield return new WaitForSeconds(4);
        caixa_policia.SetActive(true);
        caixa_lucas.SetActive(false);
        texto_txt.text = "Jovem, para facilitar o meu trabalho, eu preciso que voc� colabore.";
        yield return new WaitForSeconds(4);
        caixa_policia.SetActive(false);
        caixa_lucas.SetActive(true);
        texto_txt.text = "* Sons de choro *";
        yield return new WaitForSeconds(4);
        caixa_policia.SetActive(true);
        caixa_lucas.SetActive(false);
        texto_txt.text = "Bem� Se voc� n�o vai falar, vamos aos fatos...";
        yield return new WaitForSeconds(4);
        texto_txt.text = "Lucas, voc� foi pego invadindo e furtando um mercado.";
        yield return new WaitForSeconds(4);
        caixa_policia.SetActive(false);
        caixa_lucas.SetActive(true);
        texto_txt.text = "N�o� Eu�";
        yield return new WaitForSeconds(4);
        caixa_policia.SetActive(true);
        caixa_lucas.SetActive(false);
        texto_txt.text = "As c�meras de seguran�a filmaram toda a a��o";
        yield return new WaitForSeconds(4);
        texto_txt.text = "Hoje em dia � dif�cil fazer esse tipo de coisa e passar despercebido, sabia?";
        yield return new WaitForSeconds(4);
        caixa_policia.SetActive(false);
        caixa_lucas.SetActive(true);
        texto_txt.text = "Eu... N�o queria isso�";
        yield return new WaitForSeconds(4);        
        texto_txt.text = "N�o tive escolha�";
        yield return new WaitForSeconds(4);       
        texto_txt.text = "Meu irm�o menor precisava comer!";
        yield return new WaitForSeconds(4);
        caixa_policia.SetActive(true);
        caixa_lucas.SetActive(false);
        texto_txt.text = "Cada vez mais as pessoas escolhem o caminho f�cil�";
        yield return new WaitForSeconds(4);
        caixa_policia.SetActive(false);
        caixa_lucas.SetActive(true);
        texto_txt.text = "Eu n�o fiz o que fiz por ser f�cil! Eu fiz porque estava desesperado!";
        yield return new WaitForSeconds(4);        
        texto_txt.text = "Voc� sabe o qu�o duro foi ver as l�grimas do meu irm�o?";
        yield return new WaitForSeconds(4);
        caixa_policia.SetActive(true);
        caixa_lucas.SetActive(false);
        texto_txt.text = "Ent�o porque n�o foi trabalhar, jovem?";
        yield return new WaitForSeconds(4);
        caixa_policia.SetActive(false);
        caixa_lucas.SetActive(true);
        texto_txt.text = "Eu trabalhava, mas quando minha m�e ficou doente...";
        yield return new WaitForSeconds(4);        
        texto_txt.text = "Eu fui afastado do meu trabalho. N�o � t�o f�cil e r�pido achar um novo emprego!";
        yield return new WaitForSeconds(4);        
        texto_txt.text = "O Gabriel passava o dia na escola, se alimentava l�, ent�o n�o precisava me preocupar muito com ele.";
        yield return new WaitForSeconds(4);
        texto_txt.text = "Mas por causa da greve, ele teve que passar fome junto comigo.";
        yield return new WaitForSeconds(4);
        caixa_policia.SetActive(true);
        caixa_lucas.SetActive(false);
        texto_txt.text = "Entendo sua situa��o�";
        yield return new WaitForSeconds(4);
        texto_txt.text = "Me conte mais sobre o seu furto.";
        yield return new WaitForSeconds(4);
        caixa_lucas.SetActive(false);
        caixa_policia.SetActive(false);
        texto_obj.SetActive(false);
        while (fade_img.color != Color.black)
        {
            fade_img.color = Color.Lerp(fade_img.color, Color.black, 20 * Time.deltaTime);
            yield return new WaitForSeconds(0.01f);

        }
        cena_2.SetActive(false);
        cena_3_1.SetActive(true);
        while (fade_img.color != Color.clear)
        {
            fade_img.color = Color.Lerp(fade_img.color, Color.clear, 20 * Time.deltaTime);
            yield return new WaitForSeconds(0.01f);

        }
        caixa_lucas.SetActive(true);
        texto_obj.SetActive(true);
        caixa_gabriel.transform.position = new Vector3(0, 0, 0);
        caixa_lucas.transform.position = new Vector3(0, 0, 0);
        RectTransform TextPosition;
        TextPosition = texto_obj.GetComponent<RectTransform>();
        TextPosition.anchoredPosition = new Vector3(152.25f, 364f, 0);
        texto_txt.text = "Parab�ns Gabriel, agora voc� est� completando 6 anos! J� � um homemzinho!";
        yield return new WaitForSeconds(4);
        caixa_gabriel.SetActive(true);
        caixa_lucas.SetActive(false);
        texto_txt.text = "Hehehe...";
        yield return new WaitForSeconds(2);        
        texto_txt.text = "Eu queria que a mam�e estivesse aqui com a gente...";
        yield return new WaitForSeconds(4);
        caixa_gabriel.SetActive(false);
        caixa_lucas.SetActive(true);
        texto_txt.text = "Eu tamb�m...";
        yield return new WaitForSeconds(2);
        caixa_gabriel.SetActive(true);
        caixa_lucas.SetActive(false);
        texto_txt.text = "Lulu, quando eu vou poder ir com voc� visitar ela?";
        yield return new WaitForSeconds(4);
        caixa_gabriel.SetActive(false);
        caixa_lucas.SetActive(true);
        texto_txt.text = "Logo. Os m�dicos falaram que ela est� melhorando, e j�, j� voc� vai poder ir comigo visitar ela, ok?";
        yield return new WaitForSeconds(5);
        caixa_gabriel.SetActive(true);
        caixa_lucas.SetActive(false);
        texto_txt.text = "Que bom, estou com saudades da mam�e...";
        yield return new WaitForSeconds(3.5f);
        texto_txt.text = "Eu estava com medo, com medo que ela fosse morar com o papai e nos deixasse...";
        yield return new WaitForSeconds(4);
        texto_txt.text = "Mas ela est� melhorando, ela vem pra casa em breve, e vai me elogiar por eu j� ser um homemzinho!";
        yield return new WaitForSeconds(4);
        caixa_gabriel.SetActive(false);
        caixa_lucas.SetActive(true);
        texto_txt.text = "Gabriel...*murmurou baixinho*";
        yield return new WaitForSeconds(3);
        texto_txt.text = "Vamos mudar de assunto, hoje � seu anivers�rio.";
        yield return new WaitForSeconds(4);
        texto_txt.text = "Se voc� pudesse pedir alguma coisa, qualquer coisa, o que pediria?";
        yield return new WaitForSeconds(4);
        caixa_gabriel.SetActive(true);
        caixa_lucas.SetActive(false);
        texto_txt.text = "Eu queria a mam�e e o papai aqui.";
        yield return new WaitForSeconds(3);
        caixa_gabriel.SetActive(false);
        caixa_lucas.SetActive(true);
        texto_txt.text = "...";
        yield return new WaitForSeconds(1.5f);
        texto_txt.text = "J� sei. E se tiv�ssemos muito, muito, muito dinheiro mesmo, o que voc� gostaria de pedir?";
        yield return new WaitForSeconds(4);
        caixa_gabriel.SetActive(true);
        caixa_lucas.SetActive(false);
        texto_txt.text = "Com muito, muito dinheiro mesmo?";
        yield return new WaitForSeconds(3);
        texto_txt.text = "Eu queria um dinossauro s� meu, a� eu poderia montar nele e sair andando na rua, e todo mundo teria inveja do meu dinossauro, mas ele seria s� meu!";
        yield return new WaitForSeconds(5.5f);
        caixa_gabriel.SetActive(false);
        caixa_lucas.SetActive(true);
        texto_txt.text = "O que mais?";
        yield return new WaitForSeconds(2.5f);
        caixa_gabriel.SetActive(true);
        caixa_lucas.SetActive(false);
        texto_txt.text = "Eu iria comprar uma namorada para o irm�oz�o!";
        yield return new WaitForSeconds(3.5f);
        caixa_gabriel.SetActive(false);
        caixa_lucas.SetActive(true);
        texto_txt.text = "Uma namorada para mim? Mas porque voc� iria me comprar uma namorada?";
        yield return new WaitForSeconds(4);
        caixa_gabriel.SetActive(true);
        caixa_lucas.SetActive(false);
        texto_txt.text = "Voc� parece sempre muito triste desde que a mam�e foi para o hospital...";
        yield return new WaitForSeconds(4);
        texto_txt.text = "E eu vejo as pessoas na rua muito, muito felizes com namoradas!";
        yield return new WaitForSeconds(4);
        texto_txt.text = "Eu s� queria que o irm�oz�o ficasse muito, muito feliz!";
        yield return new WaitForSeconds(4);
        caixa_gabriel.SetActive(false);
        caixa_lucas.SetActive(true);
        texto_txt.text = "*Suspiro* O que mais?";
        yield return new WaitForSeconds(2.5f);
        caixa_gabriel.SetActive(true);
        caixa_lucas.SetActive(false);
        texto_txt.text = "Eu tamb�m iria pedir um bolo grand�o, do tamanho de um pr�dio!";
        yield return new WaitForSeconds(4);
        caixa_gabriel.SetActive(false);
        caixa_lucas.SetActive(true);
        texto_txt.text = "Mas porque voc� iria querer um bolo t�o grande assim";
        yield return new WaitForSeconds(4);
        caixa_gabriel.SetActive(true);
        caixa_lucas.SetActive(false);
        texto_txt.text = "Eu iria guardar o bolo, e toda vez que eu sentisse fome eu iria comer o bolo, e assim eu nunca mais iria sentir fome!";
        yield return new WaitForSeconds(5.5f);
        caixa_gabriel.SetActive(false);
        caixa_lucas.SetActive(true);
        texto_txt.text = "Ehhh, mas o bolo iria estragar se ficasse guardado durante tanto tempo!";
        yield return new WaitForSeconds(4);
        caixa_gabriel.SetActive(true);
        caixa_lucas.SetActive(false);
        texto_txt.text = "Eu n�o me importo, eu iria comer o bolo estragado assim mesmo!";
        yield return new WaitForSeconds(4);
        caixa_gabriel.SetActive(false);
        caixa_lucas.SetActive(true);
        texto_txt.text = "Mas voc� iria passar mal depois de comer o bolo estragado, iria ficar 3 dias inteiros no  banheiro, passando mal!";
        yield return new WaitForSeconds(4.5f);
        caixa_gabriel.SetActive(true);
        caixa_lucas.SetActive(false);
        texto_txt.text = "Eu n�o me importo! Mesmo eu passando 10 dias, mesmo eu nunca mais podendo comer bolo!";
        yield return new WaitForSeconds(4.5f);
        texto_txt.text = "Eu ainda iria preferir comer do que passar fome! Qualquer coisa � melhor do que estar sentindo tanta fome assim!";
        yield return new WaitForSeconds(5.2f);
        texto_txt.text = "Na verdade, se eu tivesse muito, muito dinheiro, eu iria querer comer, Lulu, eu s� iria querer comer alguma coisa!";
        yield return new WaitForSeconds(5.5f);
        texto_txt.text = "Eu estou morrendo de fome, Lulu...";
        yield return new WaitForSeconds(1.5f);
        cena_3_1.SetActive(false);
        cena_3_2.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        texto_txt.text = "Porque a gente tem de sentir fome Lulu? Eu s� queria um pouco de comida...";
        yield return new WaitForSeconds(3.5f);
        caixa_gabriel.SetActive(false);
        caixa_lucas.SetActive(true);
        texto_txt.text = "*Abra�a Gabriel*";
        yield return new WaitForSeconds(1.5f);
        texto_txt.text = "Gabriel, seu irm�o vai te trazer algo para comer...";
        yield return new WaitForSeconds(3);
        texto_txt.text = "Eu prometo.";
        yield return new WaitForSeconds(1.5f);
        while (fade_img.color != Color.black)
        {
            fade_img.color = Color.Lerp(fade_img.color, Color.black, 20 * Time.deltaTime);
            yield return new WaitForSeconds(0.01f);

        }
        SceneManager.LoadScene(7);
        
    }
}
