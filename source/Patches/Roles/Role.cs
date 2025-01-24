using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using Reactor.Utilities.Extensions;
using TMPro;
using TownOfUsFusion.Roles.Modifiers;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;
using TownOfUsFusion.Extensions;
using AmongUs.GameOptions;
using TownOfUsFusion.ImpostorRoles.TraitorMod;
using TownOfUsFusion.Roles.Alliances;

namespace TownOfUsFusion.Roles
{
    public abstract class Role
    {
        public static readonly Dictionary<byte, Role> RoleDictionary = new Dictionary<byte, Role>();
        public static readonly List<KeyValuePair<byte, RoleEnum>> RoleHistory = new List<KeyValuePair<byte, RoleEnum>>();

        public static bool NobodyWins;
        public static bool ApocWins;
        public static bool JackalWins;
        public static bool SurvOnlyWins;
        public static bool VampireWins;

        public List<KillButton> ExtraButtons = new List<KillButton>();

        public Func<string> ImpostorText;
        public Func<string> TaskText;

        protected Role(PlayerControl player)
        {
            Player = player;
            RoleDictionary.Add(player.PlayerId, this);
            //TotalTasks = player.Data.Tasks.Count;
            //TasksLeft = TotalTasks;
        }

        public static IEnumerable<Role> AllRoles => RoleDictionary.Values.ToList();
        protected internal string Name { get; set; }

        private PlayerControl _player { get; set; }

        public PlayerControl Player
        {
            get => _player;
            set
            {
                if (_player != null) _player.nameText().color = Color.white;

                _player = value;
                PlayerName = value.Data.PlayerName;
            }
        }

        protected float Scale { get; set; } = 1f;
        protected internal Color Color { get; set; }
        protected internal RoleEnum RoleType { get; set; }
        protected internal int TasksLeft => Player.Data.Tasks.ToArray().Count(x => !x.Complete);
        protected internal int TotalTasks => Player.Data.Tasks.Count;
        protected internal int Kills { get; set; } = 0;
        protected internal int CorrectKills { get; set; } = 0;
        protected internal int IncorrectKills { get; set; } = 0;
        protected internal int CorrectAssassinKills { get; set; } = 0;
        protected internal int IncorrectAssassinKills { get; set; } = 0;

        public bool Local => PlayerControl.LocalPlayer.PlayerId == Player.PlayerId;

        protected internal bool Hidden { get; set; } = false;

        protected internal Faction Faction { get; set; } = Faction.Crewmates;
        protected internal AllianceEnum IsAlliance { get; set; }

        public static uint NetId => PlayerControl.LocalPlayer.NetId;
        public string PlayerName { get; set; }

        public string ColorString => "<color=#" + Color.ToHtmlStringRGBA() + ">";

        private bool Equals(Role other)
        {
            return Equals(Player, other.Player) && RoleType == other.RoleType;
        }

        public void AddToRoleHistory(RoleEnum role)
        {
            RoleHistory.Add(KeyValuePair.Create(_player.PlayerId, role));
        }

        public void RemoveFromRoleHistory(RoleEnum role)
        {
            RoleHistory.Remove(KeyValuePair.Create(_player.PlayerId, role));
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(Role)) return false;
            return Equals((Role)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Player, (int)RoleType);
        }

        //public static T Gen<T>()

        internal virtual bool Criteria()
        {
            return DeadCriteria() || ImpostorCriteria() || ApocalypseCriteria() || VampireCriteria() || LoverCriteria() || SelfCriteria() || RoleCriteria() || GuardianAngelCriteria() || Local;
        }

        internal virtual bool ColorCriteria()
        {
            return SelfCriteria() || DeadCriteria() || ApocalypseCriteria() || ((ColourImpostorCriteria() || VampireCriteria() || RoleCriteria() || GuardianAngelCriteria()) && (!PlayerControl.LocalPlayer.IsHypnotised() || MeetingHud.Instance));
        }

        internal virtual bool DeadCriteria()
        {
            if (PlayerControl.LocalPlayer.Data.IsDead && CustomGameOptions.DeadSeeRoles) return Utils.ShowDeadBodies;
            return false;
        }

        internal virtual bool ColourImpostorCriteria()
        {
            if (Faction == Faction.Impostors && PlayerControl.LocalPlayer.Data.IsImpostor()) return true;
            return false;
        }

        internal virtual bool ImpostorCriteria()
        {
            if ((Faction == Faction.Impostors || IsAlliance == AllianceEnum.Crewpostor) && (PlayerControl.LocalPlayer.Data.IsImpostor() || PlayerControl.LocalPlayer.Is(AllianceEnum.Crewpostor)) &&
                CustomGameOptions.ImpostorSeeRoles) return true;
            return false;
        }
        internal virtual bool ApocalypseCriteria()
        {
            if ((Faction == Faction.NeutralApocalypse || IsAlliance == AllianceEnum.Crewpocalypse) && (PlayerControl.LocalPlayer.Is(Faction.NeutralApocalypse) || PlayerControl.LocalPlayer.Is(AllianceEnum.Crewpocalypse))) return true;
            return false;
        }

        internal virtual bool VampireCriteria()
        {
            if (RoleType == RoleEnum.Vampire && PlayerControl.LocalPlayer.Is(RoleEnum.Vampire)) return true;
            return false;
        }

