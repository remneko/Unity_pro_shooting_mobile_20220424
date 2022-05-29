using UnityEngine;
using UnityEngine.UI;
using Photon.Pun; //�ޥ�Photon.Pun API
using Photon.Realtime;//�ޥ�Photon �Y��API
using System.Reflection;

/// <summary>
/// �j�U�޲z��
/// ���a���U��ԫ��s��}�l�ǰt�ж�
/// </summary>
/// MonoBehaviour�s�u�\��^�I���O
/// �Ҧp�n�J�j�U��^�I�A���w���{��
public class LobbyManager : MonoBehaviourPunCallbacks
{
    // Gameobject �C������;�s��unity�������Ҧ�����
    //SerializeField �N�p�H�����ܦb�ݩʭ��O�W
    //Header ���D, �b�ݩʭ��O�W��ܲ���r���D
    [SerializeField, Header("�s�u���e��")]
    private GameObject goConnectview;
    [SerializeField, Header("��ԫ��s")]
    private Button btnBattle;
    [SerializeField, Header("�s�u�H��")]
    private Text textcountPlayer;
    [SerializeField, Header("�s�u�̤j�H��"), Range(2, 20)]
    private byte textcountmaxPlayer = 3;

    //����ƥ�:����C���ɰ���@���A��l�Ƴ]�w

    private void Awake()
    {
        //�ù�.�]�w�ѪR��(�e,��,�O�_���ù�)
        Screen.SetResolution(720, 405, false);
        //Photon �s�u �� �s�u�ϥγ]�w
        PhotonNetwork.ConnectUsingSettings();
    }

    //override ���\�Ƽg�~�Ӫ������O����
    //�s�u�ܱ���x�A�b ConnectUsingSettings �����|�۰ʳs�u
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        print("<color=yellow>�w�g�i�J����x</color>");
        //Photon �s�u.�i�J�j�U
        PhotonNetwork.JoinLobby();

    }
    //�s�u�ܤj�U���\��|���榹��k
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        print("<color=yellow>�w�g�i�J�j�U</color>");

        //��ԫ��s.���� = �Ұ�
        btnBattle.interactable = true;

    }

    //�����s�P�{�����q�y�{
    //1.���Ѥ��}����k Public Method
    //2.���s�b�I���� On Click �]�w�I�s����k

    //�}�l�s�u���
    public void StartConnect()
    {
        print("�}�l�s�u...");

        //�C������.�Ұʳ]�w(���L��),true ���;false ����
        goConnectview.SetActive(true);
        PhotonNetwork.JoinRandomRoom();
    }
    //�[�J�[�J�H���s�u�ж�����
    //�s�u�~��t�ɭP����
    //�٨S���ж�
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        print("<color=red>�[�J�H���s�u�ж�����</color>");

        //�s�W�ж��]�w����
        RoomOptions ro = new RoomOptions();
        //���w�ж��̤j�H��
        ro.MaxPlayers = textcountmaxPlayer;
        //�إߩж��õ����ж�����
        PhotonNetwork.CreateRoom("",ro);
        
    }
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        print("<color=yellow>�}�Ъ̶i�J�ж�</color>");

        //��e�ж��H��
        int currentCount = PhotonNetwork.CurrentRoom.PlayerCount;
        //��e�ж��̤j�H��
        int maxCount = PhotonNetwork.CurrentRoom.MaxPlayers;

        textcountPlayer.text = "�s�u�H��:" + currentCount + "/" + maxCount;

        LoadGameScene(currentCount, maxCount);
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        print("<color=yellow>���a�i�J�ж�</color>");

        //��e�ж��H��
        int currentCount = PhotonNetwork.CurrentRoom.PlayerCount;
        //��e�ж��̤j�H��
        int maxCount = PhotonNetwork.CurrentRoom.MaxPlayers;

        textcountPlayer.text = "�s�u�H��:" + currentCount + "/" + maxCount;

        LoadGameScene(currentCount, maxCount);
    }
    /// <summary>
    /// ���J����
    /// </summary>
    private void LoadGameScene(int currentCount, int maxCount)
    {

        //clean code   ���b�{��
        //1.�����ơA���D �v�T���@��
        //��i�J�ж������a ���� �̤j�ж��H�Ʈ� �N�i�J�C������

        if (currentCount == maxCount)
        {
            //�z�LPhoton �s�u�����a ���J���w����
            //����������bBuild settings��
            PhotonNetwork.LoadLevel("�C������");
        }
    }
}
