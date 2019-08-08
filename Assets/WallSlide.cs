using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSlide : MonoBehaviour
{


    public Transform wallCheckPoint;
    public bool WallCheck;
    public LayerMask WallLayer;
    public Collider[] col;
    public Rigidbody rb;
    public bool Ground;
    // Start is called before the first frame update
    void Start()
    {
        rb=GetComponent<Rigidbody>();
    }
    private void OnCollisionStay(Collision collision)
    {
        if(collision.collider.tag=="Ground")
        {
            Ground = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "Ground")
        {
            Ground = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Ground)
        {
            rb.AddForce(new Vector2(-20, 0));

        }
        
        col = Physics.OverlapSphere(wallCheckPoint.transform.position,0.1f,WallLayer);
        if(col.Length!=0)
        {
         
            if(Ground==false)
            {
                if(rb.velocity.y<0)
                {
                    rb.velocity = new Vector2(rb.velocity.x, -0.7f);
                }
          

            }
        }
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            this.transform.eulerAngles = new Vector3(0,0,0);
            if (Ground)
            {
               rb.AddForce(new Vector2(0f, 500));

            }
            else if(col.Length!=0)
            {
               if( true)
                {
                    rb.AddForce(new Vector2(-20, 30), ForceMode.Impulse);
                    this.transform.eulerAngles = new Vector3(0, 0, 0);

                }
                else
                {
                    rb.AddForce(new Vector2(20, 30), ForceMode.Impulse);
                    this.transform.eulerAngles = new Vector3(0, 180, 0);
                }

            }


        }
    }
}
