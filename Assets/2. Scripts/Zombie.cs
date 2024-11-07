using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement; // AI, ������̼� �ý��� ���� �ڵ� ��������

// ���� AI ����
public class Zombie : LivingEntity
{
    public LayerMask whatIsTarget; // ���� ��� ���̾�

    private LivingEntity targetEntity; // ���� ���
    private NavMeshAgent navMeshAgent; // ��� ��� AI ������Ʈ

    public ParticleSystem hitEffect; // �ǰ� �� ����� ��ƼŬ ȿ��
    public AudioClip deathSound; // ��� �� ����� �Ҹ�
    public AudioClip hitSound; // �ǰ� �� ����� �Ҹ�
    public AudioClip AttackSound; //���� �� ����� �Ҹ�

    private Animator zombieAnimator; // �ִϸ����� ������Ʈ
    private AudioSource zombieAudioPlayer; // ����� �ҽ� ������Ʈ
    private Renderer zombieRenderer; // ������ ������Ʈ , ���� �� �ٲ� �� ���
    private GameObject zombiePrefab; // ���� ��� ����

    public float damage = 20f; // ���ݷ�
    public float timeBetAttack = 0.5f; // ���� ����
    private float lastAttackTime; // ������ ���� ����

    // ������ ����� �����ϴ��� �˷��ִ� ������Ƽ
    private bool hasTarget
    {
        get
        {
            // ������ ����� �����ϰ�, ����� ������� �ʾҴٸ� true
            if (targetEntity != null && !targetEntity.dead)
            {
                return true;
            }

            // �׷��� �ʴٸ� false
            return false;
        }
    }

    private void Awake()
    {
        // ���� ������Ʈ�κ��� ����� ������Ʈ ��������
        navMeshAgent = GetComponent<NavMeshAgent>();
        zombieAnimator = GetComponent<Animator>();
        zombieAudioPlayer = GetComponent<AudioSource>();

        //������ ������Ʈ�� �ڽ� ���� ������Ʈ�� �����Ƿ� Children�� ���
        zombieRenderer = GetComponentInChildren<Renderer>();
        zombieAnimator.SetBool("Attack", false);
    }

    // ���� AI�� �ʱ� ������ �����ϴ� �¾� �޼���
    public void Setup(ZombieData zombieData)
    {
        int currentlevel = PlayerPrefs.GetInt("levelkey", 1); // ���� ����
        //ü�� ����
        startingHealth = zombieData.health + (currentlevel *0.5f);
        health = zombieData.health + (currentlevel * 0.5f);
        damage = zombieData.damage + 5f;
        //���ݷ� ����
        if (currentlevel >= 3)
        {
            //����޽� ������Ʈ�� �̵� �ӵ� ����
            navMeshAgent.speed = zombieData.speed + 1f;
        }
        else
        {
            //����޽� ������Ʈ�� �̵� �ӵ� ����
            navMeshAgent.speed = zombieData.speed + (currentlevel * 0.5f);

        }
    
        //�������� ��� ���� ��Ƽ������ �÷��� ����, ���� ���� ����
        //zombieRenderer.material.color = zombieData.skinColor;
        //zombiePrefab = zombieData.zombiePrefab;

        
    }

    private void Start()
    {
        // ���� ������Ʈ Ȱ��ȭ�� ���ÿ� AI�� ���� ��ƾ ����
        if(SceneManager.GetActiveScene().name != "Tutorial")
        {
            StartCoroutine(UpdatePath());
        }
        
    }

    private void Update()
    {
        // ���� ����� ���� ���ο� ���� �ٸ� �ִϸ��̼� ���
        zombieAnimator.SetBool("HasTarget", hasTarget);
    }

