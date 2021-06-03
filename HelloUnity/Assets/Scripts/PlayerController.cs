using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRigidbody;
    public float speed = 8f;
    public float rotSpeed = 120.0f;

    private Transform tr;

    public int hp = 100;
    public HPBar hpbar;

    private float spawnRate = 0.2f; // 플레이어는 0.2초마다 총알 발사!
    private float timerAfterSpawn;
    public GameObject playerbulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        timerAfterSpawn = 0f;
        tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        float xSpeed = xInput * speed;
        float zSpeed = zInput * speed;
        Vector3 newVelocity;
        newVelocity = new Vector3(xSpeed, -8f, zSpeed);
        playerRigidbody.velocity = newVelocity;


        RaycastHit hit = new RaycastHit();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray.origin, ray.direction, out hit))
        {
            //Debug.Log();
            Vector3 proejctedPos = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            Vector3 currentPos = transform.position;
            Vector3 rotation = proejctedPos - currentPos;
            tr.forward = rotation;
        }


        timerAfterSpawn += Time.deltaTime;

        if (Input.GetButton("Fire1") && timerAfterSpawn >= spawnRate)
        {
            timerAfterSpawn = 0;
            GameObject bullet = Instantiate(playerbulletPrefab, transform.position, transform.rotation);
        }

    }

    public void GetDamage(int damage)
    {
        hp -= damage;
        hpbar.SetHP(hp);
        if (hp <= 0)
        {
            Die();
        }
    }

    public void GetHeal(int heal)
    {
        hp += heal;
        if(hp > 100)
        {
            hp = 100;
        }
        hpbar.SetHP(hp);
    }

     void Die()
    {
        gameObject.SetActive(false);

        GameManager gameManager = FindObjectOfType<GameManager>();
        gameManager.EndGame();
    }
}
