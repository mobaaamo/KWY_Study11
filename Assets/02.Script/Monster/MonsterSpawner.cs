using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [Header("monster setting")]
    [SerializeField] private List<MonsterData> monsterDataList = new List<MonsterData>(); //리스트를 만들어 다수의 프리펩 넣기

    [SerializeField] WayPointPass path;

    [Header("spawn setting")]
    [SerializeField] float spawnInterval = 2.0f;
    
    void Start()
    {
        StartCoroutine(SpawnMonster());
    }
    IEnumerator SpawnMonster()
    {
        yield return new WaitForSeconds(1.0f); //첫생성때 1초 대기
        while (true)
        {
            Spawn();
            yield return new WaitForSeconds(spawnInterval); // 다음 생성까지 대기
        } 
    }
    private void Spawn()
    {
        //랜덤 소환
        int index = Random.Range(0,monsterDataList.Count);
        MonsterData randMonster = monsterDataList[index];
        //경로의 첫번째 위치에 모슨터 프리팹을 생성하고
        GameObject monsterObj = Instantiate(randMonster.prefab, path.points[0].position, Quaternion.identity);

        //존재하면
        if(monsterObj.TryGetComponent<MonsterBase>(out MonsterBase monster))
        {
            monster.Initialize(randMonster, path.GetPath());
        }

    }
}
