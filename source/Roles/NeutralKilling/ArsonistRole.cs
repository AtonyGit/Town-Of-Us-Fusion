using MiraAPI.Networking;
using TownOfUsFusion.Buttons.Arsonist;
using TownOfUsFusion.Modifiers.Doused;

namespace TownOfUsFusion.Roles;

public class ArsonistRole : ImpostorRole, ICustomRole
{
    public string RoleName => "Arsonist";
    public string RoleDescription => "Douse Players And Ignite The Light";
    public string RoleLongDescription => "Douse players and ignite to kill all douses";
    public Color RoleColor => Colors.Arsonist;
    public ModdedRoleTeams Team => ModdedRoleTeams.Custom;
    public void ResetCooldowns()
    {
        var arsonist = PlayerControl.LocalPlayer.Data.Role as ArsonistRole;
        if (HudManager.Instance.AbilityButton is DouseButton)
            HudManager.Instance.AbilityButton.ResetCoolDown();
        if (HudManager.Instance.AbilityButton is IgniteButton)
            HudManager.Instance.AbilityButton.ResetCoolDown();
        //DouseButton<arsonist>.ResetCooldownAndOrEffect();
    }
    public void Ignite()
    {
        foreach (var player in PlayerControl.AllPlayerControls)
        {
            if (player != null && !player.Data.IsDead && player.HasModifier<DousedModifier>())
            {
                player?.RpcRemoveModifier<DousedModifier>();
                PlayerControl.LocalPlayer.RpcCustomMurder(player, teleportMurderer: false);
            }
        }
    }

    public CustomRoleConfiguration Configuration => new CustomRoleConfiguration(this)
    {
        UseVanillaKillButton = false,
        CanGetKilled = true,
        CanUseVent = false,
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
        return gameOverReason == CustomGameOver.GameOverReason<ArsonistGameOver>();
    }
}
