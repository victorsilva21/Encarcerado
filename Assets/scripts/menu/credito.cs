using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class credito : MonoBehaviour
{
   [SerializeField] RectTransform creditosposition;
    [SerializeField] GameObject menuvisu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (creditosposition.anchoredPosition.y > -5687)
        {
            creditosposition.Translate(new Vector3(0, -0.005f, 0));
            
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            creditosposition.anchoredPosition = new Vector3(0, 668, 90);
            menuvisu.SetActive(true);
            this.gameObject.SetActive(false);
        }
        
    }
}
