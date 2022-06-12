using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Mui
{
    public class DataManager : MonoBehaviour
    {
        //�s�W���p�@�~�������}�A���ܧ󳣭n��s
        private string gasLink = "https://script.google.com/macros/s/AKfycbzNkdSBDiF9EyZMU7dL0CezivWwn58TQI513lPkBkoFZaMMJLn9F_qaV40zm5_tnUH3/exec";
        private WWWForm form;
        private Button btnGetData;
        private Text textPlayerName;
        private TMP_InputField inputFlied;

        private void Start()
        {
            textPlayerName = GameObject.Find("���a�W��").GetComponent<Text>();
            btnGetData = GameObject.Find("���o���a��ƫ��s").GetComponent<Button>();
            btnGetData.onClick.AddListener(GetGASData);

            inputFlied = GameObject.Find("�ܧ󪱮a�W��").GetComponent<TMP_InputField>();
            inputFlied.onEndEdit.AddListener(SetGASData);

        }
        /// <summary>
        /// ���o GAS ���
        /// </summary>
        private void GetGASData() 
        {
            form = new WWWForm();
            form.AddField("method", "���o");

            StartCoroutine(startGetGASData());
        }

        private IEnumerator startGetGASData()
        {
            //�s�W�����s�u�n�D(getLink,�����)
            using (UnityWebRequest www = UnityWebRequest.Post(gasLink,form))
            {
                //���ݺ����s�u�n�D
                yield return www.SendWebRequest();
                //���a�W�� = �s�u�n�D�U������r�T��
                textPlayerName.text = www.downloadHandler.text;
            }
        }

        private void SetGASData(string value)
        { 
            form = new WWWForm();
            form.AddField("method","�]�w");
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

