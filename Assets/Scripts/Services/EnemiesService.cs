using System;
using System.Collections.Generic;
using Interfaces;
using UniRx;
using UnityEngine;
using Views;

namespace Services
{
    public interface IGameEndObserver
    {
        Action OnGameEnded { get; set; }
    }
    
    public class EnemiesService : IListenersSolver, IGameEndObserver
    {
        private readonly IEnemy[] enemies;
        private readonly List<Vector3> searchSpots = new List<Vector3>();
        private List<IEnemy> enemiesThatNeedNewPosition = new List<IEnemy>();
        private IDisposable observable;
        public Action OnGameEnded { get; set; }

        public EnemiesService(IEnemy[] enemies, Transform[] searchPositions)
        {
            this.enemies = enemies;
            foreach (var searchPosition in searchPositions)
                searchSpots.Add(searchPosition.position);
            UpdatePositions();
            AddListeners();
        }

        public void AddListeners()
        {
            foreach (var enemyView in enemies)
            {
                enemyView.OnPlayerSpotted += FindTheClosestSpot;
                enemyView.OnPlayerLost += SetRandomTargets;
                enemyView.OnTargetReached += PutEnemyInQuery;
                enemyView.OnPlayerCaught += StopEnemies;
            }
        }

        private void StopEnemies()
        {
            foreach (var enemyView in enemies)
            {
                enemyView.StopSearching();
            }
            OnGameEnded?.Invoke();
        } 

        private void UpdatePositions()
        {
            observable = Observable.EveryUpdate().Subscribe(x => 
            {
                if (enemiesThatNeedNewPosition.Count > 0)
                {
                    var randomTarget = searchSpots[UnityEngine.Random.Range(0, searchSpots.Count)];
                    enemiesThatNeedNewPosition[0].SetNewTargetPosition(randomTarget);
                    enemiesThatNeedNewPosition.RemoveAt(0);
                }
            });
        }

        private void FindTheClosestSpot(Vector3 playerPosition)
        {
            var bestConditionFloat = 100f;
            var bestConditionVector = new Vector3();
            foreach (var position in searchSpots)
            {
                if (Vector3.Distance(position, playerPosition) < bestConditionFloat)
                {
                    bestConditionFloat = Math.Abs(Vector3.Distance(position, playerPosition));
                    bestConditionVector = position;
                }
            }
            foreach (var enemyView in enemies)
            {
                enemyView.SetNewTargetPosition(bestConditionVector);
            }
        }

        private void SetRandomTargets()
        {
            foreach (var enemyView in enemies)
            {
                PutEnemyInQuery(enemyView);
            }
        }

        private void PutEnemyInQuery(IEnemy enemyView)
        {
            enemiesThatNeedNewPosition.Add(enemyView);
        }

        public void RemoveListeners()
        {
            observable.Dispose();
            foreach (var enemyView in enemies)
            {
                enemyView.OnPlayerSpotted -= FindTheClosestSpot;
                enemyView.OnPlayerLost -= SetRandomTargets;
                enemyView.OnTargetReached -= PutEnemyInQuery;
                enemyView.OnPlayerCaught -= StopEnemies;
            }
        }
    }
}