using UnityEngine;
using UnityEngine.SceneManagement; // 씬 관리자 관련 코드
using UnityEngine.UI; // UI 관련 코드
using DG.Tweening;
using System.Collections;
using UnityEngine.Video;

// 필요한 UI에 즉시 접근하고 변경할 수 있도록 허용하는 UI 매니저
public class UIManager : MonoBehaviour {
    // 싱글톤 접근용 프로퍼티
    private static UIManager m_instance; // 싱글톤이 할당될 변수

    public Text ammoText; // 탄약 표시용 텍스트
    public Text KillText; // 점수 표시용 텍스트
    public Text levelText; // 적 웨이브 표시용 텍스트
    public GameObject gameoverUI; // 게임 오버시 활성화할 UI
    public GameObject failvideo;
    public GameObject failUI; //실패한 후 재시작 버튼
    public GameObject Option; // 소리창
    public GameObject[] mission; 

    public static UIManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<UIManager>();
            }

            return m_instance;
        }
    }
    private void Awake()
    {
        Option.SetActive(false);
        StartCoroutine("Mission");
        //UpdatelevelText();
    }
    IEnumerator Mission()
    {
        yield return new WaitForSeconds(1f);
        mission[0].SetActive(true);
        yield return new WaitForSeconds(7f);
        mission[0].SetActive(false);
        mission[1].SetActive(true);
        yield return new WaitForSeconds(7f);
        mission[1].SetActive(false);
        mission[2].SetActive(true); //생존자 구출 안내  
    }


    public void OnClickPaused()
    { //옵션버튼 눌렀을때 창이 켜지고 일시정지
        AudioManager.Instance.PlaySFX("Click");
        Option.SetActive(true);
        Time.timeScale = 0f;
    }
    
    public void OnClickResume()
    { // 창을 닫을때 다시 게임 플레이
        AudioManager.Instance.PlaySFX("Click");
        Option.SetActive(false);
        Time.timeScale = 1f;
    }

    // 탄약 텍스트 갱신
    public void UpdateAmmoText(int magAmmo, int remainAmmo) {
        ammoText.text = magAmmo + "/" + remainAmmo;
    }

    // 점수 텍스트 갱신
    public void UpdateKillText(int newScore) {
        KillText.text = "Kill : " + newScore;
    }

    // 적 웨이브 텍스트 갱신
    /*public void UpdatelevelText()
    {
        int level = PlayerPrefs.GetInt("levelkey", 1);
        levelText.text = "Lv. " + level;
    }*/

    // 게임 오버 UI 활성화
    public void SetActiveGameoverUI(bool active) {
        AudioManager.Instance.PlayMusic("Fail");
        gameoverUI.SetActive(active);
    }

    // 게임 재시작
    public void GameRestart() {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void Finish()
    {//도착지점에 도착한 후 Yes(탈출 희망)버튼을 누르면 성공여부를 확인할 메서드 실행
        if (Child.follow)
        {
            LoadScene.instance.NextScene("Success");
        }
        else
        {
            GameManager.instance.Fail();
            Debug.Log("게임ㅋ");
            StartCoroutine(Fail());
        }
    }
    IEnumerator Fail()
    {
        Debug.Log("게임실패");
        GameManager.instance.Fail(); //게임 오버
        AudioManager.Instance.PlayMusic("Fail");
        failvideo.SetActive(true); //실패 비디오 재생

        VideoPlayer videoplayer = failvideo.GetComponent<VideoPlayer>();
        videoplayer.Play();
        while (videoplayer.isPlaying)
        {
            yield return null;
        }
        FailUI();
    }
    public void FailUI()
    {
        Debug.Log("인보크");
        failUI.SetActive(true);
    }
    public void SetActiveUI(GameObject obj)
    {
        if (obj.activeSelf) {
            Time.timeScale = 1f;
            obj.SetActive(false);
        }
        else
        {
            Time.timeScale = 1f;
            obj.SetActive(true);
        }
    }
    
    
}