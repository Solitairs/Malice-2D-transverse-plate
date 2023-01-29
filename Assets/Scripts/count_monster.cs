using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class count_monster : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
        GetComponent<TextMeshProUGUI>().text="monsters:"+GameObject.FindGameObjectsWithTag("monster").Length.ToString();
    }
}
