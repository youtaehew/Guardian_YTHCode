using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElfSound : MonoBehaviour
{
    Enemy _enemy;
    [SerializeField]
    private EventReference _normalSlashSound;
    [SerializeField]
    private EventReference _fireSlashSound;
    [SerializeField]
    private EventReference _footStepSound;
    [SerializeField]
    private EventReference _prickSound;
    [SerializeField]
    private EventReference _firePrickSound;
    [SerializeField]
    private EventReference _rollSound;

    private void Awake()
    {
        _enemy = transform.root.GetComponent<Enemy>();
    }

    private void SlashSound()
    {
        if (!_enemy) return;
        if (_enemy.IsSecondStep)
        {
            SoundManager.PlaySFX(_fireSlashSound, transform.position);
        }
        else
        {
            SoundManager.PlaySFX(_normalSlashSound, transform.position);
        }
    }
    private void FootStep()
    {
        if (!_enemy) return;
        SoundManager.PlaySFX(_footStepSound, transform.position);
    }
    private void PrickSound()
    {
        if (!_enemy) return;
        if (_enemy.IsSecondStep)
        {
            SoundManager.PlaySFX(_firePrickSound, transform.position);
        }
        else
        {
            SoundManager.PlaySFX(_prickSound, transform.position);
        }
    }

    private void Roll()
    {
        if (!_enemy) return;
        SoundManager.PlaySFX(_rollSound, transform.position);
    }
}
