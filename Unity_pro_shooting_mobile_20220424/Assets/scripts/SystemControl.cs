using UnityEngine;
using UnityEngine.UI;


//�R�W�Ŷ�:�{���϶�
namespace Mui
{
    /// <summary>
    /// ����t��:���ʥ\��
    /// �����n�챱��Ⲿ��
    /// </summary>
    public class SystemControl : MonoBehaviour
    {
        [SerializeField, Header("�����n��")]
        private Joystick joystick;
        [SerializeField, Header("���ʳt��"),Range(0,30)]
        private float speed = 3.5f;
        [SerializeField, Header("�����V�ϥ�")]
        private Transform traDirectionIcon;
        [SerializeField, Header("�����V�ϥܽd��"), Range(0, 5f)]
        private float rangeDirectionIcon = 2.5f;
        [SerializeField, Header("�������t��"), Range(0, 5f)]
        private float speedTurn = 0.5f;


        private Rigidbody rig;

        private void Awake()
        {
            rig = GetComponent<Rigidbody>();
        }
        private void FixedUpdate()
        {
            Move();
        }

        private void Update()
        {
            GetJoystickvalue();
            UpdateDirectionIconPos();
            LookDirectionIcon();
        }
        /// <summary>
        /// ���o�����n���
        /// </summary>
        private void GetJoystickvalue()
        {
            print("<color=yellow>����:" + joystick.Horizontal + "</color>");
        }

        private void Move()
        {
            //����,�[�t�� = �T���V�q(x,y,z)
            rig.velocity = new Vector3(joystick.Horizontal, 0, joystick.Vertical) * speed;
            
        }

        /// <summary>
        /// ��s�����V�ϥܪ��y��
        /// </summary>
        private void UpdateDirectionIconPos()
        {
            //�s�y�� = �}��y�� + �T���V�q(x,y,z)(�����n�쪺�����P����) + ��V�ϼЪ��ϥ�
            Vector3 pos = transform.position + new Vector3(joystick.Horizontal, 1f, joystick.Vertical) * rangeDirectionIcon;
            //��s��V�ϥܪ��y�� = �s�y��
            traDirectionIcon.position = pos;
        }

        private void LookDirectionIcon()
        {
            //���o���ۨ��� = �|�줸.���ۨ���(��V�ϥ� - ����) - ��V�ϥܻP���⪺�V�q
            Quaternion look = Quaternion.LookRotation(traDirectionIcon.position - transform.position);
            //���⪺���� = �|�줸.����(���⪺����,���ۨ���,����t��*�@�V���ɶ�)
            transform.rotation = Quaternion.Lerp(transform.rotation, look, speedTurn * Time.deltaTime);
            //���⪺.�کԨ��� = �T���V�q(0,�쥻���کԨ���,0)
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        }
    }
}

 