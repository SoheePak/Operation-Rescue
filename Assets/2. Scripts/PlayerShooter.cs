using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// 주어진 Gun 오브젝트를 쏘거나 재장전
// 알맞은 애니메이션을 재생하고 IK를 사용해 캐릭터 양손이 총에 위치하도록 조정
public class PlayerShooter : MonoBehaviour {
    public Gun gun; // 사용할 총
    public GameObject ExitUI;// 탈출 UI
    public GameObject helptext; // 구조 요청 텍스트
    public GameObject portaltext; // 포탈안내 텍스트

    private PlayerInput playerInput; // 플레이어의 입력
    private Animator playerAnimator; // 애니메이터 컴포넌트
    private PlayerMovement playerMovement;

    private void Start() {
        // 사용할 컴포넌트들을 가져오기
        playerInput = GetComponent<PlayerInput>();
        playerAnimator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void OnEnable() {
        // 슈터가 활성화될 때 총도 함께 활성화
       gun.gameObject.SetActive(true);
    }
    
    private void OnDisable() {
        // 슈터가 비활성화될 때 총도 함께 비활성화
        gun.gameObject.SetActive(false);
    }

    private void Update() {
        // 입력을 감지하고 총 발사하거나 재장전
        if (playerInput.fire)
        {
            playerAnimator.SetTrigger("Shoot");
            gun.Fire(); //발사 입력 감지 시 총 발사
         }
        else if(playerInput.reload)
        {
            //재장전 입력 감지 시 재장전
            if (gun.Reload())
            {
                //재장전 성공 시에만 재장전 애니메이션 재생
                playerAnimator.SetTrigger("Reload");
                AudioManager.Instance.PlaySFX("Reload");
            }
        }
        UpdateUI();
        if (Child.follow)
        {
            playerMovement.enabled = false;
            helptext.SetActive(false);
            UIManager.instance.mission[2].SetActive(false);
            portaltext.SetActive(true);
            StartCoroutine("Follow");
        }
    }
    IEnumerator Follow()
    {
        yield return new WaitForSeconds(4f);
        playerMovement.enabled = true;
    }

    // 탄약 UI 갱신
    private void UpdateUI() {
        if (gun != null && UIManager.instance != null)
        {
            // UI 매니저의 탄약 텍스트에 탄창의 탄약과 남은 전체 탄약을 표시
            UIManager.instance.UpdateAmmoText(gun.magAmmo, gun.ammoRemain);
        }
    }

    // 애니메이터의 IK 갱신
    private void OnAnimatorIK(int layerIndex) {
        //총의 기준점 gunPivot을 3D모델의 오른쪽 팔꿈치 위치로 이동
        //gunPivot.position = playerAnimator.GetIKHintPosition(AvatarIKHint.RightElbow);

        //IK를 사용하여 왼손의 위치와 회전을 총의 왼쪽 손잡이에 맞춤
        playerAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1.0f);
        playerAnimator.SetIKRotationWeight(AvatarIKGoal.LeftHand,1.0f);

        //playerAnimator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandMount.position);
        //playerAnimator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandMount.rotation);

        //IK를 사용하여 오른손의 위치와 회전을 총ㅇ의 오른쪽 손잡이에 맞춤
        playerAnimator.SetIKPositionWeight(AvatarIKGoal.RightHand,1.0f);
        playerAnimator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1.0f);


        //playerAnimator.SetIKPosition(AvatarIKGoal.RightHand, rightHandMount.position);
        //playerAnimator.SetIKRotation(AvatarIKGoal.RightHand, rightHandMount.rotation);

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Potal"))
        {
            Time.timeScale = 0f;
            ExitUI.SetActive(true); // 탈출 여부 확인
        }
        if (collision.gameObject.CompareTag("Child"))
        {
            Time.timeScale = 0f;
            helptext.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Help"))
        {
            helptext.SetActive(true);       //구조 요청 텍스트 입력
            AudioManager.Instance.PlaySFX("Help");
            other.isTrigger = false;        //한 번만 재생할 수 있게
        }
    }

}