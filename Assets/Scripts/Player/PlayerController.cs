using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   
    public float speed, jumpForce, gravity, verticalVelocity;

    private CharacterController characterController;
    private Animator animator; 
    private Vector3 move;
    
    private bool wallSlide,turn,trampolineJump;

    private SkinnedMeshRenderer playerColor;
    public Material[] Colors;
    

    void Awake()
    {
        playerColor=GameObject.Find("PlayerColor").GetComponent<SkinnedMeshRenderer>();
        animator = transform.GetChild(0).GetComponent<Animator>();
        characterController= GetComponent<CharacterController>();
        gameObject.name = PlayerPrefs.GetString("PlayerName","Player");

        playerColor.material = Colors[PlayerPrefs.GetInt("PlayerColor", 0)];
    }

    void Update()
    {
        if (GameManager.instance.finish)
        {
            move = Vector3.zero;
            if (!characterController.isGrounded)
            {
                verticalVelocity -= gravity * Time.deltaTime;
            }
            else
            {
                verticalVelocity = 0;

            }
            move.y = verticalVelocity;
            characterController.Move(new Vector3(0,move.y*Time.deltaTime,0));
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Dance"))
            {
                animator.SetTrigger("Dance");
                transform.eulerAngles = Vector3.up * 180;
            }
            return;
        }
        if (!GameManager.instance.start)
        {
            return;
        }
        move = Vector3.zero;
        move = transform.forward;
        if (characterController.isGrounded)
        {
            wallSlide = false;
            verticalVelocity= 0f;
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                Jump();

            }
            if (turn)
            {
                turn = false;
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 180,
                    transform.eulerAngles.z);
            }
            
        }
        if (trampolineJump)
        {
            trampolineJump = false;
            verticalVelocity = jumpForce * 1.75f;

            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
            {
                animator.SetTrigger("Jump");
            }
        }

        if (!wallSlide)
        {
            gravity = 30f;
            verticalVelocity -= gravity * Time.deltaTime;

        }
        else
        {
            gravity = 15;
            verticalVelocity -= gravity * .5f * Time.deltaTime;

        }

        animator.SetBool("WallSlide", wallSlide);
        animator.SetBool("Grounded", characterController.isGrounded);

        move.Normalize();
        move *= speed;
        move.y = verticalVelocity;
        characterController.Move(move * Time.deltaTime);
       
    }

    void Jump()
    {
        verticalVelocity = jumpForce;
        animator.SetTrigger("Jump");
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (!characterController.isGrounded)
        {
            if (hit.collider.tag == "Wall" || hit.collider.tag=="Slide")
            {
                if (verticalVelocity<-.2f)
                {
                    wallSlide = true;
                }
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                {
                    Jump();   
                    transform.eulerAngles = 
                        new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 180, transform.eulerAngles.z);
                    wallSlide= false;
                }
            }
        }
        else
        {
            if (hit.collider.tag=="Trampoline"&&characterController.isGrounded)
            {
                trampolineJump = true;
            }
            if (transform.forward != hit.collider.transform.up && transform.forward !=hit.transform.right &&
                hit.collider.tag == "Ground" && !turn)
                turn = true;
        }
    }

}
