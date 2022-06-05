using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Mui
{
    public class DataManager : MonoBehaviour
    {
        //�s�W���p�@�~�������}�A���ܧ󳣭n��s
        private string gasLink = "https://script.google.com/macros/s/AKfycbyy0kl2vNDqX0gPcAFA-CDHxwB9WPQmv3oIiAtfZUBobX3YpRhc75iZVz_I6o0KzdkT/exec";
        private WWWForm form;
        private Button btnGatData;
        private Text textPlayerName;

        private void Start()
        {
            textPlayerName = GameObject.Find("���a�W��").GetComponent<Text>();
            btnGatData = GameObject.Find("���o���a��ƫ��s").GetComponent<Button>();
            btnGatData.onClick.AddListener(GetGASData);
        }
        /// <summary>
        /// ���o GAS ���
        /// </summary>
        private void GetGASData() 
        {
            form = new WWWForm();
            form.AddField("method", "���o");

            StartCoroutine(StartGetGASData());
        }

        private IEnumerator StartGetGASData()
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
    }
}

