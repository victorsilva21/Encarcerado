using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class movcamera : MonoBehaviour
{
    Camera controlecam;
  
    [SerializeField] GameObject personagem;

     bool travarcam;
    // Start is called before the first frame update
    void Start()
    {
        personagem = GameObject.FindGameObjectWithTag("Player");
       controlecam = gameObject.GetComponent<Camera>();

        travarcam = true;
        
        
        


    }

    // Update is called once per frame
    private void Update()
    {
        camerainfo();
        if (Input.mouseScrollDelta.y < 0 && controlecam.orthographicSize < 8)// zoom da camera pelo scroll do mouse
        {
            controlecam.orthographicSize += 0.2f;
        }
        if (Input.mouseScrollDelta.y > 0 && controlecam.orthographicSize > 2)
        {
            controlecam.orthographicSize -= 0.2f;
        }
       
       
        if (Input.GetKeyDown(KeyCode.Q) && travarcam == true)//trava e destrava a camera
        {
            print("cliquei");
            travarcam = false;
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            travarcam = true;
        }
       
    }
    

    void camerainfo()
    {

        
        if(travarcam == true)//retorna a camera a posição do jogador
        {
            transform.position = new Vector3(personagem.transform.position.x , personagem.transform.position.y+ 0.5f , -10) ;
        }
        if(travarcam == false)//move livremente a camera quando destravada
        {
            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(new Vector2(5 * Time.deltaTime, 0));
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(new Vector2(-5 * Time.deltaTime, 0));
            }
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(new Vector2(0, 5 * Time.deltaTime));
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(new Vector2(0, -5 * Time.deltaTime));
            }
        }




    }
}
