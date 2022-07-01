using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    public float speed = 4.5f;
    public float movingX;
    public float movingY;
    public float movingZ;
    public float directionX;
    public float directionY;
    public float directionZ;
    public int direct;

    //public BoxCollider2D collider;
    public Rigidbody rigid;

    // Start is called before the first frame update
    void Start()
    {
        //collider = GetComponent<BoxCollider2D>();
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    // The Player Moves in the overworld
    void Move()
    {
        movingX = Input.GetAxisRaw("Horizontal");
        movingY = 0;
        movingZ = Input.GetAxisRaw("Vertical");

        directionX = movingX * speed;
        directionZ = movingZ * speed;

        rigid.velocity = new Vector3(directionX, 0, directionZ);
    }
}
