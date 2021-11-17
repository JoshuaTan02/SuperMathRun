using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        Player= GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.position = new Vector3(Player.transform.position.x + 5,transform.position.y,transform.position.z);
    }
}
