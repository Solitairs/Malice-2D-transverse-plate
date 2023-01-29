using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
public class PlayerController : MonoBehaviour
{
    public Animator Player_anim;
    public Rigidbody2D Player_rigi;
    public PolygonCollider2D attack_area;
    public BoxCollider2D player_Box;
    public float move_speed,jump_long,bullet_speed;
    public Object normal_bullet,floatPoint;
    public bool faceTo=false;
    public float health = 100,vincibleTime=0;
    public bool isInvincible=false;
    public float nBullet_Damage = 1,damage;
    public Vector2[] into_way;
    private bool canmove = true,canjump=true;
    private float wait_bui=0,wait_atk=0,ctMoveTime=0;
    private string fuck = "idle";
    private GameObject temp_Bullet_normal;
    // Start is called before the first frame update
    private void Awake()
    {
        if(PlayerPrefs.GetInt("out_way")!=-1) transform.position = into_way[PlayerPrefs.GetInt("out_way")];
    }
    private void Start()
    {
        health = PlayerPrefs.GetFloat("player_health");
    }
    public void setInvincible(float time)
    {
        isInvincible = true;
        vincibleTime = time;
    }
    private static GameObject[] monsters;
    private float minDistance;
    private Vector2 minDPos;
    void Update()
    {
        monsters = GameObject.FindGameObjectsWithTag("monster");
        if (waitdeath)
        {
            Player_rigi.velocity = new Vector2(0, 0);
            for (int i = 0; i < monsters.Length; i++)
            {
                monsters[i].GetComponent<Monster_Controller>().can_move = false;
                monsters[i].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, Mathf.MoveTowards(monsters[i].GetComponent<SpriteRenderer>().color.a, 0, 0.333333f * Time.deltaTime));
                monsters[i].GetComponent<Light2D>().pointLightOuterRadius = Mathf.MoveTowards(monsters[i].GetComponent<Light2D>().pointLightOuterRadius, 0, 0.5f * Time.deltaTime);
            }
        }
        ctMoveTime -= Time.deltaTime;
        if (ctMoveTime <= 0)
        {
            canmove = true;
        }
        if (isInvincible)
        {
            vincibleTime -= Time.deltaTime;
            if (vincibleTime <= 0)
            {
                isInvincible = false;
                vincibleTime = 0;
            }
        }
        if (canmove)
        {
            if (Player_rigi.velocity.y > 0.05f)
            {
                Player_anim.SetBool("isjump", true);
            }
            else
            {
                Player_anim.SetBool("isjump", false);
            }
            if (Player_rigi.velocity.y < -0.5f)
            {
                Player_anim.SetBool("isfall", true);
            }
            else
            {
                Player_anim.SetBool("isfall", false);
            }
            if (wait_atk > 0) wait_atk -= Time.deltaTime;
            if (wait_atk <= 0) attack_area.enabled = false;
            if (Player_anim.GetCurrentAnimatorClipInfo(0)[0].clip.name != "HeroKnight_Attack3" && Player_anim.GetCurrentAnimatorClipInfo(0)[0].clip.name != "HeroKnight_Attack2" && Player_anim.GetCurrentAnimatorClipInfo(0)[0].clip.name != "HeroKnight_Attack1")
            {
                fuck = "idle";
            }
            if (fuck != Player_anim.GetCurrentAnimatorClipInfo(0)[0].clip.name && (Player_anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "HeroKnight_Attack3" || Player_anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "HeroKnight_Attack2" || Player_anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "HeroKnight_Attack1"))
            {
                fuck = Player_anim.GetCurrentAnimatorClipInfo(0)[0].clip.name;
                attack_area.enabled = true;
                wait_atk = 0.35f;
            }

            if (Input.GetButtonDown("Fire"))
            {
                Player_anim.SetTrigger("add_attack");

            }
            if (Input.GetButton("left"))
            {
                if (monsters.Length > 0)
                {
                    minDistance = 10000;
                    for (int i = 0; i < monsters.Length; i++)
                    {
                        if (minDistance > Vector2.Distance(monsters[i].transform.position, transform.position))
                        {
                            minDistance = Vector2.Distance(monsters[i].transform.position, transform.position);
                            minDPos = monsters[i].transform.position;
                        }
                    }
                    if (monsters.Length > 0)
                    {
                        if (minDPos.x >= transform.position.x)
                        {
                            faceTo = true;
                            transform.rotation = Quaternion.Euler(0, 0, 0);
                        }
                        else
                        {
                            faceTo = false;
                            transform.rotation = Quaternion.Euler(0, 180, 0);
                        }
                    }
                }
                else
                {
                    faceTo = false;
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                }
                if (!(Player_anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "HeroKnight_Attack3" || Player_anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "HeroKnight_Attack2" || Player_anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "HeroKnight_Attack1"))
                {
                    Player_rigi.velocity = new Vector2(-move_speed, Player_rigi.velocity.y);
                }
                else
                {
                    Player_rigi.velocity = new Vector2(-move_speed*0.65f, Player_rigi.velocity.y);
                }
                Player_anim.SetBool("isrun", true);
            }
            else if (Input.GetButton("right"))
            {
                if (monsters.Length > 0)
                {
                    minDistance = 10000;
                    for (int i = 0; i < monsters.Length; i++)
                    {
                        if (minDistance > Vector2.Distance(monsters[i].transform.position, transform.position))
                        {
                            minDistance = Vector2.Distance(monsters[i].transform.position, transform.position);
                            minDPos = monsters[i].transform.position;
                        }
                    }
                    if (monsters.Length > 0)
                    {
                        if (minDPos.x >= transform.position.x)
                        {
                            faceTo = true;
                            transform.rotation = Quaternion.Euler(0, 0, 0);
                        }
                        else
                        {
                            faceTo = false;
                            transform.rotation = Quaternion.Euler(0, 180, 0);
                        }
                    }
                }
                else
                {
                    faceTo = true;
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                if (!(Player_anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "HeroKnight_Attack3" || Player_anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "HeroKnight_Attack2" || Player_anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "HeroKnight_Attack1"))
                {
                    Player_rigi.velocity = new Vector2(move_speed, Player_rigi.velocity.y);
                }
                else
                {
                    Player_rigi.velocity = new Vector2(move_speed * 0.65f, Player_rigi.velocity.y);
                }
                Player_anim.SetBool("isrun", true);
            }
            else
            {
                Player_anim.SetBool("isrun", false);
                Player_rigi.velocity = new Vector2(0, Player_rigi.velocity.y);
            }

            if (player_Box.IsTouchingLayers()) canjump = true;
            if (Input.GetButtonDown("Jump"))
            {
                if (player_Box.IsTouchingLayers())
                {
                    Player_rigi.velocity = new Vector2(Player_rigi.velocity.x, jump_long);
                }
                else if (canjump)
                {
                    Player_rigi.velocity = new Vector2(Player_rigi.velocity.x, jump_long);
                    canjump = false;
                }
            }
            if (player_Box.IsTouchingLayers(LayerMask.GetMask("ground")) || player_Box.IsTouchingLayers(LayerMask.GetMask("monster"))) Player_anim.SetBool("isjump", false);
        }
    }
    public void recover(float value)
    {
        health += value;
        if (health > 100) health = 100;
        GameObject floatpoint = Instantiate(floatPoint, transform.position + new Vector3(0, 1), Quaternion.identity) as GameObject;
        floatpoint.GetComponent<Getto>().child.GetComponent<TextMesh>().text = (Mathf.Round(value*10)/10).ToString();
        floatpoint.GetComponent<Getto>().child.GetComponent<TextMesh>().color = new Color(0, 1, 0);
    }
    public bool isdeath = false, waitdeath = false;
    IEnumerator death_wait()
    {
        yield return new WaitForSeconds(3.5f);
        isdeath = true;
    }
    public void hurt(float damage,float vincibleTime=0,float ctmt=0)
    {
        if (!isInvincible)
        {
            GameObject floatpoint = Instantiate(floatPoint, transform.position+new Vector3(0,1) , Quaternion.identity) as GameObject;
            floatpoint.GetComponent<Getto>().child.GetComponent<TextMesh>().text = damage.ToString();
            Player_anim.Play("HeroKnight_Hurt");
            setInvincible(vincibleTime);
            canmove = false;
            ctMoveTime = ctmt;
            health -= damage;
            if (health <= 0)
            {
                health = 0;
                Player_anim.Play("HeroKnight_Death");
                setInvincible(100);
                ctMoveTime = 1000;
                canmove = false;
                waitdeath = true;
                StartCoroutine(death_wait());
            }
        }
    }
}
