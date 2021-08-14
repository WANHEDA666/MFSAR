using System.Collections.Generic;

public interface BalloonsPresenter
{
    void HideBalloon(Balloon balloon);
}

public class BalloonsPresenterImpl : BalloonsPresenter
{
    private readonly GeneralPreferences generalPreferences;
    private readonly CanvasController canvasController;
    private List<Balloon> hiddenBalloons = new List<Balloon>();

    public BalloonsPresenterImpl(GeneralPreferences generalPreferences, CanvasController canvasController)
    {
        this.generalPreferences = generalPreferences;
        this.canvasController = canvasController;
        SubscribeActions();
    }

    private void SubscribeActions()
    {
        canvasController.Restart += ReleaseAllBalloons;
    }

    public void HideBalloon(Balloon balloon)
    {
        hiddenBalloons.Add(balloon);

        if (hiddenBalloons.Count == 3)
            ShowBalloon(hiddenBalloons[0]);

        generalPreferences.BalloonsCountIncreaseFunc();
    }

    public void ShowBalloon(Balloon balloon)
    {
        hiddenBalloons.Remove(balloon);
        balloon.Show();
    }

    private void ReleaseAllBalloons()
    {
        for (int i = 0; i < hiddenBalloons.Count; i++)
            ShowBalloon(hiddenBalloons[i]);

        generalPreferences.ResetBalloonsCount();
    }
}