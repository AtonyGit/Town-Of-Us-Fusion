using HarmonyLib;
using TownOfUsFusion.Roles;
using UnityEngine.UI;

namespace TownOfUsFusion.NeutralRoles.HuskMod
{
    [HarmonyPatch(typeof(MeetingHud), nameof(MeetingHud.Confirm))]
public class ShowHideButtonsHusk
{
    public static void HideButtonsHusk(Husk role)
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
            role.GuessedThisMeeting = true;
        }
    }

    public static void HideSingle(
        Husk role,
        byte targetId,
        bool killedSelf
    )
    {
        if (
            killedSelf ||
            role.RemainingKills == 0 ||
            (!CustomGameOptions.HuskAssassinMultiKill)
        ) HideButtonsHusk(role);
        else HideTarget(role, targetId);
    }
    public static void HideTarget(
        Husk role,
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
        if (!PlayerControl.LocalPlayer.Is(RoleEnum.Husk)) return;
        var retributionist = Role.GetRole<Husk>(PlayerControl.LocalPlayer);
        if (!CustomGameOptions.AssassinateAfterVoting) HideButtonsHusk(retributionist);
    }
}
}
