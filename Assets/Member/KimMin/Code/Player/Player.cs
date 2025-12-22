using KimMin.Dependencies;
using UnityEngine;

namespace Code.Entities
{
    public class Player : Entity, IDependencyProvider
    {
        [Provide]
        public Player ProvidePlayer() => this;
    }
}