using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu]
public class Effect : ScriptableObject
{
    public bool PlaySoundEffect;
    [ShowIf("PlaySoundEffect")] [Header("Sound")] [SerializeField] SoundEffectData soundEffect;
    [Space]


    public bool PlayVisualEffect;
    [ShowIf("PlayVisualEffect")] [Header("VisualEffect")] [SerializeField] VisualEffectData visualEffect;
    [Space]

    public bool PlayChangeShaderEffect;
    [ShowIf("PlayChangeShaderEffect")] [Header("ChangeShader")] [SerializeField] ChangeShaderEffectData changeShaderEffect; 
    [Space]


    [ShowIf("NotClearPulsingEffect")] public bool PlayPulsingEffect;
    [ShowIf(EConditionOperator.And, "PlayPulsingEffect", "NotClearPulsingEffect")] [Header("PulsingEffect")] [SerializeField] PulsingEffectData pulsingEffect;
    [ShowIf("NotPlayPulsingEffect")] [Space] public bool ClearPulsingEffect;

    public bool NotPlayPulsingEffect() { return !PlayPulsingEffect; }
    public bool NotClearPulsingEffect() { return !ClearPulsingEffect; }


    public void Play(GameObject origin)
    {
        if (PlaySoundEffect)
            soundEffect.PlayEffect(origin);

        if (PlayVisualEffect)
            visualEffect.PlayEffect(origin);

        if (PlayChangeShaderEffect)
            changeShaderEffect.PlayEffect(origin);

        if (ClearPulsingEffect)
            ClearAllPulsingEffectsFrom(origin);

        if (PlayPulsingEffect)
            pulsingEffect.PlayEffect(origin);

    }

    private void ClearAllPulsingEffectsFrom(GameObject origin)
    {
        foreach (PulsingEffector pulsing in origin.GetComponents<PulsingEffector>())
        {
            pulsing.Stop();
        }
    }
}

[System.Serializable]
public class SoundEffectData : EffectData
{
    public AudioClip clip;
    public bool playFromOrigin;
    public float volume = 1f;
    public float randomPitchRange = 0f;

    public override void PlayEffect(GameObject origin)
    {
        Game.SoundPlayer.Play(clip, playFromOrigin ? origin : null, volume, randomPitchRange);
    }
}


[System.Serializable]
public class VisualEffectData : EffectData
{
    public Transform prefab;

    public override void PlayEffect(GameObject origin)
    {
        GameObject.Instantiate(prefab, origin.transform.position, Quaternion.identity);
    }
}

[System.Serializable]
public class ChangeShaderEffectData : EffectData
{
    public Shader shader;
    public override void PlayEffect(GameObject origin)
    {
        MeshRenderer meshRenderer = origin.GetComponent<MeshRenderer>();
        if (meshRenderer != null)
        {
            Color color = meshRenderer.material.color;
            meshRenderer.material = new Material(shader);
            meshRenderer.material.color = color;
        }
    }
}


[System.Serializable]
public class PulsingEffectData : EffectData
{
    public bool LimitedPulsing;
    public float duration;

    public override void PlayEffect(GameObject origin)
    {
        PulsingEffector effector = origin.GetComponent<PulsingEffector>();
        if (effector == null)
        {
            effector = origin.AddComponent<PulsingEffector>();
        }

        if (LimitedPulsing)
            effector.StartPulsing(duration);
        else
            effector.StartPulsing();
    }
}

public class EffectData
{
    public virtual void PlayEffect(GameObject origin)
    {
        //
    }
}
