using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class BulletSpawner : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float spawnRateMin = 0.5f;
    public float spawnRateMax = 0.5f;

    private Transform target;
    private float spawnRate;
    private float timerAfterSpawn;

    public int hp = 100;
    public HPBar hpbar;
    public GameObject level;
    
    public bool isMoving = false; //���� �������� �ƴϸ� �Ѿư��鼭 ���ݻ��������� �Ǵ��ϴ� ����
    private NavMeshAgent nvAgent; // �׺���̼��� ���� ����
    Animator animator; // �ִϸ��̼� ó���� ���� ����

    // Start is called before the first frame update
    void Start()
    {
        timerAfterSpawn = 0f;
        spawnRate = Random.Range(spawnRateMin, spawnRateMax);
        target = FindObjectOfType<PlayerController>().transform;
        StartCoroutine(MonsterAI());
        nvAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }
    // �ڷ�ƾ���� ����� ���� AI, ���Ͱ� ��� �ִ� ���� 0.2�ʾ� ��� �����ٰ�
    // �����̴� ���°� ���̸� �÷��̾ �Ѿư���, �����̸� ����������
    IEnumerator MonsterAI()
    {
        while(hp > 0)
        {
            yield return new WaitForSeconds(0.2f);

            if(isMoving)
            {
                nvAgent.destination = target.position;
                nvAgent.isStopped = false;
                animator.SetBool("isMoving", true);
            }
            else
            {
                nvAgent.isStopped = true;
                animator.SetBool("isMoving", false);
            }
        }
        
    }


    public void GetDamage(int damage)
    {
        if (hp <= 0)
            return;

        hp -= damage;
        hpbar.SetHP(hp);
        Debug.Log("BulletSpawner:" + hp);
        if (hp <= 0)
        {
            //meObject.SetActive(false);
            animator.SetTrigger("Die");
            GameManager2 gameManager = FindObjectOfType<GameManager2>();
            gameManager.DieBulletSpawner(gameObject);
            Destroy(gameObject, 5f); // 5�ʵڿ� ����
            
        }
    }


    

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0)
            return;

        timerAfterSpawn += Time.deltaTime;

        if(timerAfterSpawn >= spawnRate)
        {
            timerAfterSpawn = 0;
            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            bullet.transform.LookAt(target);
            transform.LookAt(target);
            spawnRate = Random.Range(spawnRateMin, spawnRateMax);
                            

        }
    }

    
}
