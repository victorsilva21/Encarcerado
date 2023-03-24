using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class configSom : MonoBehaviour
{
    public AudioManager audioM;

    public Slider controleSom;
  
    // Start is called before the first frame update
    void Start()
    {

        
        
    }

    // Update is called once per frame
    void Update()
    {
        if(audioM.musicVolume != controleSom.value)
        {
            audioM.change = true;
            audioM.musicVolume  = controleSom.value;
        }
        else
        {
            audioM.change = false;
        }
    }
    
}
