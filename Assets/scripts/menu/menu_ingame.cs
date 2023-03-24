using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class menu_ingame : MonoBehaviour
{
    GameObject menu_game;
    Button Sair;
    // Start is called before the first frame update
    void Awake()
    {
        menu_game = GameObject.Find("menu_ingame");
        Sair = GameObject.Find("Button").GetComponent<Button>();
        Sair.onClick.AddListener(clique);
        menu_game.SetActive(false);

    }

    void clique()
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && Time.timeScale != 0)
        {
           
            
            
            menu_game.SetActive(true);
            Time.timeScale = 0;

        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 1;
            menu_game.SetActive(false);
        }
    }
}
