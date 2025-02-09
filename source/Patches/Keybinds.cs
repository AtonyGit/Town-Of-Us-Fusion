using HarmonyLib;
using Rewired;
using Rewired.Data;
using System.Linq;
using TownOfUsFusion.Roles;
using TownOfUsFusion.Roles.Modifiers;
using Ability = TownOfUsFusion.Roles.Modifiers.Ability;

namespace TownOfUsFusion
{
    //thanks to TheOtherRolesAU/TheOtherRoles/pull/347 by dadoum for the patch and extension
    [HarmonyPatch(typeof(InputManager_Base), nameof(InputManager_Base.Awake))]
    public static class Keybinds
    {
        [HarmonyPrefix]
        private static void Prefix(InputManager_Base __instance)
        {
            //change the text shown on the screen for the kill keybind
            __instance.userData.GetAction("ActionSecondary").descriptiveName = "Kill / Secondary Abilities";
            __instance.userData.GetAction("ActionQuaternary").descriptiveName = "Primary Ability";
            __instance.userData.RegisterBind("TOU bb/disperse/mimic", "Button Modifier / Mimic Ability");
            __instance.userData.RegisterBind("TOU Hack", "Glitch's Hack Ability");
            __instance.userData.RegisterBind("TOU Cycle +", "Cycle Forwards (Guesser)");
            __instance.userData.RegisterBind("TOU Cycle -", "Cycle Backwards (Guesser)");
            __instance.userData.RegisterBind("TOU Cycle players", "Cycle Selected Player (Guesser)");
            __instance.userData.RegisterBind("TOU Confirm", "Confirm (Guesser)");
        }

        private static int RegisterBind(this UserData self, string name, string description, int elementIdentifierId = -1, int category = 0, InputActionType type = InputActionType.Button)
        {
            self.AddAction(category);
            var action = self.GetAction(self.actions.Count - 1)!;

            action.name = name;
            action.descriptiveName = description;
            action.categoryId = category;
            action.type = type;
            action.userAssignable = true;

            var map = new ActionElementMap
            {
                _elementIdentifierId = elementIdentifierId,
                _actionId = action.id,
                _elementType = ControllerElementType.Button,
                _axisContribution = Pole.Positive,
                _modifierKey1 = ModifierKey.None,
                _modifierKey2 = ModifierKey.None,
                _modifierKey3 = ModifierKey.None
            };
            self.keyboardMaps[0].actionElementMaps.Add(map);
            self.joystickMaps[0].actionElementMaps.Add(map);

            return action.id;
        }
    }

    [HarmonyPatch]
    public sealed class AssassinVigilanteKeybinds
    {
        private static PlayerVoteArea HighlightedPlayer;

        private static int PlayerIndex;

        [HarmonyPatch(typeof(MeetingHud), nameof(MeetingHud.Update))]
        [HarmonyPostfix]
        public static void Postfix(MeetingHud __instance)
        {
            if (PlayerControl.LocalPlayer.Data.IsDead) return;
            if (PlayerControl.LocalPlayer.IsJailed()) return;
            if (__instance.state == MeetingHud.VoteStates.Discussion) return;
            var role = Role.GetRole(PlayerControl.LocalPlayer);

            foreach (var player in __instance.playerStates)
            {
                if (!HighlightedPlayer) break;
                if (player.TargetPlayerId == HighlightedPlayer.TargetPlayerId)
                {
                    player.SetHighlighted(true);
                }
                else player.SetHighlighted(false);
            }

            if (role is Vigilante || role.Player.Is(AbilityEnum.Assassin) || role.Player.Is(RoleEnum.Doomsayer))
            {
                dynamic guesser = role is Vigilante ? Role.GetRole<Vigilante>(role.Player) : Ability.GetAbility<Assassin>(role.Player);
                if (guesser == null) guesser = Role.GetRole<Doomsayer>(role.Player);
                if (role is Vigilante)
                {
                    if (Role.GetRole<Vigilante>(role.Player).RemainingKills == 0) return;
                }
                else if (role.Player.Is(AbilityEnum.Assassin))
                {
                    if (Ability.GetAbility<Assassin>(role.Player).RemainingKills == 0) return;
                }
                var players = __instance.playerStates.Where(x => (guesser as IGuesser).Buttons[x.TargetPlayerId] != (null, null, null, null)
                                                                  && x.TargetPlayerId != role.Player.PlayerId).ToList();

                if (ReInput.players.GetPlayer(0).GetButtonDown("TOU Cycle Players"))
                {
                    HighlightedPlayer = players[PlayerIndex];
                    PlayerIndex = PlayerIndex == players.Count - 1 ? 0 : PlayerIndex + 1;
                }

                if (!HighlightedPlayer) return;
                if (ReInput.players.GetPlayer(0).GetButtonDown("TOU Cycle +"))
                {
                    (guesser as IGuesser).Buttons[HighlightedPlayer.TargetPlayerId].Item2.GetComponent<PassiveButton>().OnClick.Invoke();
                }
                else if (ReInput.players.GetPlayer(0).GetButtonDown("TOU Cycle -"))
                {
                    (guesser as IGuesser).Buttons[HighlightedPlayer.TargetPlayerId].Item1.GetComponent<PassiveButton>().OnClick.Invoke();
                }
                else if (ReInput.players.GetPlayer(0).GetButtonDown("TOU Confirm"))
                {
                    (guesser as IGuesser).Buttons[HighlightedPlayer.TargetPlayerId].Item3.GetComponent<PassiveButton>().OnClick.Invoke();
                }
            }
        }

        [HarmonyPatch(typeof(MeetingHud), nameof(MeetingHud.VotingComplete))]
        [HarmonyPatch(typeof(LobbyBehaviour), nameof(LobbyBehaviour.Start))]
        [HarmonyPostfix]

        public static void Reset()
        {
            HighlightedPlayer = null;
            PlayerIndex = 0;
        }
    }
}