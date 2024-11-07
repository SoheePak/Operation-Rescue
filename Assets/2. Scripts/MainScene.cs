using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene : MonoBehaviour
{

    public void OnClickSetActiveTrue(GameObject obj)
    {
        AudioManager.Instance.PlaySFX("Click");
        obj.SetActive(true);
    }

    public void OnClickSetActiveFalse(GameObject obj)
    {
        AudioManager.Instance.PlaySFX("Click");
        obj.SetActive(false);
    }
    public void SetActiveUI(GameObject obj)
    {
        AudioManager.Instance.PlaySFX("Click");
        StartCoroutine(TimeUI(obj));
    }
    IEnumerator TimeUI(GameObject obj)
    {
        obj.SetActive(true );
        yield return new WaitForSeconds(2f);
        obj.SetActive(false);
    }
}
