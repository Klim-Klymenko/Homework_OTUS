﻿using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(SwitchStateComponent))]
    public sealed class EnemyShootingController : MonoBehaviour, IGameStartListener,
        IGameFinishListener, IGameResumeListener, IGamePauseListener
    {
        private EnemyManager EnemyManager => _referenceComponent.EnemyManager;
        private EnemyAttackAgent AttackAgent => _referenceComponent.AttackAgent;
        
        [SerializeField] private EnemyReferenceComponent _referenceComponent;

        public bool IsOnlyUnityMethods { get; } = false;
        
        private void OnValidate() => _referenceComponent = GetComponent<EnemyReferenceComponent>();

        private void Enable() => AttackAgent.OnFired += RunBullet;

        private void Disable() => AttackAgent.OnFired -= RunBullet;

        private void RunBullet(Vector2 startPosition, Vector2 directionToPlayer) =>
            EnemyManager.RunBullet(startPosition, directionToPlayer);

        public void OnStart() => Enable();
        
        public void OnFinish() => Disable();
        
        public void OnResume() => Enable();
        
        public void OnPause() => Disable();
    }
}