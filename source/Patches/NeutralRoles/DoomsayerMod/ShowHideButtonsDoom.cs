using HarmonyLib;
using TownOfUsFusion.Roles;
using UnityEngine.UI;

namespace TownOfUsFusion.NeutralRoles.DoomsayerMod
{
    [HarmonyPatch(typeof(MeetingHud), nameof(MeetingHud.Confirm))]
    public class ShowHideButtonsDoom
    {
        public static void HideButtonsDoom(Doomsayer role)
        {
            foreach (var (_, (cycleBack, cycleForward, guess, guessText)) in role.Buttons)
            {
                if (cycleBack == null || cycleForward == null) continue;
                cycleBack.SetActive(false);
                cycleForward.SetActive(false);
                guess.SetActive(false);
                guessText.gameObject.SetActive(false);

                cycleBack.GetComponent<PassiveButton>().OnClick = new Button.ButtonClickedEvent();
                cycleForward.GetComponent<PassiveButton>().OnClick = new Button.ButtonClickedEvent();
                guess.GetComponent<PassiveButton>().OnClick = new Button.ButtonClickedEvent();
            }
        }
<<<<<<< Updated upstream
=======
        public static void HideTextDoom(Doomsayer role)
        {
            foreach (var (_, guessText) in role.RoleGuess)
            {
                if (!guessText.isActiveAndEnabled) continue;
                guessText.gameObject.SetActive(false);
            }
        }
>>>>>>> Stashed changes

        public static void HideSingle(
            Doomsayer role,
            byte targetId,
            bool killedSelf
        )
        {
            if (killedSelf) HideButtonsDoom(role);
            else HideTarget(role, targetId);
        }
        public static void HideTarget(
            Doomsayer role,
            byte targetId
        )
        {

            var (cycleBack, cycleForward, guess, guessText) = role.Buttons[targetId];
            if (cycleBack == null || cycleForward == null) return;
            cycleBack.SetActive(false);
            cycleForward.SetActive(false);
            guess.SetActive(false);
            guessText.gameObject.SetActive(false);

            cycleBack.GetComponent<PassiveButton>().OnClick = new Button.ButtonClickedEvent();
            cycleForward.GetComponent<PassiveButton>().OnClick = new Button.ButtonClickedEvent();
            guess.GetComponent<PassiveButton>().OnClick = new Button.ButtonClickedEvent();
            role.Buttons[targetId] = (null, null, null, null);
            role.Guesses.Remove(targetId);
        }


        public static void Prefix(MeetingHud __instance)
        {
            if (!PlayerControl.LocalPlayer.Is(RoleEnum.Doomsayer)) return;
            var doomsayer = Role.GetRole<Doomsayer>(PlayerControl.LocalPlayer);
<<<<<<< Updated upstream
            if (!CustomGameOptions.DoomsayerAfterVoting) HideButtonsDoom(doomsayer);
=======
            if (!CustomGameOptions.DoomsayerAfterVoting)
            {
                HideButtonsDoom(doomsayer);
                HideTextDoom(doomsayer);
            }
>>>>>>> Stashed changes
        }
    }
}
