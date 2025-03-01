namespace TownOfUsFusion.Buttons;

public class MeetingButton : CustomActionButton
{
    public override string Name => "Call Meeting";

    public override float Cooldown => 15;

    public override int MaxUses => 3;

    public override LoadableAsset<Sprite> Sprite => ToufAssets.ExampleButton;

    public override bool Enabled(RoleBehaviour? role)
    {
        return PlayerControl.LocalPlayer != null && PlayerControl.LocalPlayer.HasModifier<CaptainModifier>();
    }

    protected override void OnClick()
    {
        var bt = ShipStatus.Instance.EmergencyButton;

        PlayerControl.LocalPlayer.NetTransform.Halt();
        var minigame = UObject.Instantiate(bt.MinigamePrefab, Camera.main!.transform, false);

        var taskAdderGame = minigame as TaskAdderGame;
        if (taskAdderGame != null)
        {
            taskAdderGame.SafePositionWorld = bt.SafePositionLocal + (Vector2)bt.transform.position;
        }

        minigame.transform.localPosition = new Vector3(0f, 0f, -50f);
        minigame.Begin(null);

        if (UsesLeft == 0 && PlayerControl.LocalPlayer.HasModifier<CaptainModifier>())
        {
            PlayerControl.LocalPlayer.RpcRemoveModifier<CaptainModifier>();
        }
    }
}
