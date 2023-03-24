using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class tituloinicial : MonoBehaviour
{
    Canvas titulo;
    public GameObject menuInicial;
    // Start is called before the first frame update
    void Start()
    {
        titulo = gameObject.GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            menuInicial.SetActive(true);
            Destroy(this.gameObject);
        }
    }
}
