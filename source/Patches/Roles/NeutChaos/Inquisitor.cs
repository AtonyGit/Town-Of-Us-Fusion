using System.Collections.Generic;
using UnityEngine;
using System;
using TownOfUsFusion.Roles;
using System.Linq;
using TownOfUsFusion.Patches;
using Reactor.Utilities;

namespace TownOfUsFusion.Roles
{
    public class Inquisitor : Role
    {
        public List<byte> Heretics = new List<byte>();
        public List<string> HereticRoles = new List<string>();
        public bool invalidHeretics => Heretics == null || Heretics.Count < 3;
        public bool allHereticsDead => PlayerControl.AllPlayerControls.ToArray().Count(p => Heretics.Contains(p.PlayerId) && (p.Data.IsDead || p.Data.Disconnected)) == HereticCount || invalidHeretics;
        public bool didWin = false;
        public readonly List<GameObject> Buttons = new List<GameObject>();
        private KillButton _InquireButton;
        public DateTime LastInquired;
        public DateTime LastVanquished;
        public PlayerControl ClosestPlayer;
        public PlayerControl LastInquiredPlayer;
        public bool canVanquish;
        public int HereticCount;
        public string taskTextOverride;
        public string meetingTextOverride;
        public bool lostVanquish = false;
        public Inquisitor(PlayerControl player) : base(player)
        {
            Name = "Inquisitor";
            ImpostorText = () => "Vanquish The Heretics";
            HereticCount = CustomGameOptions.HereticCount;
            for (int i = 0; i < HereticCount; i++)
            {
                if(i == HereticCount) taskTextOverride += "and " + HereticRoles[i];
                else taskTextOverride += HereticRoles[i] + ", ";
            }
            for (int i = 0; i < HereticCount; i++)
            {
                if(i == HereticCount) meetingTextOverride += "or " + HereticRoles[i];
                else meetingTextOverride += HereticRoles[i] + ", ";
            }
            
            TaskText = () => allHereticsDead ? "The Heretics are all Vanquished!\nFake Tasks:" : $"The Heretics are: {taskTextOverride}.\nFake Tasks:";
            Color = Patches.Colors.Inquisitor;
            AbilitySprite = TownOfUsFusion.InquisKill;
            AbilityText = "Vanquish";
            SecondAbilityButton = InquireButton;
            SecondAbilitySprite = TownOfUsFusion.ObserveSprite;
            SecondAbilityText = "Inquire";
            RoleType = RoleEnum.Inquisitor;
            AddToRoleHistory(RoleType);
            Faction = Faction.NeutralChaos;
            canVanquish = false;
        }
        public void HereticsDead()
        {
            var role = Role.GetRole<Inquisitor>(Player);
            role.didWin = true;
            role.Invincible = true;
            if (Player == PlayerControl.LocalPlayer)
            {
                Coroutines.Start(Utils.FlashCoroutine(Patches.Colors.Inquisitor));
                role.RegenTask();
            }
        }
        
        public KillButton InquireButton
        {
            get => _InquireButton;
            set
            {
                _InquireButton = value;
                ExtraButtons.Clear();
                ExtraButtons.Add(value);
            }
        }
        public float InquireTimer()
        {
            var utcNow = DateTime.UtcNow;
            var timeSpan = utcNow - LastInquired;
            var num = CustomGameOptions.InquireCooldown * 1000f;
            var flag2 = num - (float)timeSpan.TotalMilliseconds < 0f;
            if (flag2) return 0;
            return (num - (float)timeSpan.TotalMilliseconds) / 1000f;
        }
        public float VanquishTimer()
        {
            var utcNow = DateTime.UtcNow;
            var timeSpan = utcNow - LastVanquished;
            var num = CustomGameOptions.VanquishCooldown * 1000f;
            var flag2 = num - (float)timeSpan.TotalMilliseconds < 0f;
            if (flag2) return 0;
            return (num - (float)timeSpan.TotalMilliseconds) / 1000f;
        }
        protected override void IntroPrefix(IntroCutscene._ShowTeam_d__38 __instance)
        {
            var vanTeam = new Il2CppSystem.Collections.Generic.List<PlayerControl>();
            vanTeam.Add(PlayerControl.LocalPlayer);
            __instance.teamToShow = vanTeam;
        }

    }
}