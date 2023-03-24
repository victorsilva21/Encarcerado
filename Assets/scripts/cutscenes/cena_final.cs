using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class cena_final : MonoBehaviour
{
   GameObject cena_final_1 , cena_final_2, cine_obj;
    Camera cinematica;
    
    Text texto, creditos;
    [SerializeField] RectTransform textoposition,creditosposition;
    Image fade;
   
    // Start is called before the first frame update
    void Start()
    {
        cena_final_1 = GameObject.Find("cena final 1");
        cena_final_2 = GameObject.Find("cena final 2");
        cinematica = GameObject.Find("Main Camera").GetComponent<Camera>();
        cine_obj = GameObject.Find("Main Camera");
        texto = GameObject.Find("Text").GetComponent<Text>();
        creditos = GameObject.Find("Creditos").GetComponent<Text>();
        creditosposition = GameObject.Find("Creditos").GetComponent<RectTransform>();
        textoposition = GameObject.Find("Text").GetComponent<RectTransform>();
        fade = GameObject.Find("fade").GetComponent<Image>();
        cena_final_2.SetActive(false);
        fade.color = Color.clear;
        ;
       
        StartCoroutine(final_cuts());
    }
   
    IEnumerator final_cuts()
    {
        while(textoposition.anchoredPosition.y > -798)
        {
            textoposition.Translate(new Vector3(0, -0.005f, 0));
            yield return null;
        }
        while(fade.color != Color.black)
        {
            fade.color = Color.Lerp(fade.color, Color.black, 5 * Time.deltaTime);
            yield return null;
        }
        cena_final_1.SetActive(false);
        cena_final_2.SetActive(true);
        cinematica.orthographicSize = 1.19f;
        cine_obj.transform.position = new Vector3(6.9f, -1.83f, -10);
        while (fade.color != Color.clear)
        {
            fade.color = Color.Lerp(fade.color, Color.clear, 5 * Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        while(cine_obj.transform.position!= new Vector3(0, 0, -10) )
        {
            
                cine_obj.transform.position = Vector3.MoveTowards(cine_obj.transform.position,new Vector3(0, 0, -10), 10*Time.deltaTime);
            
           
                cinematica.orthographicSize = Mathf.MoveTowards(cinematica.orthographicSize, 5f, 5 * Time.deltaTime);
            
            yield return new WaitForSeconds(0.02f);
        }
        yield return new WaitForSeconds(1.5f);
        while (fade.color != Color.black)
        {
            fade.color = Color.Lerp(fade.color, Color.black, 5 * Time.deltaTime);
            yield return null;
        }
        while (creditosposition.anchoredPosition.y > -5687)
        {
            creditosposition.Translate(new Vector3(0, -0.005f, 0));
            yield return null;
        }
        yield return new WaitForSeconds(3);
        Application.Quit();
             
    }
   
}
