using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelWavesSO_x", menuName = "Scriptable Objects/Level Waves")]
public class LevelWavesSO : ScriptableObject
{
    [Header("Enemy Waves")]
    public List<WaveEnemies> waves;
}

[Serializable]
public struct WaveEnemies
{
    [Header("Enemies in this wave")]
    public List<EnemyWithAmmount> enemies;
}

[Serializable]
public class EnemyWithAmmount
{
    [Tooltip("Enemy prefab to spawn")]
    public GameObject enemy;
    [Min(1)]
    [Tooltip("How many enemies")]
    public int amount = 1;
}
