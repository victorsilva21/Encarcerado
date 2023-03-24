using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float numero;
    bool confirmaaleatorio = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(confirmaaleatorio == true)
        {
            StartCoroutine(aleatorio());
        }
       
        if (numero >5)
        {
            transform.Translate(1*Time.deltaTime , 0, 0);
        }
        
        
    }

    IEnumerator aleatorio()
    {
        confirmaaleatorio = false;
        numero = Random.Range(0, 10);
        yield return new WaitForSeconds(3);
        confirmaaleatorio = true;
        

    }
}
