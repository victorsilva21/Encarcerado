using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.SceneManagement;
using informacoes;

public class musica : MonoBehaviour
{
    [SerializeField] AudioClip musica_prologo, musica_prologoLOOP, musica_menu, musica_prisao, musica_furtivo, musica_combate;
    public AudioManager audioM;
    public charinfo info;

    private bool next = false;
    private int music = 1;
    private bool side2 = false;
    private bool prisionCheck = false;
    private bool end = false;

    private IEnumerator timer;
    private IEnumerator player;

    // Start is called before the first frame update
    void Start()
    {
        audioM = this.GetComponent<AudioManager>();
        /*musica_menu = Resources.Load<AudioClip>("musicas/menu_musica");
        musica_prologo = Resources.Load<AudioClip>("musicas/prologo_BEGIN") ;
        musica_prologoLOOP = Resources.Load<AudioClip>("musicas/prologo_LOOP");
        musica_prisao = Resources.Load<AudioClip>("musicas/TEMA_PRISAO");
        musica_furtivo = Resources.Load<AudioClip>("musicas/TEMA_FURTIVO");
        musica_combate = Resources.Load<AudioClip>("musicas/TEMA_COMBATE");
        DontDestroyOnLoad(GameObject.Find("som"));*/

        audioM.Play("Menu");

        //fonteSom = gameObject.GetComponent<AudioSource>();
        //fonteSom.clip = musica_menu;
        //fonteSom.Play();
        StartCoroutine(sequencia_musical());
    }

    // Update is called once per frame
    void Update()
    {
        if (next)
        {
            Debug.Log("Next");
        }

        if(SceneManager.GetActiveScene().name == "refeitorio final" && !end)
        {
            end = true;
            prisionCheck = false;

            StopCoroutine(timer);
            if (player != null)
            {
                StopCoroutine(player);
            }

            audioM.Stop("Menu");
            audioM.Stop("Prologo Begin");
            audioM.Stop("Prologo Loop");
            audioM.Stop("Prologo Loop2");
            audioM.Stop("Combate");
            audioM.Stop("Combate2");
            audioM.Stop("Furtivo");
            audioM.Stop("Furtivo2");
            audioM.Stop("Prisao");
            audioM.Stop("Prisao2");

            next = true;
            music = 1;
            audioM.Play("Prologo Begin");
            timer = Timer();
            StartCoroutine(timer);
        }

        if (prisionCheck)
        {
            if(SceneManager.GetActiveScene().name == "refeitorio"){
                if(music != 4 && info.combat == false)
                {
                    Debug.Log("Musica furtivo");
                    Skip(4);  
                }else if(music != 3 && info.combat == true)
                {
                    Debug.Log("Musica combate");
                    Skip(3);
                }
            }
            else
            {
                if (info.combat == true && music != 3)
                {
                    Debug.Log("Musica combate");
                    Skip(3);
                }
                else
                {
                    if(music != 5 && info.combat == false)
                    {
                        Debug.Log("Musica prisao");
                        Skip(5);
                    }
                }
            }
        }
    }

    IEnumerator sequencia_musical()
    {
        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == "cutscene delegacia");

        audioM.Stop("Menu");
        audioM.Play("Prologo Begin");
        timer = Timer();
        StartCoroutine(timer);

        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == "armazem");

        Skip(4);

        yield return new WaitUntil(() => SceneManager.GetActiveScene().name != "armazem");

        Skip(2);

        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == "intro cadeia");

        Skip(5);
        info = GameObject.Find("char").GetComponent<charinfo>();
        prisionCheck = true;
    }



    /*
    Músicas
        1 - Menu
        2 - Prologo Loop
        3 - Combate
        4 - Furtivo
        5 - Prisao

    */

    void Skip(int nextMusic)
    {
        Debug.Log("Skip");
        StopCoroutine(timer);
        if(player != null)
        {
            StopCoroutine(player);
        }

        audioM.Stop("Menu");
        audioM.Stop("Prologo Begin");
        audioM.Stop("Prologo Loop");
        audioM.Stop("Prologo Loop2");
        audioM.Stop("Combate");
        audioM.Stop("Combate2");
        audioM.Stop("Furtivo");
        audioM.Stop("Furtivo2");
        audioM.Stop("Prisao");
        audioM.Stop("Prisao2");

        next = true;
        music = nextMusic;

        timer = Timer();
        StartCoroutine(timer);
        player = Player();
        StartCoroutine(player);
    }

    public void FullStop()
    {
        prisionCheck = false;
        audioM.Stop("Menu");
        audioM.Stop("Prologo Begin");
        audioM.Stop("Prologo Loop");
        audioM.Stop("Prologo Loop2");
        audioM.Stop("Combate");
        audioM.Stop("Combate2");
        audioM.Stop("Furtivo");
        audioM.Stop("Furtivo2");
        audioM.Stop("Prisao");
        audioM.Stop("Prisao2");
    }

    public void Restart()
    {
        StartCoroutine(InternalRestart());
    }

    IEnumerator InternalRestart()
    {
        Skip(5);

        info = null;

        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == "cadeia principal");

        info = GameObject.Find("char").GetComponent<charinfo>();

        prisionCheck = true;
    }



    IEnumerator Timer()
    {
        if(music == 1)
        {
            yield return new WaitForSeconds(96);
            if (music != 1)
            {
                yield break;
            }

            Skip(2);
        }
        while (music == 2)
        {
            yield return new WaitForSeconds(32f);
            if(music != 2)
            {
                yield break;
            }
            side2 = !side2;
            next = true;
        }
        while (music == 3)
        {
            yield return new WaitForSeconds(75.8f);
            if (music != 3)
            {
                yield break;
            }
            side2 = !side2;
            next = true;
        }
        while (music == 4)
        {
            yield return new WaitForSeconds(64f);
            if (music != 4)
            {
                yield break;
            }
            side2 = !side2;
            next = true;
        }
        while(music == 5)
        {
            yield return new WaitForSeconds(48f);
            if (music != 5)
            {
                yield break;
            }
            side2 = !side2;
            next = true;
        }
    }

    IEnumerator Player()
    {
        yield return new WaitUntil(() => next);
        next = false;

        if (music == 1)
        {
            audioM.Play("Menu");
        }

        while (music == 2)
        {
            if (!side2)
            {
                audioM.Play("Prologo Loop");
            }
            if (side2)
            {
                audioM.Play("Prologo Loop2");
            }

            yield return new WaitUntil(() => next);
            next = false;
        }

        while (music == 3)
        {
            if (!side2)
            {
                audioM.Play("Combate");
            }
            if (side2)
            {
                audioM.Play("Combate2");
            }

            yield return new WaitUntil(() => next);
            next = false;
        }

        while (music == 4)
        {
            if (!side2)
            {
                audioM.Play("Furtivo");
            }
            if(side2)
            {
                audioM.Play("Furtivo2");
            }

            yield return new WaitUntil(() => next);
            next = false;
        }

        while (music == 5)
        {
            if (!side2)
            {
                audioM.Play("Prisao");
            }
            if (side2)
            {
                audioM.Play("Prisao2");
            }

            yield return new WaitUntil(() => next);
            next = false;
        }
    }
}
