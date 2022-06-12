using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Mui
{
    public class DataManager : MonoBehaviour
    {
        //新增部署作業內的網址，有變更都要更新
        private string gasLink = "https://script.google.com/macros/s/AKfycbzNkdSBDiF9EyZMU7dL0CezivWwn58TQI513lPkBkoFZaMMJLn9F_qaV40zm5_tnUH3/exec";
        private WWWForm form;
        private Button btnGetData;
        private Text textPlayerName;
        private TMP_InputField inputFlied;

        private void Start()
        {
            textPlayerName = GameObject.Find("玩家名稱").GetComponent<Text>();
            btnGetData = GameObject.Find("取得玩家資料按鈕").GetComponent<Button>();
            btnGetData.onClick.AddListener(GetGASData);

            inputFlied = GameObject.Find("變更玩家名稱").GetComponent<TMP_InputField>();
            inputFlied.onEndEdit.AddListener(SetGASData);

        }
        /// <summary>
        /// 取得 GAS 資料
        /// </summary>
        private void GetGASData() 
        {
            form = new WWWForm();
            form.AddField("method", "取得");

            StartCoroutine(startGetGASData());
        }

        private IEnumerator startGetGASData()
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

        private void SetGASData(string value)
        { 
            form = new WWWForm();
            form.AddField("method","設定");
            form.AddField("playerName", inputFlied.text);

            StartCoroutine(startSetGASData());

        }

        private IEnumerator startSetGASData()
        {
            using (UnityWebRequest www=UnityWebRequest.Post(gasLink,form)) 
            {
                yield return www.SendWebRequest();
                textPlayerName.text = inputFlied.text;
                print(www.downloadHandler.text);
            
            }

        }
    }
}

