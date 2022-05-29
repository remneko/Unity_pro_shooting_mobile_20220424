using Photon.Pun;
using UnityEngine;

namespace Mui
{
    public class Destroyself : MonoBehaviourPun
    {
        [SerializeField, Header("�R���ɶ�"), Range(0, 10)]
        private float timeDestroy = 5;
        [SerializeField, Header("�O�_�ݭn�I�������")]
        private bool collisionDestroy;

        /// <summary>
        /// ����R����k
        /// </summary>
        private void Awake()
        {
            Invoke("DestroyDelay", timeDestroy);
        }

        private void DestroyDelay()
        {
            //�s�u�R��(�C������) - �R�����A����������
            PhotonNetwork.Destroy(gameObject);
        }
    }
}

