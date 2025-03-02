namespace TownOfUsFusion.Buttons.Werewolf;
public class RampageButton : CustomTouActionButton
{
    public override string Name => "Rampage";

    public override Color TextColor => Colors.Werewolf;
    public override string ButtonKeybind => "ActionQuaternary";
    public override ButtonLocation Location => ButtonLocation.BottomLeft;
    public override float Cooldown => OptionGroupSingleton<WerewolfRoleSettings>.Instance.RampageCooldown.Value;
    public float KillCooldown => OptionGroupSingleton<WerewolfRoleSettings>.Instance.RampageKillCooldown;
    public static float KillTimer { get; private set; } = OptionGroupSingleton<WerewolfRoleSettings>.Instance.RampageKillCooldown;

    public override float EffectDuration => OptionGroupSingleton<WerewolfRoleSettings>.Instance.RampageDuration;


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
        if (KillTimer >= 0)
        {
            KillTimer -= Time.deltaTime;
        }
        HudManager.Instance.KillButton.SetCoolDown(KillTimer, KillCooldown);
        HudManager.Instance.ImpostorVentButton.gameObject.SetActive(EffectActive || !PlayerControl.LocalPlayer.CanMove);
        
        Button.buttonLabelText.SetOutlineColor(TextColor);
        bool AbilityKey = Rewired.ReInput.players.GetPlayer(0).GetButtonDown(ButtonKeybind);
        if (Button != null && AbilityKey && !PlayerControl.LocalPlayer.Data.IsDead)
            Button?.DoClick();
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
