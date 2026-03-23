using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public abstract class MonsterBase : MonoBehaviour
{
    [Header("monster stat")]
    protected int currentHp;
    protected float moveSpeed;

    [Header("path setting")]
    protected Transform[] path;
    protected int targetIndex;
    private float arriveRange = 0.1f;

    public virtual void Initialize(MonsterData data, Transform[] pathPoints)
    {
        currentHp = data.maxHp;
        moveSpeed = data.moveSpeed;
        path = pathPoints;
        targetIndex = 0;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        Movepath();
    }
    //경로에 저장된 웨이포인트들을 순서대로 따라 이동
    protected virtual void Movepath()
    {
        if (path == null || targetIndex >= path.Length) return;
        Transform target = path[targetIndex];

        Vector2 currentPos = transform.position; //커렌트 포지션은 트랜스폼의위치인데 이게 뭔말이노
        Vector2 targetPos = target.position; // vector2는 xy좌표아닌가? 아 타겟의위치구나 타겟pos가 화긴

        transform.position = Vector2.MoveTowards(currentPos, targetPos, moveSpeed*Time.deltaTime); //vector2.이런건 어떻게 쓰는건지 난 왜 이걸 arriveRange로 했지?

        if(Vector2.Distance(currentPos, targetPos) < arriveRange) //현재 위치가 목표위치에 충분히 가까워졌으면 다음 웨이포인트로 넘겨라
        {
            targetIndex++;
        }
    }
    public virtual void TakeDamage(int damage)
    {
        currentHp -= damage;
        if (currentHp <= 0)
        {
            Die();
        }
    }
    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}
