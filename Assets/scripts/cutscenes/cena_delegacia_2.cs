using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class cena_delegacia_2 : MonoBehaviour
{
    
    Image fade_img;
    GameObject  caixa_policia, caixa_lucas, texto_obj;
    Text texto_txt;
    bool cutscene_iniciou = false;
    // Start is called before the first frame update
    void Start()
    {
        
        caixa_lucas = GameObject.Find("lucas_caixa");
        caixa_policia = GameObject.Find("policia_caixa");
        texto_obj = GameObject.Find("caixa de fala");
        texto_txt = texto_obj.GetComponent<Text>();
        caixa_lucas.SetActive(false);
        caixa_policia.SetActive(false);        
        texto_obj.SetActive(false);
        fade_img = GameObject.Find("fade").GetComponent<Image>();

        
        StartCoroutine("cutscene_inicio");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && cutscene_iniciou == true)
        {
            SceneManager.LoadScene(2);
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
        caixa_policia.SetActive(true);
        texto_obj.SetActive(true);
        texto_txt.text = "...";
        yield return new WaitForSeconds(2f);        
        texto_txt.text = "Jovem, mesmo que seus motivos sejam justific�veis, voc� ainda vai ter que responder por isso.";
        yield return new WaitForSeconds(3.3f);
        caixa_policia.SetActive(false);
        caixa_lucas.SetActive(true);
        texto_txt.text = "Eu sei�";
        yield return new WaitForSeconds(3.3f);
        caixa_policia.SetActive(true);
        caixa_lucas.SetActive(false);
        texto_txt.text = "Voc� j� ouviu falar no princ�pio da insignific�ncia?";
        yield return new WaitForSeconds(3.3f);
        caixa_policia.SetActive(false);
        caixa_lucas.SetActive(true);
        texto_txt.text = "N�o, o que � isso?";
        yield return new WaitForSeconds(3.3f);
        caixa_policia.SetActive(true);
        caixa_lucas.SetActive(false);
        texto_txt.text = "Em resumo, voc� vai pode ser solto e perdoado por esse crime de furto, j� que o motivo foi necessidade de alimento.";
        yield return new WaitForSeconds(3.3f);        
        texto_txt.text = "Mas, voc� vai ter que aguardar enquanto um promotor p�blico leve o seu caso at� um juiz, para ele pode arquivar o caso, entendeu?";
        yield return new WaitForSeconds(3.3f);
        caixa_policia.SetActive(false);
        caixa_lucas.SetActive(true);
        texto_txt.text = "Eu entendi. Mas... e o meu irm�o menor? Algu�m precisa cuidar dele, e n�s n�o temos fam�lia pr�xima.";
        yield return new WaitForSeconds(3.3f);
        caixa_policia.SetActive(true);
        caixa_lucas.SetActive(false);
        texto_txt.text = "N�o se preocupe, um guarda tutelar j� foi acionado e est� sendo mandado para buscar o seu irm�o, e at� que algum respons�vel possa retirar ele, o estado ir� tomar conta dele.";
        yield return new WaitForSeconds(3.3f);
        caixa_policia.SetActive(false);
        caixa_lucas.SetActive(true);
        texto_txt.text = "Entendo�";        
        yield return new WaitForSeconds(3.3f);
        caixa_policia.SetActive(true);
        caixa_lucas.SetActive(false);
        texto_txt.text = "Bem, agora voc� ir� ser mandado para o pres�dio, at� o seu caso ser arquivado.";
        yield return new WaitForSeconds(3.3f);
        caixa_policia.SetActive(true);
        caixa_lucas.SetActive(false);
        texto_txt.text = "Espero que voc� fique bem at� conseguir sair.";        
        caixa_lucas.SetActive(false);
        caixa_policia.SetActive(false);
        texto_obj.SetActive(false);
        while (fade_img.color != Color.black)
        {
            fade_img.color = Color.Lerp(fade_img.color, Color.black, 20 * Time.deltaTime);
            yield return new WaitForSeconds(0.01f);

        }
        SceneManager.LoadScene(2);
    }
}
