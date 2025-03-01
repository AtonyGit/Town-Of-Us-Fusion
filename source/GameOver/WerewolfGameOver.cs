namespace TownOfUsFusion.GameOver;

public class WerewolfGameOver : CustomGameOver
{
    public override bool VerifyCondition(PlayerControl playerControl)
    {
        return playerControl.Data.Role is WerewolfRole;
    }

    public override void AfterEndGameSetup(EndGameManager endGameManager)
    {
        endGameManager.WinText.text = "Werewolf Wins!";
        endGameManager.WinText.color = Colors.Werewolf;
        endGameManager.BackgroundBar.material.SetColor(ShaderID.Color, Colors.Werewolf);
    }
}
