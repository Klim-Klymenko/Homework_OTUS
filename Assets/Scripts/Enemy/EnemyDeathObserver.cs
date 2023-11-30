﻿using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(Enemy))]
    [RequireComponent(typeof(HitPointsComponent))]
    public sealed class EnemyDeathObserver : MonoBehaviour, IGameStartListener,
        IGameFinishListener, IGameResumeListener, IGamePauseListener
    {
        public EnemySpawner EnemySpawner;
        
        [SerializeField] private HitPointsComponent _hitPointsComponent;

        [SerializeField] private Enemy _referenceComponent;

        public bool IsOnlyUnityMethods { get; } = false;
        
        private void OnValidate()
        {
            _referenceComponent = GetComponent<Enemy>();
            _hitPointsComponent = GetComponent<HitPointsComponent>();
        }

        private void Enable() => _hitPointsComponent.OnDeath += UnspawnEnemy;

        private void Disable() => _hitPointsComponent.OnDeath -= UnspawnEnemy;

        private void UnspawnEnemy() =>
            EnemySpawner.UnspawnEnemy(_referenceComponent);

        public void OnStart() => Enable();

        void IGameFinishListener.OnFinish() => Disable();
        
        void IGameResumeListener.OnResume() => Enable();

        void IGamePauseListener.OnPause() => Disable();
    }
}