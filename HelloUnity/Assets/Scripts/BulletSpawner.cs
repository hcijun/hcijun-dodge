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
    
    public bool isMoving = false; //서서 공격인지 아니면 쫓아가면서 공격상태인지를 판단하는 변수
    private NavMeshAgent nvAgent; // 네비게이션을 위한 변수
    Animator animator; // 애니메이션 처리를 위한 변수

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
    // 코루틴으로 만드는 몬스터 AI, 몬스터가 살아 있는 동안 0.2초씩 잠깐 쉬었다가
    // 움직이는 상태가 참이면 플레이어를 쫓아가고, 거짓이면 정지해있음
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
            Destroy(gameObject, 5f); // 5초뒤에 삭제
            
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
