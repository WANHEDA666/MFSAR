using System.Collections.Generic;
using Scripts2._0.Holders;
using UnityEngine;
using UnityEngine.AI;

namespace Scripts2._0.MonoBehaviours
{
    public sealed class GraphMonoBehaviour : MonoBehaviour
    {
        private readonly List<object> _objects = new List<object>();
        private readonly List<object> _awakables = new List<object>();
        private readonly List<object> _startable = new List<object>();
        private readonly List<object> _fixedUpdatables = new List<object>();
        private readonly List<object> _updatables = new List<object>();

        private void Awake()
        {
            BalloonsPresenter balloons = new BalloonsPresenterImpl();
            BalloonPrefabAndBalloonsPositionsView balloonPrefabAndBalloonsPositionsView = gameObject.GetComponent<BalloonPrefabAndBalloonsPositionsView>();
            foreach(Vector3 position in balloonPrefabAndBalloonsPositionsView.BaloonsPositions)
            {
                Vector3 positionForTheBalloon = new Vector3(position.x - 0.94f, position.y, position.z);
                GameObject balloonPrefab = Instantiate(balloonPrefabAndBalloonsPositionsView.Balloon, positionForTheBalloon, Quaternion.identity);
                balloonPrefab.GetComponent<BalloonMonoBehaviour>().SetBalloon(
                    balloons,
                    balloonPrefab.GetComponentInChildren<ParticleSystem>(),
                    balloonPrefab.transform.Find("Balloon").gameObject,
                    balloonPrefab.GetComponent<CapsuleCollider>(),
                    balloonPrefab.GetComponent<AudioSource>()
                );
            }

            GameState gameState = new GameStateImpl();

            GameObject playerPrefab = Instantiate(gameObject.GetComponent<IPlayerView>().Player, gameObject.transform);
            Joystick joystick = gameObject.transform.Find("CanvasController").gameObject.transform.Find("Fixed Joystick").gameObject.GetComponent<FixedJoystick>();
            IPlayer player = new PlayerImpl(joystick, playerPrefab.GetComponent<CharacterController>(), playerPrefab.transform, playerPrefab.GetComponent<Animator>(), gameState);

            EnemiesView enemiesView = gameObject.GetComponent<EnemiesView>();
            GameObject raelle = Instantiate(enemiesView.Raelle.enemiePrefab, gameObject.transform);
            GameObject abigail = Instantiate(enemiesView.Abigail.enemiePrefab, gameObject.transform);
            GameObject tally = Instantiate(enemiesView.Tally.enemiePrefab, gameObject.transform);
            raelle.GetComponent<EnemieMonoBehaviour>().SetEnemie(raelle.GetComponent<NavMeshAgent>(), raelle.GetComponent<Animator>(), enemiesView.PositionsForSearching, gameState);
            abigail.GetComponent<EnemieMonoBehaviour>().SetEnemie(abigail.GetComponent<NavMeshAgent>(), abigail.GetComponent<Animator>(), enemiesView.PositionsForSearching, gameState);
            tally.GetComponent<EnemieMonoBehaviour>().SetEnemie(tally.GetComponent<NavMeshAgent>(), tally.GetComponent<Animator>(), enemiesView.PositionsForSearching, gameState);

            _objects.Add(player);

            _awakables.AddRange(_objects.FindAll(objectExemplar => objectExemplar is IAwakable));
            _startable.AddRange(_objects.FindAll(objectExemplar => objectExemplar is IStartable));
            _updatables.AddRange(_objects.FindAll(objectExemplar => objectExemplar is IUpdatable));
            _fixedUpdatables.AddRange(_objects.FindAll(objectExemplar => objectExemplar is IFixedUpdatable));
        }

        private void FixedUpdate()
        {
            foreach(IFixedUpdatable obj in _fixedUpdatables)         
                obj.FixedUpdate();
        }
    }
}