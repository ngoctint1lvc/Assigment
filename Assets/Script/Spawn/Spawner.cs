using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame {
    public class Spawner : MonoBehaviour {
        public Wave[] waves;
        public Enemy enemy;

        Wave currentWave;
        int currentWaveNumber = 0;

        int enemiesRemainingToSpawn;
        int enemiesRemainingActive;
        float nextSpawnTime;

        [System.Serializable]
        public struct Wave {
            public int enemyCount;
            public float timeBetweenSpawns;
        }

        void Start() {
            NextWave();
        }
        void Update() {
            if (enemiesRemainingToSpawn > 0 && Time.time > nextSpawnTime) {
                enemiesRemainingToSpawn--;
                nextSpawnTime = Time.time + currentWave.timeBetweenSpawns;

                Enemy spawnedEnemy = Instantiate(enemy, Vector3.zero, Quaternion.identity) as Enemy;
                spawnedEnemy.OnDeath += OnEnemyDeath;
            }
        }

        void OnEnemyDeath() {
            enemiesRemainingActive--;
            if (enemiesRemainingActive == 0) {
                NextWave();
            }
        }
        void NextWave() {
            currentWaveNumber++;
            print("Wave :" + currentWaveNumber);

            if (currentWaveNumber - 1 < waves.Length) {
                currentWave = waves[currentWaveNumber - 1];

                enemiesRemainingToSpawn = currentWave.enemyCount;
                enemiesRemainingActive = enemiesRemainingToSpawn;
            }
        }
    }
}