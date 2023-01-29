using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_creater : MonoBehaviour
{
    // Start is called before the first frame update
    public static int[][] map;
    public static bool[][] passed;
    public int level_num;
    public static Vector2 player_now;
    public Object[] levels;
    public Object block;
    public bool created=false;
    public GameObject Mcanvas;
    private GameObject Level;
    private Queue<Vector2> map_queue;
    void Awake()
    {
        createLevel();
    }
    public bool ispassed()
    {
        return passed[(int)player_now.x][(int)player_now.y];
    }
    public void createLevel()
    {
        PlayerPrefs.SetFloat("player_health", 100);
        PlayerPrefs.SetInt("out_way", -1);
        PlayerPrefs.Save();
        map = new int[23][];
        passed = new bool[23][];
        for (int i = 0; i < 23; i++)
        {
            map[i] = new int[23];
            passed[i] = new bool[23];
            for (int ii = 0; ii < 23; ii++)
            {
                map[i][ii] = -1;
                passed[i][ii] = false;
            }
        }
        map[11][11] = 0;
        int now_num=1;
        bool get;
        level_num = Mathf.RoundToInt(Random.Range(4, 10));
        map_queue = new Queue<Vector2>();
        map_queue.Enqueue(new Vector2(11, 11));
        player_now = new Vector2(11, 11);
        while (now_num < level_num&&map_queue.Count>0)
        {
            Vector2 temp=map_queue.Dequeue();
            bool getm = false;
            //create rooms
            while (!getm)
            {

                if (map[(int)temp.x][(int)temp.y + 1] == -1)
                {
                    if (Random.Range(0, 5) < 1.8f)
                    {
                        getm = true;
                        now_num++;
                        map[(int)temp.x][(int)temp.y + 1] = Mathf.RoundToInt(Random.Range(0.5f, levels.Length - 0.6f));
                        map_queue.Enqueue(new Vector2(temp.x, temp.y + 1));
                        if (now_num >= level_num) break;
                    }
                }
                if (map[(int)temp.x][(int)temp.y - 1] == -1)
                {
                    if (Random.Range(0, 5) < 1.8f)
                    {
                        getm = true;
                        now_num++;
                        map[(int)temp.x][(int)temp.y - 1] = Mathf.RoundToInt(Random.Range(0.5f, levels.Length - 0.6f));
                        map_queue.Enqueue(new Vector2(temp.x, temp.y - 1));
                        if (now_num >= level_num) break;
                    }
                }
                if (map[(int)temp.x + 1][(int)temp.y] == -1)
                {
                    if (Random.Range(0, 5) < 1.8f)
                    {
                        getm = true;
                        now_num++;
                        map[(int)temp.x+1][(int)temp.y] = Mathf.RoundToInt(Random.Range(0.5f, levels.Length - 0.6f));
                        map_queue.Enqueue(new Vector2(temp.x+1, temp.y));
                        if (now_num >= level_num) break;
                    }
                }
                if (map[(int)temp.x - 1][(int)temp.y] == -1)
                {
                    if (Random.Range(0, 5) < 1.8f)
                    {
                        getm = true;
                        now_num++;
                        map[(int)temp.x-1][(int)temp.y] = Mathf.RoundToInt(Random.Range(0.5f, levels.Length - 0.6f));
                        map_queue.Enqueue(new Vector2(temp.x - 1, temp.y));
                        if (now_num >= level_num) break;
                    }
                }
            }
        }
        created = true;
        Level =Instantiate(levels[0]) as GameObject;
    }
    // Update is called once per frame
    public void rebuild()
    {
        Destroy(Level);
        createLevel();
    }
    private List<GameObject> mapList;
    void Update()
    {
        if (Input.GetButtonDown("map"))
        {
            mapList=new List<GameObject>();
            GameObject te;
            for(int i=0; i<23; i++)
            {
                for(int ii = 0; ii < 23; ii++)
                {
                    if (map[i][ii] != -1)
                    {
                        te = Instantiate(block, new Vector2(1920, 1080), Quaternion.identity, Mcanvas.transform) as GameObject;
                        mapList.Add(te);
                        te.transform.localPosition = new Vector2((i - 11) * 55, (ii - 11) * 55);
                        if (i == player_now.x && ii == player_now.y) te.GetComponent<UnityEngine.UI.Image>().color = new Color(1, 1, 1, 1);
                        else if (passed[i][ii]) te.GetComponent<UnityEngine.UI.Image>().color = new Color(60/255, 1, 60/255, 0.7f);
                    }
                }
            }
        }
        if (Input.GetButtonUp("map"))
        {
            for(int i = 0; i < mapList.Count; i++)
            {
                Destroy(mapList[i]);
            }
            mapList.Clear();
        }
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().isdeath)
            {
                rebuild();
                return;
            }
            if (GameObject.FindGameObjectsWithTag("monster").Length == 0)
            {
                passed[(int)player_now.x][(int)player_now.y] = true;
                if (GameObject.FindGameObjectWithTag("Player").transform.position.y > GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().maxV.y)
                {
                    if (map[(int)player_now.x][(int)player_now.y + 1] != -1)
                    {
                        PlayerPrefs.SetInt("out_way", 0);
                        player_now += new Vector2(0, 1);
                        change_room(player_now);
                    }
                }
                if (GameObject.FindGameObjectWithTag("Player").transform.position.y < GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().minV.y)
                {
                    if (map[(int)player_now.x][(int)player_now.y - 1] != -1)
                    {
                        PlayerPrefs.SetInt("out_way", 2);
                        player_now -= new Vector2(0, 1);
                        change_room(player_now);
                    }
                }
                if (GameObject.FindGameObjectWithTag("Player").transform.position.x > GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().maxV.x)
                {
                    if (map[(int)player_now.x+1][(int)player_now.y] != -1)
                    {
                        PlayerPrefs.SetInt("out_way", 1);
                        player_now += new Vector2(1, 0);
                        change_room(player_now);
                    }
                }
                if (GameObject.FindGameObjectWithTag("Player").transform.position.x < GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().minV.x)
                {
                    if (map[(int)player_now.x-1][(int)player_now.y] != -1)
                    {
                        PlayerPrefs.SetInt("out_way", 3);
                        player_now -= new Vector2(1, 0);
                        change_room(player_now);
                    }
                }
            }
        }
    }
    void change_room(Vector2 now)
    {
        PlayerPrefs.SetFloat("player_health", GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().health);
        PlayerPrefs.Save();
        Destroy(Level);
        Level=Instantiate(levels[map[(int)now.x][(int)now.y]]) as GameObject;
    }
}
