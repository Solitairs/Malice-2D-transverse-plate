using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_atk : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "monster")
        {
            float add_atk = 0;
            if (transform.parent.GetComponent<PlayerController>().Player_anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "HeroKnight_Attack3")
            {
                add_atk = 3f;
            }
            else if(transform.parent.GetComponent<PlayerController>().Player_anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "HeroKnight_Attack2"){
                add_atk = 1.5f;
            }
            collision.gameObject.GetComponent<Monster_Controller>().hurt(transform.parent.GetComponent<PlayerController>().damage+add_atk);
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>().SetTrigger("shake");
        }
    }
}
