using TownOfUsFusion.Modifiers.Doused;

namespace TownOfUsFusion.Buttons.Arsonist;

public class IgniteButton : CustomActionButton<PlayerControl>
{
    public override string Name => "Ignite";

    public override float Cooldown => OptionGroupSingleton<ArsonistRoleSettings>.Instance.DouseCooldown.Value;

    public override int MaxUses => 0;


    public override LoadableAsset<Sprite> Sprite => ToufAssets.IgniteButton;

    protected override void OnClick()
    {
        if (Target.HasModifier<DousedModifier>())
        {
            var arsonist = PlayerControl.LocalPlayer.Data.Role as ArsonistRole;
            arsonist.Ignite();
        }
    }

    public override PlayerControl? GetTarget()
    {
        return PlayerControl.LocalPlayer.GetClosestPlayer(true, Distance);
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
