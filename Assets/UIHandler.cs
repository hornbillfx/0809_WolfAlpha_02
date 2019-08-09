using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class UIHandler : MonoBehaviourPunCallbacks
{
    public InputField createRoom;
    public InputField JoinRoom;
  //  public Text instruction;
    public PhotonView pv;
    public Button character1;
    public Button character2;
    public int chosenCharacter;
    public GameObject wait_player;
    public int PlayerCount;
    // Start is called before the first frame update

    public float BaseSpeed;
    public Text BaseSpeedtxt;

    public float TargetSpeed;
    public float MaxRunSpeed;
    public float JumpSpeed;
    public float GravityMultiplier;
    public float TerminalSpeed;
    public GameObject ControllerMenu;
    public bool EnteredFirst;

    public void Onclick_CreateRoom()
        {
        PhotonNetwork.CreateRoom(createRoom.text,new RoomOptions { MaxPlayers = 4 },null);
        EnteredFirst = true;
        //    pv.RPC("Incr", RpcTarget.AllBufferedViaServer, null);

    }
    [PunRPC]
     void BaseSpeedFunc()
    {
        BaseSpeed = BaseSpeed;
    }

    
    public void BaseSpeedUI(string num)
    {
        BaseSpeed = float.Parse(num);
        BaseSpeedtxt.text = BaseSpeed.ToString();
        pv.RPC("BaseSpeedFunc", RpcTarget.AllBuffered, null);


    }

    [PunRPC]
    public void Controller()
    {
        print("hello");
       
        PlayerCount += 1;
     //   StartCoroutine(ControllerRoutine());
       
    }
    IEnumerator ControllerRoutine()
    {
        yield return new WaitForSeconds(1f);
        //if (PlayerCount == 1)
        //{
        //    ControllerMenu.gameObject.SetActive(true);
        //    character1.gameObject.SetActive(false);
        //    character2.gameObject.SetActive(false);

        //}
    }

        public void onclick_JoinRoom()
    {
        PhotonNetwork.JoinRoom(JoinRoom.text,null);

    }
    public override void OnConnectedToMaster()
    {

    }
    public override void OnJoinedRoom()
    {

        //  
        pv.RPC("Controller", RpcTarget.AllBuffered, null);

        createRoom.gameObject.SetActive(false);
         JoinRoom.gameObject.SetActive(false);
       //  character1.gameObject.SetActive(true);
       if(EnteredFirst)
        {
            LevelSelectUI.gameObject.SetActive(true);
        }else
        {
            character1.gameObject.SetActive(true);
        }
        //  character2.gameObject.SetActive(true);

        //  StartCoroutine(waiting());
        //char1();
        if (pv.IsMine)
        {
            
          //  pv.RPC("char1", RpcTarget.AllBuffered, null);

        }



    }
    public int i = 0;
    public GameObject LevelSelectUI;
    public List<Button> LevelBtn = new List<Button>();
    public List<int> LevelSelectedCount = new List<int>();
    public int selectedLevel = 0;
    public void LevelSelectBtnListen(int temp)
    {
        pv.RPC("levelSelectSync", RpcTarget.AllBuffered, temp);

        if (pv.IsMine)
        {
            //  selectedLevel = temp;
            Debug.LogError("error");

            //   pv.RPC("levelSelectSync ", RpcTarget.AllBuffered, null);

        }

        // LevelSelectedCount[temp] += 1;
        for (int i = 0; i < LevelBtn.Count; i++)
        {
            LevelBtn[i].interactable = false;

        }
      //  pv.RPC("Incr", RpcTarget.AllBuffered, null);

        //   pv.RPC("char1", RpcTarget.AllBuffered, null);

    }

    [PunRPC]
    public void levelSelectSync(int temp)
    {
        //  LevelSelectedCount[temp] += 1;
        selectedLevel = temp;
        //for (int i = 0; i < LevelSelectedCount.Count; i++)
        //{
        //    if (selectedLevel < LevelSelectedCount[i])
        //    {
        //        selectedLevel = i;
        //    }
        //}
        LevelSelectUI.gameObject.SetActive(false);
        if(EnteredFirst)
        {
            character1.gameObject.SetActive(true);

        }

    }
    [PunRPC]
    public void char1()
    {
        character1.gameObject.SetActive(false);

        character2.gameObject.SetActive(false);
        chosenCharacter = 1;
       // instruction.text = chosenCharacter.ToString() ;
       
            pv.RPC("Incr", RpcTarget.AllBuffered, null);

        
        wait_player.gameObject.SetActive(true);
      //  LevelSelectUI.gameObject.SetActive(true);
        //  PhotonNetwork.LoadLevel("0716Level01");


    }
    public void TowardsLobby()
    {
          PhotonNetwork.LeaveRoom();
        Destroy(GameObject.Find("AudioCtrl").gameObject);
      //  Destroy(GameObject.Find("AudioCtrl").gameObject);

        PhotonNetwork.LoadLevel(0);
    }
   public void oNQuit()
    {
        Application.Quit();
    }
    [PunRPC]
    public void char2()
    {
        character1.gameObject.SetActive(false);
        character2.gameObject.SetActive(false);
        chosenCharacter = 2;
      //  instruction.text = chosenCharacter.ToString() ;
        pv.RPC("Incr", RpcTarget.AllBuffered, null);
        wait_player.gameObject.SetActive(true);
    }


    [PunRPC]
    public void Incr()
    {
        i++;
     //   instruction.text = i.ToString();
        if(i==PlayerCount)
        {
            //   Waitfunc();
            //   PhotonNetwork.LoadLevel("0716Level01");
            PhotonNetwork.LoadLevel("0716Level01");
            Debug.LogError("hi");
            


        }
        else
        {
        //    instruction.text = "\nGame Starts In Few Minutes\n Room Name-"+createRoom.text;
        
        }
        
    }
    [PunRPC]
    public void Waitfunc()
    {
        StartCoroutine(Wait());

    }
    public IEnumerator Wait()
    {
       
     //   instruction.text = "Connected To The Room \n Room Name-" + createRoom.text;
        createRoom.gameObject.SetActive(false);
        JoinRoom.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
      //  instruction.text = "3";
        yield return new WaitForSeconds(1f);

    //    instruction.text = "2";
        yield return new WaitForSeconds(1f);

      //  instruction.text = "1";
        PhotonNetwork.LoadLevel("0716Level01");

    }
    public Vector3 inputPos;
    private void Update()
    {
        inputPos = Input.mousePosition;
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        print("RoomFailed"+returnCode+"Message "+message);
    }
    void Start()
    {
       
        DontDestroyOnLoad(this.gameObject);
    }

   


}
