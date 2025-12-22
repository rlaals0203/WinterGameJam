public class HittingDeadState : EnemyState
{
    public HittingDeadState(Enemy enemy) : base(enemy) { }

    public override void Enter()
    {
        _enemy.gameObject.SetActive(false);
    }
}
