using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class EXP : MonoBehaviour
{
    public Text ExpText;
    public Text levelText;
    private int maxExp = 70; //������ �ְ� ����ġ
    private const string level = "levelkey"; //playerprefsŰ ����

    public Slider expslider;

    private void Awake()
    {
        Maxexp();
        ExpInspect();
        expslider.value = PlayerPrefs.GetInt("PlayerExpKey", 0);
    }

    public void Maxexp()
    { //������ ���� �ְ� ����ġ ����
        int currentlevel = PlayerPrefs.GetInt("levelkey", 1);   //���� ����
        currentlevel *= 70;
        expslider.maxValue = currentlevel;
        Levelup();
    }
    public void UpdateExpText(int exp)
    {
        expslider.value = exp;
        ExpText.text = exp + " / " + expslider.maxValue;
    }
    public void ExpInspect()
    {   //����ġ Ȯ��
        int exp = PlayerPrefs.GetInt("PlayerExpKey", 0); //���� ����ġ
        int level = PlayerPrefs.GetInt("levelkey", 1);   //���� ����

        if (exp >= level * maxExp)
        {//���� ����ġ�� �ְ� ����ġ���� ũ��
            exp -= level * maxExp; // �ְ� ����ġ�� �� �� 
            level++;
            PlayerPrefs.SetInt("levelkey", level); //������
            PlayerPrefs.Save();
            Maxexp();
            UpdateExpText(exp);
        }
        else
        {
            UpdateExpText(exp);
        }
    }
    public void Levelup()
    {
        int currentlevel = PlayerPrefs.GetInt(level, 1);
        levelText.text = "Lv. " + currentlevel;
    }




}
