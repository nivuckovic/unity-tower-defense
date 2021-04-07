using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Particle Container", menuName = "Particle Container")]
public class ParticlesData : ScriptableObject
{
    [System.Serializable]
    public struct Particle
    {
        public string Name;
        public ParticleSystem Particles;
    }

    public List<Particle> ListOfParticles;
}
