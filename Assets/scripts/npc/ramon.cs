using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using informacoes;

public class ramon : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(charinfo.dia_passagem == 0)
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
