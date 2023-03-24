using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using informacoes;


public class deteccao : MonoBehaviour
{
    Enemy_Combat info_combate;
    public Transform seguir;
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "patio cadeia" && charinfo.dia_passagem == 0)
        {
            Destroy(this.gameObject);
        }
        info_combate = GameObject.FindGameObjectWithTag("enemy").GetComponent<Enemy_Combat>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name != "armazem")
        {
            gameObject.transform.position = new Vector2(seguir.position.x, seguir.position.y + 1);
            gameObject.transform.rotation = seguir.rotation;
        }
        else
        {
            gameObject.transform.position = new Vector2(seguir.position.x, seguir.position.y + 0.5f);
            gameObject.transform.rotation = seguir.rotation;
        }
        
        if (info_combate.combat)
        {
            Destroitudo();
        }
    }
    
    void Destroitudo()
    {
        GameObject[] detectores = GameObject.FindGameObjectsWithTag("deteccao_enemy");
        for (int num = 0; num < detectores.Length; num++)
        {
            Destroy(detectores[num]);
        }
    }
}
