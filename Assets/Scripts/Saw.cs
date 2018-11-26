using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour {

    private float PosX1;
    private float PosY1;
    public float PosX2;
    public float PosY2;

    private bool moveToPos2;
    private bool moveToPos1;

    public float movSpeed;


    // Use this for initialization
    void Start () {
        PosX1 = gameObject.transform.position.x;
        PosY1 = gameObject.transform.position.y;

        moveToPos2 = true;
        moveToPos1 = false;
	}
	
	// Update is called once per frame
	void Update () {

        if (moveToPos2 == true)
        {
            //moove to Pos2
            GetComponent<Rigidbody2D>().velocity = new Vector2(movSpeed, GetComponent<Rigidbody2D>().velocity.y);
            if(gameObject.transform.position.x > PosX2)
            {
                moveToPos2 = false;
                moveToPos1 = true;
            }
        }
        if(moveToPos1 == true)
        {
            //moove to Pos1
            GetComponent<Rigidbody2D>().velocity = new Vector2(-movSpeed, GetComponent<Rigidbody2D>().velocity.y);
            if (gameObject.transform.position.x < PosX1)
            {
                moveToPos2 = true;
                moveToPos1 = false;
            }

        }
		
    }
}
