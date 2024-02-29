namespace TownOfUsFusion.Roles
{
    public interface IVisualAlteration
{
    bool TryGetModifiedAppearance(out VisualAppearance appearance);
}
}