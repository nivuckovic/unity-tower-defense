using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Projectile Data", menuName = "Projectile Data")]
public class ProjectileData : ScriptableObject
{
    public Sprite Icon;
    public float Damage;
    public float Speed;
}
