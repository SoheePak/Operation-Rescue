using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

// 플레이어 캐릭터를 사용자 입력에 따라 움직이는 스크립트
public class PlayerMovement : MonoBehaviour {
    public float moveSpeed = 5f; // 앞뒤 움직임의 속도
    public float rotateSpeed = 180f; // 좌우 회전 속도
    public float jumpForce = 30f;
    public bool isGrounded = true;

    private PlayerInput playerInput; // 플레이어 입력을 알려주는 컴포넌트
    private Rigidbody playerRigidbody; // 플레이어 캐릭터의 리지드바디
    private Animator playerAnimator; // 플레이어 캐릭터의 애니메이터

    private void Start() {
        // 사용할 컴포넌트들의 참조를 가져오기
        playerInput = GetComponent<PlayerInput>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
}

    // FixedUpdate는 물리 갱신 주기에 맞춰 실행됨
    private void FixedUpdate() {
        // 물리 갱신 주기마다 움직임, 회전, 애니메이션 처리 실행
        Move();
        if (playerInput.jump && isGrounded)
        {
            playerAnimator.SetBool("Grounded", isGrounded);
            playerRigidbody.AddForce(Vector3.up * jumpForce);
        }

    }

    // 입력값에 따라 캐릭터를 앞뒤로 움직임
    private void Move() {

        float moveInput = playerInput.zmove;
        Vector3 moveDirection = new Vector3(0,0,moveInput).normalized;

        if (moveDirection !=Vector3.zero)
        {
            Vector3 forward = transform.forward;
            Vector3 movement = forward * moveDirection.z * moveSpeed * Time.deltaTime;

            // 현재 위치를 업데이트
            Vector3 newPosition = playerRigidbody.position + movement;
            playerRigidbody.MovePosition(newPosition);

            // 회전 처리
            float turn = playerInput.xmove * rotateSpeed * Time.deltaTime;
            if (turn != 0)
            {
                Quaternion deltaRotation = Quaternion.Euler(new Vector3(0, turn, 0));
                playerRigidbody.MoveRotation(playerRigidbody.rotation * deltaRotation);
            }
        }
        // 애니메이션 처리
        float moveMagnitude = moveDirection.magnitude * moveSpeed;
        playerAnimator.SetFloat("Move", moveMagnitude);
    }

    private void Jump()
    {   
        Debug.Log("Jump");
        playerAnimator.SetBool("Grounded", isGrounded);
        playerRigidbody.AddForce(Vector3.up*jumpForce);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Map"))
        {
            isGrounded = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        isGrounded =false;
        playerAnimator.SetBool("Grounded", isGrounded);
    }

}