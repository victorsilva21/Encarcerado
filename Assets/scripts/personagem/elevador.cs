using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elevador : MonoBehaviour
{
    private float andar0 = -3.77f;
    private float andar1 = 6.02f;
    public bool andar0conf = false;
    private bool andar1conf = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (andar0conf == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(this.gameObject.transform.position.x , andar1), 3 * Time.deltaTime);
            
        }
        if(andar1conf == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(this.gameObject.transform.position.x , andar0), 3 * Time.deltaTime);
            
        }
        
    }
   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("0f"))
        {
            transform.Translate(new Vector2(-0.5f, 0));
            andar0conf = true;
            andar1conf = false;
        }
        if (collision.gameObject.CompareTag("1f"))
        {
            transform.Translate(new Vector2(-0.5f, 0));
            andar1conf = true;
            andar0conf = false;
        }
    }
}