    internal virtual bool RecruitCriteria()
    {/*
        if (PlayerControl.LocalPlayer.Is(RoleEnum.Jackal))
        {
            if (Local) return true;
            var jackal = GetRole<Jackal>(PlayerControl.LocalPlayer);
            var recruit1 = Alliance.GetAlliance<Recruit>(jackal.Recruit1.Player);
            var recruit2 = Alliance.GetAlliance<Recruit>(jackal.Recruit2.Player);
            if (recruit1.OtherRecruit.Player != Player && recruit2.OtherRecruit.Player != Player) return false;
            //if (!PlayerControl.LocalPlayer.Is(RoleEnum.Aurial)) return true;
            if (MeetingHud.Instance || Utils.ShowDeadBodies) return true;
            if (recruit1.OtherRecruit.Player.Is(RoleEnum.Mayor))
            {
                var mayor = GetRole<Mayor>(recruit1.OtherRecruit.Player);
                if (mayor.Revealed) return true;
            } else
            if (recruit2.OtherRecruit.Player.Is(RoleEnum.Mayor))
            {
                var mayor = GetRole<Mayor>(recruit2.OtherRecruit.Player);
                if (mayor.Revealed) return true;
            }
        }*/
        /*
        if (PlayerControl.LocalPlayer.Is(AllianceEnum.Recruit))
        {
            if (Local) return true;
            var recruit = Alliance.GetAlliance<Recruit>(PlayerControl.LocalPlayer);
            if (recruit.OtherRecruit.Player == null || recruit.OtherRecruit.Player != Player) return false;
            if (!PlayerControl.LocalPlayer.Is(RoleEnum.Aurial)) return true;
            if (MeetingHud.Instance || Utils.ShowDeadBodies) return true;
            if (recruit.OtherRecruit.Player.Is(RoleEnum.Mayor))
            {
                var mayor = GetRole<Mayor>(recruit.OtherRecruit.Player);
                if (mayor.Revealed) return true;
            }
        }*/
        return false;
    }
    internal virtual bool LoverCriteria()
    {
        if (PlayerControl.LocalPlayer.Is(AllianceEnum.Lover))
        {
            if (Local) return true;
            var lover = Alliance.GetAlliance<Lover>(PlayerControl.LocalPlayer);
            if (lover.OtherLover.Player != Player) return false;
            if (!PlayerControl.LocalPlayer.Is(RoleEnum.Aurial)) return true;
            if (MeetingHud.Instance || Utils.ShowDeadBodies) return true;
            if (lover.OtherLover.Player.Is(RoleEnum.Mayor))
            {
                var mayor = GetRole<Mayor>(lover.OtherLover.Player);
                if (mayor.Revealed) return true;
            }
        }
        return false;
    }

        internal virtual bool SelfCriteria()
        {
            return GetRole(PlayerControl.LocalPlayer) == this;
        }

        internal virtual bool RoleCriteria()
        {
            return PlayerControl.LocalPlayer.Is(ModifierEnum.Sleuth) && Modifier.GetModifier<Sleuth>(PlayerControl.LocalPlayer).Reported.Contains(Player.PlayerId);
        }
        internal virtual bool GuardianAngelCriteria()
        {
            return PlayerControl.LocalPlayer.Is(RoleEnum.GuardianAngel) && CustomGameOptions.GAKnowsTargetRole && Player == GetRole<GuardianAngel>(PlayerControl.LocalPlayer).target;
        }

        protected virtual void IntroPrefix(IntroCutscene._ShowTeam_d__38 __instance)
        {
        }

        public static void NobodyWinsFunc()
        {
            NobodyWins = true;
        }
        public static void SurvOnlyWin()
        {
            SurvOnlyWins = true;
        }
        public static void VampWin()
        {
            foreach (var jest in GetRoles(RoleEnum.Jester))
            {
                var jestRole = (Jester)jest;
                if (jestRole.VotedOut && CustomGameOptions.NeutralEvilWinEndsGame) return;
            }
            foreach (var exe in GetRoles(RoleEnum.Executioner))
            {
                var exeRole = (Executioner)exe;
                if (exeRole.TargetVotedOut && CustomGameOptions.NeutralEvilWinEndsGame) return;
            }
            foreach (var doom in GetRoles(RoleEnum.Doomsayer))
            {
                var doomRole = (Doomsayer)doom;
                if (doomRole.WonByGuessing && CustomGameOptions.NeutralEvilWinEndsGame) return;
            }
            foreach (var can in GetRoles(RoleEnum.Cannibal))
            {
                var canRole = (Cannibal)can;
                if (canRole.EatWin) return;
            }

            VampireWins = true;

            Utils.Rpc(CustomRPC.VampireWin);
        }
        public static void ApocWin()
        {
            foreach (var jest in GetRoles(RoleEnum.Jester))
            {
                var jestRole = (Jester)jest;
                if (jestRole.VotedOut) return;
            }
            foreach (var exe in GetRoles(RoleEnum.Executioner))
            {
                var exeRole = (Executioner)exe;
                if (exeRole.TargetVotedOut) return;
            }
            foreach (var can in GetRoles(RoleEnum.Cannibal))
            {
                var canRole = (Cannibal)can;
                if (canRole.EatWin) return;
            }
            foreach (var can in GetRoles(RoleEnum.Cannibal))
            {
                var canRole = (Cannibal)can;
                if (canRole.EatWin) return;
            }
            foreach (var doom in GetRoles(RoleEnum.Doomsayer))
            {
                var doomRole = (Doomsayer)doom;
                if (doomRole.WonByGuessing) return;
            }

            ApocWins = true;

            Utils.Rpc(CustomRPC.ApocWin);
        }/*
        public static void JackalWin()
        {
            foreach (var jest in GetRoles(RoleEnum.Jester))
            {
                var jestRole = (Jester)jest;
                if (jestRole.VotedOut) return;
            }
            foreach (var exe in GetRoles(RoleEnum.Executioner))
            {
                var exeRole = (Executioner)exe;
                if (exeRole.TargetVotedOut) return;
            }
            foreach (var can in GetRoles(RoleEnum.Cannibal))
            {
                var canRole = (Cannibal)can;
                if (canRole.EatWin) return;
            }
            foreach (var can in GetRoles(RoleEnum.Cannibal))
            {
                var canRole = (Cannibal)can;
                if (canRole.EatWin) return;
            }
            foreach (var doom in GetRoles(RoleEnum.Doomsayer))
            {
                var doomRole = (Doomsayer)doom;
                if (doomRole.WonByGuessing) return;
            }

            JackalWins = true;

            Utils.Rpc(CustomRPC.JackalWin);
        }*/

