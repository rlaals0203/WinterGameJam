using UnityEngine;

[CreateAssetMenu(fileName = "Enemies data", menuName = "SO/EnemyData")]
public class EnemyDataSO : ScriptableObject
{
    public string enemyName;
    [TextArea]
    public string description;

    public int maxHealth;
    public int damage;

    public float moveSpeed;
    public float attackCooldown;
    public float attackRange;
    public int occupySpaceX;
    public int occupySpaceY;
}
