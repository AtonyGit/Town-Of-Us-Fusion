namespace TownOfUsFusion.Buttons;

public class NeutralKillerButton : CustomActionButton
{
    public override string Name => "Win Game";
    public override float Cooldown => 0f;
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
