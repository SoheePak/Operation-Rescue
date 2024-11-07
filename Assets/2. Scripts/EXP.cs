using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class EXP : MonoBehaviour
{
    public Text ExpText;
    public Text levelText;
    private int maxExp = 70; //레벨의 최고 경험치
    private const string level = "levelkey"; //playerprefs키 선언

    public Slider expslider;

    private void Awake()
    {
        Maxexp();
        ExpInspect();
        expslider.value = PlayerPrefs.GetInt("PlayerExpKey", 0);
    }

    public void Maxexp()
    { //레벨에 따른 최고 경험치 설정
        int currentlevel = PlayerPrefs.GetInt("levelkey", 1);   //현재 레벨
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
    {   //경험치 확인
        int exp = PlayerPrefs.GetInt("PlayerExpKey", 0); //현재 경험치
        int level = PlayerPrefs.GetInt("levelkey", 1);   //현재 레벨

        if (exp >= level * maxExp)
        {//현재 경험치가 최고 경험치보다 크면
            exp -= level * maxExp; // 최고 경험치를 뺀 후 
            level++;
            PlayerPrefs.SetInt("levelkey", level); //레벨업
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
