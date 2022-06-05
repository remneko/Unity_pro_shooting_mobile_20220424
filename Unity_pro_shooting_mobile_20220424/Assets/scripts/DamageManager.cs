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
    [SerializeField, Header("血量"), Range(0, 1000)]
    private float Hp = 200;
    [SerializeField, Header("擊中特效")]
    private GameObject goVFXHit;
    [SerializeField, Header("溶解著色器")]
    private Shader shaderDissolve;

    private string nameBullet = "子彈";

    //模型所有的網格渲染物件，包含材質球
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

        //取得子物件們的原件
        smr = GetComponentsInChildren<SkinnedMeshRenderer>();
        //新增 溶解著色器 材質球
        materialDissolve = new Material(shaderDissolve);
        for (int i = 0; i < smr.Length; i++)
        {
            smr[i].material = materialDissolve;
        }

        if (photonView.IsMine) { textHp.text = Hp.ToString(); }


    }
    //進入
    private void OnCollisionEnter(Collision collision)
    {
        //如果 
        if (!photonView.IsMine) return;

        //如果 碰撞物件名稱 包含 () 就處理()
        if (collision.gameObject.name.Contains(nameBullet))
        {
            //collision.contacts[0] 碰到的第一個物件
            //碰到物件的座標
            Damage(collision.contacts[0].point);
        }
    }

    //持續
    private void OnCollisionExit(Collision collision)
    {

    }

    //離開
    private void OnCollisionStay(Collision collision)
    {

    }

    private void Damage(Vector3 posHit)
    {
        Hp -= 20;
        Imghp.fillAmount = Hp / maxHp;
        Hp = Mathf.Clamp(Hp, 0, maxHp);

        textHp.text = Hp.ToString();

        //連線.生成(特效,擊中座標,角度)
        PhotonNetwork.Instantiate(goVFXHit.name, posHit, Quaternion.identity);
        //如果血量<=0 就透過RPC所有人的物件執行死亡方法
        if (Hp <= 0) photonView.RPC("Dead", RpcTarget.All);

    }

    // 需要同步的方法必須添加 PunRPC 屬性 Remote Procedure 遠端程式呼叫
    [PunRPC]
    private void Dead()
    {
        StartCoroutine(Dissolve());
        systemAttack.enabled = false;
        systemControl.enabled = false;
        systemControl.traDirectionIcon.gameObject.SetActive(false);

    }

    private IEnumerator Dissolve()                                                  //溶解數值起始值
    {
        float valueDissolve = 5;

        if (systemControl.traDirectionIcon) systemControl.traDirectionIcon.gameObject.SetActive(false);
        

        for (int i = 0; i < 20; i++)                                                //迴圈實行遞減
        {
            valueDissolve -= 0.3f;                                                   //溶解值遞減0.3
            materialDissolve.SetFloat("dissolve", valueDissolve);                    //更新著色器屬性，注意要控制Reference
            yield return new WaitForSeconds(0.08f);
        }
    }
}
