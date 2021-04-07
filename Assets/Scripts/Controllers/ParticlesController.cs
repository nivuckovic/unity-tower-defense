using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ParticlesController : BaseController
{
    private ParticlesData _particles;
    private Transform _parent;

    public enum ParticleType 
    {
        Cash
    }

    public ParticlesController(GameSystem gameSystem, ParticlesData data, Transform parent) : base(gameSystem)
    {
        _particles = data;
        _parent = parent;
    }

    public void PlayParticle(ParticleType type, Vector3 position)
    {
        ParticleSystem particle = _particles.ListOfParticles.Find(p => p.Name == type.ToString("g")).Particles;

        ParticleSystem particleGO = Instantiate(particle, _parent);
        particleGO.transform.position = position;

        Destroy(particleGO.gameObject, particleGO.main.duration);
    }
}
