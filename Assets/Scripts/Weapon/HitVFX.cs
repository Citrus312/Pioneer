using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitVFX : MonoBehaviour
{
    public string _prefabPath;
    [SerializeField] float lifetime = 1;

    WaitForSeconds waitLifetime;

    private void Awake()
    {
        waitLifetime = new WaitForSeconds(lifetime);
    }

    private void OnEnable()
    {
        StartCoroutine(HitCoroutine());
    }
    IEnumerator HitCoroutine()
    {
        yield return waitLifetime;

        // Destroy(gameObject);
        ObjectPool.getInstance().remove(_prefabPath, gameObject);
    }
}
