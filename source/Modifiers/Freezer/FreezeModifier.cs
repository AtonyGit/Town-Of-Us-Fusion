namespace TownOfUsFusion.Modifiers.Freezer;

public class FreezeModifier : TimedModifier
{
    public override string ModifierName => "Freezed";
    public override bool HideOnUi => false;
    public override float Duration => 15f;
    public override bool AutoStart => true;
    public override bool RemoveOnComplete => true;

    public override void OnActivate()
    {
        if (Player?.AmOwner == true)
        {
            Player.moveable = false;
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (Player?.AmOwner == true || PlayerControl.LocalPlayer.Data.Role is FreezerRole)
        {
            Player?.cosmetics.SetOutline(true, new Il2CppSystem.Nullable<Color>(Palette.LightBlue));
        }
    }

    public override void OnTimerComplete()
    {
        if (Player?.AmOwner == true)
        {
            Player.moveable = true;
        }
    }
}
