using TownOfUsFusion.Buttons.Arsonist;
using TownOfUsFusion.Buttons.Werewolf;

namespace TownOfUsFusion
{
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
    public class KillButtonSprite
    {
        private static Sprite Kill;

        public static void Postfix(HudManager __instance)
        {
            var role = PlayerControl.LocalPlayer.Data.Role as ICustomRole;

            if (!Kill) Kill = __instance.KillButton.graphic.sprite;
            var button = __instance.KillButton;
            var vent = __instance.ImpostorVentButton;
            if (role != null)
            {
                button.buttonLabelText.SetOutlineColor(role.RoleColor);
                vent.buttonLabelText.SetOutlineColor(role.RoleColor);
            }
            switch(role) {
                case WerewolfRole:
                    button.graphic.sprite = ToufAssets.WerewolfKill;
                    vent.graphic.sprite = ToufAssets.WerewolfVent;
                    break;
                case null:
                    break;
            }
            bool KillKey = Rewired.ReInput.players.GetPlayer(0).GetButtonDown("ActionSecondary");
            var controller = ConsoleJoystick.player.GetButtonDown(8);
            if ((KillKey || controller) && button != null && !PlayerControl.LocalPlayer.Data.IsDead)
                button.DoClick();

            bool VentKey = Rewired.ReInput.players.GetPlayer(0).GetButtonDown("UseVent");
            if (VentKey && vent != null && !PlayerControl.LocalPlayer.Data.IsDead)
                vent.DoClick();

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
