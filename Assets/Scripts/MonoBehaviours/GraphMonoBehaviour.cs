using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public sealed class GraphMonoBehaviour : MonoBehaviour
{
    private readonly List<object> _objects = new List<object>();
    private readonly List<object> _awakables = new List<object>();
    private readonly List<object> _startable = new List<object>();
    private readonly List<object> _fixedUpdatables = new List<object>();
    private readonly List<object> _updatables = new List<object>();

    private void Awake()
    {
        GeneralPreferences generalPreferences = new GeneralPreferencesImpl();

        GameState gameState = new GameStateImpl();

        Transform canvasPanel = gameObject.transform.Find("CanvasController").transform;
        CanvasUIView canvasUIView = gameObject.GetComponent<CanvasUIView>();
        GameObject Cup = Instantiate(canvasUIView.Cup, canvasPanel);
        GameObject Balloon = Instantiate(canvasUIView.Balloon, canvasPanel);
        GameObject ResetPanel = Instantiate(canvasUIView.ResetPanel, canvasPanel);
        GameObject Joystic = Instantiate(canvasUIView.Joystic, canvasPanel);
        CanvasController canvasController = new CanvasControllerImpl(
            gameState,
            Cup.GetComponentInChildren<Text>(),
            Balloon.GetComponentInChildren<Text>(),
            ResetPanel.transform.Find("ButtonRestart").GetComponent<Button>(),
            ResetPanel.transform.Find("ButtonHome").GetComponent<Button>(),
            generalPreferences,
            ResetPanel.transform.Find("PanelSpeaker").gameObject,
            ResetPanel.transform.Find("PanelSpeaker").transform.Find("ImageSpeakerBackground").transform.Find("ImageSpeakerFace").transform.GetComponent<Image>(),
            ResetPanel.transform.Find("PanelSpeaker").transform.Find("TextSpeech").GetComponent<Text>(),
            ResetPanel.transform.Find("PanelSpeaker").transform.Find("TextSpeakerName").GetComponent<Text>()
        );

        GameObject playerPrefab = Instantiate(gameObject.GetComponent<IPlayerView>().Player, gameObject.transform);
        IPlayer player = new PlayerImpl(
            Joystic.GetComponent<FixedJoystick>(),
            playerPrefab.GetComponent<CharacterController>(),
            playerPrefab.transform,
            playerPrefab.GetComponent<Animator>(),
            gameState,
            canvasController
        );

        EnemiesView enemiesView = gameObject.GetComponent<EnemiesView>();
        GameObject raelle = Instantiate(enemiesView.Raelle.enemiePrefab, gameObject.transform);
        GameObject abigail = Instantiate(enemiesView.Abigail.enemiePrefab, gameObject.transform);
        GameObject tally = Instantiate(enemiesView.Tally.enemiePrefab, gameObject.transform);
        raelle.GetComponent<EnemieMonoBehaviour>().SetEnemie(
            raelle.GetComponent<NavMeshAgent>(),
            raelle.GetComponent<Animator>(),
            enemiesView.PositionsForSearching,
            gameState,
            canvasController,
            enemiesView.Raelle
        );
        abigail.GetComponent<EnemieMonoBehaviour>().SetEnemie(
            abigail.GetComponent<NavMeshAgent>(),
            abigail.GetComponent<Animator>(),
            enemiesView.PositionsForSearching,
            gameState,
            canvasController,
            enemiesView.Abigail
        );
        tally.GetComponent<EnemieMonoBehaviour>().SetEnemie(
            tally.GetComponent<NavMeshAgent>(),
            tally.GetComponent<Animator>(),
            enemiesView.PositionsForSearching,
            gameState,
            canvasController,
            enemiesView.Tally
        );

        BalloonsPresenter balloons = new BalloonsPresenterImpl(generalPreferences, canvasController);
        BalloonPrefabAndBalloonsPositionsView balloonPrefabAndBalloonsPositionsView = gameObject.GetComponent<BalloonPrefabAndBalloonsPositionsView>();
        foreach (Vector3 position in balloonPrefabAndBalloonsPositionsView.BaloonsPositions)
        {
            Vector3 positionForTheBalloon = new Vector3(position.x - 0.94f, position.y, position.z);
            GameObject balloonPrefab = Instantiate(balloonPrefabAndBalloonsPositionsView.Balloon, gameObject.transform);
            balloonPrefab.transform.localPosition = positionForTheBalloon;
            balloonPrefab.GetComponent<BalloonMonoBehaviour>().SetBalloon(
                balloons,
                balloonPrefab.GetComponentInChildren<ParticleSystem>(),
                balloonPrefab.transform.Find("Balloon").gameObject,
                balloonPrefab.GetComponent<CapsuleCollider>(),
                balloonPrefab.GetComponent<AudioSource>()
            );
        }

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