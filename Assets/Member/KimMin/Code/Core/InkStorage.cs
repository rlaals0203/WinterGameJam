using System;
using System.Collections.Generic;
using KimMin.Dependencies;
using UnityEngine;

namespace Code.Core
{
    public class InkStorage : MonoSingleton<InkStorage>
    {
        private Dictionary<InkType, int> _inkDict = new();

        public void ModifyInk(InkType inkType, int amount)
        {
            if (!_inkDict.TryAdd(inkType, amount))
                _inkDict[inkType] += amount;
        }

        public int GetRemainInk(InkType inkType)
        {
            if(_inkDict.TryGetValue(inkType, out int amount))
                return amount;
            
            return 0;
        }
        
        public bool HasInk(InkType inkType) => _inkDict[inkType] > 0;

        public Dictionary<InkType, int> GetInkDict() => _inkDict;
    }
}