using TownOfUsFusion.Buttons.Werewolf;

namespace TownOfUsFusion
{
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
    public class KillButtonSprite
    {
        private static Sprite Kill;

        public static void Postfix(HudManager __instance)
        {
            var role = PlayerControl.LocalPlayer.Data.Role;
            if (__instance.KillButton == null) return;

            if (!Kill) Kill = __instance.KillButton.graphic.sprite;
            var button = __instance.KillButton;
            CustomActionButton button2 = null;
            var vent = __instance.ImpostorVentButton;

            var flag = false;
            var buttonKills = false;
            var otherButtonKills = false;
            switch(role) {
                case WerewolfRole:
                    buttonKills = true;
                    flag = true;
                    button.graphic.sprite = ToufAssets.WerewolfKill;
                    vent.graphic.sprite = ToufAssets.WerewolfVent;
                    button2 = new RampageButton();
                    break;
                case null:
                    break;
            }

            bool KillKey = Rewired.ReInput.players.GetPlayer(0).GetButtonDown("ActionSecondary");
            if (!buttonKills) KillKey = Rewired.ReInput.players.GetPlayer(0).GetButtonDown("ActionQuaternary");
            var controller = ConsoleJoystick.player.GetButtonDown(8);
            if ((KillKey || controller) && button != null && flag && !PlayerControl.LocalPlayer.Data.IsDead)
                button.DoClick();
            bool AbilityKey = Rewired.ReInput.players.GetPlayer(0).GetButtonDown("ActionSecondary");
            if (!otherButtonKills) AbilityKey = Rewired.ReInput.players.GetPlayer(0).GetButtonDown("ActionQuaternary");
            if (button2 != null && AbilityKey && !PlayerControl.LocalPlayer.Data.IsDead)
                button2?.ClickHandler();

/*
            if (Modifier.GetModifier<ButtonBarry>(PlayerControl.LocalPlayer)?.ButtonUsed == false &&
                Rewired.ReInput.players.GetPlayer(0).GetButtonDown("TOU bb/disperse/mimic") &&
                !PlayerControl.LocalPlayer.Data.IsDead)
            {
                Modifier.GetModifier<ButtonBarry>(PlayerControl.LocalPlayer).ButtonButton.DoClick();
            }
            else if (Modifier.GetModifier<Disperser>(PlayerControl.LocalPlayer)?.ButtonUsed == false &&
                     Rewired.ReInput.players.GetPlayer(0).GetButtonDown("TOU bb/disperse/mimic") &&
                     !PlayerControl.LocalPlayer.Data.IsDead)
            {
                Modifier.GetModifier<Disperser>(PlayerControl.LocalPlayer).DisperseButton.DoClick();
            }*/
        }
/*
        [HarmonyPatch(typeof(AbilityButton), nameof(AbilityButton.Update))]
        class AbilityButtonUpdatePatch
        {
            static void Postfix()
            {
                if (AmongUsClient.Instance.GameState != InnerNet.InnerNetClient.GameStates.Started)
                {
                    HudManager.Instance.AbilityButton.gameObject.SetActive(false);
                    return;
                }
                else if (GameOptionsManager.Instance.CurrentGameOptions.GameMode == GameModes.HideNSeek)
                {
                    HudManager.Instance.AbilityButton.gameObject.SetActive(!PlayerControl.LocalPlayer.Data.IsImpostor());
                    return;
                }
                var ghostRole = false;
                if (PlayerControl.LocalPlayer.Is(RoleEnum.Haunter))
                {
                    var haunter = Role.GetRole<Haunter>(PlayerControl.LocalPlayer);
                    if (!haunter.Caught) ghostRole = true;
                }
                else if (PlayerControl.LocalPlayer.Is(RoleEnum.Phantom))
                {
                    var phantom = Role.GetRole<Phantom>(PlayerControl.LocalPlayer);
                    if (!phantom.Caught) ghostRole = true;
                }
                HudManager.Instance.AbilityButton.gameObject.SetActive(!ghostRole && Utils.ShowDeadBodies && !MeetingHud.Instance);
            }
        }*/
    }
}
