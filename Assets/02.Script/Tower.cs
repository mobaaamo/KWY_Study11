
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("bullet setting")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;

    [Header("attck setting")]
    [SerializeField] private float range = 4.0f;//없어도 됨 
    [SerializeField] private float fireRate = 1.0f;

    private float fireTimer;
    private List<MonsterBase> monstersInRange = new List<MonsterBase>();

    private void Update()
    {
        fireTimer += Time.deltaTime;

        MonsterBase target = GetNearMonster();
        if (target == null) return;

        Vector2 dir = target.transform.position - transform.position;
        transform.right = dir;

        if (fireTimer >= 1.0f / fireRate)
        {
            Fire(target);
            fireTimer = 0.0f;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<MonsterBase>(out var monster))
        {
            //리스트안에 없으면 추가
            if (!monstersInRange.Contains(monster))
            {
                monstersInRange.Add(monster);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.TryGetComponent<MonsterBase>(out var monster))
        {
            if (monstersInRange.Contains(monster))
            {
                monstersInRange.Remove(monster);
            }
        }
    }
    //사거리 안에 저장된 몬스터중에서 가장 가까운 몬스터 찾아야함.
    private MonsterBase GetNearMonster()
    {
        if (monstersInRange.Count == 0) return null;
        MonsterBase nearMonster = null;

        float nearDist = Mathf.Infinity;//현재까지의 가장 짧은 거리

        Vector2 towerPos = transform.position;//타워위치

        foreach (var monster in monstersInRange)
        {
            if(monster == null) continue;
            float dist = Vector2.Distance(towerPos, monster.transform.position);


            //즉 현재 보고 있는 몬스터가 지금까지 본것보다 가까우면
            //해당 몬스터를 가장 가까운몬스터로 바꾸자
            //if현재거리<지금까지 가장 짧은 거리
            //지금까지가장짧은거리 = 현재거리
            //가장가까운 몬스터 = 이번몬스터

            if(dist < nearDist)
            {
                nearDist = dist;
                nearMonster = monster;
            }
        }
        return nearMonster;
    }
    private void Fire(MonsterBase target)
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        bullet.GetComponent<Bullet>().Initialize(target);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
