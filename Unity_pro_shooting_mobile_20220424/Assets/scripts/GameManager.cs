using UnityEngine;
using Photon.Pun;
using System.Linq;                  //�ޥ� �t�άd�߻y�� (��Ƶ��c�ഫ API)
using System.Collections.Generic;   //�ޥ� �t�ΩM�@��(��Ƶ��c,list,arrayList...)
using Photon.Realtime;

namespace Mui
{
    /// <summary>
    /// �C���޲z��
    /// �P�_�p�G�O�s�u�i�J�����a
    /// �N�ͦ����⪫��(�Ԥh)
    /// </summary>
    public class GameManager : MonoBehaviourPunCallbacks
    {
        [SerializeField, Header("���⪫��")]
        private GameObject goCharacter;
        /// <summary>
        /// �x�s�ͦ��y�вM��
        /// </summary>
        [SerializeField, Header("���a�ͦ��y�Ъ���")]
        private Transform[] traSpawnPoint;
        [SerializeField]
        private List<Transform> traSpawnPointList;

        private void Awake()
        {
            //���a�w�g�i�J�ж�����...

            //Photon �s�u.��e�ж�.�i���� = �_(��L���a�L�k�[�J)
            PhotonNetwork.CurrentRoom.IsVisible = false;

            traSpawnPointList = new List<Transform>();      //�s�W�M�檫��
            traSpawnPointList = traSpawnPoint.ToList();     //�}�C�ର�M���Ƶ��c

            //�p�G�O�O�s�u�i�J�����a�N�b���A���ͦ����⪫��
            
            //if (photonView.IsMine)
            //{
            {
                int indexRandom = Random.Range(0, traSpawnPointList.Count);             //���o�H���M��(0,�M�����)
                Transform tra = traSpawnPointList[indexRandom];                         //���o�H���y��

                //�z�LPhoton���A��.(�ͦ�����.�W��,�y��,����)
                PhotonNetwork.Instantiate(goCharacter.name, tra.position,tra.rotation);

                traSpawnPointList.RemoveAt(indexRandom);                                //�R���w�g���o�L���ͦ��y�и��
            }
            //}
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            base.OnPlayerLeftRoom(otherPlayer);

            //�p�G��e���a�H�� �ѤU1�H�� �ӧQ
            if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
            {
                Win();
            }

        }

        private void Win()
        {
            print("�ӧQ");
        }


    }
}

