using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private int damage = 1;

    private Rigidbody2D rb;
    private MonsterBase targetMonster;

    public void Initialize(MonsterBase target)
    {
        targetMonster = target;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        if(targetMonster == null)
        {
            Destroy(gameObject);
            return;
        }
        //현재 총알위치->몬스터 위치로 가는 방향 벡터
        Vector2 dir = ((Vector2)targetMonster.transform.position - rb.position).normalized;

        rb.MovePosition(rb.position + dir * speed * Time.fixedDeltaTime);

        //총알이랑 몬스터 사이가의 거리가 아주 가까워졌으면
        if (Vector2.Distance(rb.position, targetMonster.transform.position) < 0.1f)
        {
            targetMonster.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
