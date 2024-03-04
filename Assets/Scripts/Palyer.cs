using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    GameObject currentFloor; //建立一個GameObject變數
    [SerializeField] int Hp; //建立血量
    [SerializeField] GameObject HpBar; //建立血條框
    [SerializeField] TMP_Text scoreText ; //建立樓層文字UI

    int score;
    float scoreTime;
    Animator anim;
    SpriteRenderer render;
    AudioSource deathSound;
    [SerializeField] GameObject replayButton;

    // Start is called before the first frame update
    void Start()
    {
        Hp = 10;
        score = 0;
        scoreTime = 0f;
        render = GetComponent<SpriteRenderer>();
        // 在Unity中的Animation點擊對應節點後可建立動畫
        // 在Unity中的Animator可建立動畫觸發條件與調整流程,建立後可在腳本中控制;
        anim = GetComponent<Animator>();
        deathSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // 左右移動製作
        if(Input.GetKey(KeyCode.RightArrow)){
            transform.Translate(moveSpeed*Time.deltaTime, 0, 0);
            render.flipX = false;
            anim.SetBool("run", true);
        }
        else if(Input.GetKey(KeyCode.LeftArrow)){
            transform.Translate(-moveSpeed*Time.deltaTime, 0, 0);
            render.flipX = true;
            anim.SetBool("run", true);
            
        }
        else{
            anim.SetBool("run", false); // 若不動時關閉run動畫
        }
        UpdateScore();
       
    }

    
    void OnCollisionEnter2D(Collision2D other) { // 角色碰撞事件
        // 如果踩到一般地板
        if (other.gameObject.tag == "Normal"){
            // 如果碰撞到底板的上邊
            if(other.contacts[0].normal == new Vector2(0f, 1f)){ 
                Debug.Log("Normal");
                currentFloor = other.gameObject;
                // 回1滴血
                ModifyHp(1);
                other.gameObject.GetComponent<AudioSource>().Play();
            }
        }
        // 如果踩到有刺地板
        else if (other.gameObject.tag == "Nails"){
            if(other.contacts[0].normal == new Vector2(0f, 1f)){
                Debug.Log("Nails");
                currentFloor = other.gameObject;
                ModifyHp(-3); // 扣三滴血
                anim.SetTrigger("hurt"); // 觸發hurt動畫
                other.gameObject.GetComponent<AudioSource>().Play();
            }
        }

        // 若碰到天花板則地板取消碰撞狀態
        else if (other.gameObject.tag == "Ceiling"){
            Debug.Log("Ceiling");
            currentFloor.GetComponent<BoxCollider2D>().enabled = false;
            ModifyHp(-3); // 扣三滴血
            anim.SetTrigger("hurt"); // 觸發hurt動畫
            other.gameObject.GetComponent<AudioSource>().Play();
        }
        
    }
    private void OnTriggerEnter2D(Collider2D other) { // 角色掉落事件
        if (other.gameObject.tag == "die"){
            Debug.Log("die");
            Die();
            
        }
    }

    void ModifyHp(int num){ // 更新當前血量
        Hp += num;
        if(Hp > 10){
            Hp = 10;
        }
        else if(Hp < 0){
            Hp = 0;
            Die();
        }
        UpdateHpBar();
    }

    void UpdateHpBar(){ // 更新當前HpBar
        for(int i=0; i<HpBar.transform.childCount; i++){
            if(Hp>i){
                HpBar.transform.GetChild(i).gameObject.SetActive(true);
            }
            else{
                HpBar.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
       
    }

    void UpdateScore(){  // 更新闖關分數
        scoreTime += Time.deltaTime;
        if(scoreTime>2f){
            score++;
            scoreTime = 0f;
            scoreText.text = "地下" + score.ToString() + "層";
        }
    }
    void Die(){ // 死亡觸發事件
        deathSound.Play(); // 撥放死亡音效
        Time.timeScale = 0f; // 遊戲暫停
        replayButton.SetActive(true);
    }

    public void Replay(){ // Replay按鍵功能
        Time.timeScale = 1f;
        SceneManager.LoadScene("SampleScene");
    }

}
