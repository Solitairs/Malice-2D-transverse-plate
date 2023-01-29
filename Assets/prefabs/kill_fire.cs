using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kill_fire : MonoBehaviour
{
    public float keeptime;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,keeptime);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
