using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using informacoes;
using UnityEngine.UI;

public class barra_stamina : MonoBehaviour
{

    float stamina;
    bool combat;
    public Combat_Hud parent;
    charinfo info;

    public Image sta;
    public Image obj;

    // Start is called before the first frame update
    void Start()
    {
        info = parent.info;
        sta = GetComponent<Image>();// pega o componente de slider do objeto
    }

    // Update is called once per frame
    void Update()
    {
        combat = info.combat;
        if (combat)
        {
            obj.enabled = true;
            sta.enabled = true;
        }
        else
        {
            obj.enabled = false;
            sta.enabled = false;
        }

        stamina = charinfo.stamina;// atribui o valor de fome, do script1, para uma variavel

        sta.fillAmount = (stamina / 100);// atribui o valor a barra, mostrando no resultado final

    }
}