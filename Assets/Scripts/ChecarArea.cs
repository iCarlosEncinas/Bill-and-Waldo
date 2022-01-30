using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChecarArea : MonoBehaviour
{
    [SerializeField] float punchForce = 3f;

    [SerializeField] float rayDistance = 5f;
    [SerializeField] Vector3 rayPosition;

    [SerializeField] Color rayColor = Color.red;
    [SerializeField] LayerMask LuzLayer;
    Rigidbody2D rb2D;



    
    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(IsTouchingAreaRight)
        {
            rb2D.AddForce(Vector2.left * punchForce, ForceMode2D.Impulse);
            Debug.Log("Estás tocando luz.");
        }
        if(IsTouchingAreaLeft)
        {
            rb2D.AddForce(Vector2.right * punchForce, ForceMode2D.Impulse);
            Debug.Log("Estás tocando luz.");
        }
    }

    bool IsTouchingAreaRight => Physics2D.Raycast(transform.position, Vector2.right, rayDistance, LuzLayer);
    bool IsTouchingAreaLeft => Physics2D.Raycast(transform.position, Vector2.left, rayDistance, LuzLayer);

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = rayColor;
        Gizmos.DrawRay(transform.position, Vector2.right * rayDistance);

        Gizmos.color = rayColor;
        Gizmos.DrawRay(transform.position, Vector2.left * rayDistance);        
    }
}
