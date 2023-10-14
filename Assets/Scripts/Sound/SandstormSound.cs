using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SandstormSound : MonoBehaviour {
    public AudioSource audioSource;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
        audioSource.spatialBlend = 1f; // Make the sound 3D
        audioSource.rolloffMode = AudioRolloffMode.Logarithmic; // Use logarithmic rolloff
        audioSource.minDistance = 10f; // The distance within which the sound is loudest
        audioSource.maxDistance = 50f; // The distance beyond which the sound can't be heard

        // Play the sound
        audioSource.Play();
    }
}
