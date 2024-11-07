using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 좀비 게임 오브젝트를 주기적으로 생성
public class ZombieSpawner : MonoBehaviour
{
    //public Zombie zombiePrefab; // 생성할 좀비 원본 프리팹

    public ZombieData[] zombieDatas; // 사용할 좀비 셋업 데이터들
    public Transform[] spawnPoints; // 좀비 AI를 소환할 위치들

    private List<Zombie> zombies = new List<Zombie>(); // 생성된 좀비들을 담는 리스트
    private int Level; // 현재 레벨

    private void Awake()
    {
        Level = PlayerPrefs.GetInt("levelkey", 1);
    }
    private void Start()
    {
        StartCoroutine("SpawnLevel");
    }

    private void Update()
    {
        // 게임 오버 상태일때는 생성하지 않음
        if (GameManager.instance != null && GameManager.instance.isGameover)
        {
            return;
        }
    }

    // 웨이브 정보를 UI로 표시

    // 현재 웨이브에 맞춰 좀비들을 생성
    IEnumerator SpawnLevel()
    {
        yield return new WaitForSeconds(3f);
        //현재  * 1.5를 반올림한 수만큼 좀비 생성
        int spawnCount = Mathf.RoundToInt(Level * 15f);
        //spawnCount만큼 좀비 생성
        for(int i=0;i<spawnCount; i++)
        {
            //좀비 생성 처리 실행
            CreateZombie();
        }
    }

    // 좀비를 생성하고 생성한 좀비에게 추적할 대상을 할당
    private void CreateZombie()
    {
        //사용할 좀비 데이터 랜덤으로 결정
        ZombieData zombieData = zombieDatas[Random.Range(0, zombieDatas.Length)];
        //생성할 위치를 랜덤으로 결정
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        //좀비 프리팹으로부터 좀비 생성
        GameObject zombiePrefab = zombieData.zombiePrefab;
        GameObject zombieObj = Instantiate(zombiePrefab, spawnPoint.position,spawnPoint.rotation);
        Zombie zombie = zombieObj.GetComponent<Zombie>();
        zombie.Setup(zombieData); //생성된 좀비의 능력치 설정
        zombies.Add(zombie); //생성된 좀비를 리스트에 추가

        //좀비의 onDeath 이벤트에 익명 메서드 등록 (람다식)
        zombie.onDeath += () => zombies.Remove(zombie); //사망한 좀비를 리스트에서 제거
        zombie.onDeath += () => Destroy(zombie.gameObject,10f);//사망한 좀비를 10초 뒤에 파괴
        zombie.onDeath += () => GameManager.instance.AddExp(1); //좀비 사망 시 점수 상승
    }
}