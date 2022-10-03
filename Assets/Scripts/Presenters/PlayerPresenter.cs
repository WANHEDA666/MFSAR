using System;
using Interfaces;
using UniRx;
using Views;

namespace Presenters
{
    public class PlayerPresenter : IListenersSolver
    {
        private readonly PlayerView playerView;
        private readonly IGameControllerView gameControllerView;
        private IDisposable observable;

        public PlayerPresenter(PlayerView playerView, IGameControllerView gameControllerView)
        {
            this.playerView = playerView;
            this.gameControllerView = gameControllerView;
            AddListeners();
        }

        public void AddListeners()
        {
            observable = Observable.EveryFixedUpdate().Subscribe(x => 
            {
                playerView.SetDirection(gameControllerView.MoveVector, gameControllerView.TempVector);
            });
        }

        public void RemoveListeners()
        {
            observable.Dispose();
        }
    }
}