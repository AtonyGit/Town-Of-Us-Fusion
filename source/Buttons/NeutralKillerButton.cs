namespace TownOfUsFusion.Buttons;

public class NeutralKillerButton : CustomTouActionButton
{
    public override string Name => "Win Game";
    public override float Cooldown => 0f;
    public override Color TextColor => Colors.Impostor;
    public override string ButtonKeybind => "ActionQuaternary";
    public override int MaxUses => 0;
    public override LoadableAsset<Sprite> Sprite => ToufAssets.ExampleButton;
    protected override void OnClick()
    {
        CustomGameOver.Trigger<NeutralKillerGameOver>([PlayerControl.LocalPlayer.Data]);
    }

    public override bool Enabled(RoleBehaviour? role)
    {
        return role is NeutralKillerRole;
    }
}
