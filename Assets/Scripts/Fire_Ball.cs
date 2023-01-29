using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_Ball : MonoBehaviour
{
    public Vector2 way;//œÚ¡ø
    public float speed,damage,health;
    public Object particle;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        health -= Time.deltaTime;
        if (health < 0) Destroy(gameObject);
        transform.position += new Vector3((way * speed * Time.deltaTime).x, (way * speed * Time.deltaTime).y);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().hurt(damage,0.2f,0.2f);
        }
        else if (collision.gameObject.tag == "Player_atk")
        {
            if (Random.Range(0, 4) > 1)
            {
                return;
            }
        }
        Instantiate(particle,transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
