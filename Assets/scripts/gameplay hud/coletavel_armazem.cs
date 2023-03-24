using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coletavel_armazem : MonoBehaviour
{
    [SerializeField] GameObject porta;
    [SerializeField] GameObject tutorial;
    // Start is called before the first frame update
    void Start()
    {
        tutorial = GameObject.Find("teclado tuto");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            porta.SetActive(true);
            tutorial.SetActive(false);
            Destroy(this.gameObject);
        }
    }
}
