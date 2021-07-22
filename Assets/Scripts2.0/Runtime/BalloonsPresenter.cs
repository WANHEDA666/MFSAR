using System.Collections.Generic;

public interface BalloonsPresenter
{
    void HideBalloon(Balloon balloon);
}

public class BalloonsPresenterImpl : BalloonsPresenter
{
    private List<Balloon> hiddenBalloons = new List<Balloon>();

    public void HideBalloon(Balloon balloon)
    {
        hiddenBalloons.Add(balloon);
        if (hiddenBalloons.Count == 3)
        {
            ShowBalloon(hiddenBalloons[0]);
            hiddenBalloons.Remove(hiddenBalloons[0]);
        }
    }

    public void ShowBalloon(Balloon balloon)
    {
        balloon.Show();
    }
}