        internal static bool NobodyEndCriteria(LogicGameFlowNormal __instance)
        {
            bool CheckNoImpsNoCrews()
            {
                var alives = PlayerControl.AllPlayerControls.ToArray()
                    .Where(x => !x.Data.IsDead && !x.Data.Disconnected).ToList();
                if (alives.Count == 0) return false;
                var flag = alives.All(x =>
                {
                    var role = GetRole(x);
                    if (role == null) return false;
                    var flag2 = role.Faction == Faction.NeutralEvil || role.Faction == Faction.NeutralBenign;

                    return flag2;
                });

                return flag;
            }

            bool SurvOnly()
            {
                var alives = PlayerControl.AllPlayerControls.ToArray()
                    .Where(x => !x.Data.IsDead && !x.Data.Disconnected).ToList();
                if (alives.Count == 0) return false;
                var flag = false;
                foreach (var player in alives)
                {
                    if (player.Is(RoleEnum.Survivor)) flag = true;
                }
                return flag;
            }

            if (CheckNoImpsNoCrews())
            {
                if (SurvOnly())
                {
                    Utils.Rpc(CustomRPC.SurvivorOnlyWin);

                    SurvOnlyWin();
                    Utils.EndGame();
                    return false;
                }
                else
                {
                    Utils.Rpc(CustomRPC.NobodyWins);

                    NobodyWinsFunc();
                    Utils.EndGame();
                    return false;
                }
            }
            return true;
        }

        internal virtual bool GameEnd(LogicGameFlowNormal __instance)
        {
            return true;
        }

        internal bool PauseEndCrit = false;

        protected virtual string NameText(bool revealTasks, bool revealRole, bool revealAlliance, bool revealRecruit, bool revealLover, PlayerVoteArea player = null)
        {
            if (PlayerControl.LocalPlayer.IsHypnotised() && Player.GetCustomOutfitType() == CustomPlayerOutfitType.Morph && player == null) return PlayerControl.LocalPlayer.GetDefaultOutfit().PlayerName;
            else if (((CamouflageUnCamouflage.IsCamoed && !PlayerControl.LocalPlayer.IsHypnotised()) || (PlayerControl.LocalPlayer.IsHypnotised() && PlayerControl.LocalPlayer != Player)) && player == null) return "";

            if (Player == null) return "";

            String PlayerName = Player.GetDefaultOutfit().PlayerName;

            foreach (var role in GetRoles(RoleEnum.GuardianAngel))
            {
                var ga = (GuardianAngel) role;
                if (Player == ga.target && ((Player == PlayerControl.LocalPlayer && CustomGameOptions.GATargetKnows)
                    || (PlayerControl.LocalPlayer.Data.IsDead && !ga.Player.Data.IsDead)))
                {
                    PlayerName += "<color=#B3FFFFFF> ★</color>";
                }
            }

            foreach (var role in GetRoles(RoleEnum.Executioner))
            {
                var exe = (Executioner) role;
                if (Player == exe.target && PlayerControl.LocalPlayer.Data.IsDead && !exe.Player.Data.IsDead)
                {
                    PlayerName += "<color=#8C4005FF> X</color>";
                }
            }
/*
            var modifier = Modifier.GetModifier(Player);
            if (modifier != null && modifier.GetColoredSymbol() != null)
            {
                if (modifier.ModifierType == AllianceEnum.Lover && (revealModifier || revealLover))
                    PlayerName += $" {modifier.GetColoredSymbol()}";
                else if (modifier.ModifierType != AllianceEnum.Lover && revealModifier)
                    PlayerName += $" {modifier.GetColoredSymbol()}";
            }*/
            
        var alliance = Alliance.GetAlliance(Player);
        if (alliance != null && alliance.GetColoredSymbol() != null)
        {
            if (alliance.AllianceType == AllianceEnum.Lover && (revealAlliance || revealLover))
                PlayerName += $" {alliance.GetColoredSymbol()}";
            else if (alliance.AllianceType != AllianceEnum.Lover && revealAlliance)
                PlayerName += $" {alliance.GetColoredSymbol()}";
            else if (alliance.AllianceType == AllianceEnum.Recruit && (revealAlliance || revealRecruit))
                PlayerName = Utils.GradientColorText("B7B9BA", "5E576B", PlayerName);
            else if (alliance.AllianceType != AllianceEnum.Recruit && revealAlliance)
                PlayerName = Utils.GradientColorText("B7B9BA", "5E576B", PlayerName);
        }

            if (revealTasks && (Faction == Faction.Crewmates || RoleType == RoleEnum.Phantom))
            {
                if ((PlayerControl.LocalPlayer.Data.IsDead && CustomGameOptions.SeeTasksWhenDead) || (MeetingHud.Instance && CustomGameOptions.SeeTasksDuringMeeting) || (!PlayerControl.LocalPlayer.Data.IsDead && !MeetingHud.Instance && CustomGameOptions.SeeTasksDuringRound))
                {
                    PlayerName += $" ({TotalTasks - TasksLeft}/{TotalTasks})";
                }
            }

            if (player != null && (MeetingHud.Instance.state == MeetingHud.VoteStates.Proceeding ||
                                   MeetingHud.Instance.state == MeetingHud.VoteStates.Results)) return PlayerName;

            if (!revealRole) return PlayerName;

            Player.nameText().transform.localPosition = new Vector3(0f, 0.15f, -0.5f);

            return PlayerName + "\n" + Name;
        }

