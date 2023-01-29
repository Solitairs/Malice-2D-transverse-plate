using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Normal_Bullet : MonoBehaviour
{
    public Vector3 vector;

    private float speed,life_time=4;
    private void Start()
    {
        speed = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().bullet_speed;
    }
    void Update()
    {
        transform.position += vector*speed*Time.deltaTime;
        life_time -= Time.deltaTime;
        if(life_time<0)Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "monster")
        {
            collision.gameObject.GetComponent<Monster_Controller>().hurt(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().nBullet_Damage);
        }
        Destroy(gameObject);
    }
}
