﻿using System;
using Atomic.Elements;
using UnityEngine;

namespace GameEngine
{
    [Serializable]
    public sealed class HealthFXComponent
    {
        [SerializeField] 
        private AudioClip _takeDamageClip;

        [SerializeField]
        private AudioClip _deathClip;

        [SerializeField]
        private ParticleSystem _damageParticle;
        
        private TakeDamageSoundController _takeDamageSoundController;
        private DeathSoundController _deathSoundController;

        private TakeDamageParticleController _takeDamageParticleController;
        
        public void Compose(AudioSource audioSource, IAtomicObservable<int> takeDamageObservable, 
            IAtomicObservable deathObservable, IAtomicValue<bool> takeDamageClipCondition)
        {
            _takeDamageSoundController = new TakeDamageSoundController(takeDamageObservable, takeDamageClipCondition, audioSource, _takeDamageClip);
            _deathSoundController = new DeathSoundController(deathObservable, audioSource, _deathClip);
            
            _takeDamageParticleController = new TakeDamageParticleController(takeDamageObservable, _damageParticle);
        }

        public void OnEnable()
        {
            _takeDamageSoundController.OnEnable();
            _deathSoundController.OnEnable();
            
            _takeDamageParticleController.OnEnable();
        }

        public void OnDisable()
        {
            _takeDamageSoundController.OnDisable();
            _deathSoundController.OnDisable();
            
            _takeDamageParticleController.OnDisable();
        }
    }
}