        public static bool operator ==(Role a, Role b)
        {
            if (a is null && b is null) return true;
            if (a is null || b is null) return false;
            return a.RoleType == b.RoleType && a.Player.PlayerId == b.Player.PlayerId;
        }

        public static bool operator !=(Role a, Role b)
        {
            return !(a == b);
        }

        public void RegenTask()
        {
            bool createTask;
            try
            {
                var firstText = Player.myTasks.ToArray()[0].Cast<ImportantTextTask>();
                createTask = !firstText.Text.Contains("Role:");
            }
            catch (InvalidCastException)
            {
                createTask = true;
            }

            if (createTask)
            {
                var task = new GameObject(Name + "Task").AddComponent<ImportantTextTask>();
                task.transform.SetParent(Player.transform, false);
                task.Text = $"{ColorString}Role: {Name}\n{TaskText()}</color>";
                Player.myTasks.Insert(0, task);
                return;
            }

            Player.myTasks.ToArray()[0].Cast<ImportantTextTask>().Text =
                $"{ColorString}Role: {Name}\n{TaskText()}</color>";
        }

        public static T Gen<T>(Type type, PlayerControl player, CustomRPC rpc)
        {
            var role = (T)Activator.CreateInstance(type, new object[] { player });

            Utils.Rpc(rpc, player.PlayerId);
            return role;
        }

        public static T GenRole<T>(Type type, PlayerControl player)
        {
            var role = (T)Activator.CreateInstance(type, new object[] { player });

            Utils.Rpc(CustomRPC.SetRole, player.PlayerId, (string)type.FullName);
            return role;
        }

        public static T GenModifier<T>(Type type, PlayerControl player)
        {
            var modifier = (T)Activator.CreateInstance(type, new object[] { player });

            Utils.Rpc(CustomRPC.SetModifier, player.PlayerId, (string)type.FullName);
            return modifier;
        }

        public static T GenRole<T>(Type type, List<PlayerControl> players)
        {
            var player = players[Random.RandomRangeInt(0, players.Count)];

            var role = GenRole<T>(type, player);
            players.Remove(player);
            return role;
        }
        public static T GenModifier<T>(Type type, List<PlayerControl> players)
        {
            var player = players[Random.RandomRangeInt(0, players.Count)];

            var modifier = GenModifier<T>(type, player);
            players.Remove(player);
            return modifier;
        }

        public static Role GetRole(PlayerControl player)
        {
            if (player == null) return null;
            if (RoleDictionary.TryGetValue(player.PlayerId, out var role))
                return role;

            return null;
        }
        
        public static T GetRole<T>(PlayerControl player) where T : Role
        {
            return GetRole(player) as T;
        }

        public static Role GetRole(PlayerVoteArea area)
        {
            var player = PlayerControl.AllPlayerControls.ToArray()
                .FirstOrDefault(x => x.PlayerId == area.TargetPlayerId);
            return player == null ? null : GetRole(player);
        }

        public static IEnumerable<Role> GetRoles(RoleEnum roletype)
        {
            return AllRoles.Where(x => x.RoleType == roletype);
        }

        public static class IntroCutScenePatch
        {
            public static TextMeshPro ModifierText;

            public static float Scale;

            [HarmonyPatch(typeof(IntroCutscene), nameof(IntroCutscene.BeginCrewmate))]
            public static class IntroCutscene_BeginCrewmate
            {
                public static void Postfix(IntroCutscene __instance)
                {
                    var modifier = Modifier.GetModifier(PlayerControl.LocalPlayer);
                    if (modifier != null)
                        ModifierText = Object.Instantiate(__instance.RoleText, __instance.RoleText.transform.parent, false);
                    else
                        ModifierText = null;
                }
            }

            [HarmonyPatch(typeof(IntroCutscene), nameof(IntroCutscene.BeginImpostor))]
            public static class IntroCutscene_BeginImpostor
            {
                public static void Postfix(IntroCutscene __instance)
                {
                    var modifier = Modifier.GetModifier(PlayerControl.LocalPlayer);
                    if (modifier != null)
                        ModifierText = Object.Instantiate(__instance.RoleText, __instance.RoleText.transform.parent, false);
                    else
                        ModifierText = null;
                }
            }

            [HarmonyPatch(typeof(IntroCutscene._ShowTeam_d__38), nameof(IntroCutscene._ShowTeam_d__38.MoveNext))]
            public static class IntroCutscene_ShowTeam__d_MoveNext
            {
                public static void Prefix(IntroCutscene._ShowTeam_d__38 __instance)
                {
                    var role = GetRole(PlayerControl.LocalPlayer);

                    if (role != null) role.IntroPrefix(__instance);
                }

