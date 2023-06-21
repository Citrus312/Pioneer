using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Boss : MonoBehaviour
{
    public GameObject Enemy;

    public ParticleSystem Particle;

    private Material _material;
    // Start is called before the first frame update
    void Start()
    {
        _material = Enemy.GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Die();
        }
    }

    private void Die()
    {
        _material.DOFloat(-1, "_Strength", 2f);
        Particle.Play();
    }
}
