using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
    [SerializeField] private float m_JumpForce = 400f;                  // Amount of force added when the player jumps.
    private Animator m_Anim;            // Reference to the player's animator component.
    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.

    private bool m_Grounded;
    private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    [SerializeField] private LayerMask m_WhatIsGround;


    [SerializeField] GameObject projectilePrefab;
    [SerializeField] GameObject FirePoint;
    // Start is called before the first frame update
    private void Awake()
    {
        m_GroundCheck = transform.Find("GroundCheck");
        m_Anim = GetComponent<Animator>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {

        m_Grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
                m_Grounded = true;
        }
        m_Anim.SetBool("Ground", m_Grounded);

        // Set the vertical animation
        m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);

        var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        FirePoint.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public void Move(float hAxis, bool jump)
    {
        
        m_Anim.SetFloat("Speed", Mathf.Abs(hAxis));

        // Move the character
        m_Rigidbody2D.velocity = new Vector2(hAxis * m_MaxSpeed, m_Rigidbody2D.velocity.y);

        // If the input is moving the player right and the player is facing left...
        if (hAxis > 0 && !m_FacingRight)
        {
            // ... flip the player.
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (hAxis < 0 && m_FacingRight)
        {
            // ... flip the player.
            Flip();
        }

        if (m_Grounded && jump && m_Anim.GetBool("Ground") && m_Rigidbody2D.velocity.y <= Mathf.Epsilon)
        {
            // Add a vertical force to the player.
            m_Grounded = false;
            m_Anim.SetBool("Ground", false);
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce),ForceMode2D.Impulse);
        }

        if(!jump && !m_Grounded && m_Rigidbody2D.velocity.y > 0)
        {
            m_Rigidbody2D.velocity *= .5f;
        }
    }

    public void Fire()
    {
        //Placeholder

        // Transform RealFirePoint = m_FacingRight ? FirePoint.transform : InversedFirePoint.transform;
        //Aim();


        GameObject bullet = Instantiate(projectilePrefab, FirePoint.transform);
        //bullet.GetComponent<Bullet>().source = gameObject;
        //bullet.GetComponent<Rigidbody2D>().AddForce( (FirePoint.transform.position - transform.position) * ForceAmount, ForceMode2D.Force);
        bullet.transform.parent = null;
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;

    }
}
