using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Mui
{
    public class DataManager : MonoBehaviour
    {
        //新增部署作業內的網址，有變更都要更新
        private string gasLink = "https://script.google.com/macros/s/AKfycbyy0kl2vNDqX0gPcAFA-CDHxwB9WPQmv3oIiAtfZUBobX3YpRhc75iZVz_I6o0KzdkT/exec";
        private WWWForm form;
        private Button btnGatData;
        private Text textPlayerName;

        private void Start()
        {
            textPlayerName = GameObject.Find("玩家名稱").GetComponent<Text>();
            btnGatData = GameObject.Find("取得玩家資料按鈕").GetComponent<Button>();
            btnGatData.onClick.AddListener(GetGASData);
        }
        /// <summary>
        /// 取得 GAS 資料
        /// </summary>
        private void GetGASData() 
        {
            form = new WWWForm();
            form.AddField("method", "取得");

            StartCoroutine(StartGetGASData());
        }

        private IEnumerator StartGetGASData()
        {
            //新增網頁連線要求(getLink,表單資料)
            using (UnityWebRequest www = UnityWebRequest.Post(gasLink,form))
            {
                //等待網頁連線要求
                yield return www.SendWebRequest();
                //玩家名稱 = 連線要求下載的文字訊息
                textPlayerName.text = www.downloadHandler.text;
            }
        }
    }
}

