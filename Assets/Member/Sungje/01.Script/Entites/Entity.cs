using System;
using System.Collections.Generic;
using System.Linq;
using Code.Entities;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Events;

public abstract class Entity : MonoBehaviour
{
    public bool IsDead { get; set; }
    public UnityEvent OnHitEvent;
    public UnityEvent OnDeadEvent;

    public int RemainShieldCount { get; set; } = 0;

    protected Dictionary<Type, IEntityComponent> _components;

    protected virtual void Awake()
    {
        _components = new Dictionary<Type, IEntityComponent>();
        AddedComponents();
        InitializeComponent();
        AfterInitialize();
    }
    protected virtual void AddedComponents()
    {
        GetComponentsInChildren<IEntityComponent>().ToList()
                .ForEach(component => _components.Add(component.GetType(), component));
    }

    protected virtual void InitializeComponent()
    {
        _components.Values.ToList().ForEach(component => component.Initialize(this));
    }
    
    protected virtual void AfterInitialize()
    {
        _components.Values.OfType<IAfterInitialize>()
            .ToList().ForEach(compo => compo.AfterInitialize());
    }

    public T GetCompo<T>() where T : IEntityComponent
            => (T)_components.GetValueOrDefault(typeof(T));
    public IEntityComponent GetCompo(Type type)
        => _components.GetValueOrDefault(type);

    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}
