using KimMin.Dependencies;
using UnityEngine;

namespace Code.Entities
{
    public class Player : Entity, IDependencyProvider
    {
        [field: SerializeField] public PlayerInputSO PlayerInput { get; private set; }
        public Vector3 Position => transform.position;
        [Provide]
        public Player ProvidePlayer() => this;
    }
}