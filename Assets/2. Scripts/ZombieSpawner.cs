using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� ���� ������Ʈ�� �ֱ������� ����
public class ZombieSpawner : MonoBehaviour
{
    //public Zombie zombiePrefab; // ������ ���� ���� ������

    public ZombieData[] zombieDatas; // ����� ���� �¾� �����͵�
    public Transform[] spawnPoints; // ���� AI�� ��ȯ�� ��ġ��

    private List<Zombie> zombies = new List<Zombie>(); // ������ ������� ��� ����Ʈ
    private int Level; // ���� ����

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
        // ���� ���� �����϶��� �������� ����
        if (GameManager.instance != null && GameManager.instance.isGameover)
        {
            return;
        }
    }

    // ���̺� ������ UI�� ǥ��

    // ���� ���̺꿡 ���� ������� ����
    IEnumerator SpawnLevel()
    {
        yield return new WaitForSeconds(3f);
        //����  * 1.5�� �ݿø��� ����ŭ ���� ����
        int spawnCount = Mathf.RoundToInt(Level * 15f);
        //spawnCount��ŭ ���� ����
        for(int i=0;i<spawnCount; i++)
        {
            //���� ���� ó�� ����
            CreateZombie();
        }
    }

    // ���� �����ϰ� ������ ���񿡰� ������ ����� �Ҵ�
    private void CreateZombie()
    {
        //����� ���� ������ �������� ����
        ZombieData zombieData = zombieDatas[Random.Range(0, zombieDatas.Length)];
        //������ ��ġ�� �������� ����
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        //���� ���������κ��� ���� ����
        GameObject zombiePrefab = zombieData.zombiePrefab;
        GameObject zombieObj = Instantiate(zombiePrefab, spawnPoint.position,spawnPoint.rotation);
        Zombie zombie = zombieObj.GetComponent<Zombie>();
        zombie.Setup(zombieData); //������ ������ �ɷ�ġ ����
        zombies.Add(zombie); //������ ���� ����Ʈ�� �߰�

        //������ onDeath �̺�Ʈ�� �͸� �޼��� ��� (���ٽ�)
        zombie.onDeath += () => zombies.Remove(zombie); //����� ���� ����Ʈ���� ����
        zombie.onDeath += () => Destroy(zombie.gameObject,10f);//����� ���� 10�� �ڿ� �ı�
        zombie.onDeath += () => GameManager.instance.AddExp(1); //���� ��� �� ���� ���
    }
}