    // �ֱ������� ������ ����� ��ġ�� ã�� ��� ����
    private IEnumerator UpdatePath()
    {
        // ��� �ִ� ���� ���� ����
        while (!dead)
        {
            if(hasTarget)
            {
                yield return new WaitForSeconds(1f);
                //���� ��� ����: ��θ� �����ϰ� AI�̵��� ��� ����
                if (navMeshAgent.isActiveAndEnabled && navMeshAgent.isOnNavMesh)
                {
                    navMeshAgent.isStopped = false;
                    // ��ǥ ��ġ�� �޾� ���� �޴� �޼���
                    navMeshAgent.SetDestination(targetEntity.transform.position);
                }
            }
            else
            {
                //���� ��� ����:AI �̵� ����
                navMeshAgent.isStopped = true;

                //20������ �������� ���� ������ ���� �׷��� �� ���� ��ġ�� ��� �ݶ��̴��� ������
                //��, whatIsTarget ���̾ ���� �ݶ��̴��� ���������� ���͸�
                Collider[] colliders = Physics.OverlapSphere(transform.position,20f,whatIsTarget);

                //��� �ݶ��̴��� ��ȸ�ϸ鼭 ��� �ִ� LivingEntity ã��
                for(int i=0;i<colliders.Length;i++)
                {
                    //�ݶ��̴��κ��� LivingEntity ������Ʈ ��������
                    LivingEntity livingEntity = colliders[i].GetComponent<LivingEntity>();

                    //Livingentity ������Ʈ�� �����ϸ�, �ش� LivigEntity�� ��� �ִٸ�
                    if(livingEntity != null && !livingEntity.dead)
                    {
                        //���� ����� �ش� LivingEntity�� ����
                        targetEntity = livingEntity;
                        //for �� ���� ��� ����
                        break;
                    }

                }
            }
            // 0.25�� �ֱ�� ó�� �ݺ�
            yield return new WaitForSeconds(0.25f);
        }
    }

    // �������� �Ծ��� �� ������ ó��
    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        if(!dead)
        {
            //���ݹ��� ������ �������� ��ƼŬ ȿ�� ���
            hitEffect.transform.position = hitPoint;
            hitEffect.transform.rotation = Quaternion.LookRotation(hitNormal);
            hitEffect.Play();

            //�ǰ� ȿ���� ���
            zombieAudioPlayer.PlayOneShot(hitSound);
        }
        
        // LivingEntity�� OnDamage()�� �����Ͽ� ������ ����
        base.OnDamage(damage, hitPoint, hitNormal);
    }

    // ��� ó��
    public override void Die()
    {
        // LivingEntity�� Die()�� �����Ͽ� �⺻ ��� ó�� ����
        base.Die();

        //�ٸ� AI�� �������� �ʵ��� �ڽ��� ��� �ݶ��̴��� ��Ȱ��ȭ
        Collider[] zombieColliders = GetComponents<Collider>();
        for(int i= 0; i<zombieColliders.Length; i++)
        {
            zombieColliders[i].enabled =false;
        }
        //AI ������ �����ϰ� ����޽� ������Ʈ ��Ȱ��ȭ
        navMeshAgent.isStopped = true;
        navMeshAgent.enabled = false;
        //��� �ִϸ��̼� ���
        zombieAnimator.SetTrigger("Die");
        //��� ȿ���� ���
        zombieAudioPlayer.PlayOneShot(deathSound);
    }

    private void OnTriggerStay(Collider other)
    {
        //�ڽ��� ������� �ʾ�����.
        // �ֱ� ���� �������� timeBetAttack�̻� �ð��� �����ٸ� ���� ����
        if(!dead && Time.time >= lastAttackTime + timeBetAttack)
        {
            //������ LivingEntity Ÿ�� �������� �õ�
            LivingEntity attackTarget = other.GetComponent<LivingEntity>();

            //������ LivingEntity�� �ڽ��� ���� ����̶�� ���� ����
            if(attackTarget != null && attackTarget == targetEntity)
            {
                //�ֱ� ���� �ð� ����
                lastAttackTime = Time.time;

                //������ �ǰ� ��ġ�� �ǰ� ������ �ٻ����� ���
                Vector3 hitPoint = other.ClosestPoint(transform.position);
                Vector3 hitNormal = transform.position - other.transform.position;
                zombieAnimator.SetBool("Attack", true);
                zombieAudioPlayer.PlayOneShot(AttackSound);

                //���� ����
                attackTarget.OnDamage(damage, hitPoint, hitNormal);
            }
            else
            {
                zombieAnimator.SetBool("Attack", false);
            }
        }
        // Ʈ���� �浹�� ���� ���� ������Ʈ�� ���� ����̶�� ���� ����
    }
}