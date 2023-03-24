using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace informacoes
{
    public class charinfo : MonoBehaviour
    {
        public static float fome = 100, doente = 0, vida =  100, stamina = 100, temposcript,dia_tempo= 420;
        public bool nasujeira = false;
        public bool nacomida = false;
        public bool detectado = false;
        bool memedio = false;
        public float vel = 3;
        public bool stealth;
        public bool seguro = false;
        public GameObject morte;
        public GameObject demorou;
        public AudioManager audioM;
        float regeneracao = 10;
        public static int dia_remedio = 0;

        public static int dia_passagem = 0;

        // Ignite's Variables :3 ----- IGNYS
        public bool armazem = false;            // True if active scene is armazem. ----- IGNYS
        public bool combat = false;             // Active when detected and not killed. ----- IGNYS
        private float staminaRegeneration = 10; // Time it takes for the stamina to regenerate. ----- IGNYS
        public bool manualControl = false;      // It takes out the IA and puts manual control. ----- IGNYS

        // Start is called before the first frame update
        void Start()
        {
           
            if (SceneManager.GetActiveScene().name == "intro cadeia")
            {
                dia_tempo = 420;
                dia_passagem = 0;
                dia_remedio = 0;
                demorou = GameObject.Find("demorou");
                audioM = GameObject.Find("som").GetComponent<AudioManager>();
                
                demorou.SetActive(false);
            }
            Debug.Log("começei");
            morte = GameObject.Find("morte");
            morte.SetActive(false);

            
        }
       

        // Update is called once per frame

        void FixedUpdate()
        {
           
            informacoes();



        }
        private void Update()
        {//------------------------------------------------------- codigo do bleu||
            if (Input.GetKeyDown(KeyCode.P))
            {
                dia_passagem = 4;
                vida = 100;
                fome = 100;
                doente = 0;
                dia_tempo = 110;


            }
         //------------------------------------------------------fim do codigo do bleu||
            movimento();
        }

        void movimento()// movimento e interação do personagem
        {
            if (Input.GetKeyDown(KeyCode.E) && nacomida == true)
            {
                fome += 35;
                if (fome > 100)
                {
                    fome = 100;
                }
                Destroy(GameObject.FindGameObjectWithTag("comida"));
            }// interação com comida
            if (Input.GetKeyDown(KeyCode.E) && memedio == true)
            {
                doente -= 45;
                if (doente < 0)
                {
                    doente = 0;
                }
                dia_remedio++;
                Destroy(GameObject.FindGameObjectWithTag("memedio"));
            }

            if (Input.GetKey(KeyCode.LeftShift) || seguro == true )
            {
                
                stealth = true;
            }
            else 
            {
               
                stealth = false;
            }


        }
        void informacoes()
        {
            if(SceneManager.GetActiveScene().name != "armazem" && SceneManager.GetActiveScene().name != "intro cadeia")
            if (temposcript <= 1)
            {
                temposcript += Time.deltaTime;
            }// contagem de tempo
            else { temposcript = 0; }

            if (nasujeira == true && doente < 100 && temposcript >= 1)
            {
                doente += 0.5f;


            }// soma contador de doença caso em contato com sujeira
            else if (nasujeira == false && doente > 0 && temposcript >= 1)
            {
                doente -= 0.2f;
            }//reduz contador de doença caso fora da sujeira
            
            if (fome < 5f && temposcript >= 1)
            {
                vida -= 5;
            }
            else if(fome < 15f && temposcript >= 1)
            {
                vida -= 1;
            }

            if (fome > 50f && temposcript >= 1)
            {
                vida += (regeneracao/100)*(100 - doente);
                if (vida > 100)
                {
                    vida = 100;
                }
            }

            // Stamina increaser ----- IGNYS
            if (temposcript > 1)
            {
                if(fome > 20f)
                {
                    if(doente < 60)
                    {
                        stamina += ((staminaRegeneration / 100)  *  ((100 - doente) / 2))*((fome/100)+0.8f);        // Same thing as vida but for stamina. ----- IGNYS
                    }
                    else
                    {
                        stamina += ((staminaRegeneration / 100) * 20 * ((fome / 100) + 0.8f));
                    }
                }
                else
                {
                    if (doente < 60)
                    {
                        stamina += ((staminaRegeneration / 100) * ((100 - doente) / 2));
                    }
                    else
                    {
                        stamina += ((staminaRegeneration / 100) * 20);
                    }
                }

                if (stamina > 100)
                {
                    stamina = 100;
                }
            }
            //contador de dias-----VIC
            if(temposcript >= 1)
            {
                dia_tempo--;
                
            }

            if(dia_tempo == 110)
            {
                audioM.Play("Sirene");
            }
            if(dia_tempo == 100)
            {
                audioM.FadeOut("Sirene", 0.1f);
            }

            if(dia_tempo <= 50 )
            {
                
                StartCoroutine(dia_passagem_forcada());
                dia_passagem++;
                dia_tempo = 420;

            }

            if (fome > 0 && temposcript >= 1)
            {
                fome -= 0.1f;
                temposcript = 0;


            }
           
            if (detectado == true)
            {
                if (armazem)            // If it's on scene "armazem"...
                {
                
                morte.SetActive(true);  // Die.
                }
                else                    // If not...
                {
                    combat = true;      // Enter combat.
                }

            }
            /*if(detectado == false)
            {
                combat = false;         // Leave combat.
            }*/
            if(vida <= 0)
            {
                
                morte.SetActive(true);
            }

            //contador de fome cresce com o tempo

        }// valores numericos dos atributos dos personagens

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("sujeira"))//detecçao de contato com sujeira
            {
                nasujeira = true;
            }
            if (other.CompareTag("stealthseguro"))
            {
                seguro = true;
            }
            if (other.CompareTag("memedio"))
            {
                memedio = true;
            }

            if (other.CompareTag("comida"))//detecção de contato com comida
            {
                nacomida = true;
            }
        }// detector de trigger, caso entre
        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.CompareTag("deteccao") && stealth == false)
            {
                detectado = true;
            }
            if (other.CompareTag("deteccao 2") && seguro == false)
            {
                detectado = true;
            }
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("sujeira"))//detecçao de sair da sujeira
            {
                nasujeira = false;
            }
            if (other.CompareTag("comida"))//detecçao de sair da comida
            {
                nacomida = false;
            }
            if (other.CompareTag("memedio"))
            {
                memedio = false;
            }
            if (other.CompareTag("deteccao 2"))
            {
                detectado = false;
            }
            if (other.CompareTag("deteccao"))
            {
                detectado = false;
            }
            if (other.CompareTag("stealthseguro"))
            {
                seguro = false;
                

            }

        }//detector de trigger, caso saia

        IEnumerator dia_passagem_forcada()
        {
            
           
            
            if (vida < 0) { vida = 0; }
            fome -= 15;
            if (fome < 0) { fome = 0; }            
            transform.position = new Vector3(-19.40809f, 7.917951f, 0);            
            demorou.SetActive(true);
            yield return new WaitForSeconds(4);            
            
            SceneManager.LoadScene(3);
            dia_remedio = dia_passagem;
            demorou.SetActive(false);
            yield return new WaitForSeconds(0.1f);
            vida -= 25;



        }


    }
}
