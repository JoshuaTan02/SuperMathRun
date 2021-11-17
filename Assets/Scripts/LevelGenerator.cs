using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private int LEVEL_DISTANCE = 30;
    public static int DESTROY_DISTANCE = 20;
    private float ypos = 2.74f;
    public GameObject Obstacle1;
    public GameObject Obstacle2;
    public GameObject Obstacle3;
    public GameObject Obstacle4;

    public Transform PlayerPos;

    public float currentObstacleX;
    // Start is called before the first frame update
    void Start()
    {
        CreateObstacle(30,Obstacle1);
        PlayerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentObstacleX-PlayerPos.position.x < LEVEL_DISTANCE){
            CreateObstacle(currentObstacleX+LEVEL_DISTANCE,randomObstacle());
        }
    }

    void CreateObstacle(float xPos,GameObject obstacle){
        GameObject newObstacle = Instantiate(obstacle,new Vector3(xPos,0,0),Quaternion.identity);
        newObstacle.transform.parent=  gameObject.transform;
        newObstacle.AddComponent<Ground>();
        currentObstacleX = newObstacle.GetComponent<Transform>().position.x;
    }

    GameObject randomObstacle(){
        int index = Random.Range(1,5);
        switch (index){
            case 1:
            return Obstacle1;
            case 2:
            return Obstacle2;
            case 3:
            return Obstacle3;
            case 4:
            return Obstacle4;
        }
        return Obstacle1;
    }
}
