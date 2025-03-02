using TownOfUsFusion.Modifiers.Camped;

namespace TownOfUsFusion.Buttons.Deputy;

public class CampButton : CustomTouActionButton<PlayerControl>
{
    public override string Name => "Camp";
    public override Color TextColor => Colors.Deputy;
    public override string ButtonKeybind => "ActionQuaternary";

    public override float Cooldown => 0.0001f;

    public override LoadableAsset<Sprite> Sprite => ToufAssets.CampButton;
    protected override void FixedUpdate(PlayerControl playerControl)
    {
        base.FixedUpdate(playerControl);
        Button.buttonLabelText.SetOutlineColor(TextColor);
        bool AbilityKey = Rewired.ReInput.players.GetPlayer(0).GetButtonDown(ButtonKeybind);
        if (Button != null && AbilityKey && !PlayerControl.LocalPlayer.Data.IsDead)
            Button?.DoClick();
    }
    protected override void OnClick()
    {
        var dep = PlayerControl.LocalPlayer.Data.Role as DeputyRole;
        if (!dep.HasCamped)
        {
            dep.HasCamped = true;
            Target?.RpcAddModifier<CampedModifier>();
        }
        
    }

    public override PlayerControl? GetTarget()
    {
        return PlayerControl.LocalPlayer.GetClosestPlayer(true, Distance);
    }

    public override void SetOutline(bool active)
    {
        Target?.cosmetics.SetOutline(active, new Il2CppSystem.Nullable<Color>(Colors.Deputy));
    }

    public override bool IsTargetValid(PlayerControl? target)
    {
        return true;
    }

    public override bool Enabled(RoleBehaviour? role)
    {
        return role is DeputyRole;
    }
}