                public static void Postfix(IntroCutscene._ShowRole_d__41 __instance)
                {
                    var role = GetRole(PlayerControl.LocalPlayer);
                    // var alpha = __instance.__4__this.RoleText.color.a;
                    if (role != null && !role.Hidden)
                    {
                        if (role.Faction == Faction.NeutralKilling || role.Faction == Faction.NeutralEvil || role.Faction == Faction.NeutralBenign
                        || role.Faction == Faction.NeutralChaos || role.Faction == Faction.NeutralNeophyte || role.Faction == Faction.NeutralApocalypse)
                        {
                            __instance.__4__this.TeamTitle.text = "Neutral";
                            __instance.__4__this.TeamTitle.color = Color.white;
                            __instance.__4__this.BackgroundBar.material.color = Color.white;
                            PlayerControl.LocalPlayer.Data.Role.IntroSound = GetIntroSound(RoleTypes.Shapeshifter);
                        }
                        __instance.__4__this.RoleText.text = role.Name;
                        __instance.__4__this.RoleText.color = role.Color;
                        __instance.__4__this.YouAreText.color = role.Color;
                        __instance.__4__this.RoleBlurbText.color = role.Color;
                        __instance.__4__this.RoleBlurbText.text = role.ImpostorText();
                        //    __instance.__4__this.ImpostorText.gameObject.SetActive(true);
                        // __instance.__4__this.BackgroundBar.material.color = role.Color;
                        //                        TestScale = Mathf.Max(__instance.__this.Title.scale, TestScale);
                        //                        __instance.__this.Title.scale = TestScale / role.Scale;
                    }
                    /*else if (!__instance.isImpostor)
                    {
                        __instance.__this.ImpostorText.text = "Haha imagine being a boring old crewmate";
                    }*/

                    if (ModifierText != null)
                    {
                        var modifier = Modifier.GetModifier(PlayerControl.LocalPlayer);
                        if (modifier.GetType() == typeof(Lover))
                        {
                            ModifierText.text = $"<size=3>{modifier.TaskText()}</size>";
                        }
                        else
                        {
                            ModifierText.text = "<size=4>Modifier: " + modifier.Name + "</size>";
                        }
                        ModifierText.color = modifier.Color;

                        //
                        ModifierText.transform.position =
                            __instance.__4__this.transform.position - new Vector3(0f, 1.6f, 0f);
                        ModifierText.gameObject.SetActive(true);
                    }
                }
            }

            [HarmonyPatch(typeof(IntroCutscene._ShowRole_d__41), nameof(IntroCutscene._ShowRole_d__41.MoveNext))]
            public static class IntroCutscene_ShowRole_d__24
            {
                public static void Postfix(IntroCutscene._ShowRole_d__41 __instance)
                {
                    var role = GetRole(PlayerControl.LocalPlayer);
                    if (role != null && !role.Hidden)
                    {
                        if (role.Faction == Faction.NeutralKilling || role.Faction == Faction.NeutralEvil || role.Faction == Faction.NeutralBenign
                        || role.Faction == Faction.NeutralChaos || role.Faction == Faction.NeutralNeophyte)
                        {
                            __instance.__4__this.TeamTitle.text = "Neutral";
                            __instance.__4__this.TeamTitle.color = Color.white;
                            __instance.__4__this.BackgroundBar.material.color = Color.white;
                            PlayerControl.LocalPlayer.Data.Role.IntroSound = GetIntroSound(RoleTypes.Shapeshifter);
                        }
                        else if(role.Faction == Faction.NeutralApocalypse)
                        {
                            __instance.__4__this.TeamTitle.text = "Apocalypse";
                            __instance.__4__this.TeamTitle.color = Patches.Colors.Apocalypse;
                            __instance.__4__this.BackgroundBar.material.color = Patches.Colors.Apocalypse;
                            PlayerControl.LocalPlayer.Data.Role.IntroSound = GetIntroSound(RoleTypes.Phantom);
                        }
                        else if(role.Faction == Faction.Crewmates)
                        {
                            switch(role)
                            {
                                case Aurial:
                                case Lookout:
                                case Medium:
                                case Snitch:
                                case Coroner:
                                case Investigator:
                                case Psychic:
                                case Spy:
                                case Tracker:
                                case Trapper:
                                PlayerControl.LocalPlayer.Data.Role.IntroSound = GetIntroSound(RoleTypes.Tracker);
                                break;
                                //case Bodyguard:
                                case Medic:
                                //case MirrorMaster:
                                case Oracle:
                                PlayerControl.LocalPlayer.Data.Role.IntroSound = GetIntroSound(RoleTypes.Scientist);
                                break;
                                //case Captain:
                                case Politician:
                                case Prosecutor:
                                case Swapper:
                                PlayerControl.LocalPlayer.Data.Role.IntroSound = GetIntroSound(RoleTypes.Noisemaker);
                                break;
                                case Engineer:
                                case Imitator:
                                case Transporter:
                                PlayerControl.LocalPlayer.Data.Role.IntroSound = GetIntroSound(RoleTypes.Engineer);
                                break;
                            }
                        }
                        __instance.__4__this.RoleText.text = role.Name;
                        __instance.__4__this.RoleText.color = role.Color;
                        __instance.__4__this.YouAreText.color = role.Color;
                        __instance.__4__this.RoleBlurbText.color = role.Color;
                        __instance.__4__this.RoleBlurbText.text = role.ImpostorText();
                        // __instance.__4__this.BackgroundBar.material.color = role.Color;
                    }

                    if (ModifierText != null)
                    {
                        var modifier = Modifier.GetModifier(PlayerControl.LocalPlayer);
                        if (modifier.GetType() == typeof(Lover))
                        {
                            ModifierText.text = $"<size=3>{modifier.TaskText()}</size>";
                        }
                        else
                        {
                            ModifierText.text = "<size=4>Modifier: " + modifier.Name + "</size>";
                        }
                        ModifierText.color = modifier.Color;

                        ModifierText.transform.position =
                            __instance.__4__this.transform.position - new Vector3(0f, 1.6f, 0f);
                        ModifierText.gameObject.SetActive(true);
                    }

                    var players = GameData.Instance.PlayerCount;
                    if (players > 6)
                    {
                        List<RoleOptions> buckets = [CustomGameOptions.Slot1, CustomGameOptions.Slot2, CustomGameOptions.Slot3, CustomGameOptions.Slot4, CustomGameOptions.Slot5, CustomGameOptions.Slot6, CustomGameOptions.Slot7];
                        bool isAny = false;

                        if (players > 7) buckets.Add(CustomGameOptions.Slot8);
                        if (players > 8) buckets.Add(CustomGameOptions.Slot9);
                        if (players > 9) buckets.Add(CustomGameOptions.Slot10);
                        if (players > 10) buckets.Add(CustomGameOptions.Slot11);
                        if (players > 11) buckets.Add(CustomGameOptions.Slot12);
                        if (players > 12) buckets.Add(CustomGameOptions.Slot13);
                        if (players > 13) buckets.Add(CustomGameOptions.Slot14);
                        if (players > 14) buckets.Add(CustomGameOptions.Slot15);

                        foreach (var roleOption in buckets)
                        {
                            if (roleOption == RoleOptions.Any)
                            {
                                isAny = true;
                                break;
                            }
                        }

                        if (isAny) __instance.__4__this.ImpostorText.text = "There are an <color=#FF0000FF>Unknown Number of Impostors</color> among us";
                    }
                }
            }

