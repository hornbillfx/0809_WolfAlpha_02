using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class launch : MonoBehaviourPunCallbacks
{
    public GameObject ConnectedScreen;
    public GameObject DisconnectedScreen;
    public void Onclick_ConnectBtn()
    {
        print("s");
        PhotonNetwork.ConnectUsingSettings();
        if (PhotonNetwork.IsConnected)
        {
            ConnectedScreen.gameObject.SetActive(true);

        }
    }
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }
    public override void OnJoinedLobby()
    {
       
        if(ConnectedScreen)
        {
            ConnectedScreen.gameObject.SetActive(true);

        }


    }
    public override void OnJoinedRoom()
    {
        print("joined");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        if(DisconnectedScreen!=null)
        {
            DisconnectedScreen.SetActive(true);

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        print(PhotonNetwork.IsConnected);
    }
}
