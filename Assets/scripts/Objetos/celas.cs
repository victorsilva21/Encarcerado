using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using informacoes;

public class celas : MonoBehaviour
{
    Sprite cela_vazia, cela_cheia;
    
    SpriteRenderer imagem;
    // Start is called before the first frame update
    void Start()
    {
        
        imagem = gameObject.GetComponent<SpriteRenderer>();
        cela_vazia = Resources.Load<Sprite>("sprites/prisao vazia");
        cela_cheia = Resources.Load<Sprite>("sprites/prisao cheia");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(charinfo.dia_tempo < 100)
        {
            imagem.sprite = cela_cheia;
        }
        else
        {
            imagem.sprite = cela_vazia;
        }
    }
}
