namespace TownOfUsFusion.Patches.CustomGuide
{
    public class RoleData
{
    public CrewRoleMetadata[] Crewmates { get; set; }
    public NeutRoleMetadata[] Neutral { get; set; }
    public ImpRoleMetadata[] Impostors { get; set; }
    public ModifierMetadata[] Modifiers { get; set; }
}
    public class CrewRoleMetadata
{
    public string DisplayName { get; set; }
    public string Role { get; set; }
    public string Color { get; set; }
    public string Alias { get; set; }
    public string Alignment { get; set; }
    public string Description { get; set; }
}
    public class NeutRoleMetadata
{
    public string DisplayName { get; set; }
    public string Role { get; set; }
    public string Color { get; set; }
    public string Alias { get; set; }
    public string Alignment { get; set; }
    public string Description { get; set; }
}
    public class ImpRoleMetadata
{
    public string DisplayName { get; set; }
    public string Role { get; set; }
    public string Color { get; set; }
    public string Alias { get; set; }
    public string Alignment { get; set; }
    public string Description { get; set; }
}
    public class ModifierMetadata
{
    public string DisplayName { get; set; }
    public string Modifier { get; set; }
    public string Color { get; set; }
    public string AppliesTo { get; set; }
    public string Description { get; set; }
}
}