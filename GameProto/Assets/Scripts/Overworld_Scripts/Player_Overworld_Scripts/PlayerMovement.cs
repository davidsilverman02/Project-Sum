using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    public float speed = 4.5f;
    public float movingX;
    public float movingY;
    public float movingZ;
    public int direct;

    //public BoxCollider2D collider;
    public CharacterController cc;

    // Start is called before the first frame update
    void Start()
    {
        //collider = GetComponent<BoxCollider2D>();
        cc = GetComponent<CharacterController>();
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

        Vector3 movement = new Vector3(movingX, 0, movingZ);

        movement = Quaternion.FromToRotation(Vector3.forward, transform.forward) * movement;

        movement = movement.normalized * speed;

        cc.SimpleMove(movement);
    }
}
