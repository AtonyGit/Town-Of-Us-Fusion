namespace TownOfUsFusion.GameOver;

public class ArsonistGameOver : CustomGameOver
{
    public override bool VerifyCondition(PlayerControl playerControl)
    {
        return playerControl.Data.Role is ArsonistRole;
    }

    public override void AfterEndGameSetup(EndGameManager endGameManager)
    {
        endGameManager.WinText.text = "Arsonist Wins!";
        endGameManager.WinText.color = Colors.Arsonist;
        endGameManager.BackgroundBar.material.SetColor(ShaderID.Color, Colors.Arsonist);
    }
}
