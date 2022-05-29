using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Unity.Mathematics;

public class DamageManager : MonoBehaviourPun
{
    [SerializeField, Header("��q"),Range(0,1000)]
    private float Hp = 200;
    [SerializeField, Header("�����S��")]
    private GameObject goVFXHit;

    private string nameBullet = "�l�u";

    [HideInInspector]
    public Image Imghp;
    [HideInInspector]
    public Text textHp;
    private float maxHp;


    private void Awake()
    {
        maxHp = Hp;
        if (photonView.IsMine) { textHp.text = Hp.ToString(); }
        
    }
    //�i�J
    private void OnCollisionEnter(Collision collision)
    {
        //�p�G 
        if (photonView.IsMine) return;
        {
            //�p�G �I������W�� �]�t () �N�B�z()
            if (collision.gameObject.name.Contains(nameBullet))
            {
                //collision.contacts[0] �I�쪺�Ĥ@�Ӫ���
                //�I�쪫�󪺮y��
                Damage(collision.contacts[0].point);
            }
        }
        
    }

    //����
    private void OnCollisionExit(Collision collision)
    {
        
    }

    //���}
    private void OnCollisionStay(Collision collision)
    {
        
    }

    private void Damage(Vector3 posHit)
    {
        Hp -= 20;
        Imghp.fillAmount = Hp / maxHp;
        Hp = Mathf.Clamp(Hp,0, maxHp);

        textHp.text = Hp.ToString();

        //�s�u.�ͦ�(�S��,�����y��,����)
        PhotonNetwork.Instantiate(goVFXHit.name, posHit, Quaternion.identity);
    }
}
