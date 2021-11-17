using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public bool speed = true;
    float offsetX;
    Material mat;
    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if(speed){

        offsetX+=0.0005f;
        mat.SetTextureOffset("_MainTex",new Vector2(offsetX,0));

        }
    }
}
