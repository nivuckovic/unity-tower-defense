using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level Data", menuName = "Level Data")]
public class LevelData : ScriptableObject
{
    [Header("Enemy Data")]
    public EnemyData Soldier;
    public EnemyData Tank;

    [System.Serializable]
    public struct WaveEnemy
    {
        public string Name;
        public EnemyType Type;
        public int Amount;
        public float DeltaTime;
    }
    [System.Serializable]
    public struct WaveData
    {
        public string Name;
        public List<WaveEnemy> ListOfEnemies;
        public float NextWaveTime;
    }

    [SerializeField] public List<WaveData> LevelWavesData;

}
