namespace TownOfUsFusion.Roles
{
    public class Impostor : Role
{
    public Impostor(PlayerControl player) : base(player)
    {
        Name = "Impostor";
        Hidden = true;
        Faction = Faction.Impostors;
        RoleType = RoleEnum.Impostor;
        Color = Palette.ImpostorRed;
        AddToRoleHistory(RoleType);
    }
}

public class Crewmate : Role
{
    public Crewmate(PlayerControl player) : base(player)
    {
        Name = "Crewmate";
        Hidden = true;
        Faction = Faction.Crewmates;
        RoleType = RoleEnum.Crewmate;
        Color = Patches.Colors.Crewmate;
        AddToRoleHistory(RoleType);
    }
}
}