using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    [Header("Rigidbody of Player")]
    [SerializeField] private Rigidbody2D playerBody;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float jumpTime = 0.3f;
    private bool isJumping = false;
    private float jumpTimer = 0f;

    [Header("Crouch")]
    [SerializeField] private float crouchHeight = 0.5f;
    [SerializeField] private Transform GFX;

    [Header("Ground")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundDistance = 0.25f;
    [SerializeField] private Transform feetPos;
    private bool isGrounded = false;

    [Header("ParticleSystem")]
    [SerializeField] private ParticleSystem VFX;

    private void Awake()
    {
        playerBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, groundDistance, groundLayer);    //check if ground layer is touched via feetPos 

        //JUMP
        if (isGrounded && Input.GetButtonDown("Jump"))  //Jump only when on Ground
        {
            isJumping = true;
            jumpTimer = 0f;
            playerBody.velocity = Vector2.up * jumpForce;
        }

        if (isJumping && Input.GetButton("Jump"))
        {
            if (jumpTimer < jumpTime)   //Jump as long as the jumpTime
            {
                playerBody.velocity = new Vector2(playerBody.velocity.x, jumpForce);
                jumpTimer += Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if (Input.GetButtonUp("Jump"))  //let go of Space button and the Jump is done
        {
            isJumping = false;
        }

        //CROUCH
        if (isGrounded && Input.GetButton("Crouch"))    //Chrouch only when on Ground
        {
            GFX.localScale = new Vector3(GFX.localScale.x, crouchHeight, GFX.localScale.z);

            if (isJumping)  //don't Crouch when Jumping
            {
                GFX.localScale = new Vector3(GFX.localScale.x, 1f, GFX.localScale.z);
            }
        }

        
        if (Input.GetButtonUp("Crouch"))    //let go of Shift button and the Crouch is done
        {
            GFX.localScale = new Vector3(GFX.localScale.x, 1f, GFX.localScale.z);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            VFX.Play(); //activate the ParticleSystem when on Ground

            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            VFX.Stop(); //Stop the ParticleSystem in air

            isGrounded = false;
        }
    }
}
