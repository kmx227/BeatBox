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
    public bool onCreate;

    Vector3 TrapVec = new Vector3(-7.2f, 1.26f, 1);
    Vector3 RopeVec = new Vector3(-4f, -1f, 1);
    Vector3 AppleVec = new Vector3(-2f, -4f, 1);
    Vector3 SPlayer = new Vector3(-7f, -4f, 1);
    Vector3 LPlayer = new Vector3(8f, -4f, 1);

    // Start is called before the first frame update
    void Awake()
    {
        Screen.SetResolution(960, 540, false);
        PhotonNetwork.SendRate = 60;
        PhotonNetwork.SerializationRate = 30;

        PhotonNetwork.ConnectUsingSettings();
    }

    //public void Connet() => PhotonNetwork.ConnectUsingSettings();

    public override void OnConnectedToMaster() //������Ŭ���̾�Ʈ
    {
        /*onNetwork.SetActive(false);
        onServer.SetActive(true);*/
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions { MaxPlayers = 2 }, null);
    }

    /*public void JoinOrCreateRoom()
    {
        PhotonNetwork.JoinOrCreateRoom(RoomName.text, new RoomOptions { MaxPlayers = 2 }, null);
        onServer.SetActive(false);
    }*/
    public override void OnCreatedRoom() //����� �ݹ�
    {
        PhotonNetwork.Instantiate("Trap", TrapVec, Quaternion.identity);
        PhotonNetwork.Instantiate("Middle", RopeVec, Quaternion.identity);
        PhotonNetwork.Instantiate("Apple", AppleVec, Quaternion.identity);
        onCreate = true;
    }

    public override void OnJoinedRoom() //������ �ݹ�
    {
        print("�� ����");
        //PhotonNetwork.Instantiate("PLayer", LPlayer, Quaternion.identity);
        PhotonNetwork.Instantiate("PLayerL", SPlayer, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && PhotonNetwork.IsConnected)
        {
            PhotonNetwork.Disconnect();
        }
    }

    public override void OnDisconnected(DisconnectCause cause) //���� ���� ����
    {
        pause.SetActive(true);
        onNetwork.SetActive(false);
    }

   /* public void Spawn() //�÷��̾� �ҷ�����
    {

            PhotonNetwork.Instantiate("PLayer", SPlayer, Quaternion.identity);

        pause.SetActive(false);
    }*/

    [ContextMenu("����")] //������ Ȯ��
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
