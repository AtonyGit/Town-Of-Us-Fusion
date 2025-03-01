namespace TownOfUsFusion.Modifiers.Doused;

public class DousedModifier : TimedModifier
{
    public override string ModifierName => "Doused";
    public override bool HideOnUi => true;
    public override float Duration => 0f;
    public override bool AutoStart => false;
    public override bool RemoveOnComplete => false;

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (Player?.AmOwner == true || PlayerControl.LocalPlayer.Data.Role is ArsonistRole)
        {
            Player?.cosmetics.SetOutline(true, new Il2CppSystem.Nullable<Color>(Colors.Arsonist));
        }
    }
}
