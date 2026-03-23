using UnityEngine;

public class WayPointPass : MonoBehaviour
{
    //경로의 각점을 나타내는 트랜스폼 배열
    public Transform[] points;

    //경로를 반환하는 메서드
    public Transform[] GetPath()
    {
        return points; //설정된 경로 반환
    }

    private void OnDrawGizmos()
    {
        if (points == null) return; //없으면 하지말고

        Gizmos.color = Color.yellow;//노란색
        for (int i = 0; i < points.Length - 1; i++)
        {
            Gizmos.DrawLine(points[i].position, points[i+1].position);
        }
    }

}
