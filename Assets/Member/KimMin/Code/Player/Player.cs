using Code.Combat;
using Code.Core;
using DG.Tweening;
using KimMin.Core;
using KimMin.Dependencies;
using KimMin.Events;
using UnityEngine;

namespace Code.Entities
{
    public class Player : Entity, IDependencyProvider
    {
        [SerializeField] private StateDataSO[] states;
        
        [Inject] private GridManager _gridManager;
        
        private EntityStateMachine _stateMachine;
        private EntityHealth _healthCompo;

        [field: SerializeField] public PlayerInputSO PlayerInput { get; private set; }
        public int RemainTripleAttack { get; set; } = 0;
        public int RemainDoubleRadius { get; set; } = 0;
        public bool IsCombatMode { get; set; } = false;
        public Vector3 Position => transform.position;

        [Provide]
        public Player ProvidePlayer() => this;

        protected override void Awake()
        {
            base.Awake();
            _stateMachine = new EntityStateMachine(this, states);
            _healthCompo = GetCompo<EntityHealth>();
            
            OnDeadEvent.AddListener(HandleDeadEvent);
            _healthCompo.OnHealthChangeEvent += HandleHealthChange;
        }

        private void HandleHealthChange(float current, float max)
        {
            GameEventBus.RaiseEvent(PlayerEvents.PlayerHealthEvent.Initialize(current, max));
        }

        private void HandleDeadEvent()
        {
            if (IsDead) return;
            IsDead = true;
            //나중에 이벤트 발행
            ChangeState("DEAD", true); 
        }

        private void Start()
        {
            Vector2 position = new Vector2(_gridManager.Row / 2, _gridManager.Col / 2);
            transform.position = position;
            
            _stateMachine.ChangeState("IDLE");
        }

        private void Update()
        {
            _stateMachine.UpdateStateMachine();
        }
        
        public void ChangeState(string newStateName, bool forced = false) 
            => _stateMachine.ChangeState(newStateName, forced);

        public void TakeDamage(int damage)
        {
            
        }
    }
}
