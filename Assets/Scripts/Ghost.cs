using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Monster_Controller
{
    public Object fire_ball;
    public float speed;
    private int fire_level;
    private float wait_fire=0;
    private Vector2 goat;
    private GameObject fire_ball_t;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (can_move && GameObject.FindGameObjectWithTag("Player") != null && Vector2.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position + new Vector3(0.01f, 0.86f)) < 20)
        {
            wait_fire -= Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, goat, speed * Time.deltaTime);
            if (transform.position.x == goat.x && transform.position.y == goat.y)
            {
                if (wait_fire <= 0 && fire_level < 3)
                {
                    wait_fire = 0.33f;
                    fire_level++;
                    Vector2 wtf = GameObject.FindGameObjectWithTag("Player").transform.position + new Vector3(0.01f, 0.86f) - transform.position;
                    wtf.Normalize();
                    fire_ball_t = Instantiate(fire_ball, transform.position, Quaternion.identity) as GameObject;
                    fire_ball_t.GetComponent<Fire_Ball>().way = wtf;
                    fire_ball_t.GetComponent<Fire_Ball>().damage = damage;
                }
                else if(fire_level >2 )
                {
                    fire_level = 0;
                    goat = new Vector3(Random.Range(0.15f, 1f) * 10, Random.Range(0.15f, 1f) * 8) + GameObject.FindGameObjectWithTag("Player").transform.position + new Vector3(0.01f, 0.86f);
                }
            }

        }
    }
}
