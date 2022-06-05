using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Unity.Mathematics;
using TMPro;
using System.Collections;
using Mui;
using mui;

public class DamageManager : MonoBehaviourPun
{
    [SerializeField, Header("��q"), Range(0, 1000)]
    private float Hp = 200;
    [SerializeField, Header("�����S��")]
    private GameObject goVFXHit;
    [SerializeField, Header("���ѵۦ⾹")]
    private Shader shaderDissolve;

    private string nameBullet = "�l�u";

    //�ҫ��Ҧ��������V����A�]�t����y
    private SkinnedMeshRenderer[] smr;


    [HideInInspector]
    public Image Imghp;
    [HideInInspector]
    public TextMeshProUGUI textHp;
    private float maxHp;

    private Material materialDissolve;
    private SystemControl systemControl;
    private SystemAttack systemAttack;

    private void Awake()
    {
        maxHp = Hp;
        systemControl = GetComponent<SystemControl>();
        systemAttack = GetComponent<SystemAttack>();

        //���o�l����̪����
        smr = GetComponentsInChildren<SkinnedMeshRenderer>();
        //�s�W ���ѵۦ⾹ ����y
        materialDissolve = new Material(shaderDissolve);
        for (int i = 0; i < smr.Length; i++)
        {
            smr[i].material = materialDissolve;
        }

        if (photonView.IsMine) { textHp.text = Hp.ToString(); }


    }
    //�i�J
    private void OnCollisionEnter(Collision collision)
    {
        //�p�G 
        if (!photonView.IsMine) return;

        //�p�G �I������W�� �]�t () �N�B�z()
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
        Hp = Mathf.Clamp(Hp, 0, maxHp);

        textHp.text = Hp.ToString();

        //�s�u.�ͦ�(�S��,�����y��,����)
        PhotonNetwork.Instantiate(goVFXHit.name, posHit, Quaternion.identity);
        //�p�G��q<=0 �N�z�LRPC�Ҧ��H��������榺�`��k
        if (Hp <= 0) photonView.RPC("Dead", RpcTarget.All);

    }

    // �ݭn�P�B����k�����K�[ PunRPC �ݩ� Remote Procedure ���ݵ{���I�s
    [PunRPC]
    private void Dead()
    {
        StartCoroutine(Dissolve());
        systemAttack.enabled = false;
        systemControl.enabled = false;
        systemControl.traDirectionIcon.gameObject.SetActive(false);

    }

    private IEnumerator Dissolve()                                                  //���ѼƭȰ_�l��
    {
        float valueDissolve = 5;

        if (systemControl.traDirectionIcon) systemControl.traDirectionIcon.gameObject.SetActive(false);
        

        for (int i = 0; i < 20; i++)                                                //�j���滼��
        {
            valueDissolve -= 0.3f;                                                   //���ѭȻ���0.3
            materialDissolve.SetFloat("dissolve", valueDissolve);                    //��s�ۦ⾹�ݩʡA�`�N�n����Reference
            yield return new WaitForSeconds(0.08f);
        }
    }
}
