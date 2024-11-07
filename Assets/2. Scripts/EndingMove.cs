using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndingMove : MonoBehaviour
{
    private Rigidbody playerRigidbody; // �÷��̾� ĳ������ ������ٵ�
    private Animator playerAnimator; // �÷��̾� ĳ������ �ִϸ�����
    public GameObject target; //�̵��� ��ġ
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
        Debug.Log("������");
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
