using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineJump : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetMouseButtonDown(0))
        {
            StartCoroutine(Jump());
        }
    }
    Vector3 m_Velocity = Vector3.zero;

    IEnumerator Jump()
    {
        Vector3 targetVelocity = new Vector2(0 , 2500);
        // And then smoothing it out and applying it to the character
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity*Time.deltaTime, ref m_Velocity, 10*Time.deltaTime);
        yield return null;

    }
}
