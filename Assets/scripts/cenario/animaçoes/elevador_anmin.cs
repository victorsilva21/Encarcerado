using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elevador_anmin : MonoBehaviour
{
    private Animator anmin;
    // Start is called before the first frame update
    void Start()
    {
        anmin = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            anmin.SetBool("parado", false);
            anmin.SetBool("abrindo", true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            anmin.SetBool("abrindo", false);
            anmin.SetBool("fechando", true);
        }
    }
}
