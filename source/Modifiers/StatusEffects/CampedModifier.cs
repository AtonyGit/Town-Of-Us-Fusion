namespace TownOfUsFusion.Modifiers.Camped;

public class CampedModifier : TimedModifier
{
    public override string ModifierName => "Camped";
    public override bool HideOnUi => true;
    public override float Duration => 0f;
    public override bool AutoStart => false;
    public override bool RemoveOnComplete => false;

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (PlayerControl.LocalPlayer.Data.Role is DeputyRole)
        {
            Player?.cosmetics.SetOutline(true, new Il2CppSystem.Nullable<Color>(Colors.Deputy));
        }
    }
}
