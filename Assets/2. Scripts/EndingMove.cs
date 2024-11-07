using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndingMove : MonoBehaviour
{
    private Rigidbody playerRigidbody; // 플레이어 캐릭터의 리지드바디
    private Animator playerAnimator; // 플레이어 캐릭터의 애니메이터
    public GameObject target; //이동할 위치
    public float speed = 1f;
    public GameObject EndingUI;
    
    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
    }
    void Start()
    {
        StartCoroutine("Move");
    }
    IEnumerator Move()
    {
        Debug.Log("움직여");
        yield return new WaitForSeconds(2f);

        playerAnimator.SetBool("run", true);
        while (Vector3.Distance(transform.position, target.transform.position) > 0.1f)
        {
             Vector3 direction = (target.transform.position - transform.position).normalized;
             transform.position += direction * speed * Time.deltaTime;
            yield return null;
        }
        transform.position = target.transform.position;
        playerAnimator.SetBool("run", false);
        StartCoroutine("UI");
    }
    
    IEnumerator UI()
    {
        yield return new WaitForSeconds(2f);
        EndingUI.SetActive(true);
    }

  public void ButtonUI()
    {
        SceneManager.LoadScene("Main");
    }

}
