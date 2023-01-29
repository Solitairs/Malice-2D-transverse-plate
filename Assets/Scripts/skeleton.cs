using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skeleton : Monster_Controller
{    // Start is called before the first frame update
    public BoxCollider2D foot,forward;
    public PolygonCollider2D attack_area;
    public float speed,jump,atk_away;
    public bool canjump_anim=false;
    private float wait;
    private bool canAtk=true;
    IEnumerator back_atk()
    {
        yield return new WaitForSeconds(0.3f);
        attack_area.enabled = false;
        yield return new WaitForSeconds(0.5f);
        canAtk = true;
    }
    // Update is called once per frame
    void Update()
    {
        base.Update();
        if (can_move&& Vector2.Distance(GameObject.FindGameObjectWithTag("Player").transform.position + new Vector3(0.01f, 0.86f), transform.position) < 15&& GameObject.FindGameObjectWithTag("Player") != null)
        {
            if (attack_area.IsTouchingLayers(LayerMask.GetMask("Player")))
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().hurt(damage,0,0.1f);
                attack_area.enabled = false;
            }
            if (Mathf.Abs(my_Rigi.velocity.x) > 0.1f) my_Anim.SetBool("isrun", true);
            if (Mathf.Abs(my_Rigi.velocity.x) <= 0.1f) my_Anim.SetBool("isrun", true);
            if (canjump_anim)
            {
                if (my_Rigi.velocity.y > 0.2f) my_Anim.SetBool("isjump", true);
                else my_Anim.SetBool("isjump", false);
            }
            if (Vector2.Distance(GameObject.FindGameObjectWithTag("Player").transform.position + new Vector3(0.01f, 0.86f), transform.position) < 15)
            {
                if (Vector2.Distance(GameObject.FindGameObjectWithTag("Player").transform.position + new Vector3(0.01f, 0.86f), transform.position) <= atk_away)
                {
                    if (canAtk)
                    {
                        my_Anim.Play("attack");
                        attack_area.enabled = true;
                        canAtk = false;
                        StartCoroutine(back_atk());
                    }
                }
                else
                {
                    if((GameObject.FindGameObjectWithTag("Player").transform.position.y + 0.86f - transform.position.y > 1.2f||forward.IsTouchingLayers()) && wait <= 0&&foot.IsTouchingLayers())
                    {
                        my_Rigi.velocity = new Vector2(my_Rigi.velocity.x, jump);
                        wait = 2f;
                    }
                    if(GameObject.FindGameObjectWithTag("Player").transform.position.x + 0.01f - transform.position.x<0)
                    {
                        transform.rotation = Quaternion.Euler(0, 0, 0);
                        my_Rigi.velocity = new Vector2(-speed, my_Rigi.velocity.y);
                    }
                    else
                    {
                        transform.rotation = Quaternion.Euler(0, 180 , 0);
                        my_Rigi.velocity = new Vector2(speed, my_Rigi.velocity.y);
                    }
                }
            }
            if (wait > 0)
            {
                wait -= Time.deltaTime;
                if (wait < 0)
                {
                    wait = 0;
                }
            }
        }
        if (GameObject.FindGameObjectWithTag("Player") == null || Vector2.Distance(GameObject.FindGameObjectWithTag("Player").transform.position + new Vector3(0.01f, 0.86f), transform.position) >= 15)
        {
            my_Rigi.velocity = new Vector2(0, my_Rigi.velocity.y);
        }
    }
}
