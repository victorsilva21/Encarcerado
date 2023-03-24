using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using informacoes;
using UnityEngine.SceneManagement;

public class policial : MonoBehaviour { 

 public float velo;
public float dista;
public float numero;
public bool direitinha = true;
    

public Transform bloquinho;

    private void Start()
    {
        bloquinho = gameObject.GetComponent<Transform>();
        if (charinfo.dia_passagem == 0 && SceneManager.GetActiveScene().name == "patio cadeia")
        {
            transform.position = new Vector3(0.19f,transform.position.y,transform.position.z);
        }
    }


    // Update is called once per frame
    void Update()
{
        transform.Translate(1 * Time.deltaTime, 0, 0); 

       

   
}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("limitenpc"))
        {
            if (direitinha == true)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                direitinha = false;
                
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
                direitinha = true;
                
                
            }
            
        }
    }
    
}