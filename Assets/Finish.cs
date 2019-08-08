using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    public GameObject player;
    public GameObject startPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject==player.transform.GetChild(0).transform.GetChild(0).gameObject)
        {
            player.transform.position = startPos.transform.position;
            player.transform.GetChild(0).transform.GetChild(0).gameObject.transform.position = startPos.transform.position;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
