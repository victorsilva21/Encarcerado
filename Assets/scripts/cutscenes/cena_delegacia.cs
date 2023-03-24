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
        texto_txt.text = "Seu nome é Lucas, certo?";
        yield return new WaitForSeconds(4);
        caixa_policia.SetActive(false);
        caixa_lucas.SetActive(true);
        texto_txt.text = "* Sons de choro *";
        yield return new WaitForSeconds(4);
        caixa_policia.SetActive(true);
        caixa_lucas.SetActive(false);
        texto_txt.text = "Jovem, para facilitar o meu trabalho, eu preciso que você colabore.";
        yield return new WaitForSeconds(4);
        caixa_policia.SetActive(false);
        caixa_lucas.SetActive(true);
        texto_txt.text = "* Sons de choro *";
        yield return new WaitForSeconds(4);
        caixa_policia.SetActive(true);
        caixa_lucas.SetActive(false);
        texto_txt.text = "Bem… Se você não vai falar, vamos aos fatos...";
        yield return new WaitForSeconds(4);
        texto_txt.text = "Lucas, você foi pego invadindo e furtando um mercado.";
        yield return new WaitForSeconds(4);
        caixa_policia.SetActive(false);
        caixa_lucas.SetActive(true);
        texto_txt.text = "Não… Eu…";
        yield return new WaitForSeconds(4);
        caixa_policia.SetActive(true);
        caixa_lucas.SetActive(false);
        texto_txt.text = "As câmeras de segurança filmaram toda a ação";
        yield return new WaitForSeconds(4);
        texto_txt.text = "Hoje em dia é difícil fazer esse tipo de coisa e passar despercebido, sabia?";
        yield return new WaitForSeconds(4);
        caixa_policia.SetActive(false);
        caixa_lucas.SetActive(true);
        texto_txt.text = "Eu... Não queria isso…";
        yield return new WaitForSeconds(4);        
        texto_txt.text = "Não tive escolha…";
        yield return new WaitForSeconds(4);       
        texto_txt.text = "Meu irmão menor precisava comer!";
        yield return new WaitForSeconds(4);
        caixa_policia.SetActive(true);
        caixa_lucas.SetActive(false);
        texto_txt.text = "Cada vez mais as pessoas escolhem o caminho fácil…";
        yield return new WaitForSeconds(4);
        caixa_policia.SetActive(false);
        caixa_lucas.SetActive(true);
        texto_txt.text = "Eu não fiz o que fiz por ser fácil! Eu fiz porque estava desesperado!";
        yield return new WaitForSeconds(4);        
        texto_txt.text = "Você sabe o quão duro foi ver as lágrimas do meu irmão?";
        yield return new WaitForSeconds(4);
        caixa_policia.SetActive(true);
        caixa_lucas.SetActive(false);
        texto_txt.text = "Então porque não foi trabalhar, jovem?";
        yield return new WaitForSeconds(4);
        caixa_policia.SetActive(false);
        caixa_lucas.SetActive(true);
        texto_txt.text = "Eu trabalhava, mas quando minha mãe ficou doente...";
        yield return new WaitForSeconds(4);        
        texto_txt.text = "Eu fui afastado do meu trabalho. Não é tão fácil e rápido achar um novo emprego!";
        yield return new WaitForSeconds(4);        
        texto_txt.text = "O Gabriel passava o dia na escola, se alimentava lá, então não precisava me preocupar muito com ele.";
        yield return new WaitForSeconds(4);
        texto_txt.text = "Mas por causa da greve, ele teve que passar fome junto comigo.";
        yield return new WaitForSeconds(4);
        caixa_policia.SetActive(true);
        caixa_lucas.SetActive(false);
        texto_txt.text = "Entendo sua situação…";
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
        texto_txt.text = "Parabéns Gabriel, agora você está completando 6 anos! Já é um homemzinho!";
        yield return new WaitForSeconds(4);
        caixa_gabriel.SetActive(true);
        caixa_lucas.SetActive(false);
        texto_txt.text = "Hehehe...";
        yield return new WaitForSeconds(2);        
        texto_txt.text = "Eu queria que a mamãe estivesse aqui com a gente...";
        yield return new WaitForSeconds(4);
        caixa_gabriel.SetActive(false);
        caixa_lucas.SetActive(true);
        texto_txt.text = "Eu também...";
        yield return new WaitForSeconds(2);
        caixa_gabriel.SetActive(true);
        caixa_lucas.SetActive(false);
        texto_txt.text = "Lulu, quando eu vou poder ir com você visitar ela?";
        yield return new WaitForSeconds(4);
        caixa_gabriel.SetActive(false);
        caixa_lucas.SetActive(true);
        texto_txt.text = "Logo. Os médicos falaram que ela está melhorando, e já, já você vai poder ir comigo visitar ela, ok?";
        yield return new WaitForSeconds(5);
        caixa_gabriel.SetActive(true);
        caixa_lucas.SetActive(false);
        texto_txt.text = "Que bom, estou com saudades da mamãe...";
        yield return new WaitForSeconds(3.5f);
        texto_txt.text = "Eu estava com medo, com medo que ela fosse morar com o papai e nos deixasse...";
        yield return new WaitForSeconds(4);
        texto_txt.text = "Mas ela está melhorando, ela vem pra casa em breve, e vai me elogiar por eu já ser um homemzinho!";
        yield return new WaitForSeconds(4);
        caixa_gabriel.SetActive(false);
        caixa_lucas.SetActive(true);
        texto_txt.text = "Gabriel...*murmurou baixinho*";
        yield return new WaitForSeconds(3);
        texto_txt.text = "Vamos mudar de assunto, hoje é seu aniversário.";
        yield return new WaitForSeconds(4);
        texto_txt.text = "Se você pudesse pedir alguma coisa, qualquer coisa, o que pediria?";
        yield return new WaitForSeconds(4);
        caixa_gabriel.SetActive(true);
        caixa_lucas.SetActive(false);
        texto_txt.text = "Eu queria a mamãe e o papai aqui.";
        yield return new WaitForSeconds(3);
        caixa_gabriel.SetActive(false);
        caixa_lucas.SetActive(true);
        texto_txt.text = "...";
        yield return new WaitForSeconds(1.5f);
        texto_txt.text = "Já sei. E se tivéssemos muito, muito, muito dinheiro mesmo, o que você gostaria de pedir?";
        yield return new WaitForSeconds(4);
        caixa_gabriel.SetActive(true);
        caixa_lucas.SetActive(false);
        texto_txt.text = "Com muito, muito dinheiro mesmo?";
        yield return new WaitForSeconds(3);
        texto_txt.text = "Eu queria um dinossauro só meu, aí eu poderia montar nele e sair andando na rua, e todo mundo teria inveja do meu dinossauro, mas ele seria só meu!";
        yield return new WaitForSeconds(5.5f);
        caixa_gabriel.SetActive(false);
        caixa_lucas.SetActive(true);
        texto_txt.text = "O que mais?";
        yield return new WaitForSeconds(2.5f);
        caixa_gabriel.SetActive(true);
        caixa_lucas.SetActive(false);
        texto_txt.text = "Eu iria comprar uma namorada para o irmãozão!";
        yield return new WaitForSeconds(3.5f);
        caixa_gabriel.SetActive(false);
        caixa_lucas.SetActive(true);
        texto_txt.text = "Uma namorada para mim? Mas porque você iria me comprar uma namorada?";
        yield return new WaitForSeconds(4);
        caixa_gabriel.SetActive(true);
        caixa_lucas.SetActive(false);
        texto_txt.text = "Você parece sempre muito triste desde que a mamãe foi para o hospital...";
        yield return new WaitForSeconds(4);
        texto_txt.text = "E eu vejo as pessoas na rua muito, muito felizes com namoradas!";
        yield return new WaitForSeconds(4);
        texto_txt.text = "Eu só queria que o irmãozão ficasse muito, muito feliz!";
        yield return new WaitForSeconds(4);
        caixa_gabriel.SetActive(false);
        caixa_lucas.SetActive(true);
        texto_txt.text = "*Suspiro* O que mais?";
        yield return new WaitForSeconds(2.5f);
        caixa_gabriel.SetActive(true);
        caixa_lucas.SetActive(false);
        texto_txt.text = "Eu também iria pedir um bolo grandão, do tamanho de um prédio!";
        yield return new WaitForSeconds(4);
        caixa_gabriel.SetActive(false);
        caixa_lucas.SetActive(true);
        texto_txt.text = "Mas porque você iria querer um bolo tão grande assim";
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
        texto_txt.text = "Eu não me importo, eu iria comer o bolo estragado assim mesmo!";
        yield return new WaitForSeconds(4);
        caixa_gabriel.SetActive(false);
        caixa_lucas.SetActive(true);
        texto_txt.text = "Mas você iria passar mal depois de comer o bolo estragado, iria ficar 3 dias inteiros no  banheiro, passando mal!";
        yield return new WaitForSeconds(4.5f);
        caixa_gabriel.SetActive(true);
        caixa_lucas.SetActive(false);
        texto_txt.text = "Eu não me importo! Mesmo eu passando 10 dias, mesmo eu nunca mais podendo comer bolo!";
        yield return new WaitForSeconds(4.5f);
        texto_txt.text = "Eu ainda iria preferir comer do que passar fome! Qualquer coisa é melhor do que estar sentindo tanta fome assim!";
        yield return new WaitForSeconds(5.2f);
        texto_txt.text = "Na verdade, se eu tivesse muito, muito dinheiro, eu iria querer comer, Lulu, eu só iria querer comer alguma coisa!";
        yield return new WaitForSeconds(5.5f);
        texto_txt.text = "Eu estou morrendo de fome, Lulu...";
        yield return new WaitForSeconds(1.5f);
        cena_3_1.SetActive(false);
        cena_3_2.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        texto_txt.text = "Porque a gente tem de sentir fome Lulu? Eu só queria um pouco de comida...";
        yield return new WaitForSeconds(3.5f);
        caixa_gabriel.SetActive(false);
        caixa_lucas.SetActive(true);
        texto_txt.text = "*Abraça Gabriel*";
        yield return new WaitForSeconds(1.5f);
        texto_txt.text = "Gabriel, seu irmão vai te trazer algo para comer...";
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
