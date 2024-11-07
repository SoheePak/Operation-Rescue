using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Child : MonoBehaviour
{
    public GameObject help;
    private Transform playerTransform;
    public static bool follow = false;
    public Vector3 offset = new Vector3(0, 0, -1);
    private Animator animator;
    public GameObject icon; // 아이의 위치를 나타내는 게임 오브젝트

    private void Awake()
    {
        help = GetComponent<GameObject>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (follow)
        {
            Follow();
        }
    }
    public void Follow(Transform newParent)
    {
        playerTransform = newParent;
        transform.SetParent(playerTransform); //자식으로 등록
        transform.localPosition = offset;
        follow = true;
        animator.SetBool("walk", true); // 애니메이션 동작 바꾸기
        icon.SetActive(false);
        transform.forward = Camera.main.transform.forward;
    }
    private void Follow() //실시간으로 플레이어를 따라다님
    {
        transform.position = playerTransform.position + playerTransform.TransformDirection(offset);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            UIManager.instance.
            StartCoroutine(save());
            Follow(collision.transform);
            collision.transform.SetParent(transform);
            follow = true;
        }
    }
    IEnumerator save()
    {
        Debug.Log("Save");
        yield return new WaitForSeconds(1f);
        AudioManager.Instance.PlaySFX("Follow");
        yield return new WaitForSeconds(5f);
    }
    
   
}
