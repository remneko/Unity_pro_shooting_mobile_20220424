using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Cinemachine;
using mui;


//�R�W�Ŷ�:�{���϶�
namespace Mui
{
    /// <summary>
    /// ����t��:���ʥ\��
    /// �����n�챱��Ⲿ��
    /// </summary>
    public class SystemControl : MonoBehaviourPun
    {
        
        [SerializeField, Header("���ʳt��"),Range(0,30)]
        private float speed = 3.5f;      
        [SerializeField, Header("�����V�ϥܽd��"), Range(0, 5f)]
        private float rangeDirectionIcon = 2.5f;
        [SerializeField, Header("�������t��"), Range(0, 5f)]
        private float speedTurn = 0.5f;
        [SerializeField, Header("�ʵe�ѦҶ]�B")]
        private string parameterWalk = "�}���]�B";
        [SerializeField, Header("�e��")]
        private GameObject goCanvas;
        [SerializeField, Header("�e�����a��T")]
        private GameObject goCanvasplayerInfo;
        [SerializeField, Header("�����V�ϥ�")]
        private GameObject goDirection;



        private Rigidbody rig;
        private Animator ani;
        private Joystick joystick;
        private Transform traDirectionIcon;
        private CinemachineVirtualCamera cvc;
        private SystemAttack systemAttack;
        private DamageManager damageManager;

        private void Awake()
        {
            rig = GetComponent<Rigidbody>();
            ani = GetComponent<Animator>();
            systemAttack = GetComponent<SystemAttack>();
            damageManager = GetComponent<DamageManager>();

            if (photonView.IsMine)
            {
                PlayerUIfollow follow = Instantiate(goCanvasplayerInfo).GetComponent<PlayerUIfollow>();
                follow.traPlayer = transform;

                traDirectionIcon = Instantiate(goDirection).transform;                                                  //���o�����V�ϥ�

                //transform.Find(�l����W��).�z�L�W�ٷj�M������

                GameObject tempCanvas = Instantiate(goCanvas);
                joystick = tempCanvas.transform.Find("Dynamic Joystick").GetComponent<Joystick>();                     //���o�e�����������n��

                systemAttack.btnFire = tempCanvas.transform.Find("�o�g���s").GetComponent<Button>();

                cvc = GameObject.Find("CM �޲z��").GetComponent<CinemachineVirtualCamera>();                           //���o��v��CM�޲z��
                cvc.Follow = transform;                                                                               //���w�l�ܪ���

                damageManager.Imghp = GameObject.Find("���a��q�Ϥ�").GetComponent<Image>();
                damageManager.textHp = GameObject.Find("���a��q��r").GetComponent<Text>();
            }
            //�_�h���O�i�J�����a �N��������t��,�קK�����h�Ӫ���
            else
            {
                enabled = false;
            }

            
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
            Updateanimation();
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
        /// <summary>
        /// ��s�ʵe
        /// </summary>
        private void Updateanimation()
        {
            //�O�_�]�B = �����n�� ���� ����0 �� ���� ����0
            bool run = joystick.Horizontal != 0 || joystick.Vertical != 0;
            ani.SetBool(parameterWalk, run);
        }
    }
}

 