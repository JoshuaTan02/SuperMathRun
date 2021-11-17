using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    private Transform playerPos;
    public static int DESTROY_DISTANCE = 20;

    // Start is called before the first frame update
    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerPos.position.x - transform.position.x > DESTROY_DISTANCE){
            Destroy(this.gameObject);
        }
    }
}
