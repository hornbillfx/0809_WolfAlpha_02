using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{

    public List<GameObject> playerPrefab;
    public static Manager manage;
    public int reach;
    public Button ReloadBtn;
    public Button attackBtn;
    public int id;
    public List<GameObject> TrafficLight = new List<GameObject>();
    public List<GameObject> col = new List<GameObject>();
    public PhotonView pv;
    public List<GameObject> totalPlayer = new List<GameObject>();
    public List<float> PlayerDist = new List<float>();
    public List<Slider> PlayerDistUI = new List<Slider>();
    public List<Image> PlayerDistBG = new List<Image>();
    public List<Color> PlayerDistBGColor = new List<Color>();
    public List<GameObject> levels = new List<GameObject>();



    public float FinalDist;
    public GameObject finish;
  //  public List<float> Distances = new List<float>();
    public Text t1;
    public Text t2;
    public GameObject LocalPlayer;
    public int startCount;
   // public float IncreasedrunSpeed = 55f;

   // public float IncreaseRateSpeed = 1;


    public float BaseSpeed = 100f;
    public Text BaseSpeedtxt;


    public float TargetSpeed = 150;
    public Text TargetSpeedTxt;


    public float MaxRunForce = 50;
    public Text MaxRunForceTxt;


    public float m_JumpForce = 400f;                          // Amount of force added when the player jumps.
     public Text m_JumpForceTxt;

     public float  playerGravityScale, terminalVelocity;
     public Text playerGravityScaleTxt;
    public Text terminalVelocityTxt;

    public Button startBtn;
    public GameObject PlayerReady;

    public int StartSec=30;

    //public void IncreaseRatefunc(int val)
    //{
    //    IncreaseRateSpeed = val;
    //}
    // Start is called before the first frame update

    IEnumerator StartSecRoutine()
    {

        while (StartSec > 0)
        {
            yield return new WaitForSeconds(1f);
            pv.RPC("StartSecfunc", RpcTarget.AllBuffered, null);

        }
    }
    [PunRPC]
    public void StartSecfunc()
    {
        StartSec -= 1;
        t1.text = StartSec.ToString();
    }
    public UIHandler UI;

    IEnumerator Start()
    {
        manage = this;
        UI = GameObject.Find("Launcher").GetComponent<UIHandler>();

        levels[UI.selectedLevel].gameObject.SetActive(true);

        FinalDist = Vector3.Distance(new Vector3(0,0,0),finish.transform.position);
        pv = GetComponent<PhotonView>();
        SpawnPlayer();

        if (pv.IsMine)
        {
            StartCoroutine(StartSecRoutine());

        }
        //  Distances = new List<float>(2);
        //  PhotonNetwork.SetMasterClient(PhotonNetwork.PlayerList[1]);
        t2.text = "prabu";
        if (PlayerPrefs.GetFloat("BaseSpeed") == 0.0f)
        {
            PlayerPrefs.SetFloat("BaseSpeed", 20f);
            BaseSpeedtxt.text = "20";
           
        }
        else
        {
            BaseSpeed = PlayerPrefs.GetFloat("BaseSpeed");
            BaseSpeedtxt.text = BaseSpeed.ToString();
        }

        if (PlayerPrefs.GetFloat("TargetSpeed") == 0.0f)
        {
            PlayerPrefs.SetFloat("TargetSpeed", 30);
            TargetSpeedTxt.text = "30";
        }
        else
        {
            TargetSpeed = PlayerPrefs.GetFloat("TargetSpeed");
            TargetSpeedTxt.text = TargetSpeed.ToString();
        }

        if (PlayerPrefs.GetFloat("MaxRunForce") == 0.0f)
        {
            PlayerPrefs.SetFloat("MaxRunForce", 50f);
            MaxRunForceTxt.text = "50";
        }
        else
        {
            MaxRunForce = PlayerPrefs.GetFloat("MaxRunForce");
            MaxRunForceTxt.text = MaxRunForce.ToString();
        }


        if (PlayerPrefs.GetFloat("m_JumpForce") == 0.0f)
        {
            PlayerPrefs.SetFloat("m_JumpForce", 12f);
            m_JumpForceTxt.text = "12";
        }
        else
        {
           m_JumpForce = PlayerPrefs.GetFloat("m_JumpForce");
            m_JumpForceTxt.text = m_JumpForce.ToString();
        }

        while(LocalPlayer==null)
        {
            yield return null;
        }
        if (PlayerPrefs.GetFloat("playerGravityScale") == 0.0f)
        {
            PlayerPrefs.SetFloat("playerGravityScale", 0.8f);
            playerGravityScaleTxt.text = "0.8";
            //GetComponent<Rigidbody2D>().gravityScale = 0.8f;
            LocalPlayer.GetComponent<Rigidbody2D>().gravityScale = 0.8f;


        }
        else
        {
           playerGravityScale = PlayerPrefs.GetFloat("playerGravityScale");
            playerGravityScaleTxt.text = playerGravityScale.ToString();
            LocalPlayer.GetComponent<Rigidbody2D>().gravityScale = playerGravityScale;
          
            //   gravityScale =.playerGravityScale;
        }


        if (PlayerPrefs.GetFloat("terminalVelocity") == 0.0f)
        {
            PlayerPrefs.SetFloat("terminalVelocity", -20);
          terminalVelocityTxt.text = "-20";


        }
        else
        {
          terminalVelocity = PlayerPrefs.GetFloat("terminalVelocity");
           terminalVelocityTxt.text = terminalVelocity.ToString();

        }


    }
    public void BaseSpeedUI(string num)
    {
        BaseSpeed = float.Parse(num);
        PlayerPrefs.SetFloat("BaseSpeed", BaseSpeed);
        BaseSpeedtxt.text = BaseSpeed.ToString();
        t2.text = BaseSpeed.ToString();

    }

    public void TargetSpeedFunc(string num)
    {
        TargetSpeed = float.Parse(num);
        PlayerPrefs.SetFloat("TargetSpeed", TargetSpeed);
        TargetSpeedTxt.text = TargetSpeed.ToString();
    }

    public void MaxRunForceFunc(string num)
    {
        MaxRunForce = float.Parse(num);

        PlayerPrefs.SetFloat("MaxRunForce", MaxRunForce);
        MaxRunForceTxt.text = MaxRunForce.ToString();
    }

    public void JumpSpeed(string num)
    {
       m_JumpForce = float.Parse(num);
        PlayerPrefs.SetFloat("m_JumpForce",m_JumpForce);
        m_JumpForceTxt.text = m_JumpForce.ToString();


    }

    public void PlayerWeight(string num)
    {
        //  GetComponent<Rigidbody2D>().gravityScale = float.Parse(num);
     playerGravityScale = float.Parse(num);
        // GetComponent<Rigidbody2D>().gravityScale = GetComponent<CharacterController2D>().playerGravityScale;
                LocalPlayer.GetComponent<Rigidbody2D>().gravityScale = playerGravityScale;
        PlayerPrefs.SetFloat("playerGravityScale", playerGravityScale);
        playerGravityScaleTxt.text = playerGravityScale.ToString();
    }

    public void TerminalFallSpeed(string num)
    {
        terminalVelocity = float.Parse(num);
        PlayerPrefs.SetFloat("terminalVelocity",terminalVelocity);
       terminalVelocityTxt.text = terminalVelocity.ToString();
    }
    public AudioControl Ac;
   public void TowardsLobby()
    {
        Ac = GameObject.Find("AudioCtrl").GetComponent<AudioControl>();
        Ac.GetComponent<AudioSource>().Stop();
        Ac.GetComponent<AudioSource>().clip = Ac.BG_Menu;
        Ac.GetComponent<AudioSource>().Play();
        GameObject temp = GameObject.Find("Launcher");
        Destroy(temp);
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel("mainMenu");
    }

   public  void Quit()
    {
        Application.Quit();
    }
  
    void SpawnPlayer()
    {
        UIHandler temp1 = GameObject.Find("Launcher").GetComponent<UIHandler>();
        id = temp1.chosenCharacter;
        print(id);
        GameObject temp = PhotonNetwork.Instantiate(playerPrefab[0].name,playerPrefab[0].transform.position,playerPrefab[0].transform.rotation);
        
        Camera.main.transform.GetComponent<CameraFollow>().target = temp.transform;
       
    }
    public List<float> speed = new List<float>();
    [PunRPC]
    public void distFunc()
    {
       // if(PhotonNetwork.IsMasterClient)
        {
            //Distances.Clear();
            //for (int i = 0; i < totalPlayer.Count; i++)
            //{
            //    float dist = Vector3.Distance(finish.transform.position, totalPlayer[i].transform.position);
            //    Distances.Add(dist);

            //}
           // t1.text = "yes";
            //  t1.text = Distances[0].ToString();
            
        }
        //if (Distances.Count >= 2)
        //{
        //    // t2.text = Distances[1].ToString();
        //    if (Distances[0] > Distances[1])
        //    {
        //        totalPlayer[0].GetComponent<PlayerMovement>().info = "low";
        //        //  t1.text = "slow";
        //        speed.Clear();

        //        speed.Add(IncreasedrunSpeed);
        //        speed.Add(50);
        //        //   totalPlayer[0].GetComponent<PlayerMovement>().runSpeed = 100;
        //        // totalPlayer[1].GetComponent<PlayerMovement>().runSpeed = 50;

        //    }
        //    else
        //    {
        //        totalPlayer[1].GetComponent<PlayerMovement>().info = "high";
        //        //  t1.text = "fast";
        //        speed.Clear();
        //        speed.Add(50);
        //        speed.Add(IncreasedrunSpeed);
        //        //  totalPlayer[0].GetComponent<PlayerMovement>().runSpeed = 50;
        //        //  totalPlayer[1].GetComponent<PlayerMovement>().runSpeed = 100;

        //    }

        //}

    }
   
    // Update is called once per frame
    void Update()
    {
         if(pv.IsMine)
        {
            pv.RPC("distFunc", RpcTarget.AllBuffered, null);

        }
        // distFunc();

    }
}
