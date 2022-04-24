using UnityEngine;

/// <summary>
/// 大廳管理器
/// 當玩家按下對戰按鈕後開始匹配房間
/// </summary>
public class LobbyManager : MonoBehaviour
{
    // Gameobject 遊戲物件;存放unity場景內所有物件
    //SerializeField 將私人欄位顯示在屬性面板上
    //Header 標題, 在屬性面板上顯示粗體字標題
    [SerializeField, Header("連線中畫面")]
    private GameObject goConnectview;

    //讓按鈕與程式溝通流程
    //1.提供公開的方法 Public Method
    //2.按鈕在點擊後 On Click 設定呼叫的方法

    public void StartConnect()
    {
        print("開始連線...");

        //遊戲物件.啟動設定(布林值),true 顯示;false 隱藏
        goConnectview.SetActive(true);
    }
}
