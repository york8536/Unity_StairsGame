using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    GameObject currentFloor; //建立一個GameObject變數
    [SerializeField] int Hp;
    [SerializeField] GameObject HpBar;
    [SerializeField] TMP_Text scoreText ; 

    int score;
    float scoreTime;

    // Start is called before the first frame update
    void Start()
    {
        Hp = 10;
        score = 0;
        scoreTime = 0f;

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
        UpdateScore();
       
    }

    
    void OnCollisionEnter2D(Collision2D other) {
        // 如果踩到一般地板
        if (other.gameObject.tag == "Normal"){
            // 如果碰撞到底板的上邊
            if(other.contacts[0].normal == new Vector2(0f, 1f)){ 
                Debug.Log("Normal");
                currentFloor = other.gameObject;
                // 回1滴血
                ModifyHp(1);
            }
        }
        // 如果踩到有刺地板
        else if (other.gameObject.tag == "Nails"){
            if(other.contacts[0].normal == new Vector2(0f, 1f)){
                Debug.Log("Nails");
                currentFloor = other.gameObject;
                // 扣三滴血
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
        UpdateHpBar();
    }

    void UpdateHpBar()
    {
        for(int i=0; i<HpBar.transform.childCount; i++){
            if(Hp>i){
                HpBar.transform.GetChild(i).gameObject.SetActive(true);
            }
            else{
                HpBar.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
       
    }

    void UpdateScore(){
        scoreTime += Time.deltaTime;
        if(scoreTime>2f){
            score++;
            scoreTime = 0f;
            scoreText.text = "地下" + score.ToString() + "層";
        }
    }

}
