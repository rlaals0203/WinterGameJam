using System;
using System.Collections.Generic;
using KimMin.Dependencies;
using UnityEngine;

namespace Code.Core
{
    [Provide]
    public class InkStorage : MonoBehaviour, IDependencyProvider
    {
        private Dictionary<InkType, int> _inkDict = new();

        private void Awake()
        {
            //테스트
            _inkDict.Add(InkType.Red, 100);
            _inkDict.Add(InkType.Blue, 200);
            _inkDict.Add(InkType.Yellow, 300);
        }

        public void ModifyInk(InkType inkType, int amount)
        {
            if (!_inkDict.TryAdd(inkType, amount))
                _inkDict[inkType] += amount;
        }

        public int GetRemainInk(InkType inkType) => _inkDict[inkType];
        public bool HasInk(InkType inkType) => _inkDict[inkType] > 0;

        public Dictionary<InkType, int> GetInkDict() => _inkDict;
    }
}