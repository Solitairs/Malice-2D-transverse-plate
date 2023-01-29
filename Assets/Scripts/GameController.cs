using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Experimental.Rendering.Universal;
public class GameController : MonoBehaviour
{
    public Light2D Player_light;
    public PlatformEffector2D platformEffect;
    private GameObject Camera;
    private Camera mainCamera;
    public Vector2 minV,maxV;
    private GameObject player;
    private float tempx, tempy;
    private PlayerController player_controller;
    // Start is called before the first frame update
    void Start()
    {
        Camera = GameObject.FindGameObjectWithTag("camera_move");
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    private Vector2 temp;
    private float to_light;
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player_controller = player.GetComponent<PlayerController>();
        if (Input.GetButton("down")) platformEffect.rotationalOffset = 180;
        else platformEffect.rotationalOffset = 0;
        if (player != null)
        {
            mainCamera.orthographicSize = 4+Mathf.Lerp(mainCamera.orthographicSize-4, player_controller.health*0.06f,0.1f);
            temp= Vector2.Lerp(Camera.transform.position, new Vector3(player.transform.position.x + 0.01f, player.transform.position.y + 0.216f, Camera.transform.position.z), 0.05f);
            tempx = Mathf.Clamp(temp.x, minV.x + mainCamera.orthographicSize * 192 / 108, maxV.x - mainCamera.orthographicSize * 192 / 108);
            if(minV.x + mainCamera.orthographicSize * 192 / 108 >= maxV.x - mainCamera.orthographicSize * 192 / 108)tempx=0;
            tempy = Mathf.Clamp(temp.y, minV.y + mainCamera.orthographicSize, maxV.y - mainCamera.orthographicSize);
            if (minV.y + mainCamera.orthographicSize >= maxV.y - mainCamera.orthographicSize) tempy = 0;
            Camera.transform.position = new Vector2(tempx,tempy);
            to_light = player_controller.health / 2;
            if (to_light == 0) to_light = 10;
            Player_light.pointLightOuterRadius = Mathf.MoveTowards(Player_light.pointLightOuterRadius, to_light, 8f * Time.deltaTime);
        }
    }
}
