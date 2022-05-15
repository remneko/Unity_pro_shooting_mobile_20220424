using UnityEngine;
using UnityEngine.UI;

namespace mui
{ 
    public class SystemAttack : MonoBehaviour
    {
        /// <summary>
        /// �����t��
        /// </summary>
        [HideInInspector]
        public Button btnFire;
        [SerializeField, Header("�l�u")]
        private GameObject goBullet;
        [SerializeField, Header("�l�u�̤j�ƶq")]
        private int bulletCountMax = 3;
        [SerializeField, Header("�l�u�ͦ���m")]
        private Transform trafire;
        [SerializeField, Header("�l�u�o�g�t��"), Range(0, 3000)]
        private int speedfire = 500;

        private int bulletCountCurrent;

        private void Awake()
        {
            //�o�g���s.�I��.�I�[��ť��(�}�j��k)�A���U�o�g���s����}�j��k
            btnFire.onClick.AddListener(Fire);
            
        }

        /// <summary>
        /// �}�j
        /// </summary>
        private void Fire()
        {
            //�ͦ�(����,�y��,����)
            Instantiate(goBullet, trafire.position, Quaternion.identity);
        }

    }

}



