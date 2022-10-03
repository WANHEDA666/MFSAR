using Factories;
using Interfaces;
using Views;

namespace Presenters
{
    public class MainMenuPresenter : IListenersSolver
    {
        private readonly MainMenuView mainMenuView;
        private readonly GameFactory gameFactory;

        public MainMenuPresenter(MainMenuView mainMenuView, GameFactory gameFactory)
        {
            this.mainMenuView = mainMenuView;
            this.gameFactory = gameFactory;
            AddListeners();
            ManageSound();
        }

        private void ManageSound()
        {
            mainMenuView.ManageSound(Preferences.SoundState == 0);
        }

        public void AddListeners()
        {
            mainMenuView.OnSoundButtonClicked += ChangeSoundState;
            mainMenuView.OnAboutButtonClicked += CreateAboutScreen;
            mainMenuView.OnDefaultGameButtonClicked += CreateDefaultGame;
        }

        private void CreateDefaultGame()
        {
            gameFactory.CreateDefaultGame();
        }

        private void ChangeSoundState()
        {
            Preferences.SoundState = Preferences.SoundState == 0 ? 1 : 0;
            ManageSound();
        }

        private void CreateAboutScreen()
        {
            gameFactory.CreateAboutScreen();
        }

        public void RemoveListeners()
        {
            
        }
    }
}