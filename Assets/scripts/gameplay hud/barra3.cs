using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using informacoes;
using UnityEngine.UI;

public class barra3 : MonoBehaviour { 
 
float vidabar;

public Image bar;

// Start is called before the first frame update
void Start()
{
    bar = GetComponent<Image>();
}

// Update is called once per frame
void Update()
{
    vidabar = charinfo.vida;// atribui o valor de doença, do script1, para uma variavel

    bar.fillAmount = (vidabar / 100);// atribui o valor a barra, mostrando no resultado final

}
}