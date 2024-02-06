using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;

    GameObject currentFloor; //建立一個GameObject變數

    [SerializeField] int Hp;

    // Start is called before the first frame update
    void Start()
    {
        Hp = 10;


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
        // 將當前踩到的地板帶入變數中
        if (other.gameObject.tag == "Normal"){
            if(other.contacts[0].normal == new Vector2(0f, 1f)){ // 如果碰撞到底板的上邊
                Debug.Log("Normal");
                currentFloor = other.gameObject;
                ModifyHp(1);
            }
        }

        else if (other.gameObject.tag == "Nails"){
            if(other.contacts[0].normal == new Vector2(0f, 1f)){
                Debug.Log("Nails");
                currentFloor = other.gameObject;
                ModifyHp(-3);
            }
        }

        // 若碰到天花板則地板取消碰撞狀態
        else if (other.gameObject.tag == "Ceiling"){
            Debug.Log("Ceiling");
            currentFloor.GetComponent<BoxCollider2D>().enabled = false;
            ModifyHp(-3);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "die"){
            Debug.Log("die");
        }
    }

    void ModifyHp(int num){
        Hp += num;
        if(Hp > 10){
            Hp = 10;
        }
        else if(Hp < 0){
            Hp = 0;
        }
    }
}
