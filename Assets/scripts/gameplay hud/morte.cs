using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using informacoes;

public class morte : MonoBehaviour
{
    Button tentar_novamente;
    GameObject hud, personagem, cameri, combathud, falas, dormiu, demorou, interactcanvas, naodestroi;
    musica som;
    Combat combate;
    // Start is called before the first frame update
    private void Awake()
    {
        som = GameObject.Find("som").GetComponent<musica>();
        interactcanvas = GameObject.Find("interagirCanvas");
        combate = GameObject.Find("char").GetComponent<Combat>();
        personagem = GameObject.Find("char");
        demorou = GameObject.Find("demorou");
        dormiu = GameObject.Find("dormiu");
        hud = GameObject.Find("hud");
        falas = GameObject.Find("cena de falas");
        cameri = GameObject.Find("Main Camera");
        combathud = GameObject.Find("Combat Hud");
        naodestroi = GameObject.Find("indestrutiveis");
    }
    void Start()
    {
        
        tentar_novamente = GameObject.Find("tente novamente").GetComponent<Button>();
        tentar_novamente.onClick.AddListener(tentarretorno);
        
    }
    private void Update()
    {
        
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (this.gameObject.activeInHierarchy == true)
        {
            Time.timeScale = 0;
        }
    }
    void tentarretorno()
    {
        Time.timeScale = 1;
        combate.enabled = false;
        charinfo.fome = 100; charinfo.doente = 0; charinfo.vida = 100; charinfo.dia_passagem = 0;charinfo.dia_remedio = 0; charinfo.dia_tempo = 420;
        
        
        
        if (SceneManager.GetActiveScene().name == "armazem") { SceneManager.LoadScene(SceneManager.GetActiveScene().name); }
        else {
            som.FullStop();
            
            
                   //----- IGNYS
            
            SceneManager.MoveGameObjectToScene(naodestroi, SceneManager.GetActiveScene());
            som.Restart();
            SceneManager.LoadScene(2);
        }
        

        
        
    }
}
