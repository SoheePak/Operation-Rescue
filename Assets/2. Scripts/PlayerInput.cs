using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;
using System;

// 플레이어 캐릭터를 조작하기 위한 사용자 입력을 감지
// 감지된 입력값을 다른 컴포넌트들이 사용할 수 있도록 제공
public class PlayerInput : MonoBehaviour {
    public string zmoveAxisName = "Vertical"; // 앞뒤 움직임을 위한 입력축 이름
    public string xmoveAxisName = "Horizontal"; // 좌우 움직임을 위한 입력축 이름
    public string fireButtonName = "Fire1"; // 발사를 위한 입력 버튼 이름
    public Button fireButton;
    public string reloadButtonName = "Reload"; // 재장전을 위한 입력 버튼 이름
    public string jumpName = "Jump";
    public VariableJoystick joystick;

    // 값 할당은 내부에서만 가능
    public float zmove { get; private set; } // 감지된 움직임 입력값
    public float xmove { get; private set; } // 감지된 좌우 움직임 입력값
    public float rotate { get; private set; } // 감지된 회전 입력값
    public bool fire { get; private set; } // 감지된 발사 입력값
    public bool reload { get; private set; } // 감지된 재장전 입력값
    public bool jump { get; private set; }

    // 매프레임 사용자 입력을 감지
    private void FixedUpdate() {
        // 게임오버 상태에서는 사용자 입력을 감지하지 않는다
        if (GameManager.instance != null && GameManager.instance.isGameover)
        {
            zmove = 0;
            xmove = 0;
            rotate = 0;
            fire = false;
            reload = false;
            jump = false;
            return;
        }
        if(fire) fire = false;
        if(reload) reload = false;
        //zmove = Input.GetAxis(zmoveAxisName);                       // move에 관한 입력 감지
        //xmove = Input.GetAxis(xmoveAxisName);                       // move에 관한 입력 감지
        xmove = joystick.Horizontal;
        zmove = joystick.Vertical;
        rotate = joystick.Horizontal;
        //rotate = Input.GetAxis(xmoveAxisName);
        //fire = Input.GetButtonDown(fireButtonName);
        reload = Input.GetButtonDown(reloadButtonName);             // reload에 관한 입력 감지
        jump = Input.GetButtonDown(jumpName);
    }
    public void FireOnClick()
    {
        fire = true;
    }
    public void ReloadOnClick()
    {
        reload = true;
    }

}