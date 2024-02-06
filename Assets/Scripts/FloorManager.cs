using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    [SerializeField] GameObject[] floorPrefabs;
    public void SpawnFloor()
    {
        // 隨機選擇一種地板
        var r = Random.Range(0, floorPrefabs.Length);

        // 將地板創建出來
        var floor = Instantiate(floorPrefabs[r], transform);

        // 將地板設定在指定位置內
        floor.transform.position = new Vector3(Random.Range(-4.6f, 2.6f), -6f, 0f);


    }
}
