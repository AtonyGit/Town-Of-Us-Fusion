namespace TownOfUsFusion.GameOver;

public class NeutralKillerGameOver : CustomGameOver
{
    public override bool VerifyCondition(PlayerControl playerControl)
    {
        return playerControl.Data.Role is NeutralKillerRole;
    }

    public override void AfterEndGameSetup(EndGameManager endGameManager)
    {
        endGameManager.WinText.text = "Outcast Killer Wins!";
        endGameManager.WinText.color = Color.magenta;
        endGameManager.BackgroundBar.material.SetColor(ShaderID.Color, Color.magenta);
    }
}
