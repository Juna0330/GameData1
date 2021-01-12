using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb2d;
    SpriteRenderer spriteRenderer;

    bool isGrounded;

    [SerializeField]
    GameObject attackHitBox;
    [SerializeField]
    GameObject attackHitBox2;
    [SerializeField]
    Transform groundCheck;
    [SerializeField]
    Transform groundCheckL;
    [SerializeField]
    Transform groundCheckR;

    [SerializeField]
    private float runSpeed = 1.5f;
    [SerializeField]
    private float jumpSpeed = 5f;

    bool isAttacking = false;
    bool isHenshin = false;

    private bool isDoubleTapStart;
    private float doubleTapTime;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        attackHitBox.SetActive(false);
        attackHitBox2.SetActive(false);
    }
    void Update()
    {
        if (isDoubleTapStart)
        {
            doubleTapTime += Time.deltaTime;
            if (doubleTapTime < 0.2f)
            {
                if (Input.GetMouseButtonDown(0) && !isAttacking)
                {
                    isDoubleTapStart = false;
                    Attacking(true);
                    doubleTapTime = 0.0f;
                }
            }
            else
            {
                Attacking(false);
                isDoubleTapStart = false;
                doubleTapTime = 0.0f;
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                isDoubleTapStart = true;
            }

        }
           
        
    }
    private void ResetAttack()
    {
        isAttacking = false;
    }
   private void Attacking(bool isDoubleClick)
            {
                if (isDoubleClick)
                {
                    isAttacking = true;
                    animator.Play("Player_attack2");
            StartCoroutine(DoDoAttack());

                }
                else
                {
                    isAttacking = true;
                    animator.Play("Player_attack1");
                    StartCoroutine(DoAttack());
                }
            }
            IEnumerator DoAttack()
            {
                attackHitBox.SetActive(true);
                yield return new WaitForSeconds(.4f);
                attackHitBox.SetActive(false);
                isAttacking = false;
            }
    IEnumerator DoDoAttack()
    {
        attackHitBox.SetActive(true);
        yield return new WaitForSeconds(.2f);
        attackHitBox.SetActive(false);
 
        attackHitBox2.SetActive(true);
        yield return new WaitForSeconds(.2f);
        attackHitBox2.SetActive(false);
        isAttacking = false;
    }
            // Update is called once per frame
            private void FixedUpdate()
    {
        if ((Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"))) ||
            (Physics2D.Linecast(transform.position, groundCheckL.position, 1 << LayerMask.NameToLayer("Ground"))) ||
                (Physics2D.Linecast(transform.position, groundCheckR.position, 1 << LayerMask.NameToLayer("Ground"))))
        {
            isGrounded = true;


        }
        else
        {
            isGrounded = false;
        }
        if (Input.GetKey("d") || Input.GetKey("right"))
        {
            rb2d.velocity = new Vector2(runSpeed, rb2d.velocity.y);

            if (isGrounded && !isAttacking)
                animator.Play("right_walk");

            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (Input.GetKey("a") || Input.GetKey("left"))
        {
            rb2d.velocity = new Vector2(-runSpeed, rb2d.velocity.y);
            if (isGrounded && !isAttacking)
                animator.Play("right_walk");
            transform.localScale = new Vector3(-1, 1, 1);
      
        }
        else if (isGrounded)
        {
   

            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
        }
        if (Input.GetKey("space") && isGrounded)
        {

            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpSpeed);
            animator.Play("Player_jump");

        }
    }
    
}
