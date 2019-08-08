using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PowerUp : MonoBehaviour
{
    public bool canAttack = false;
    public Animator animator;
    public Button attackButton;
    public float throwableSpeed = 500.0f;

    public GameObject shuriken;
    public PhotonView pv;

    private void Start()
    {
      //  animator = GetComponent<Animator>();
        attackButton = GetComponent<PlayerMovement>().manage.attackBtn;
        attackButton.onClick.AddListener(() => Attack());
    }

    private void Update()
    {
        
      //  attackButton.GetComponent<Button>().enabled = canAttack;
      //  attackButton.GetComponent<Image>().enabled = canAttack;    
    }


    public void Attack()
    {
        animator.SetTrigger("Attack");
        canAttack = false;

        float move = GetComponent<PlayerMovement>().runSpeed;
        move *= .1f;
        if(pv.IsMine)
        {
            GameObject throwable = PhotonNetwork.Instantiate(shuriken.name, this.transform.position, Quaternion.identity);
            throwable.GetComponent<ThrowWeapon>().mine = this.gameObject;
            attackButton.GetComponent<Button>().enabled = false;
            attackButton.GetComponent<Image>().enabled = false;
        }
        //  GameObject throwable = Instantiate(shuriken, this.GetComponent<Transform>(), false);
        Debug.Log(move);
        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //   Debug.Log("Triggered");

        if (collision.tag == "Shuriken")
        {
            canAttack = true;
            GameObject collide = collision.gameObject;
          //  collide.SetActive(false);
        }
    }
}
