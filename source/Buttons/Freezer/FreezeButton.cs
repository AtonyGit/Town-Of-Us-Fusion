using TownOfUsFusion.Modifiers.Freezer;

namespace TownOfUsFusion.Buttons.Freezer;

public class FreezeButton : CustomTouActionButton<PlayerControl>
{
    public override string Name => "Freeze";
    public override Color TextColor => Colors.Impostor;
    public override string ButtonKeybind => "ActionQuaternary";

    public override float Cooldown => OptionGroupSingleton<FreezerRoleSettings>.Instance.FreezeDuration;

    public override int MaxUses => (int)OptionGroupSingleton<FreezerRoleSettings>.Instance.FreezeUses;

    public override LoadableAsset<Sprite> Sprite => ToufAssets.ExampleButton;

    protected override void OnClick()
    {
        Target?.RpcAddModifier<FreezeModifier>();
    }

    public override PlayerControl? GetTarget()
    {
        return PlayerControl.LocalPlayer.GetClosestPlayer(true, Distance);
    }

    public override void SetOutline(bool active)
    {
        Target?.cosmetics.SetOutline(active, new Il2CppSystem.Nullable<Color>(Palette.Blue));
    }

    public override bool IsTargetValid(PlayerControl? target)
    {
        return true;
    }

    public override bool Enabled(RoleBehaviour? role)
    {
        return role is FreezerRole;
    }
}
