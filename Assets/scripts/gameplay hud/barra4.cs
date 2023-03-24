using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using informacoes;
using UnityEngine.UI;

public class barra4 : MonoBehaviour
{

    float diasbar;
    
    public Image bar;
    public Text dias;

    // Start is called before the first frame update
    void Start()
    {
        
        bar = GetComponent<Image>();
        dias = GameObject.Find("Text").GetComponent<Text>() ;
    }

    // Update is called once per frame
    void Update()
    {
        diasbar = charinfo.dia_tempo;// atribui o valor de doença, do script1, para uma variavel

        bar.fillAmount = (diasbar / 420);// atribui o valor a barra, mostrando no resultado final

        dias.text = charinfo.dia_passagem.ToString();

    }
}