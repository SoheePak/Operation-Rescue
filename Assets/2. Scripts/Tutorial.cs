using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public GameObject[] usage1;
    public GameObject[] usage2;
    public GameObject[] usage3;
    public GameObject finish;
    public GameObject portal1;
    public GameObject portal2;
    public GameObject option;
    public bool Onusage2 = false;
    private Transform mytransform;
    public Text Killtext;
    public int killcount = 0;
    public GameObject traning;
    public GameObject potalopen;

    private void Start()
    {
        StartCoroutine("Usage");
        mytransform = GetComponent<Transform>();
    }
    private void Update()
    {
       if(killcount == 5)
        {
            traning.SetActive(false);
            potalopen.SetActive(true);
            portal2.SetActive(true);
        }
    }

    IEnumerator Usage()
    {
        yield return new WaitForSeconds(2f);        //ó�� �ȳ�
        usage1[0].SetActive(true);

        yield return new WaitForSeconds(7f);        //�ɼ� ����
        usage1[0].SetActive(false);
        usage1[1].SetActive(true);

        yield return new WaitForSeconds(3f);        //�ɼ�â
        option.SetActive(true);                    
        
        yield return new WaitForSeconds(5f);        //�̴ϸ� ����
        usage1[1].SetActive(false);
        option.SetActive(false);
        usage1[2].SetActive(true);    
        
        yield return new WaitForSeconds(7f);        //���� �ȳ�
        usage1[2].SetActive(false);
        usage1[3].SetActive(true);

        yield return new WaitForSeconds(7f);        //ü��
        usage1[3].SetActive(false);
        usage1[4].SetActive(true);

        yield return new WaitForSeconds(7f);        //������
        usage1[4].SetActive(false);
        usage1[5].SetActive(true);
        portal1.SetActive(true);
    }

    public void KillText()
    {
        killcount++;
        Killtext.text = "Kill : " + killcount;
    }

    public void OnClickbtn(GameObject obj)
    {
        obj.SetActive(false);
        if (Onusage2)
        {
            //��ݿ��� ����
            traning.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(Onusage2 && other.CompareTag("Player"))
        {
            finish.SetActive(true);
        }
        else if (other.CompareTag("Player"))
        {
            portal1.SetActive(false);
            transform.position = new Vector3(0, 0.5f, 25f);
            StartCoroutine("Usage2");       //Ʃ2����
        }
    }
    IEnumerator Usage2()
    {
        Onusage2 = true;
       yield return new WaitForSeconds(1f);     //��� ���� ����
        usage2[0].SetActive(true);

        yield return new WaitForSeconds(4f);    //źâ �� �ȳ�
        usage2[0].SetActive(false);
        usage2[1].SetActive(true);

        yield return new WaitForSeconds(7f);    //������ �ȳ�
        usage2[1].SetActive(false);
        usage2[2].SetActive(true);

        yield return new WaitForSeconds(7f);    //��� ���� ��ư
        usage2[2].SetActive(false);
        usage2[3].SetActive(true);
    }

    public void ClickScene()
    {
        LoadScene.instance.NextScene("Main");
    }
}

