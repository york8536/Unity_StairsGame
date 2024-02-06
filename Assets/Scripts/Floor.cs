using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    
    public float moveSpeed = 2f;
    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        // 地板持續向上移動
        transform.Translate(0, moveSpeed*Time.deltaTime, 0);
        // 移除畫面外的地板
        if (transform.position.y > 6f)
        {
            Destroy(gameObject);
            
            transform.parent.GetComponent<FloorManager>().SpawnFloor();
        }
    }
}
