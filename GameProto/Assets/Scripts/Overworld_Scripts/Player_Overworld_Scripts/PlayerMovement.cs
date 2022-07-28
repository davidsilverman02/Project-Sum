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

    private Vector3 movement;

    public float grav = 9.8f;
    public float minFallSpeed;
    public float jumpHeight;
    private float maxJumpForce;
    private float currentJumpForce;

    //public BoxCollider2D collider;
    public CharacterController cc;

    // Start is called before the first frame update
    void Start()
    {
        //collider = GetComponent<BoxCollider2D>();
        cc = GetComponent<CharacterController>();
        maxJumpForce = Mathf.Sqrt(grav * jumpHeight * 2);
        currentJumpForce = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        Jump();
        cc.Move(movement * Time.deltaTime);
    }

    // The Player Moves in the overworld
    void Move()
    {
        movingX = Input.GetAxisRaw("Horizontal");
        movingZ = Input.GetAxisRaw("Vertical");

        Vector3 groundMovement = new Vector3(movingX, 0, movingZ);

        groundMovement = Quaternion.FromToRotation(Vector3.forward, transform.forward) * groundMovement;

        groundMovement = groundMovement.normalized * speed;

        movement.x = groundMovement.x;
        movement.z = groundMovement.z;

    }
    
    void Jump()
    {
        if (!cc.isGrounded)
        {
            if (currentJumpForce > 0)
            {
                movement.y = currentJumpForce;
                currentJumpForce -= 10 * Time.deltaTime;
            }
            else if (movement.y > -minFallSpeed)
            {
                movement.y -= grav * Time.deltaTime;
            }
        }
        else
        {
            movement.y = 0;
            if (Input.GetButton("Jump"))
            {
                currentJumpForce = maxJumpForce;
                movement.y = currentJumpForce;
            }
        }
    }
}
