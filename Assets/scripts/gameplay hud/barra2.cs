using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using informacoes;
using UnityEngine.UI;

public class barra2 : MonoBehaviour
{
    
    float doencabar;

    public Image bar;

    // Start is called before the first frame update
    void Start()
    {
        bar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        doencabar = charinfo.doente;// atribui o valor de doença, do script1, para uma variavel

        bar.fillAmount = (doencabar/100);// atribui o valor a barra, mostrando no resultado final

    }
}