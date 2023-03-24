using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using informacoes;
using UnityEngine.SceneManagement;

public class MEDKIT : MonoBehaviour
{
    GameObject medclose, medopen, medicamento, varios_med;
    
    // Start is called before the first frame update
    void Start()
    {
       medclose =  GameObject.Find("medikit fechado");
       medopen = GameObject.Find("medikit aberto");
       varios_med = GameObject.Find("memedio grupo");
       medicamento = GameObject.Find("memedio");
        medopen.SetActive(false);
        varios_med.SetActive(false);
        medicamento.SetActive(false);
       

    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (charinfo.dia_remedio != charinfo.dia_passagem || charinfo.dia_passagem <2)
            {
                medclose.SetActive(false);
                medopen.SetActive(true);
                varios_med.SetActive(false);
                medicamento.SetActive(false); 
            }
            else
            {
                medclose.SetActive(false);
                medopen.SetActive(true);
                varios_med.SetActive(true);
                medicamento.SetActive(true);
            }
            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            medclose.SetActive(true);
            medopen.SetActive(false);
            varios_med.SetActive(false);
            medicamento.SetActive(false);
        }
    }
}
