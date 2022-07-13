using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveForce = 10f;
    [SerializeField] private float jumpForce = 11f;
    private float movementX;
    private Rigidbody2D myBody;
    private SpriteRenderer sr;
    private Animator anim;
    private string RUN_ANIMATION = "Run";
    private string JUMP_ANIMATION = "Jump";
    private bool isGrounded = true;
    private string GROUND_TAG = "Ground";
    private string ENEMY_TAG = "Enemy";

    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        AnimatePlayer();
    }

    void FixedUpdate()
    {
        PlayerJump();
    }

    private void PlayerMove()
    {
        movementX = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(movementX, 0f, 0f) * Time.deltaTime * moveForce;
    }

    void AnimatePlayer()
    {
        // moving to right
        if (movementX > 0 && isGrounded)
        {
            anim.SetBool(RUN_ANIMATION, true);
            sr.transform.localScale = new Vector3(8.5f, 8.5f, 8.5f);
        }
        else if (movementX < 0 && isGrounded)
        {
            // moving to left
            anim.SetBool(RUN_ANIMATION, true);
            sr.transform.localScale = new Vector3(-8.5f, 8.5f, 8.5f);
        }
        else
            anim.SetBool(RUN_ANIMATION, false);
    }

    private void PlayerJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isGrounded = false;
            myBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            anim.SetBool(JUMP_ANIMATION, true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(GROUND_TAG))
        {
            isGrounded = true;
            anim.SetBool(JUMP_ANIMATION, false);
        }
        if (collision.gameObject.CompareTag(ENEMY_TAG))
            Destroy(gameObject);
    }
}
