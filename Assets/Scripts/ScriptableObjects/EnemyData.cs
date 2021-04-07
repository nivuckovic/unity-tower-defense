using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Data", menuName = "Enemy Data")]
public class EnemyData : ScriptableObject
{
    public EnemyType EnemyType;
    public Sprite Image;
    public int Health;
    public float Speed;
    public int Damage;
    public int Reward;
}
