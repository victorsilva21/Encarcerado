using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paralax_1 : MonoBehaviour
{
     Renderer quad; // seta o componente renderer no codigo
    [SerializeField] float vel = 4;
     Transform personagem;

    // seta uma variavel de numero quebrado
    // Start is called before the first frame update
    void Start()
    {
        personagem = GameObject.Find("char").GetComponent<Transform>();
        quad = gameObject.GetComponent<Renderer>();// pega o componente do objeto
        quad.sortingOrder = 8;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       
           
            quad.material.mainTextureOffset = new Vector2((personagem.position.x +8)*vel,0) ; // transforma a textura de acordo ao vetor
        
        
        
            

    }
}