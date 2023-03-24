using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class menuopcoes : MonoBehaviour
{
    [SerializeField] Button video, som, teclas, voltar;
    [SerializeField] GameObject optionsvisu, menuvisu, somvisu, teclasvisu;
    // Start is called before the first frame update
    void Start()
    {
        voltar.onClick.AddListener(voltarretorno);
        som.onClick.AddListener(somretorno);
        teclas.onClick.AddListener(teclasretorno);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void voltarretorno()
    {
        menuvisu.SetActive(true);
        optionsvisu.SetActive(false);
       
        
    }
    void somretorno()
    {
        somvisu.SetActive(true);
        optionsvisu.SetActive(false);
    }
    void teclasretorno()
    {
        teclasvisu.SetActive(true);
        optionsvisu.SetActive(false);
    }
}
