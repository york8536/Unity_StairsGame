using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public float moveSpeed = 5f;
    // Start is called before the first frame update
    void Start()
    {



        
    }

    // Update is called once per frame
    void Update()
    {
        // 左右移動製作
        if(Input.GetKey(KeyCode.RightArrow)){
            transform.Translate(moveSpeed*Time.deltaTime, 0, 0);
        }
        else if(Input.GetKey(KeyCode.LeftArrow)){
            transform.Translate(-moveSpeed*Time.deltaTime, 0, 0);
        }
       
    }
    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Normal"){
            Debug.Log("Normal");
        }
        else if (other.gameObject.tag == "Nails"){
            Debug.Log("Nails");
        }
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "die"){
            Debug.Log("die");
        }
    }
}
