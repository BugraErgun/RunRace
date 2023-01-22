using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AIController : MonoBehaviour
{
    public float speed, jumpForce, gravity, verticalVelocity;

    private CharacterController characterController;
    private Animator animator;

    private Vector3 move;

    private bool wallSlide,jump,trampolineJump;
    void Awake()
    {
        characterController= GetComponent<CharacterController>();
        animator = transform.GetChild(0).GetComponent<Animator>();
        gameObject.name = Names.botNames[Random.Range(0,Names.botNames.Length)];

    }

    void Update()
    {
        if (GameManager.instance.finish)
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Dance"))
            {
                animator.SetTrigger("Dance");
                transform.eulerAngles= Vector3.up*180;
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
            jump = true;
            verticalVelocity = 0;
            Raycasting();
     
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
            gravity = 30;
            verticalVelocity -= gravity * Time.deltaTime;
        }
        else
        {
            gravity = 15;
            verticalVelocity-= gravity * Time.deltaTime;
        }
        animator.SetBool("Grounded", characterController.isGrounded);
        animator.SetBool("WallSlide", wallSlide);

        move.Normalize();
        move *= speed;
        move.y = verticalVelocity;
        characterController.Move(move * Time.deltaTime);

    }
    void Raycasting()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position , transform.forward, out hit, 7.5f))
        {
            Debug.DrawLine(transform.position , hit.point,Color.red);
            if (hit.collider.tag=="Wall")
            {
                verticalVelocity = jumpForce;
                animator.SetTrigger("Jump");
            }
        }
    }
    IEnumerator LateJump(float time)
    {
        jump = false;
        wallSlide = true;
        yield return new WaitForSeconds(time);
        if (!characterController.isGrounded)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 180, transform.eulerAngles.z);
            verticalVelocity = jumpForce;
            animator.SetTrigger("Jump");

        }
        jump = true;
        wallSlide = false;


    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.tag == "Wall")
        {
            if (jump)
                StartCoroutine(LateJump(Random.Range(0.2f, .5f)));

            if (verticalVelocity < 0)
                wallSlide = true;
        }

        if (hit.collider.tag == "Trampoline" && characterController.isGrounded)
            trampolineJump = true;

        if (hit.collider.tag == "Slide" && characterController.isGrounded)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y - 180,
                    transform.eulerAngles.z);
        }
        else if (hit.collider.tag == "Slide")
        {
            wallSlide = true;
        }
    }
}
