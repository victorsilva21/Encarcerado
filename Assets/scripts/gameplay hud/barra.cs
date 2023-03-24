using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using informacoes;
using UnityEngine.UI;

public class barra : MonoBehaviour
{
    
    float fomebar;
    
    public Image bar;

    // Start is called before the first frame update
    void Start()
    {
        bar = GetComponent<Image>();// pega o componente de slider do objeto
    }

    // Update is called once per frame
    void Update()
    {
        fomebar = charinfo.fome;// atribui o valor de fome, do script1, para uma variavel

        bar.fillAmount = (fomebar/100);// atribui o valor a barra, mostrando no resultado final

    }
}