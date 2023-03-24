using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class menuprincipal : MonoBehaviour
{
    public Button play, options, credits, exit;
    public GameObject optionsvisu, menuvisu, creditvisu;
    // Start is called before the first frame update
    void Start()
    {
        play.onClick.AddListener(playretorno);
        options.onClick.AddListener(optionsretorno);
        exit.onClick.AddListener(sairretorno);
        credits.onClick.AddListener(creditosretorno);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void playretorno()
    {
        SceneManager.LoadScene(8);
    }
    void optionsretorno()
    {
       
        optionsvisu.SetActive(true);
        menuvisu.SetActive(false);
    }
    void creditosretorno()
    {
        creditvisu.SetActive(true);
        menuvisu.SetActive(false);
    }
    void sairretorno()
    {
        Application.Quit();
    }
}
