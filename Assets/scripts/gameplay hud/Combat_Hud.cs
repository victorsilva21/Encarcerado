using System.Collections;
using System.Collections.Generic;
using informacoes;
using UnityEngine;

public class Combat_Hud : MonoBehaviour
{
    public GameObject Player;
    public charinfo info;
    public Camera cam;
    Canvas canvas;


    // Start is called before the first frame update
    private void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        canvas = this.GetComponent<Canvas>();
        canvas.worldCamera = cam;
    }
    void Awake()
    {
        Player = GameObject.Find("char");
        info = Player.GetComponent<charinfo>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Player.transform.position;
    }
}
