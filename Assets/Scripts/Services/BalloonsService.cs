using System;
using Interfaces;
using Models;
using Views;

namespace Services
{
    public interface IBalloonsCollector
    {
        Action<int> OnCurrentScoreChanged { get; set; }
        Action<int> OnBestScoreChanged { get; set; }
        (int CurrentScore, int BestScore) CurrentInfo { get; set; }
    }
    
    public class BalloonsService : IListenersSolver, IBalloonsCollector
    {
        private readonly IBalloonCollector[] balloonCollectors;
        private readonly BalloonsModel balloonsModel;
        public Action<int> OnCurrentScoreChanged { get; set; }
        public Action<int> OnBestScoreChanged { get; set; }
        public (int CurrentScore, int BestScore) CurrentInfo { get; set; }
        private IBalloonCollector hiddenBalloon;

        public BalloonsService(IBalloonCollector[] balloonCollectors, BalloonsModel balloonsModel)
        {
            this.balloonCollectors = balloonCollectors;
            this.balloonsModel = balloonsModel;
            CurrentInfo = (balloonsModel.CurrentBalloonsCount, balloonsModel.BestBalloonsCount);
            AddListeners();
        }

        public void AddListeners()
        {
            foreach (var balloonCollector in balloonCollectors)
            {
                balloonCollector.OnBalloonCollected += IncreaseBalloonsCount;
            }
        }
        
        private void IncreaseBalloonsCount(IBalloonCollector balloonCollector)
        {
            balloonsModel.CurrentBalloonsCount += 1;
            OnCurrentScoreChanged?.Invoke(balloonsModel.CurrentBalloonsCount);
            if (balloonsModel.CurrentBalloonsCount > balloonsModel.BestBalloonsCount)
            {
                balloonsModel.BestBalloonsCount = balloonsModel.CurrentBalloonsCount;
                OnBestScoreChanged?.Invoke(balloonsModel.BestBalloonsCount);
            }
            hiddenBalloon?.ReleaseBalloon();
            hiddenBalloon = balloonCollector;
        }

        public void RemoveListeners()
        {
            foreach (var balloonCollector in balloonCollectors)
            {
                balloonCollector.OnBalloonCollected -= IncreaseBalloonsCount;
            }
        }
    }
}