using UnityEngine;
using UnityEngine.PlayerLoop;

// 점수와 게임 오버 여부를 관리하는 게임 매니저
public class GameManager : MonoBehaviour {
    // 싱글톤 접근용 프로퍼티
    public static GameManager instance
    {
        get
        {
            // 만약 싱글톤 변수에 아직 오브젝트가 할당되지 않았다면
            if (m_instance == null)
            {
                // 씬에서 GameManager 오브젝트를 찾아 할당
                m_instance = FindObjectOfType<GameManager>();
            }

            // 싱글톤 오브젝트를 반환
            return m_instance;
        }
    }

    private static GameManager m_instance; // 싱글톤이 할당될 static 변수

    public int kill { get; private set; } // 죽인 좀비 수
    public bool isGameover { get; private set; } // 게임 오버 상태
    private int exp;
    private const string PlayerExpKey = "PlayerExpKey"; //PlayerPrefs키 선언, 초기화
    public EXP Exp;

    private void Awake() {
        // 씬에 싱글톤 오브젝트가 된 다른 GameManager 오브젝트가 있다면
        if (instance != this)
        {
            // 자신을 파괴
            Destroy(gameObject);
        }
        kill = 0;
        exp = PlayerPrefs.GetInt(PlayerExpKey, 0);
    }

    private void Start() {
        // 플레이어 캐릭터의 사망 이벤트 발생시 게임 오버
        FindObjectOfType<PlayerHealth>().onDeath+= EndGame;
    }

    // 점수를 추가하고 UI 갱신
    public void AddExp(int newScore) {
        //게임 오버가 아닌 상태에서만 좀비 사망시 킬수 증가
        if (!isGameover)
        {
            // 점수 추가
            kill += newScore;
            // 경험치 계산
            int gainedExp = newScore * 10; // 예: 점수에 비례하여 경험치 추가
            int currentExp = PlayerPrefs.GetInt(PlayerExpKey, 0);
            currentExp += gainedExp;

            // 경험치를 PlayerPrefs에 저장
            PlayerPrefs.SetInt(PlayerExpKey, currentExp);
            PlayerPrefs.Save();

            // EXP 업데이트
            EXP expComponent = FindObjectOfType<EXP>();
            if (expComponent != null)
            {
                expComponent.ExpInspect(); // 경험치 검사 및 UI 업데이트
            }


            // 점수 UI 텍스트 갱신
            UIManager.instance.UpdateKillText(kill);
        }
        
    }

    // 게임 오버 처리
    public void EndGame() { //죽었을 때
        // 게임 오버 상태를 참으로 변경
        isGameover = true;
        // 게임 오버 UI를 활성화
        UIManager.instance.SetActiveGameoverUI(true);
    }
    public void Fail()
    {
        isGameover = true;
    }
}