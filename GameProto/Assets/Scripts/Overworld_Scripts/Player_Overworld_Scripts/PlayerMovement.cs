using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    public float speed = 4.5f;
    public float movingX;
    public float movingY;
    public float directionX;
    public float directionY;
    public int direct;

    public BoxCollider2D collider;
    public Rigidbody2D rigid;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        spriteDirection();
        Move();
    }

    // The Player Moves in the overworld
    void Move()
    {
        movingX = Input.GetAxisRaw("Horizontal");
        movingY = Input.GetAxisRaw("Vertical");

        directionX = movingX * speed;
        directionY = movingY * speed;

        rigid.velocity = new Vector2(directionX, directionY);
    }

    // Changes Sprite Direction
    void spriteDirection()
    {

    }
}
