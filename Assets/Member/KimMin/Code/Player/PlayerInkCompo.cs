using System;
using Code.Core;
using KimMin.Dependencies;
using UnityEngine;

namespace Code.Entities
{
    public class PlayerInkCompo : MonoBehaviour, IEntityComponent
    {
        [Inject] private InkStorage _inkStorage;

        public InkType CurrentInk { get; private set; }
        
        
        public void Initialize(Entity entity)
        {
            CurrentInk = InkType.Red;
        }

        private void Update()
        {
            
        }
    }
}