            [HarmonyPatch(typeof(IntroCutscene._CoBegin_d__35), nameof(IntroCutscene._CoBegin_d__35.MoveNext))]
            public static class IntroCutscene_CoBegin_d__29
            {
                public static void Postfix(IntroCutscene._CoBegin_d__35 __instance)
                {
                    var role = GetRole(PlayerControl.LocalPlayer);
                    if (role != null && !role.Hidden)
                    {
                        if (role.Faction == Faction.NeutralKilling || role.Faction == Faction.NeutralEvil || role.Faction == Faction.NeutralBenign
                        || role.Faction == Faction.NeutralChaos || role.Faction == Faction.NeutralNeophyte || role.Faction == Faction.NeutralApocalypse)
                        {
                            __instance.__4__this.TeamTitle.text = "Neutral";
                            __instance.__4__this.TeamTitle.color = Color.white;
                            __instance.__4__this.BackgroundBar.material.color = Color.white;
                            PlayerControl.LocalPlayer.Data.Role.IntroSound = GetIntroSound(RoleTypes.Shapeshifter);
                        }
                        __instance.__4__this.RoleText.text = role.Name;
                        __instance.__4__this.RoleText.color = role.Color;
                        __instance.__4__this.YouAreText.color = role.Color;
                        __instance.__4__this.RoleBlurbText.color = role.Color;
                        __instance.__4__this.RoleBlurbText.text = role.ImpostorText();
                        __instance.__4__this.BackgroundBar.material.color = role.Color;
                    }

                    if (ModifierText != null)
                    {
                        var modifier = Modifier.GetModifier(PlayerControl.LocalPlayer);
                        if (modifier.GetType() == typeof(Lover))
                        {
                            ModifierText.text = $"<size=3>{modifier.TaskText()}</size>";
                        }
                        else
                        {
                            ModifierText.text = "<size=4>Modifier: " + modifier.Name + "</size>";
                        }
                        ModifierText.color = modifier.Color;

                        ModifierText.transform.position =
                            __instance.__4__this.transform.position - new Vector3(0f, 1.6f, 0f);
                        ModifierText.gameObject.SetActive(true);
                    }

                    var players = GameData.Instance.PlayerCount;
                    if (players > 6)
                    {
                        List<RoleOptions> buckets = [CustomGameOptions.Slot1, CustomGameOptions.Slot2, CustomGameOptions.Slot3, CustomGameOptions.Slot4, CustomGameOptions.Slot5, CustomGameOptions.Slot6, CustomGameOptions.Slot7];
                        bool isAny = false;

                        if (players > 7) buckets.Add(CustomGameOptions.Slot8);
                        if (players > 8) buckets.Add(CustomGameOptions.Slot9);
                        if (players > 9) buckets.Add(CustomGameOptions.Slot10);
                        if (players > 10) buckets.Add(CustomGameOptions.Slot11);
                        if (players > 11) buckets.Add(CustomGameOptions.Slot12);
                        if (players > 12) buckets.Add(CustomGameOptions.Slot13);
                        if (players > 13) buckets.Add(CustomGameOptions.Slot14);
                        if (players > 14) buckets.Add(CustomGameOptions.Slot15);

                        foreach (var roleOption in buckets)
                        {
                            if (roleOption == RoleOptions.Any)
                            {
                                isAny = true;
                                break;
                            }
                        }

                        if (isAny) __instance.__4__this.ImpostorText.text = "There are an <color=#FF0000FF>Unknown Number of Impostors</color> among us";
                    }
                }
            }
        }

        [HarmonyPatch(typeof(PlayerControl._CoSetTasks_d__141), nameof(PlayerControl._CoSetTasks_d__141.MoveNext))]
        public static class PlayerControl_SetTasks
        {
            public static void Postfix(PlayerControl._CoSetTasks_d__141 __instance)
            {
                if (__instance == null) return;
                var player = __instance.__4__this;
                var role = GetRole(player);
                var modifier = Modifier.GetModifier(player);
                var alliance = Alliance.GetAlliance(player);

                if (alliance != null)
                {
                    var modTask = new GameObject(alliance.Name + "Task").AddComponent<ImportantTextTask>();
                    modTask.transform.SetParent(player.transform, false);
                    modTask.Text =
                        $"<size=80%>{alliance.ColorString}Alliance: {alliance.Name}\n{alliance.TaskText()}</color></size>";
                    player.myTasks.Insert(0, modTask);
                }

                if (modifier != null)
                {
                    var modTask = new GameObject(modifier.Name + "Task").AddComponent<ImportantTextTask>();
                    modTask.transform.SetParent(player.transform, false);
                    modTask.Text =
                        $"<size=80%>{modifier.ColorString}Modifier: {modifier.Name}\n{modifier.TaskText()}</color></size>";
                    player.myTasks.Insert(0, modTask);
                }

                if (role == null || role.Hidden) return;
                if (role.RoleType == RoleEnum.Amnesiac && role.Player != PlayerControl.LocalPlayer) return;
                var task = new GameObject(role.Name + "Task").AddComponent<ImportantTextTask>();
                task.transform.SetParent(player.transform, false);
                task.Text = $"{role.ColorString}Role: {role.Name}\n{role.TaskText()}</color>";
                player.myTasks.Insert(0, task);
            }
        }

