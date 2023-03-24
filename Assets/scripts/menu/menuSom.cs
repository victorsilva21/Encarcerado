using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class menuSom : MonoBehaviour
{
    public Button voltar;
    public GameObject menuop, menusom;
    // Start is called before the first frame update
    void Start()
    {
        voltar.onClick.AddListener(voltarretorno);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void voltarretorno() 
    {
        menuop.SetActive(true);
        menusom.SetActive(false);
}
}
