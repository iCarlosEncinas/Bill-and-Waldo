using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpForce = 7f;
    [SerializeField] float punchForce = 3f;
    [SerializeField] float punchForce2 = 7f;

    [SerializeField] float rayDistance = 5f;
    [SerializeField] float rayDistance2 = 5f;
    [SerializeField] float rayDistance3 = 0.2f;
    [SerializeField] Color rayColor = Color.red;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask LuzLayer;

    Animator anim;
    Rigidbody2D rb2D;
    SpriteRenderer spr;
    [SerializeField] GameObject player;

    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {

    }

    void FixedUpdate()
    {
        rb2D.position += Vector2.right * Axis.x * moveSpeed * Time.fixedDeltaTime;
    }

    void Update()
    {
        //transform.Translate(Vector2.right * Axis.x * moveSpeed * Time.deltaTime);
        spr.flipX = FlipSpriteX;
        if(JumpButtom && IsGrounding)
        {
            rb2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            anim.SetTrigger("jump");

        }
        if(IsTouchingAreaRight)
        {
            rb2D.AddForce(Vector2.left * punchForce, ForceMode2D.Impulse);
            //Debug.Log("Estás tocando luz.");
        }
        if(IsTouchingAreaLeft)
        {
            rb2D.AddForce(Vector2.right * punchForce, ForceMode2D.Impulse);
            //Debug.Log("Estás tocando luz.");
        }
        if(IsTouchingAreaUp)
        {
            rb2D.AddForce(Vector2.left * punchForce2, ForceMode2D.Impulse);
            //Debug.Log("Estás tocando luz.");
        }

        //Debug.Log(donePlayer);
        if(donePlayer == true){
            donePlayer = true;
        }
    }

    void LateUpdate()
    {
        anim.SetFloat("AxisX", Mathf.Abs(Axis.x));
        anim.SetBool("ground", IsGrounding);
    }

    Vector2 Axis => new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    bool JumpButtom => Input.GetButtonDown("Jump");
    bool IsGrounding => Physics2D.Raycast(transform.position, Vector2.down, rayDistance, groundLayer);
    bool IsTouchingAreaRight => Physics2D.Raycast(transform.position, Vector2.right, rayDistance2, LuzLayer);
    bool IsTouchingAreaLeft => Physics2D.Raycast(transform.position, Vector2.left, rayDistance2, LuzLayer);
    bool IsTouchingAreaUp => Physics2D.Raycast(transform.position, Vector2.up, rayDistance2, LuzLayer);

    bool FlipSpriteX => Axis.x > 0f ? false : Axis.x < 0f ? true : spr.flipX;

    public bool donePlayer = false;
    

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = rayColor;
        Gizmos.DrawRay(transform.position, Vector2.down * rayDistance);

        Gizmos.color = rayColor;
        Gizmos.DrawRay(transform.position, Vector2.right * rayDistance2);

        Gizmos.color = rayColor;
        Gizmos.DrawRay(transform.position, Vector2.left * rayDistance2); 
        
        Gizmos.color = rayColor;
        Gizmos.DrawRay(transform.position, Vector2.up * rayDistance3); 
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Puerta"))
        {
            donePlayer = true;
            //Debug.Log(donePlayer);
            player.SetActive(false);
        }
    }
}
