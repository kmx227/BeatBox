using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public InputField RoomName;
    public GameObject onNetwork;
    public GameObject onServer;
    public GameObject pause;

    // Start is called before the first frame update
    void Awake()
    {
        Screen.SetResolution(960, 540, false);
        PhotonNetwork.SendRate = 60;
        PhotonNetwork.SerializationRate = 30;
    }

    public void Connet() => PhotonNetwork.ConnectUsingSettings();

    public override void OnConnectedToMaster()
    {
        onNetwork.SetActive(false);
        onServer.SetActive(true);
    }

    public void JoinOrCreateRoom()
    {
        PhotonNetwork.JoinOrCreateRoom(RoomName.text, new RoomOptions { MaxPlayers = 2 }, null);
        onServer.SetActive(false);
    }

    public override void OnJoinedRoom()
    {
        print("�� ����");
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && PhotonNetwork.IsConnected)
        {
            PhotonNetwork.Disconnect();
        }
    }

    public void Spawn() //�÷��̾� �ҷ�����
    {
        PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);
        pause.SetActive(false);
    }

    public override void OnDisconnected(DisconnectCause cause) //���� ���� ����
    {
        pause.SetActive(true);
        onNetwork.SetActive(false);
    }

    [ContextMenu("����")]
    void Info()
    {
        if (PhotonNetwork.InRoom)
        {
            print("����� �̸� : " + PhotonNetwork.CurrentRoom.Name);
            print("����� �ο� �� : " + PhotonNetwork.CurrentRoom.PlayerCount);
            print("����� �ִ� �ο� �� : " + PhotonNetwork.CurrentRoom.MaxPlayers);

            string platyerStr = "�濡 �ִ� �÷��̾� ��� : ";
            for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
            {
                platyerStr += PhotonNetwork.PlayerList[i].NickName + ", ";
            }
            print(platyerStr);
        }
        else
        {
            print("������ �ο� �� : " + PhotonNetwork.CountOfPlayers);
            print("�� ���� : " + PhotonNetwork.CountOfRooms);
            print("��� �濡 �ִ� �ο� �� : " + PhotonNetwork.CountOfPlayersInRooms);
            print("�κ� ����? : " + PhotonNetwork.InLobby);
            print("����? : " + PhotonNetwork.IsConnected);
        }
    }
}
