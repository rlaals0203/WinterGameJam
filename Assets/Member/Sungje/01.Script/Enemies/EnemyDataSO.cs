using UnityEngine;

[CreateAssetMenu(fileName = "Enemies data", menuName = "SO/EnemyData")]
public class EnemyDataSO : ScriptableObject
{
    public string enemyName;
    [TextArea]
    public string description;

    public int maxHealth;
    public int damage;

    public float attackCooldown;
    public float attackRange;
    public float attackAngle;
    public int occupySpaceX;
    public int occupySpaceY;
}
