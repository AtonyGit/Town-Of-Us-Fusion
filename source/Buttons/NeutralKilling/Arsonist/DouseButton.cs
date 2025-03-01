using TownOfUsFusion.Modifiers.Doused;

namespace TownOfUsFusion.Buttons.Arsonist;

public class DouseButton : CustomActionButton<PlayerControl>
{
    public override string Name => "Douse";

    public override float Cooldown => OptionGroupSingleton<ArsonistRoleSettings>.Instance.DouseCooldown.Value;

    public override int MaxUses => (int)OptionGroupSingleton<ArsonistRoleSettings>.Instance.DouseUses;
    public int DouseUses = (int)OptionGroupSingleton<ArsonistRoleSettings>.Instance.DouseUses;

    public override LoadableAsset<Sprite> Sprite => ToufAssets.DouseButton;

    protected override void OnClick()
    {
        Target?.RpcAddModifier<DousedModifier>();
    }

    public override PlayerControl? GetTarget()
    {
        return PlayerControl.LocalPlayer.GetClosestPlayer(true, Distance);
    }
    protected override void FixedUpdate(PlayerControl playerControl)
    {
        base.FixedUpdate(playerControl);
        UsesLeft = DouseUses - PlayerControl.AllPlayerControls.ToArray().Count(x => Utils.PlayerById(x.PlayerId) != null
        && Utils.PlayerById(x.PlayerId).Data != null && !Utils.PlayerById(x.PlayerId).Data.IsDead && !Utils.PlayerById(x.PlayerId).Data.Disconnected
        && Utils.PlayerById(x.PlayerId).HasModifier<DousedModifier>());
    }

    public override void SetOutline(bool active)
    {
        Target?.cosmetics.SetOutline(active, new Il2CppSystem.Nullable<Color>(Colors.Arsonist));
    }

    public override bool IsTargetValid(PlayerControl? target)
    {
        return true;
    }

    public override bool Enabled(RoleBehaviour? role)
    {
        return role is ArsonistRole;
    }
}
