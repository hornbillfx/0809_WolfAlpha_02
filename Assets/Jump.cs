using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.up * 10;
        }
        if(rb.velocity.y<0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * 2 * Time.deltaTime;

        }else
        {
           rb.velocity += Vector2.up * Physics2D.gravity.y * 2.5f * Time.deltaTime;

        }

    }
}
