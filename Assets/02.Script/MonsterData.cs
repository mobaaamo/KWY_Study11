using UnityEngine;
/**************************
 [Scriptable Object] 스크립터블 오브젝트
- 클래스 인스턴스와는 별도로 대량의 데이터를 저장하는데 사용할 수 있느 데이터 컨테이너
- 값이 사본이 생성되는 것을 방지하여 메모리 사용을 줄여줌
- 이는 연결된 MonoBehaviour 스크립트에 변경되지 않는 데이터를 저장하는 프리펩이 있는 프로젝트의 경우 유용
- 게임 오브젝트에 붙지 않고 프로젝트 폴더 .asset파일로 저장되어 런타임 중에도 여러 오브젝트가 같은 데이터를 공유
-   
  
  
 ******************************/
[CreateAssetMenu(fileName = "MonsterData",menuName = "SO/MonsterData")]
public class MonsterData : ScriptableObject
{
    public string monsterName;
    public int maxHp;
    public int moveSpeed;
    public GameObject prefab;
}
