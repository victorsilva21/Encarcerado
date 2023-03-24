using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact_script : MonoBehaviour
{
    GameObject personagem;
    RectTransform local;
    // Start is called before the first frame update
    void Start()
    {
        personagem = GameObject.Find("char");
        local = gameObject.GetComponent<RectTransform>();

    }

    // Update is called once per frame
    void Update()
    {
       local.transform.position = new Vector3(personagem.transform.position.x, personagem.transform.position.y + 2.3f, transform.position.z);
    }
}
