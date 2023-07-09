using System;
using UnityEngine;

public class SoundEvents : MonoBehaviour
{
    public static event Action<AudioClip> OnPlaySfx;
    public static void Fire_OnPlaySfx(AudioClip clip) { OnPlaySfx?.Invoke(clip); }
}
