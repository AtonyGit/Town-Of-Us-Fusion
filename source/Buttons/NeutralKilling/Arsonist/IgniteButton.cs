using TownOfUsFusion.Modifiers.Doused;

namespace TownOfUsFusion.Buttons.Arsonist;

public class IgniteButton : CustomTouActionButton<PlayerControl>
{
    public override string Name => "Ignite";

    public override Color TextColor => Colors.Arsonist;
    public override string ButtonKeybind => "ActionSecondary";
    public override float Cooldown => OptionGroupSingleton<ArsonistRoleSettings>.Instance.DouseCooldown.Value;



    public override LoadableAsset<Sprite> Sprite => ToufAssets.IgniteButton;

    protected override void OnClick()
    {
        if (Target.HasModifier<DousedModifier>())
        {
            var arsonist = PlayerControl.LocalPlayer.Data.Role as ArsonistRole;
            arsonist.Ignite();
            arsonist.ResetCooldowns();
        }
    }
    protected override void FixedUpdate(PlayerControl playerControl)
    {
        base.FixedUpdate(playerControl);
        
        Button.buttonLabelText.SetOutlineColor(TextColor);
        bool AbilityKey = Rewired.ReInput.players.GetPlayer(0).GetButtonDown(ButtonKeybind);
        if (Button != null && AbilityKey && !PlayerControl.LocalPlayer.Data.IsDead)
            Button?.DoClick();
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
