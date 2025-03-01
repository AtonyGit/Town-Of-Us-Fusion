namespace TownOfUsFusion.Buttons.Werewolf;
public class RampageButton : CustomActionButton
{
    public override string Name => "Rampage";
    public override ButtonLocation Location => ButtonLocation.BottomRight;
    public override float Cooldown => OptionGroupSingleton<WerewolfOptions>.Instance.RampageCooldown.Value;

    public override float EffectDuration => OptionGroupSingleton<WerewolfOptions>.Instance.RampageDuration;

    public override int MaxUses => 0;

    public override LoadableAsset<Sprite> Sprite => ToufAssets.RampageButton;
    public static bool IsRampaging { get; private set; }

    public override bool Enabled(RoleBehaviour? role)
    {
        return role is WerewolfRole;
    }
    protected override void OnClick()
    {
        Coroutines.Start(StartRampage());
    }

    public override void OnEffectEnd()
    {
        Coroutines.Start(EndRampage());
    }

    protected override void FixedUpdate(PlayerControl playerControl)
    {
        base.FixedUpdate(playerControl);

        HudManager.Instance.KillButton.gameObject.SetActive(EffectActive);
        HudManager.Instance.ImpostorVentButton.gameObject.SetActive(EffectActive || !PlayerControl.LocalPlayer.CanMove);
    }

    private static IEnumerator StartRampage()
    {
        IsRampaging = true;
        return null;
    }

    private static IEnumerator EndRampage()
    {
        IsRampaging = false;
        return null;
    }
}
