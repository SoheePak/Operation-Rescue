using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/*public class PlayerMove : MonoBehaviour
{
    public float speed = 3.0f;
    public InputActionProperty moveAction;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 컨트롤러 입력 => 벡터로 가져옴
        Vector2 input = moveAction.action.ReadValue<Vector2>();

        // 방향
        Vector3 moveDirection = new Vector3(input.x, 0, input.y);
        moveDirection = transform.TransformDirection(moveDirection);

        // 이동
        transform.position += moveDirection * speed * Time.deltaTime;
    }
}*/
