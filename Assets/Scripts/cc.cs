﻿using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class cc : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] public float m_JumpForce ;                          // Amount of force added when the player jumps.
    [SerializeField] public Text m_JumpForceTxt;

    [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;          // Amount of maxSpeed applied to crouching movement. 1 = 100%
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
    [SerializeField] private bool m_AirControl = false;                         // Whether or not a player can steer while jumping;
    [SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
    [SerializeField] public Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
    [SerializeField] private Transform m_CeilingCheck;                          // A position marking where to check for ceilings
    [SerializeField] private Collider2D m_CrouchDisableCollider;                // A collider that will be disabled when crouching

    [SerializeField] private float fallMultiplier;
    [SerializeField] public float lowJumpMultiplier, playerGravityScale, terminalVelocity;
    [SerializeField] public Text playerGravityScaleTxt;
    [SerializeField] public Text terminalVelocityTxt;
    public float RunSpeedReduction = 0.8f;

    const float k_GroundedRadius = .3f; // Radius of the overlap circle to determine if grounded
    public bool m_Grounded;            // Whether or not the player is grounded.
    const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.
    private Vector3 m_Velocity = Vector3.zero;
    public Collider2D[] colliders;

    public Image groundImg;
    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    public BoolEvent OnCrouchEvent;
    private bool m_wasCrouching = false;


    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();

        if (OnCrouchEvent == null)
            OnCrouchEvent = new BoolEvent();
    }

    //private void FixedUpdate()
    //{
    //}
    private void Start()
    {
        //  m_Rigidbody2D.gravityScale *= playerGravityScale;

    }

    private void FixedUpdate()
    {
        //if (GetComponent<PM>().res.Length != 0)
        //{
        //    groundImg.color = Color.green;
        //}
        //else
        //{
        //    groundImg.color = Color.red;

        //}
        bool wasGrounded = m_Grounded;
        m_Grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        //Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        //Collider2D[] colliders = Physics2D.RaycastAll(m_GroundCheck.position, Vector2.down, k_GroundedRadius, m_WhatIsGround);
        colliders = Physics2D.OverlapBoxAll(m_GroundCheck.position, new Vector2(1f, 1), k_GroundedRadius, m_WhatIsGround);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
                Debug.Log("grounded");
                this.transform.eulerAngles = new Vector3(0, 0, 0);

                if (!wasGrounded)
                    OnLandEvent.Invoke();
            }
        }




        if (m_Rigidbody2D.velocity.y < terminalVelocity)
        {
            //Debug.Log(m_Rigidbody2D.velocity.y);
            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, terminalVelocity);

        }


        //if (m_Rigidbody2D.velocity.y < 0)
        //{
        //    m_Rigidbody2D.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier) * Time.deltaTime;
        //}else if (m_Rigidbody2D.velocity.y > 0)
        //{
        //    m_Rigidbody2D.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier) * Time.deltaTime;
        //}
    }

    public void Move(float move, bool crouch, bool jump)
    {

        // If crouching, check to see if the character can stand up
        if (!crouch)
        {
            // If the character has a ceiling preventing them from standing up, keep them crouching
            if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
            {
                crouch = true;
            }
        }

        //only control the player if grounded or airControl is turned on
        if (m_Grounded || m_AirControl)
        {

            // If crouching
            if (crouch)
            {
                if (!m_wasCrouching)
                {
                    m_wasCrouching = true;
                    OnCrouchEvent.Invoke(true);
                }

                // Reduce the speed by the crouchSpeed multiplier
                move *= m_CrouchSpeed;


                // Disable one of the colliders when crouching
                if (m_CrouchDisableCollider != null)
                {
                    //    m_CrouchDisableCollider.enabled = false;
                    //   animator.SetTrigger("2To4");
                    //   animator.ResetTrigger("4To2");

                }

            }
            else
            {
                // Enable the collider when not crouching
                if (m_CrouchDisableCollider != null)
                {
                    //  m_CrouchDisableCollider.enabled = true;
                    //  animator.SetTrigger("4To2");
                    // animator.ResetTrigger("2To4");
                }


                if (m_wasCrouching)
                {
                    m_wasCrouching = false;

                    OnCrouchEvent.Invoke(false);
                }
            }



            if (GetComponent<PM>().res.Length == 0)
            {
                // Move the character by finding the target velocity
                Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
                // And then smoothing it out and applying it to the character
                m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
                //   print(Time.deltaTime);

            }
            else
            {

                // m_Rigidbody2D.AddForce(new Vector2(move * 10f, m_WallJumpForce) * Time.deltaTime, ForceMode2D.Impulse);
            }

            /*
                // If the input is moving the player right and the player is facing left...
                if (move > 0 && !m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
                // Otherwise if the input is moving the player left and the player is facing right...
                else if (move < 0 && m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
            */
        }
        //  If the player should jump...
        if (jump)
        {
            // Add a vertical force to the player.

            if (m_Grounded)
            {
                m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0);
                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce), ForceMode2D.Impulse);
                animator.SetTrigger("Jump");
                move *= RunSpeedReduction;

                m_Grounded = false;
            }
        }
        else
        {

            animator.ResetTrigger("Jump");
        }
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

    public void OnDrawGizmos()
    {
        //Gizmos.DrawCube(m_GroundCheck.position, new Vector3(1f, 1, 0));

        Gizmos.DrawWireCube(m_GroundCheck.position, new Vector3(1f, 1, 0));
    }
}
