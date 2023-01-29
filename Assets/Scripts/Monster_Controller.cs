using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Controller : MonoBehaviour
{
    public Animator my_Anim;
    public Rigidbody2D my_Rigi;
    public SpriteRenderer SR;
    public bool can_move = true,can_hurt=true;
    public Object particle_blood,fire_death,floatPoint;
    public float damage,health,waitBack_time;
    private float wait_back;
    public Vector2 AddHealthRandom;
    public void hurt(float damage)
    {
        GameObject floatpoint= Instantiate(floatPoint, transform.position, Quaternion.identity) as GameObject;
        floatpoint.GetComponent<Getto>().child.GetComponent<TextMesh>().text=(damage * 13).ToString();
        Instantiate(particle_blood, transform.position, Quaternion.identity);
        SR.color = new Color(SR.color.r, SR.color.g, SR.color.b, 0.8f);
        if(can_hurt)my_Anim.Play("hurt");
        my_Rigi.velocity = new Vector2(0, 0);
        if(can_hurt)can_move = false;
        wait_back = waitBack_time;
        health -= damage;
        if (health <= 0)
        {
            GameObject wtf=Instantiate(fire_death, transform.position, Quaternion.identity) as GameObject;
            wtf.transform.localScale = transform.localScale;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().recover(Random.Range(AddHealthRandom.x, AddHealthRandom.y));
            Destroy(gameObject);
        }
    }
    public void Update()
    {
        if (GameObject.FindGameObjectWithTag("level_controller").GetComponent<Level_creater>().ispassed()) Destroy(gameObject);
        if (GameObject.FindGameObjectWithTag("Player")!=null && GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().health <= 0)
        {
            can_move = false;
            wait_back = -100;
            my_Anim.Play("idle");
        }
        if(wait_back!=-100)wait_back -= Time.deltaTime;
        if (wait_back <= 0&&wait_back!=-100)
        {
            wait_back = -100;
            if (can_hurt) my_Anim.Play("idle");
            can_move = true;
            SR.color = new Color(SR.color.r, SR.color.g, SR.color.b, 1);
        }
    }
}
