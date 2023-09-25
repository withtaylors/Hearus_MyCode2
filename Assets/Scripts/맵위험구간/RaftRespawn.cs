using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaftRespawn : MonoBehaviour
{
    [SerializeField] private Transform Raft;
    [SerializeField] private Transform respawnPoint2;
    [SerializeField] private Renderer raftRenderer; 
    private Material originalMaterial; 
    private bool isFading = false; 
    private float fadeSpeed = 0.5f; 

    private void Start()
    {
        raftRenderer = Raft.GetComponent<Renderer>();
        originalMaterial = raftRenderer.material;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isFading)
        {
            StartCoroutine(FadeRaft());
        }
    }

    private IEnumerator FadeRaft()
    {
        isFading = true;

        // 페이드 아웃 (투명하게 만들기)
        float t = 0f;
        Color startColor = originalMaterial.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0f);

        while (t < 1f)
        {
            t += Time.deltaTime * fadeSpeed;
            Color lerpedColor = Color.Lerp(startColor, endColor, t);
            raftRenderer.material.color = lerpedColor;
            yield return null;
        }

        // 물체 위치를 재설정
        Raft.transform.position = respawnPoint2.transform.position;

        // 페이드 인 (다시 불투명하게 만들기)
        t = 0f;
        startColor = endColor;
        endColor = new Color(startColor.r, startColor.g, startColor.b, 1f);

        while (t < 1f)
        {
            t += Time.deltaTime * fadeSpeed;
            Color lerpedColor = Color.Lerp(startColor, endColor, t);
            raftRenderer.material.color = lerpedColor;
            yield return null;
        }

        isFading = false;
    }
}
