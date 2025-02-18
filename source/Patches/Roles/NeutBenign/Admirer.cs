using System.Collections.Generic;
using System.Linq;
using TownOfUsFusion.Extensions;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TownOfUsFusion.Roles
{
    public class Admirer : Role
    {
        
        public Admirer(PlayerControl player) : base(player)
        {
            Name = "Admirer";
            ImpostorText = () => "Carry The Mantle Of The Fallen";
            TaskText = () => "Admire a player to become their role after their death.";
            Color = Patches.Colors.Admirer;
            AbilitySprite = TownOfUsFusion.RememberSprite;
            AbilityText = "Admire";
            RoleType = RoleEnum.Admirer;
            AddToRoleHistory(RoleType);
            Faction = Faction.NeutralBenign;
            AdmiredPlayer = null;
        }
        public PlayerControl ClosestPlayer;
        public bool ButtonUsable = false;

        public PlayerControl AdmiredPlayer { get; set; }

        protected override void IntroPrefix(IntroCutscene._ShowTeam_d__38 __instance)
        {
            var admirerTeam = new Il2CppSystem.Collections.Generic.List<PlayerControl>();
            admirerTeam.Add(PlayerControl.LocalPlayer);
            __instance.teamToShow = admirerTeam;
        }

    }
}