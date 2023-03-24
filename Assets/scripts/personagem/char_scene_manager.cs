using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using informacoes;
using UnityEngine.UI;

public class char_scene_manager : MonoBehaviour
{
    // Requirements
    public charinfo info;
     [SerializeField]GameObject Cam, hud, morteobj, CombatHud, dormiu, interactvisu, naodestroi ;
     Canvas hudview;
     Canvas morte;
     Camera camview;
    bool porta = false,dormir = false;
    bool porta_libera = false;
    [SerializeField] bool refeitorio = false;
    Animator animin;

    // Start is called before the first frame update
    private void Awake()
    {
        interactvisu = GameObject.Find("interagirCanvas");
        morteobj = GameObject.Find("morte");
        dormiu = GameObject.Find("dormiu");
        CombatHud = GameObject.Find("Combat Hud");
        naodestroi = GameObject.Find("indestrutiveis");
    }
    void Start()
    {
        
       morte = morteobj.GetComponent<Canvas>();
        Cam = GameObject.Find("Main Camera");
        camview = Cam.GetComponent<Camera>();
        animin = this.gameObject.GetComponent<Animator>();
        hud = GameObject.Find("hud");
        if(SceneManager.GetActiveScene().name != "armazem")
        {
            hudview = hud.GetComponent<Canvas>();
            hudview.worldCamera = camview;
        }
        
        
        morte.worldCamera = camview;
        if (SceneManager.GetActiveScene().name == "intro cadeia")
        {
            hud.SetActive(false);

            DontDestroyOnLoad(naodestroi);
            dormiu.SetActive(false);
        }
        
        


    }
    // Check Active scene
    void Update()
    {
        if (charinfo.dia_tempo > 110)
        {
            dormir = false;
        }
        Scene scene = SceneManager.GetActiveScene();    // Get Active scene.
        if (scene.name == "armazem")                    // If scene is "armazem"...       
        {
            info.armazem = true;                        // Pass info to charinfo.
        }
        else                                            // If not...
        {
            info.armazem = false;                       // Pass info to charinfo as well.
        }

        /*if(scene.name == "Mecânica combate" && Input.GetKeyDown(KeyCode.P))
        {
            if(info.detectado == false)
            {
                info.detectado = true;
                info.combat = true;
            }
            else
            {
                info.detectado = false;
                info.combat = false;
            }
        }*/
        if (Input.GetKeyDown(KeyCode.E) && porta == true && SceneManager.GetActiveScene().name == "cadeia principal" && porta_libera == true)
        {
            
            animin.SetBool("movimento", false);
            

            transform.position = new Vector3(-8.909538f, -1.34f, 0);
            transform.eulerAngles = new Vector3(0, 0, 0);
            porta = false;
            SceneManager.LoadScene(5);
            
        }
        else if(Input.GetKeyDown(KeyCode.E) && porta == true && SceneManager.GetActiveScene().name == "ala medica")
        {
            
            animin.SetBool("movimento", false);
            porta_libera = true;
            transform.position = new Vector3(3.082075f, -1.83f, 0);
            transform.eulerAngles = new Vector3(0, 180, 0);
            SceneManager.LoadScene(3);
            


           
        }
        if(Input.GetKeyDown(KeyCode.E) && dormir == true)
        {


            StartCoroutine(dormido());
            

        }
        if (Input.GetKeyDown(KeyCode.E) && refeitorio == true && SceneManager.GetActiveScene().name == "patio cadeia" && charinfo.dia_passagem < 5)
        {
            
            animin.SetBool("movimento", false);
            transform.position = new Vector3(34.85f, -3.293648f, 0);
            transform.eulerAngles = new Vector3(0, 180, 0);
            SceneManager.LoadScene(6);
            interactvisu.SetActive(false);


        }
        if (Input.GetKeyDown(KeyCode.E) && refeitorio == true && SceneManager.GetActiveScene().name == "patio cadeia" && charinfo.dia_passagem == 5)
        {

            animin.SetBool("movimento", false);
            transform.position = new Vector3(34.85f, -3.293648f, 0);
            transform.eulerAngles = new Vector3(0, 180, 0);
            SceneManager.LoadScene(10);
            interactvisu.SetActive(false);


        }


    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "trigger_scene" && SceneManager.GetActiveScene().name == "intro cadeia")// leva da introdução para a parte principal da cadeia
        {
           
            animin.SetBool("movimento", false);

            hud.SetActive(true);

            transform.position = new Vector3(-19.40809f, 7.917951f, 0);
            transform.eulerAngles = new Vector3(0, 0, 0);
            
           
            SceneManager.LoadScene(3);
           




        }
        if(collision.gameObject.name == "porta"&& SceneManager.GetActiveScene().name == "armazem")
        {
            SceneManager.LoadScene(9);
        }
        if (collision.gameObject.name == "trigger_scene" && SceneManager.GetActiveScene().name == "cadeia principal")// leva da cadeia principal, para o patio
        {
            
            animin.SetBool("movimento", false);
            
            transform.position = new Vector3(5.935225f, -1.768467f, 0);
            transform.eulerAngles = new Vector3(0, 180, 0);
            SceneManager.LoadScene(4);
            

        }
        if(collision.gameObject.name == "trigger_ala_medica"  )
        {
            interactvisu.SetActive(true);
            porta = true;
            
        }
        if (collision.gameObject.name == "trigger_refeitorio")
        {
            interactvisu.SetActive(true);
            refeitorio = true;

        }
        if (collision.gameObject.name == "trigger_refeitorio" && SceneManager.GetActiveScene().name == "refeitorio")
        {
            
            animin.SetBool("movimento", false);
            transform.position = new Vector3(-22.92825f, -1.800454f, 0);
            transform.eulerAngles = new Vector3(0, 0, 0);
            SceneManager.LoadScene(4);


        }
        if (collision.gameObject.name == "trigger_scene" && SceneManager.GetActiveScene().name == "patio cadeia")// leva do patio de volta pra cadeia 
        {
           
            animin.SetBool("movimento", false);
           
            transform.position = new Vector3(-39.48341f, -1.858475f, 0);
            transform.eulerAngles = new Vector3(0, 0, 0);
            SceneManager.LoadScene(3);
            


        }
        if (collision.gameObject.CompareTag("cela_lucas") && charinfo.dia_tempo < 110)
        {
            interactvisu.SetActive(true);
            dormir = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "trigger_ala_medica" )
        {
            interactvisu.SetActive(false);
            porta = false;
           
        }
        if (collision.gameObject.name == "trigger_refeitorio")
        {
            interactvisu.SetActive(false);
            refeitorio = false;

        }
        if (collision.gameObject.CompareTag("cela_lucas") )
        {
            interactvisu.SetActive(false);
            dormir = false;
        }
    }
    IEnumerator dormido()
    {
        charinfo.fome -= 8;
        if (charinfo.fome < 0) { charinfo.fome = 0; }
        transform.position = new Vector3(-19.40809f, 7.917951f, 0);
        charinfo.dia_passagem++;
        charinfo.dia_tempo = 420;
        charinfo.dia_remedio = charinfo.dia_passagem;
        Image dormiuimg = dormiu.GetComponentInChildren<Image>();
        dormiuimg.color = Color.clear;
        dormiu.SetActive(true);
        while (dormiuimg.color != Color.black)
        {
            dormiuimg.color = Color.Lerp(dormiuimg.color, Color.black, 7 * Time.deltaTime);
            yield return null;

        }
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(3);
        while (dormiuimg.color != Color.clear)
        {
            dormiuimg.color = Color.Lerp(dormiuimg.color, Color.clear, 7 * Time.deltaTime);
            yield return null;

        }
        dormiu.SetActive(false);
    }
    
    
}
