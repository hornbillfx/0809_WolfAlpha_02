using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ThrowWeapon : MonoBehaviour
{
    private float throwableSpeed = 1500.0f;
    public PhotonView pv;

    // Start is called before the first frame update
    void Start()
    {
        pv = GetComponent<PhotonView>();
        
        this.GetComponent<Rigidbody2D>().velocity = (Time.deltaTime * throwableSpeed) * Vector2.right;
        Debug.Log(this.GetComponent<Rigidbody2D>().velocity);
        Destroy(this.gameObject, 10.0f);

    }
    [PunRPC]
    void characterStop()
    {
        if(enemy!=null)
        {
            enemy.GetComponent<PlayerMovement>().winpos = enemy.transform.position;
            enemy.GetComponent<PlayerMovement>().run = true;
            enemy.GetComponent<Animator>().SetTrigger("Dizzy");
           StartCoroutine(characterWait());
        }
        

    }
    IEnumerator  characterWait()
    {
        yield return new WaitForSeconds(3f);
        pv.RPC("characterStart", RpcTarget.AllBuffered, null);

      

    }
    [PunRPC]
    void characterStart()
    {
     //   

        if (enemy != null)
        {
            enemy.GetComponent<PlayerMovement>().run = false;
            //   enemy.GetComponent<PlayerMovement>().winpos = enemy.transform.position;
        }
        Destroy(this.gameObject, 0.0f);

    }
    public GameObject enemy;
    public GameObject mine;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        print(collision.name);
        if (collision.tag == "Player")
        {
            if(enemy!=this.gameObject)
            {
                if (mine!=collision.gameObject) {
                    enemy = collision.gameObject;
                    //characterStop();
                    if (!pv.IsMine)
                    {
                        pv.RPC("characterStop", RpcTarget.AllBuffered, null);

                    }
                }

            }


        }
        else
        {
          //  Destroy(this.gameObject);
        }
    }
}
