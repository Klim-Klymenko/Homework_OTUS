﻿using System;
using Atomic.Elements;
using Atomic.Objects;
using Common;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace GameEngine
{
    [Serializable]
    [Is(ObjectTypes.Striker)]
    public sealed class ShootComponent : IDisposable
    {
        [SerializeField] 
        private Transform _firePoint;

        [SerializeField] 
        private AtomicVariable<int> _charges;

        [SerializeField] 
        [Get(ShooterAPI.ShootingInterval)]
        private AtomicValue<float> _shootingInterval;
        
        [SerializeField]
        [HideInInspector]
        private AndExpression _shootCondition;
        
        [SerializeField]
        [HideInInspector]
        private AtomicAction _bulletSpawnAction;

        [SerializeField] 
        [HideInInspector]
        [Get(ShooterAPI.ShootAction)]
        private ShootAction _shootAction;
        
        private readonly AtomicEvent _shootEvent = new();

        public IAtomicVariable<int> Charges => _charges;
        public IAtomicValue<bool> ShootCondition => _shootCondition;
        public IAtomicObservable ShootObservable => _shootEvent;
        
        public void Compose(DiContainer diContainer, IAtomicValue<bool> isAlive)
        {
            ISpawner<Transform> bulletSpawner = diContainer.Resolve<ISpawner<Transform>>();
            _bulletSpawnAction.Compose(() => bulletSpawner.Spawn());

            _shootCondition.Append(isAlive);
            _shootCondition.Append(new AtomicFunction<bool>(() => _charges.Value > 0));

            _shootAction.Compose(_charges, _bulletSpawnAction, _shootEvent, _shootCondition);
        }
        
        public void Dispose()
        {
            _charges?.Dispose();
            _shootEvent?.Dispose();
        }
    }
}