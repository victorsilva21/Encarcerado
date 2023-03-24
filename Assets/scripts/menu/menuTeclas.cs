using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class menuTeclas : MonoBehaviour
{
    public Button voltar;
    [SerializeField] GameObject menuop, menuteclas;
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
        menuteclas.SetActive(false);
    }
}
