using System;
using System.Threading.Tasks;
using Main_Controller;
using Managers.Game_Manager;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Obstacles.Obstacle_Spawner
{
    public class ObstacleAndPointSpawner : MonoBehaviour
    {
        [Header("Prefabs")] 
        [SerializeField] private GameObject obstaclePrefab;
        [SerializeField] private GameObject pointPrefab;
        [Header("Spawn points")] 
        [SerializeField] private Transform[] spawnPoints;
        [Header("Parent transform")]
        [SerializeField] private Transform placedObstacles;
        [Header("Parameters")] 
        [SerializeField] private float spawnDuration;
        [SerializeField] private int pointOnObstacleFrequency;
        [SerializeField] private float pointBacklog;
        
        private const float ObstaclesDeletingDuration = 1.5f;
    
        private float _spawnTimer;
        
        private int _obstaclesWithoutPointCount;

        private bool _isEnabled;

        private void Update()
        {
            if (_isEnabled)
            {
                IncrementSpawnTimer();
        
                TryToSpawnObstacle();
            }
        }

        private void OnEnable()
        {
            GameManager.OnStart += EnableSpawner;
        
            PlayerBallCollisionController.OnPlayerCircleDestroyed += DisableSpawnerAndClearObstacles;
        }

        private void OnDisable()
        {
            GameManager.OnStart -= EnableSpawner;
        
            PlayerBallCollisionController.OnPlayerCircleDestroyed -= DisableSpawnerAndClearObstacles;
        }

        private void IncrementSpawnTimer()
        {
            _spawnTimer += Time.deltaTime;
        }

        private void TryToSpawnObstacle()
        {
            if (_spawnTimer < spawnDuration) return;

            bool isWithPoint = _obstaclesWithoutPointCount >= pointOnObstacleFrequency;
            
            SpawnNewObstacle(GetRandomSpawnPosition(), isWithPoint);

            _obstaclesWithoutPointCount++;
            _spawnTimer = 0f;
        }

        #region Obstacles

        private void SpawnNewObstacle(Vector2 spawnPosition, bool isWithPoint)
        {
            var newObstacle = Instantiate(obstaclePrefab, placedObstacles.transform, true);
            
            newObstacle.transform.position = spawnPosition;
            
            if (isWithPoint)
            {
                var newPoint = Instantiate(pointPrefab, newObstacle.transform, true);

                var obstaclePosition = newObstacle.transform.position;
                
                newPoint.transform.position = new Vector3(obstaclePosition.x - pointBacklog,
                    obstaclePosition.y, obstaclePosition.z);

                _obstaclesWithoutPointCount = 0;
            }
        }

        private Vector2 GetRandomSpawnPosition()
        {
            try
            {
                var transformsCount = spawnPoints.Length;
        
                int randomPoint = Random.Range(0, transformsCount);

                return spawnPoints[randomPoint].position;
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine($"Null spawn points array: {e}");
                throw;
            }
        }
    
        private async Task ClearAllObstacles()
        {
            var obstaclesCount = placedObstacles.transform.childCount;
            float latency = ObstaclesDeletingDuration / obstaclesCount;
        
            for (int i = obstaclesCount - 1; i >= 0; i--)
            {
                DestroyObstacle(i);
                await Task.Delay(TimeSpan.FromSeconds(latency));
            }
        
        }
    
        private async void TryToClearAllObstacles()
        {
            if (placedObstacles.transform.childCount > 0)
            {
                await ClearAllObstacles();
            }
        }

        private void DestroyObstacle(int i)
        {
            Transform obstacle = placedObstacles.transform.GetChild(i);
            Destroy(obstacle.gameObject);
        }

        private void EnableSpawner()
        {
            _isEnabled = true;
        }

        #endregion

        private void DisableSpawnerAndClearObstacles()
        {
            _isEnabled = false;
            //TryToClearAllObstacles();
        }
    }
}
