using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using informacoes;

public class charmove : MonoBehaviour
{
    float vel;
    [SerializeField] float stealh_vel = 1.5f;
    [SerializeField] float normal_speed = 3f;
    [SerializeField] bool libera_andar = true, to_terreo = true, permite_escada = false, escada_interagiu = false, elevador_interagiu = false, camera_travada = false;
    Animator move_view;
    [SerializeField] GameObject andar_0, andar_1, interagirvisu;
    private float andar0armazem = -3.77f;
    private float andar1armazem = 6.02f;
    public bool andar0conf = false;
    private bool andar1conf = false;
    charinfo dias;

    private void Awake()
    {
        interagirvisu = GameObject.Find("interagirCanvas");
    }
    // Start is called before the first frame update
    void Start()
    {
        
        dias = FindObjectOfType<charinfo>().GetComponent<charinfo>();
        move_view = gameObject.GetComponent<Animator>();
       
    }
    

    // Update is called once per frame
    void Update()
    {
        movimento();
        automatico();
    }
    
    void movimento()// movimento e interação do personagem
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            camera_travada = !(camera_travada);
            if(libera_andar == false && escada_interagiu == false && SceneManager.GetActiveScene().name !="armazem" )
            {
                libera_andar = true;
            }
            else if(libera_andar == false && elevador_interagiu == false && SceneManager.GetActiveScene().name == "armazem")
            {
                libera_andar = true;
            }
            else { libera_andar = false; }
            if(escada_interagiu== false || elevador_interagiu == false)
            {
                move_view.SetBool("movimento", false);
            }
                
            


        }
        if (Input.GetKey(KeyCode.LeftShift) && libera_andar == true)
        {
            vel = stealh_vel;
            move_view.speed = 0.4f;
         
        }
        else
        {
            vel = normal_speed;
            move_view.speed = 1f;
        }
        if(Input.GetKeyDown(KeyCode.E) && permite_escada == true && libera_andar == true)
        {
            if(to_terreo == false)
            {
                transform.position = andar_0.transform.position;
            }
            else
            {
                transform.position = andar_1.transform.position;
            }
            escada_interagiu = true;
            libera_andar = false;
           
        }
        
        if (Input.GetKey(KeyCode.D) && libera_andar == true )
        {
            move_view.SetBool("movimento", true);
            transform.eulerAngles = new Vector3(0, 0, 0);
            transform.Translate(new Vector2(vel * Time.deltaTime, 0));
            
        }//movimento pra esquerda
       
        if (Input.GetKey(KeyCode.A) && libera_andar == true)
        {
            move_view.SetBool("movimento", true);
            transform.eulerAngles = new Vector3(0, 180, 0);
            transform.Translate(new Vector2(vel * Time.deltaTime, 0));
           
        }//movimento pra direita
        if(Input.GetKeyUp(KeyCode.A) && libera_andar == true|| Input.GetKeyUp(KeyCode.D) && libera_andar == true)//voltar para o idle
        {
            move_view.SetBool("movimento", false);
        }

    }
    void automatico()
    {
        if(SceneManager.GetActiveScene().name == "cadeia principal")
        {
            andar_0 = GameObject.Find("terreo");
            andar_1 = GameObject.Find("primeiro andar");
        }
        if(to_terreo == false && escada_interagiu == true)
        {
            dias.enabled = false;
            move_view.SetBool("movimento", true);
            
            transform.eulerAngles = new Vector3(0, 0, 0);

            transform.position = Vector2.MoveTowards(transform.position,andar_1.transform.position , vel * Time.deltaTime);
            if(this.gameObject.transform.position == andar_1.transform.position)
            {
                move_view.SetBool("movimento", false);
               if(camera_travada == false) { libera_andar = true; }
                
                escada_interagiu = false;
                to_terreo = true;
                dias.enabled = true;
            }
        }
        if (to_terreo == true && escada_interagiu == true)
        {
            dias.enabled = false;
            move_view.SetBool("movimento", true);
            
            transform.eulerAngles = new Vector3(0, 180, 0);
            transform.position = Vector2.MoveTowards(transform.position, andar_0.transform.position, vel * Time.deltaTime);
            if (this.gameObject.transform.position == andar_0.transform.position)
            {

                move_view.SetBool("movimento", false);
                if (camera_travada == false) { libera_andar = true; }
                escada_interagiu = false;
                to_terreo = false;
                dias.enabled = true;

            }
        }
        if (andar0conf == true)
        {
            libera_andar = false;
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(this.gameObject.transform.position.x, andar1armazem), 3 * Time.deltaTime);
            if (transform.position.y == andar1armazem)
            {
                andar0conf = false;
                if (camera_travada == false) { libera_andar = true; }
                elevador_interagiu = false;


            }
        }
        if (andar1conf == true)
        {
            libera_andar = false;
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(this.gameObject.transform.position.x, andar0armazem), 3 * Time.deltaTime);
            if(transform.position.y == andar0armazem)
            {
                andar1conf = false;
                if (camera_travada == false) { libera_andar = true; }
                elevador_interagiu = false;

            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if(collision.gameObject.CompareTag("prisao andar 1") || collision.gameObject.CompareTag("prisao andar 0"))
        {
            interagirvisu.SetActive(true);
            permite_escada = true;
            
        }
        if (collision.gameObject.CompareTag("prisao andar 1")&& escada_interagiu ==false)
        {
            to_terreo = false;
        }
        if (collision.gameObject.CompareTag("prisao andar 0") && escada_interagiu == false)
        {
            to_terreo = true;
        }



    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("prisao andar 1") || collision.gameObject.CompareTag("prisao andar 0"))
        {
            interagirvisu.SetActive(false);
            permite_escada = false;

        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("0f"))
        {
            transform.Translate(new Vector2(-0.5f, 0));
            andar0conf = true;
            elevador_interagiu = true;


        }
        if (collision.gameObject.CompareTag("1f"))
        {
            transform.Translate(new Vector2(-0.5f, 0));
            andar1conf = true;
            elevador_interagiu = true;


        }
    }
}
