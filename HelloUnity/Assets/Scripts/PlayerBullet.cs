using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 8f;
    private Rigidbody bulletRigidbody;
    public int damage = 100;  

   

    // Start is called before the first frame update
    void Start()
    {
        bulletRigidbody = GetComponent<Rigidbody>();
        bulletRigidbody.velocity = transform.forward * 8f;

        Destroy(gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // Bullet에 맞는 경우 서로 상쇄 시킴!
        // BulletSpawner에 맞는 경우 데미지를 줌
        if (other.tag == "Bullet")
        {
            Bullet bullet = other.GetComponent<Bullet>();

            if(bullet != null)
            {
                Destroy(bullet.gameObject);
                //playerController.Die();
            }

            Destroy(gameObject);
        }
        else if(other.tag == "BulletSpawner")
        {
           
            BulletSpawner spawner = other.GetComponent<BulletSpawner>();

            if (spawner != null)
            {
                Debug.Log(other.tag);
                spawner.GetDamage(damage);
                //playerController.Die();
            }

            Destroy(gameObject);
        }
    }
}
