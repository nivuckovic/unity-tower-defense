using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Building Data", menuName = "Building Data")]
public class BuildingsData : ScriptableObject
{
    [System.Serializable]
    public struct BuildingLevel
    {
        public Sprite Icon;
        public int Price;
        public float FireRate;
        public float Radius;
        public ProjectileData ProjectileData;
    }

    public int MaxLevel => Data.Count;
    public BuildingType Type;
    public List<BuildingLevel> Data;
}
