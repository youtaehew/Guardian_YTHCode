using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemSound : MonoBehaviour
{
    Enemy _enemy;
    [SerializeField] private EventReference grabSound;
    [SerializeField] private EventReference groundCrackSound;
    [SerializeField] private EventReference oneSwingSound;
    [SerializeField] private EventReference lightSwingSound;
    [SerializeField] private EventReference jumpStart;
    [SerializeField] private EventReference jumpEnd;
    [SerializeField] private EventReference footStep;
    [SerializeField] private EventReference footAttack;
    [SerializeField] private EventReference dashWind;
    [SerializeField] private EventReference digSound;

    [SerializeField] private ParticleSystem[] _footSteps;

    private void Awake()
    {
        _enemy = transform.parent.GetComponent<Enemy>();
    }

    private void GrabSound()
    {
        if (!_enemy) return;
        SoundManager.PlaySFX(grabSound, transform.position);
    }

    private void GroundCrackSound()
    {
        if (!_enemy) return;
        SoundManager.PlaySFX(groundCrackSound, transform.position);
    }

    private void OneSwingSound()
    {
        if (!_enemy) return;
        SoundManager.PlaySFX(oneSwingSound, transform.position);
    }

    private void LightSwingSound()
    {
        if (!_enemy) return;
        SoundManager.PlaySFX(lightSwingSound, transform.position);
    }

    private void JumpStartSound()
    {
        if (!_enemy) return;
        SoundManager.PlaySFX(jumpStart, transform.position);
    }

    private void JumpEndSound()
    {
        if (!_enemy) return;
        SoundManager.PlaySFX(jumpEnd, transform.position);
    }

    private void FootStep(int idx)
    {
        if (!_enemy) return;
        _footSteps[idx].Play();
        SoundManager.PlaySFX(footStep, transform.position);
    }

    private void FootAttack()
    {
        if (!_enemy) return;
        SoundManager.PlaySFX(footAttack, transform.position);
    }

    private void DashWind()
    {
        if (!_enemy) return;
        SoundManager.PlaySFX(dashWind, transform.position);
    }

    private void Dig()
    {
        if (!_enemy) return;
        SoundManager.PlaySFX(digSound, transform.position);
    }
}