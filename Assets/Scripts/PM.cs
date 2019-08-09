using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;


public class PM : MonoBehaviour
{
    public cc controller;
    public Rigidbody2D rb2d;
    public Animator animator;
    public float runSpeed = 40f;

    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;
    bool run = false;
    public float BaseSpeed = 100f;
    public Text BaseSpeedtxt;


    public float TargetSpeed = 150;
    public Text TargetSpeedTxt;

    public float MaxRunForce = 50;
    public Text MaxRunForceTxt;


    public float MinRunForce;
    public GameObject btn;

    public GameObject startbtn;


    public AudioControl Ac;

    public float FallMultiplier;
    public float LowJumpMultiplayer;
    public GameObject MainPanel;
    public ControlData controldata;

    public void SinglePlayer()
    {
        Ac = GameObject.Find("AudioCtrl").GetComponent<AudioControl>();

        Ac.GetComponent<AudioSource>().Stop();

        Ac.GetComponent<AudioSource>().clip = Ac.BG_Game;
        Ac.GetComponent<AudioSource>().Play();
        MainPanel.gameObject.SetActive(false);
        startbtn.gameObject.SetActive(true);

    }
    public Vector3 FrontCheckOffset;
    public Vector3 BackCheckOffset;

    private void Start()
    {
        FrontCheckOffset = this.transform.position - frontCheck.transform.position;
        BackCheckOffset = this.transform.position - BackCheck.transform.position;
         //PlayerPrefs.DeleteAll();
        if (PlayerPrefs.GetFloat("BaseSpeed") == 0.0f)
        {
            PlayerPrefs.SetFloat("BaseSpeed", 30f);
            BaseSpeedtxt.text = "30";
            controldata.BaseSpeed = 30;
        }
        else
        {
            BaseSpeed = PlayerPrefs.GetFloat("BaseSpeed");
            BaseSpeedtxt.text = BaseSpeed.ToString();
            controldata.BaseSpeed = BaseSpeed;

        }


        if (PlayerPrefs.GetFloat("TargetSpeed") == 0.0f)
        {
            PlayerPrefs.SetFloat("TargetSpeed", 60f);
            TargetSpeedTxt.text = "60";
            controldata.TargetSpeed = 60;

        }
        else
        {
            TargetSpeed = PlayerPrefs.GetFloat("TargetSpeed");
            TargetSpeedTxt.text = TargetSpeed.ToString();
            controldata.TargetSpeed = TargetSpeed;
        }

        if (PlayerPrefs.GetFloat("MaxRunForce") == 0.0f)
        {
            PlayerPrefs.SetFloat("MaxRunForce", 100f);
            MaxRunForceTxt.text = "100";
            controldata.MaxRunForce = 100;
        }
        else
        {
            MaxRunForce = PlayerPrefs.GetFloat("MaxRunForce");
            MaxRunForceTxt.text = MaxRunForce.ToString();
            controldata.MaxRunForce = MaxRunForce;
        }


        if (PlayerPrefs.GetFloat("m_JumpForce") == 0.0f)
        {
            PlayerPrefs.SetFloat("m_JumpForce", 25f);
            GetComponent<cc>().m_JumpForce = 25f;
            GetComponent<cc>().m_JumpForceTxt.text = "25";
            controldata.m_JumpForce = 25;
        }
        else
        {
            GetComponent<cc>().m_JumpForce = PlayerPrefs.GetFloat("m_JumpForce");
            GetComponent<cc>().m_JumpForceTxt.text = GetComponent<cc>().m_JumpForce.ToString();
            controldata.m_JumpForce = GetComponent<cc>().m_JumpForce;

        }


        if (PlayerPrefs.GetFloat("playerGravityScale") == 0.0f)
        {
            PlayerPrefs.SetFloat("playerGravityScale", 5f);
            GetComponent<cc>().playerGravityScaleTxt.text = "5";
            GetComponent<Rigidbody2D>().gravityScale = 5f;
            controldata.playerGravityScale = 5f;


        }
        else
        {
            GetComponent<cc>().playerGravityScale = PlayerPrefs.GetFloat("playerGravityScale");
            GetComponent<cc>().playerGravityScaleTxt.text = GetComponent<cc>().playerGravityScale.ToString();
            GetComponent<Rigidbody2D>().gravityScale = GetComponent<cc>().playerGravityScale;
            controldata.playerGravityScale = GetComponent<cc>().playerGravityScale;

        }


        if (PlayerPrefs.GetFloat("terminalVelocity") == 0.0f)
        {
            PlayerPrefs.SetFloat("terminalVelocity", -100f);
            GetComponent<cc>().terminalVelocityTxt.text = "-100";
            controldata.terminalVelocity = -100;

        }
        else
        {
            GetComponent<cc>().terminalVelocity = PlayerPrefs.GetFloat("terminalVelocity");
            GetComponent<cc>().terminalVelocityTxt.text = GetComponent<cc>().terminalVelocity.ToString();
            controldata.terminalVelocity = GetComponent<cc>().terminalVelocity;

        }

        if (PlayerPrefs.GetFloat("walljumpAmplitudeLeft") == 0.0f)
        {
            PlayerPrefs.SetFloat("walljumpAmplitudeLeft", 3);
            walljumpAmplitudeLeftTxt.text = "3";
            walljumpAmplitudeLeft = 3;
            controldata.walljumpAmplitudeLeft = 3;
        }
        else
        {
            walljumpAmplitudeLeft = PlayerPrefs.GetFloat("walljumpAmplitudeLeft");
            walljumpAmplitudeLeftTxt.text = walljumpAmplitudeLeft.ToString();
            controldata.walljumpAmplitudeLeft = walljumpAmplitudeLeft;

        }

        if (PlayerPrefs.GetFloat("walljumpForceLeft") == 0.0f)
        {
            PlayerPrefs.SetFloat("walljumpForceLeft", 12.5f);
            walljumpForceLeftTxt.text = "12.5";
            walljumpForceLeft = 12.5f;
            controldata.walljumpForceLeft = walljumpForceLeft;

        }
        else
        {
            walljumpForceLeft = PlayerPrefs.GetFloat("walljumpForceLeft");
            walljumpForceLeftTxt.text = walljumpForceLeft.ToString();
            controldata.walljumpForceLeft = walljumpForceLeft;

        }
        if (PlayerPrefs.GetFloat("WallSlideGravity") == 0.0f)
        {
            PlayerPrefs.SetFloat("WallSlideGravity", 12f);

            WallSlideGravityTxt.text = "12";
            controldata.WallSlideGravity = 12f;
            //  UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
        else
        {
            WallSlideGravity = PlayerPrefs.GetFloat("WallSlideGravity");
            WallSlideGravityTxt.text = WallSlideGravity.ToString();
            controldata.WallSlideGravity = WallSlideGravity;
        }

    }

    // Update is called once per frame

  
    public void BaseSpeedFunc(string num)
    {
        BaseSpeed = float.Parse(num);
        PlayerPrefs.SetFloat("BaseSpeed", BaseSpeed);
        BaseSpeedtxt.text = BaseSpeed.ToString();
        controldata.BaseSpeed = BaseSpeed;

    }
    public void TargetSpeedFunc(string num)
    {
        TargetSpeed = float.Parse(num);
        PlayerPrefs.SetFloat("TargetSpeed", TargetSpeed);
        TargetSpeedTxt.text = TargetSpeed.ToString();
        controldata.TargetSpeed = TargetSpeed;
    }
    public void MaxRunForceFunc(string num)
    {
        MaxRunForce = float.Parse(num);

        PlayerPrefs.SetFloat("MaxRunForce", MaxRunForce);
        MaxRunForceTxt.text = MaxRunForce.ToString();
        controldata.MaxRunForce = MaxRunForce;
    }
    public void MinRunForceFunc(string num)
    {
        MinRunForce = float.Parse(num);
    }
    public void JumpSpeed(string num)
    {
        GetComponent<cc>().m_JumpForce = float.Parse(num);
        PlayerPrefs.SetFloat("m_JumpForce", GetComponent<cc>().m_JumpForce);
        GetComponent<cc>().m_JumpForceTxt.text = GetComponent<cc>().m_JumpForce.ToString();
        controldata.m_JumpForce = GetComponent<cc>().m_JumpForce;
    }
    public void TerminalFallSpeed(string num)
    {
        GetComponent<cc>().terminalVelocity = float.Parse(num);
        PlayerPrefs.SetFloat("terminalVelocity", GetComponent<cc>().terminalVelocity);
        GetComponent<cc>().terminalVelocityTxt.text = GetComponent<cc>().terminalVelocity.ToString();
        controldata.terminalVelocity = GetComponent<cc>().terminalVelocity;
    }
    public void PlayerWeight(string num)
    {
        //  GetComponent<Rigidbody2D>().gravityScale = float.Parse(num);
        GetComponent<cc>().playerGravityScale = float.Parse(num);
        GetComponent<Rigidbody2D>().gravityScale = GetComponent<cc>().playerGravityScale;
        PlayerPrefs.SetFloat("playerGravityScale", GetComponent<cc>().playerGravityScale);
        GetComponent<cc>().playerGravityScaleTxt.text = GetComponent<cc>().playerGravityScale.ToString();
        controldata.playerGravityScale = GetComponent<cc>().playerGravityScale;
    }

    public void JumpSPeedReductiom(string num)
    {
        GetComponent<cc>().RunSpeedReduction = float.Parse(num);
    }

    public void Startx()
    {
        runSpeed = 10;
        MinRunForce = MaxRunForce / 10.0f;


        run = true;
        btn.gameObject.SetActive(false);
    }
    void Update()
    {
        if (Input.touchCount == 0)
        {
            DOTouchCount = true;
        }
        frontCheck.transform.position = this.transform.position - FrontCheckOffset;
        BackCheck.transform.position = this.transform.position - BackCheckOffset;

        if (run)
        {
            //  runSpeed = Mathf.Lerp(100, 25, Time.deltaTime * 20f);
            if (runSpeed > BaseSpeed && runSpeed < TargetSpeed)
            {
                runSpeed += Time.deltaTime * MinRunForce;
            }

            if (runSpeed < BaseSpeed && runSpeed > 0f)
            {
                runSpeed += Time.deltaTime * MaxRunForce;
                //  Debug.Log(runSpeed);
            }

        }
        else
        {
            runSpeed = 0;
        }
        horizontalMove = runSpeed;

        if (Input.GetKeyDown(KeyCode.Space) && runSpeed == 0)
        {
            runSpeed = 10;
            run = true;
        }

      //  if (Input.GetKeyDown(KeyCode.RightArrow))
      //  {
            //   rb2d.AddTorque(1000.0f, ForceMode2D.Impulse);
            //   rb2d.AddForce(Vector2.right * 100.0f, ForceMode2D.Impulse);

            //animator.SetTrigger("2To4");
      //  }

        if (Input.touchCount > 1 || Input.GetKey(KeyCode.DownArrow))
        {
            print("down");
            crouch = true;
            animator.SetTrigger("2To4");

        }
        else
        {

        }
        if ((Input.touchCount > 0 && Input.touchCount < 2) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            print("down");

            animator.ResetTrigger("2To4");
            animator.SetTrigger("4To2");
            crouch = false;

        }

        if (Input.GetMouseButton(0))
        {
            jump = true;
            //animator.SetTrigger("Jump");
        }
        else
        {
            jump = false;
        }
    }
    public Transform wallCheckPoint;
    public LayerMask WallLayer;
    public Collider2D[] res;
    public int num;
    public bool WallJumpActive;
    public float walljumpAmplitudeLeft;
    public Text walljumpAmplitudeLeftTxt;

    public float walljumpForceLeft;
    public Text walljumpForceLeftTxt;

    public float WallSlideGravity;
    public Text WallSlideGravityTxt;

    public GameObject frontCheck;
    public GameObject BackCheck;
    public bool DOTouchCount;


    public void WallJumpAmpLeftFunc(string num)
    {
        walljumpAmplitudeLeft = float.Parse(num);
        walljumpAmplitudeLeftTxt.text = walljumpAmplitudeLeft.ToString();

        PlayerPrefs.SetFloat("walljumpAmplitudeLeft", walljumpAmplitudeLeft);
        controldata.walljumpAmplitudeLeft = walljumpAmplitudeLeft;
        // GetComponent<CharacterController2D>().m_WallJumpForceTxt.text = GetComponent<CharacterController2D>().m_WallJumpForce.ToString();

    }
    public void WallJumpforceLeftFunc(string num)
    {
        walljumpForceLeft = float.Parse(num);
        walljumpForceLeftTxt.text = walljumpForceLeft.ToString();
        controldata.walljumpForceLeft = walljumpForceLeft;
        PlayerPrefs.SetFloat("walljumpForceLeft", walljumpForceLeft);
        // PlayerPrefs.SetFloat("m_WallJumpForce", GetComponent<CharacterController2D>().m_WallJumpForce);
        // GetComponent<CharacterController2D>().m_WallJumpForceTxt.text = GetComponent<CharacterController2D>().m_WallJumpForce.ToString();

    }
  
    public void WallSlideGravityFunc(string num)
    {
        WallSlideGravity = float.Parse(num);
        WallSlideGravityTxt.text = WallSlideGravity.ToString();
        controldata.WallSlideGravity = WallSlideGravity;
        PlayerPrefs.SetFloat("WallSlideGravity", WallSlideGravity);
        // PlayerPrefs.SetFloat("m_WallJumpForce", GetComponent<CharacterController2D>().m_WallJumpForce);
        // GetComponent<CharacterController2D>().m_WallJumpForceTxt.text = GetComponent<CharacterController2D>().m_WallJumpForce.ToString();

    }
    IEnumerator WallJumpRoutine()
    {
        WallJumpActive = true;
        yield return null;
        //  yield return new WaitForSeconds(0.5f);
        WallJumpActive = false;

    }
    public bool NormalMove;
    public float wallCheckRadius = 0.15f;

    public bool straigtJump;
    private void FixedUpdate()
    {

        //res = Physics2D.OverlapCircleAll(wallCheckPoint.transform.position, wallCheckRadius, WallLayer);
        
        res = Physics2D.OverlapBoxAll(wallCheckPoint.position, new Vector2(wallCheckRadius, 2f), wallCheckRadius, WallLayer);

        if (GetComponent<cc>().m_Grounded == false && res.Length == 0)
        {
            if (NormalMove == false)
            {
                controller.Move(horizontalMove * Time.deltaTime, crouch, false);

            }

        }
        else
        {
            controller.Move(horizontalMove * Time.deltaTime, crouch, false);

        }

        if (!GetComponent<cc>().m_Grounded && res.Length != 0)
        {
            if (straigtJump)
            {
                //  print("ssss");
                StartCoroutine(WallJumpRoutine());

                straigtJump = false;
            }
        }
        if (GetComponent<cc>().m_Grounded)
        {
            if (straigtJump == false)
            {
                straigtJump = true;
            }
            if (this.GetComponent<SpriteRenderer>().flipX)
            {
                this.GetComponent<SpriteRenderer>().flipX = false;

            }
            if (res.Length == 0)
            {
                animator.SetBool("wallslide", false);

                if (Input.touchCount == 1 && !MainPanel.activeInHierarchy && !btn.activeInHierarchy || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    if (DOTouchCount)
                    {
                        controller.Move(0 * Time.deltaTime, crouch, true);
                        DOTouchCount = false;
                    }
                    // controller.Move(horizontalMove * Time.deltaTime, crouch, true);

                }
                else
                {
                    if (wallCheckPoint != frontCheck)
                    {
                        wallCheckPoint = frontCheck.transform;
                    }
                    //     controller.Move(horizontalMove * Time.deltaTime, crouch, false);
                    if (NormalMove == true)
                    {
                        NormalMove = false;
                    }
                }


            }
            else
            {

                if (Input.touchCount == 1 || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    if (DOTouchCount)
                    {
                        if (NormalMove == false)
                        {
                            print("as");

                            runSpeed = BaseSpeed - 1;
                            NormalMove = true;

                        }
                        controller.Move(0 * Time.deltaTime, crouch, true);
                        //  StartCoroutine(WallJumpRoutine());
                        print("jump");
                        DOTouchCount = false;
                    }
                }

            }

        }
        else if (GetComponent<cc>().m_Grounded == false && res.Length != 0)
        {
            if (NormalMove == false)
            {
                print("as");
                runSpeed = BaseSpeed - 1;
                NormalMove = true;

            }

            if (Input.touchCount == 1 || Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (DOTouchCount)
                {
                    if (WallJumpActive == false)
                    {
                        if (wallCheckPoint.gameObject == BackCheck)
                        {

                            if (WallJumpActiveBool == false)
                            {
                                // if (res[0].GetComponent<WallDirectionCheck>().Flip == false)
                                this.GetComponent<SpriteRenderer>().flipX = false;
                                print("walljump1" + res[0]);
                                BoxCollider2D temp = res[0].GetComponent<BoxCollider2D>();

                                StartCoroutine(WallJumpRoutine());

                                StartCoroutine(Walljumpactivate(true, temp));
                                WallJumpActiveBool = true;

                            }

                        }
                        else if (wallCheckPoint.gameObject == frontCheck)
                        {
                            if (WallJumpActiveBool == false)
                            {
                                //   if (res[0].GetComponent<WallDirectionCheck>().Flip == false)
                                this.GetComponent<SpriteRenderer>().flipX = true;

                                BoxCollider2D temp = res[0].GetComponent<BoxCollider2D>();


                                StartCoroutine(Walljumpactivate(false, temp));
                                StartCoroutine(WallJumpRoutine());

                                print("walljump2" + res[0]);
                                WallJumpActiveBool = true;
                            }


                        }
                    }
                }
            }

        }
        if (GetComponent<cc>().m_Grounded == false && res.Length != 0)
        {

            if (rb2d.velocity.y < 0)
            {
                //  print(GetComponent<CharacterController2D>().m_Grounded);
                animator.SetBool("wallslide", true);
                rb2d.velocity = new Vector2(rb2d.velocity.x, -WallSlideGravity);
                //  rb2d.velocity = new Vector2(rb2d.velocity.x, GetComponent<CharacterController2D>().terminalVelocity);
            }
            else
            {
                animator.SetBool("wallslide", false);

            }

        }
        else if (GetComponent<cc>().m_Grounded == false && res.Length == 0)
        {
            animator.SetBool("wallslide", false);

        }
        else if (GetComponent<cc>().m_Grounded == true && res.Length != 0)
        {
            animator.SetBool("wallslide", false);

        }



        jump = false;
    }
    IEnumerator coliderOff(BoxCollider2D temp)
    {

        //  Debug.LogError(temp.enabled);
        yield return new WaitForSeconds(0.2f);
        if (temp.GetComponent<BoxCollider2D>())
        {
            temp.enabled = true;
        }
        WallJumpActiveBool = false;


    }
    IEnumerator lateFlip(Vector3 temp)
    {
        // yield return new WaitForSeconds(0.3f);
        //while(res.Length!=0)
        //{
        //    yield return null;
        //}
        yield return new WaitForSeconds(0.3f);
        this.transform.eulerAngles = temp;

    }
    public bool WallJumpActiveBool;
    IEnumerator Walljumpactivate(bool front, BoxCollider2D temp)
    {
        if (temp.GetComponent<BoxCollider2D>())
        {
            temp.enabled = false;

        }
        yield return null;
        if (front)
        {
            transform.position = new Vector2(transform.position.x + 0.5f, transform.position.y);
            Debug.LogError("i");
            rb2d.velocity = new Vector2(0, 0);
            rb2d.angularVelocity = 0f;
            rb2d.AddForce(new Vector2((walljumpForceLeft * 2.5f * 1000f * Time.deltaTime), (GetComponent<cc>().m_JumpForce * walljumpAmplitudeLeft * 1000f * Time.deltaTime)));

            wallCheckPoint = frontCheck.transform;

        }
        else
        {
            transform.position = new Vector2(transform.position.x - 0.5f, transform.position.y);
            rb2d.velocity = new Vector2(0, 0);
            rb2d.angularVelocity = 0f;
            rb2d.AddForce(new Vector2((-walljumpForceLeft * 1000f * 2.5f * Time.deltaTime), (GetComponent<cc>().m_JumpForce * walljumpAmplitudeLeft * 1000f * Time.deltaTime)));

            wallCheckPoint = BackCheck.transform;

        }
        yield return null;
        StartCoroutine(coliderOff(temp));

    }
    public void Crouch()
    {
        crouch = true;
    }
    public void QuitApp()
    {
        Application.Quit();
    }
    public void ReloadApp()
    {
        Ac.GetComponent<AudioSource>().Stop();
        Destroy(Ac.gameObject);
        Destroy(GameObject.Find("ControlData").gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnDrawGizmos()
    {
        //Gizmos.DrawSphere(wallCheckPoint.position, wallCheckRadius);
        Gizmos.DrawWireCube(wallCheckPoint.position, new Vector3(wallCheckRadius, 2f, 3));
    }
}
