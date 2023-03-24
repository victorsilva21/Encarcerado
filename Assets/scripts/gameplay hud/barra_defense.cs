using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using informacoes;
using UnityEngine.UI;

public class barra_defense : MonoBehaviour
{

    float defense;
    bool combat;
    public Combat_Hud parent;
    charinfo info;
    Combat playerCombat;

    public Image def;
    public Image obj;

    // Start is called before the first frame update
    void Start()
    {
        info = parent.info;
        def = GetComponent<Image>();// pega o componente de slider do objeto
        playerCombat = parent.Player.GetComponent<Combat>();
    }

    // Update is called once per frame
    void Update()
    {
        combat = info.combat;
        if (combat)
        {
            if (defense == 1)
            {
                obj.enabled = false;
                def.enabled = false;
            }
            else
            {
                obj.enabled = true;
                def.enabled = true;
            }
        }
        else
        {
            obj.enabled = false;
            def.enabled = false;
        }

        defense = playerCombat.defense;// atribui o valor de fome, do script1, para uma variavel

        def.fillAmount = (defense);// atribui o valor a barra, mostrando no resultado final

    }
}