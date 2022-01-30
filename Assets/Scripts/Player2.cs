using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Player2 : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpForce = 7f;
    [SerializeField] float punchForce = 7f;

    [SerializeField] float rayDistance = 5f;
    [SerializeField] float rayDistance2 = 5f;
    [SerializeField] Color rayColor = Color.red;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask LuzLayer;
    [SerializeField] Vector3 originRayRight;
    [SerializeField] Vector3 originRayLeft;
    public Player player;
    Animator anim;
    Rigidbody2D rb2D;
    SpriteRenderer spr;

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
        if(!IsTouchingAreaRight)
        {
            rb2D.AddForce(Vector2.left * punchForce, ForceMode2D.Impulse);
            //Debug.Log("No estás tocando luz.");
        }
        if(!IsTouchingAreaLeft)
        {
            rb2D.AddForce(Vector2.right * punchForce, ForceMode2D.Impulse);
            //Debug.Log("No estás tocando luz.");
        }
        //guardarDone();
    }

    void LateUpdate()
    {
        anim.SetFloat("AxisX", Mathf.Abs(Axis.x));
        anim.SetBool("ground", IsGrounding);
    }

    Vector2 Axis => new Vector2(Input.GetAxis("Horizontal2"), Input.GetAxis("Vertical2"));
    bool JumpButtom => Input.GetButtonDown("Jump2");
    bool IsGrounding => Physics2D.Raycast(transform.position, Vector2.down, rayDistance, groundLayer);
    bool IsTouchingAreaRight => Physics2D.Raycast(transform.position + originRayRight, Vector2.right, rayDistance2, LuzLayer);
    bool IsTouchingAreaLeft => Physics2D.Raycast(transform.position + originRayLeft, Vector2.left, rayDistance2, LuzLayer);

    bool FlipSpriteX => Axis.x > 0f ? false : Axis.x < 0f ? true : spr.flipX;

    bool donePlayer2 = false;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = rayColor;
        Gizmos.DrawRay(transform.position, Vector2.down * rayDistance);

        Gizmos.color = rayColor;
        Gizmos.DrawRay(transform.position + originRayRight, Vector2.right * rayDistance2);

        Gizmos.color = rayColor;
        Gizmos.DrawRay(transform.position + originRayLeft, Vector2.left * rayDistance2); 

    }


bool guardarDone(){
    bool playerDone1 = player.donePlayer;
    return playerDone1;
}
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Puerta"))
        {
            donePlayer2 = true;
            //Debug.Log(donePlayer2);
            
            if(guardarDone() && donePlayer2 == true){
                UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            }
        }
    }


}
