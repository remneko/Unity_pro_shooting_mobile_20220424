using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class DamageManager : MonoBehaviourPun
{
    [SerializeField, Header("��q"),Range(0,1000)]
    private float Hp = 200;
    [SerializeField, Header("�����S��")]
    private GameObject goVFXHit;

    private string nameBullet = "�l�u";

    public Image Imghp;
    private float maxHp;

    private void Awake()
    {
        maxHp = Hp;
    }
    //�i�J
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains(nameBullet))
        {
            //collision.contacts[0] �I�쪺�Ĥ@�Ӫ���
            //�I�쪫�󪺮y��
            Damage(collision.contacts[0].point);
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

        //�s�u.�ͦ�(�S��,�����y��,����)
        PhotonNetwork.Instantiate(goVFXHit.name, posHit, Quaternion.identity);
    }
}
