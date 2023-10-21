using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PauseAudioSource : MonoBehaviour, IDependancy<Pauser>
{
    private new AudioSource audio;
    private Pauser pauser;
    public void Construct(Pauser obj) => pauser = obj;

    private void Start()
    {
        audio = GetComponent<AudioSource>();

        pauser.PauseStateChange += OnPauseStateChange;
    }

    private void OnDestroy()
    {
        pauser.PauseStateChange -= OnPauseStateChange;
    }

    private void OnPauseStateChange(bool arg0)
    {
        if (pauser == true)
            audio.Stop();

        if (pauser == false)
            audio.Play();
    }
}
