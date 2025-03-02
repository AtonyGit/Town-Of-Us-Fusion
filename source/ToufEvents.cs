
using MiraAPI.Events.Vanilla.Gameplay;
using TownOfUsFusion.Modifiers.Camped;

namespace TownOfUsFusion;
public class ToufEvents
{
        private static void AfterMurderEventHandler(AfterMurderEvent murderEvent)
        {
            if (murderEvent.Target != null && murderEvent.Target.HasModifier<CampedModifier>())
            {
                foreach (var player in PlayerControl.AllPlayerControls)
                {
                    if (player.Data.Role is DeputyRole)
                    {
                        var dep = player.Data.Role as DeputyRole;
                        dep.Killer = murderEvent.Source;
                        Logger<TownOfUsFusion>.Info($"The Camped Player is {murderEvent.Target}, the killer is {murderEvent.Source}");
                    }
                }
                murderEvent.Target?.RpcRemoveModifier<CampedModifier>();
            }
        }
}