        [HarmonyPatch]
        public static class ShipStatus_KMPKPPGPNIH
        {
            [HarmonyPatch(typeof(LogicGameFlowNormal), nameof(LogicGameFlowNormal.CheckEndCriteria))]
            public static bool Prefix(LogicGameFlowNormal __instance)
            {
                if (GameOptionsManager.Instance.CurrentGameOptions.GameMode == GameModes.HideNSeek) return true;
                if (!AmongUsClient.Instance.AmHost) return false;
                if (ShipStatus.Instance.Systems != null)
                {
                    if (ShipStatus.Instance.Systems.ContainsKey(SystemTypes.LifeSupp))
                    {
                        var lifeSuppSystemType = ShipStatus.Instance.Systems[SystemTypes.LifeSupp].Cast<LifeSuppSystemType>();
                        if (lifeSuppSystemType.Countdown < 0f) return true;
                    }

                    if (ShipStatus.Instance.Systems.ContainsKey(SystemTypes.Laboratory))
                    {
                        var reactorSystemType = ShipStatus.Instance.Systems[SystemTypes.Laboratory].Cast<ReactorSystemType>();
                        if (reactorSystemType.Countdown < 0f) return true;
                    }

                    if (ShipStatus.Instance.Systems.ContainsKey(SystemTypes.Reactor))
                    {
                        var reactorSystemType = ShipStatus.Instance.Systems[SystemTypes.Reactor].Cast<ICriticalSabotage>();
                        if (reactorSystemType.Countdown < 0f) return true;
                    }
                }

                if (GameData.Instance.TotalTasks <= GameData.Instance.CompletedTasks)
                {
                    GameManager.Instance.RpcEndGame(GameOverReason.HumansByTask, false);
                    return false;
                }
                
                var result = true;
                foreach (var role in AllRoles)
                {
                    var roleIsEnd = role.GameEnd(__instance);
                    var modifier = Modifier.GetModifier(role.Player);
                    bool modifierIsEnd = true;
                    var alives = PlayerControl.AllPlayerControls.ToArray().Where(x => !x.Data.IsDead && !x.Data.Disconnected).ToList();
                    var impsAlive = PlayerControl.AllPlayerControls.ToArray().Where(x => !x.Data.IsDead && !x.Data.Disconnected && x.Data.IsImpostor()).ToList();
                    var traitorIsEnd = true;
                    if (SetTraitor.WillBeTraitor != null)
                    {
                        traitorIsEnd = SetTraitor.WillBeTraitor.Data.IsDead || SetTraitor.WillBeTraitor.Data.Disconnected || alives.Count < CustomGameOptions.LatestSpawn || impsAlive.Count * 2 >= alives.Count;
                    }
                    if (modifier != null)
                        modifierIsEnd = modifier.ModifierWin(__instance);
                    if (!roleIsEnd || !modifierIsEnd || !traitorIsEnd || role.PauseEndCrit) result = false;
                }

                if (!NobodyEndCriteria(__instance)) result = false;

                return result;
            }
        }

        [HarmonyPatch]
        public static class EndGameDisconnect
        {
            [HarmonyPatch(typeof(GameManager), nameof(GameManager.RpcEndGame))]
            public static bool Prefix(GameManager __instance, GameOverReason endReason)
            {
                if (endReason == GameOverReason.ImpostorDisconnect)
                {
                    foreach (var player in PlayerControl.AllPlayerControls)
                    {
                        if (player.Data.IsDead || player.Data.Disconnected) continue;
                        if (player.Is(Faction.NeutralKilling) || player.Is(Faction.NeutralNeophyte) || player.Is(Faction.NeutralApocalypse) || (player.IsCrewKiller() && CustomGameOptions.CrewKillersContinue)) return false;
                    }
                }
                else if (endReason == GameOverReason.HumansDisconnect)
                {
                    foreach (var player in PlayerControl.AllPlayerControls)
                    {
                        if (player.Data.IsDead || player.Data.Disconnected) continue;
                        if (player.Is(Faction.NeutralKilling) || player.Is(Faction.NeutralNeophyte) || player.Is(Faction.NeutralApocalypse) || (SetTraitor.WillBeTraitor == player &&
                            PlayerControl.AllPlayerControls.ToArray().Where(x => !x.Data.IsDead && !x.Data.Disconnected).ToList().Count >= CustomGameOptions.LatestSpawn)) return false;
                    }
                }
                return true;
            }
        }

        [HarmonyPatch(typeof(LobbyBehaviour), nameof(LobbyBehaviour.Start))]
        public static class LobbyBehaviour_Start
        {
            private static void Postfix(LobbyBehaviour __instance)
            {
                foreach (var role in AllRoles.Where(x => x.RoleType == RoleEnum.Snitch))
                {
                    ((Snitch)role).ImpArrows.DestroyAll();
                    ((Snitch)role).SnitchArrows.Values.DestroyAll();
                    ((Snitch)role).SnitchArrows.Clear();
                }
                foreach (var role in AllRoles.Where(x => x.RoleType == RoleEnum.Tracker))
                {
                    ((Tracker)role).TrackerArrows.Values.DestroyAll();
                    ((Tracker)role).TrackerArrows.Clear();
                }
                foreach (var role in AllRoles.Where(x => x.RoleType == RoleEnum.Aurial))
                {
                    ((Aurial)role).SenseArrows.Values.DestroyAll();
                    ((Aurial)role).SenseArrows.Clear();
                }
                foreach (var role in AllRoles.Where(x => x.RoleType == RoleEnum.Amnesiac))
                {
                    ((Amnesiac)role).BodyArrows.Values.DestroyAll();
                    ((Amnesiac)role).BodyArrows.Clear();
                }
                foreach (var role in AllRoles.Where(x => x.RoleType == RoleEnum.Medium))
                {
                    ((Medium)role).MediatedPlayers.Values.DestroyAll();
                    ((Medium)role).MediatedPlayers.Clear();
                    ((Medium)role).BodyArrows.Values.DestroyAll();
                    ((Medium)role).BodyArrows.Clear();
                }
                foreach (var role in AllRoles.Where(x => x.RoleType == RoleEnum.SerialKiller))
                {
                    ((SerialKiller)role).PreyArrow.Destroy();
                }

                RoleDictionary.Clear();
                RoleHistory.Clear();
                Modifier.ModifierDictionary.Clear();
                Ability.AbilityDictionary.Clear();
            }
        }

