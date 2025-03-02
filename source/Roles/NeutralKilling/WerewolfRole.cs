namespace TownOfUsFusion.Roles;

public class WerewolfRole : ImpostorRole, ICustomRole
{
    public string RoleName => "Werewolf";
    public string RoleDescription => "Rampage To Kill Everyone";
    public string RoleLongDescription => "Rampage to kill everyone in your path!";
    public Color RoleColor => Colors.Werewolf;
    public ModdedRoleTeams Team => ModdedRoleTeams.Custom;

    public CustomRoleConfiguration Configuration => new CustomRoleConfiguration(this)
    {
        UseVanillaKillButton = true,
        CanGetKilled = true,
        CanUseVent = true,
    };
    public RoleOptionsGroup RoleOptionsGroup { get; } = new("Outcast Killers", Color.gray);

    public TeamIntroConfiguration? IntroConfiguration { get; } = new(
        Color.gray,
        "OUTCAST",
        "You are an Outcast. You do not have a team.");

    public override void SpawnTaskHeader(PlayerControl playerControl)
    {
        // remove existing task header.
    }

    public override bool DidWin(GameOverReason gameOverReason)
    {
        return gameOverReason == CustomGameOver.GameOverReason<WerewolfGameOver>();
    }
}
