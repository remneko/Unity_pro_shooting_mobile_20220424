using UnityEngine;
using UnityEngine.UI;
using Photon.Pun; //引用Photon.Pun API
using Photon.Realtime;//引用Photon 即時API
using System.Reflection;

/// <summary>
/// 大廳管理器
/// 當玩家按下對戰按鈕後開始匹配房間
/// </summary>
/// MonoBehaviour連線功能回呼類別
/// 例如登入大廳後回呼你指定的程式
public class LobbyManager : MonoBehaviourPunCallbacks
{
    // Gameobject 遊戲物件;存放unity場景內所有物件
    //SerializeField 將私人欄位顯示在屬性面板上
    //Header 標題, 在屬性面板上顯示粗體字標題
    [SerializeField, Header("連線中畫面")]
    private GameObject goConnectview;
    [SerializeField, Header("對戰按鈕")]
    private Button btnBattle;
    [SerializeField, Header("連線人數")]
    private Text textcountPlayer;
    [SerializeField, Header("連線最大人數"), Range(2, 20)]
    private byte textcountmaxPlayer = 3;

    //喚醒事件:撥放遊戲時執行一次，初始化設定

    private void Awake()
    {
        //螢幕.設定解析度(寬,高,是否全螢幕)
        Screen.SetResolution(720, 405, false);
        //Photon 連線 的 連線使用設定
        PhotonNetwork.ConnectUsingSettings();
    }

    //override 允許複寫繼承的父類別成員
    //連線至控制台，在 ConnectUsingSettings 執行後會自動連線
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        print("<color=yellow>已經進入控制台</color>");
        //Photon 連線.進入大廳
        PhotonNetwork.JoinLobby();

    }
    //連線至大廳成功後會執行此方法
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        print("<color=yellow>已經進入大廳</color>");

        //對戰按鈕.互動 = 啟動
        btnBattle.interactable = true;

    }

    //讓按鈕與程式溝通流程
    //1.提供公開的方法 Public Method
    //2.按鈕在點擊後 On Click 設定呼叫的方法

    //開始連線對戰
    public void StartConnect()
    {
        print("開始連線...");

        //遊戲物件.啟動設定(布林值),true 顯示;false 隱藏
        goConnectview.SetActive(true);
        PhotonNetwork.JoinRandomRoom();
    }
    //加入加入隨機連線房間失敗
    //連線品質差導致失敗
    //還沒有房間
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        print("<color=red>加入隨機連線房間失敗</color>");

        //新增房間設定物件
        RoomOptions ro = new RoomOptions();
        //指定房間最大人數
        ro.MaxPlayers = textcountmaxPlayer;
        //建立房間並給予房間物件
        PhotonNetwork.CreateRoom("",ro);
        
    }
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        print("<color=yellow>開房者進入房間</color>");

        //當前房間人數
        int currentCount = PhotonNetwork.CurrentRoom.PlayerCount;
        //當前房間最大人數
        int maxCount = PhotonNetwork.CurrentRoom.MaxPlayers;

        textcountPlayer.text = "連線人數:" + currentCount + "/" + maxCount;

        LoadGameScene(currentCount, maxCount);
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        print("<color=yellow>玩家進入房間</color>");

        //當前房間人數
        int currentCount = PhotonNetwork.CurrentRoom.PlayerCount;
        //當前房間最大人數
        int maxCount = PhotonNetwork.CurrentRoom.MaxPlayers;

        textcountPlayer.text = "連線人數:" + currentCount + "/" + maxCount;

        LoadGameScene(currentCount, maxCount);
    }
    /// <summary>
    /// 載入場景
    /// </summary>
    private void LoadGameScene(int currentCount, int maxCount)
    {

        //clean code   乾淨程式
        //1.不重複，問題 影響維護性
        //當進入房間的玩家 等於 最大房間人數時 就進入遊戲場景

        if (currentCount == maxCount)
        {
            //透過Photon 連線讓玩家 載入指定場景
            //場景必須放在Build settings裡
            PhotonNetwork.LoadLevel("遊戲場景");
        }
    }
}