        [HarmonyPatch(typeof(TranslationController), nameof(TranslationController.GetString), typeof(StringNames),
            typeof(Il2CppReferenceArray<Il2CppSystem.Object>))]
        public static class TranslationController_GetString
        {
            public static void Postfix(ref string __result, [HarmonyArgument(0)] StringNames name)
            {
                if (ExileController.Instance == null) return;
                switch (name)
                {
                    case StringNames.NoExileTie:
                        if (ExileController.Instance.initData.networkedPlayer == null)
                        {
                            foreach (var oracle in GetRoles(RoleEnum.Oracle))
                            {
                                var oracleRole = (Oracle)oracle;
                                if (oracleRole.SavedBlessed)
                                {
                                    oracleRole.SavedBlessed = false;
                                    __result = $"{oracleRole.BlessedPlayer.GetDefaultOutfit().PlayerName} was blessed by an Oracle!";
                                }
                            }
                        }
                        return;
                    case StringNames.ExileTextPN:
                    case StringNames.ExileTextSN:
                    case StringNames.ExileTextPP:
                    case StringNames.ExileTextSP:
                        {
                            if (ExileController.Instance.initData.networkedPlayer == null) return;
                            var info = ExileController.Instance.initData.networkedPlayer;
                            var role = GetRole(info.Object);
                            if (role == null) return;
                            var roleName = role.RoleType == RoleEnum.Glitch ? role.Name : $"The {role.Name}";
                            __result = $"{info.PlayerName} was {roleName}.";
                            return;
                        }
                }
            }
        }

        [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
        public static class HudManager_Update
        {
            private static void UpdateMeeting(MeetingHud __instance)
            {
                foreach (var player in __instance.playerStates)
                {
                    player.ColorBlindName.transform.localPosition = new Vector3(-0.93f, -0.2f, -0.1f);

                    var role = GetRole(player);
                    if (role != null)
                    {
                        bool selfFlag = role.SelfCriteria();
                        bool deadFlag = role.DeadCriteria();
                        bool impostorFlag = role.ImpostorCriteria();
                        bool apocFlag = role.ApocalypseCriteria();
                        bool vampireFlag = role.VampireCriteria();
                        bool loverFlag = role.LoverCriteria();
                        bool recruitFlag = role.RecruitCriteria();
                        bool roleFlag = role.RoleCriteria();
                        bool gaFlag = role.GuardianAngelCriteria();
                        player.NameText.text = role.NameText(
                            selfFlag || deadFlag || role.Local,
                            selfFlag || deadFlag || apocFlag || impostorFlag || vampireFlag || roleFlag || gaFlag,
                            selfFlag || deadFlag,
                            loverFlag,
                            recruitFlag,
                            player
                        );
                        if(role.ColorCriteria())
                            player.NameText.color = role.Color;
                    }
                    else
                    {
                        try
                        {
                            player.NameText.text = role.Player.GetDefaultOutfit().PlayerName;
                        }
                        catch
                        {
                        }
                    }
                }
            }

            [HarmonyPriority(Priority.First)]
            private static void Postfix(HudManager __instance)
            {
                if (MeetingHud.Instance != null) UpdateMeeting(MeetingHud.Instance);

                if (PlayerControl.AllPlayerControls.Count <= 1) return;
                if (PlayerControl.LocalPlayer == null) return;
                if (PlayerControl.LocalPlayer.Data == null) return;

                foreach (var player in PlayerControl.AllPlayerControls)
                {
                    if (!(player.Data != null && player.Data.IsImpostor() && PlayerControl.LocalPlayer.Data.IsImpostor()))
                    {
                        player.nameText().text = player.name;
                        player.nameText().color = Color.white;
                    }

                    var role = GetRole(player);
                    if (role != null)
                    {
                        if (role.Criteria())
                        {
                            bool selfFlag = role.SelfCriteria();
                            bool deadFlag = role.DeadCriteria();
                            bool impostorFlag = role.ImpostorCriteria();
                            bool apocFlag = role.ApocalypseCriteria();
                            bool vampireFlag = role.VampireCriteria();
                            bool loverFlag = role.LoverCriteria();
                            bool recruitFlag = role.RecruitCriteria();
                            bool roleFlag = role.RoleCriteria();
                            bool gaFlag = role.GuardianAngelCriteria();
                            player.nameText().text = role.NameText(
                                selfFlag || deadFlag || role.Local,
                                selfFlag || deadFlag || impostorFlag || apocFlag || vampireFlag || roleFlag || gaFlag,
                                selfFlag || deadFlag,
                                loverFlag,
                                recruitFlag
                             );

                            if (role.ColorCriteria())
                                player.nameText().color = role.Color;
                        }
                        else player.nameText().transform.localPosition = new Vector3(0f, 0f, 0f);
                    }
                }
            }
        }
        public static AudioClip GetIntroSound(RoleTypes roleType)
        {
            return RoleManager.Instance.AllRoles.Where((role) => role.Role == roleType).FirstOrDefault().IntroSound;
        }
    }
}