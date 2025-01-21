using System.Linq;
using HarmonyLib;
using TownOfUsFusion.Extensions;
using TownOfUsFusion.Roles;
using Il2CppSystem.Collections.Generic;
using UnityEngine;
using System.Collections;
using System;

namespace TownOfUsFusion.NeutralRoles.SentinelMod
{
    public class AbilityCoroutine
    {
        private static Dictionary<byte, DateTime> tickStunDictionary = new();
        private static Sprite LockeSprite = TownOfUsFusion.LockSprite;

        public static IEnumerator Stun(Sentinel __instance, PlayerControl stunPlayer)
        {
            GameObject[] lockImg = { null, null, null, null };
            ImportantTextTask stunText;

            if (tickStunDictionary.ContainsKey(stunPlayer.PlayerId))
            {
                tickStunDictionary[stunPlayer.PlayerId] = DateTime.UtcNow;
                yield break;
            }

            stunText = new GameObject("_Player").AddComponent<ImportantTextTask>();
            stunText.transform.SetParent(PlayerControl.LocalPlayer.transform, false);
            stunText.Text =
                $"{__instance.ColorString}Stunned {stunPlayer.Data.PlayerName} ({CustomGameOptions.StunDuration}s)</color>";
            stunText.Index = stunPlayer.PlayerId;
            tickStunDictionary.Add(stunPlayer.PlayerId, DateTime.UtcNow);
            PlayerControl.LocalPlayer.myTasks.Insert(0, stunText);

            while (true)
            {
                if (PlayerControl.LocalPlayer == stunPlayer)
                {
                    if (HudManager.Instance.KillButton != null)
                    {
                        if (lockImg[0] == null)
                        {
                            lockImg[0] = new GameObject();
                            var lockImgR = lockImg[0].AddComponent<SpriteRenderer>();
                            lockImgR.sprite = LockeSprite;
                        }

                        lockImg[0].layer = 5;
                        lockImg[0].transform.position =
                            new Vector3(HudManager.Instance.KillButton.transform.position.x,
                                HudManager.Instance.KillButton.transform.position.y, -50f);
                        HudManager.Instance.KillButton.enabled = false;
                        HudManager.Instance.KillButton.graphic.color = Palette.DisabledClear;
                        HudManager.Instance.KillButton.graphic.material.SetFloat("_Desat", 1f);
                    }

                    if (HudManager.Instance.UseButton != null || HudManager.Instance.PetButton != null)
                    {
                        if (lockImg[1] == null)
                        {
                            lockImg[1] = new GameObject();
                            var lockImgR = lockImg[1].AddComponent<SpriteRenderer>();
                            lockImgR.sprite = LockeSprite;
                        }
                        if (HudManager.Instance.UseButton != null)
                        {
                            lockImg[1].transform.position =
                            new Vector3(HudManager.Instance.UseButton.transform.position.x,
                                HudManager.Instance.UseButton.transform.position.y, -50f);
                            lockImg[1].layer = 5;
                            HudManager.Instance.UseButton.enabled = false;
                            HudManager.Instance.UseButton.graphic.color = Palette.DisabledClear;
                            HudManager.Instance.UseButton.graphic.material.SetFloat("_Desat", 1f);
                        }
                        else
                        {
                            lockImg[1].transform.position =
                                new Vector3(HudManager.Instance.PetButton.transform.position.x,
                                HudManager.Instance.PetButton.transform.position.y, -50f);
                            lockImg[1].layer = 5;
                            HudManager.Instance.PetButton.enabled = false;
                            HudManager.Instance.PetButton.graphic.color = Palette.DisabledClear;
                            HudManager.Instance.PetButton.graphic.material.SetFloat("_Desat", 1f);
                        }
                    }

                    if (HudManager.Instance.ReportButton != null)
                    {
                        if (lockImg[2] == null)
                        {
                            lockImg[2] = new GameObject();
                            var lockImgR = lockImg[2].AddComponent<SpriteRenderer>();
                            lockImgR.sprite = LockeSprite;
                        }

                        lockImg[2].transform.position =
                            new Vector3(HudManager.Instance.ReportButton.transform.position.x,
                                HudManager.Instance.ReportButton.transform.position.y, -50f);
                        lockImg[2].layer = 5;
                        HudManager.Instance.ReportButton.enabled = false;
                        HudManager.Instance.ReportButton.SetActive(false);
                    }

                    var role = Role.GetRole(PlayerControl.LocalPlayer);
                    if (role?.ExtraButtons.Count > 0)
                    {
                        if (lockImg[3] == null)
                        {
                            lockImg[3] = new GameObject();
                            var lockImgR = lockImg[3].AddComponent<SpriteRenderer>();
                            lockImgR.sprite = LockeSprite;
                        }

                        lockImg[3].transform.position = new Vector3(
                            role.ExtraButtons[0].transform.position.x,
                            role.ExtraButtons[0].transform.position.y, -50f);
                        lockImg[3].layer = 5;
                        role.ExtraButtons[0].enabled = false;
                        role.ExtraButtons[0].graphic.color = Palette.DisabledClear;
                        role.ExtraButtons[0].graphic.material.SetFloat("_Desat", 1f);
                    }

                    if (Minigame.Instance)
                    {
                        Minigame.Instance.Close();
                        Minigame.Instance.Close();
                    }

                    if (MapBehaviour.Instance)
                    {
                        MapBehaviour.Instance.Close();
                        MapBehaviour.Instance.Close();
                    }
                }

                var totalStuntime = (DateTime.UtcNow - tickStunDictionary[stunPlayer.PlayerId]).TotalMilliseconds /
                                    1000;
                stunText.Text =
                    $"{__instance.ColorString}Stunned {stunPlayer.Data.PlayerName} ({CustomGameOptions.StunDuration - Math.Round(totalStuntime)}s)</color>";
                if (MeetingHud.Instance || totalStuntime > CustomGameOptions.StunDuration || stunPlayer?.Data.IsDead != false)
                {
                    foreach (var obj in lockImg)
                    {
                        obj?.SetActive(false);
                    }

                    if (PlayerControl.LocalPlayer == stunPlayer)
                    {
                        if (HudManager.Instance.UseButton != null)
                        {
                            HudManager.Instance.UseButton.enabled = true;
                            HudManager.Instance.UseButton.graphic.color = Palette.EnabledColor;
                            HudManager.Instance.UseButton.graphic.material.SetFloat("_Desat", 0f);
                        }
                        else
                        {
                            HudManager.Instance.PetButton.enabled = true;
                            HudManager.Instance.PetButton.graphic.color = Palette.EnabledColor;
                            HudManager.Instance.PetButton.graphic.material.SetFloat("_Desat", 0f);
                        }
                        HudManager.Instance.ReportButton.enabled = true;
                        HudManager.Instance.KillButton.enabled = true;
                        var role = Role.GetRole(PlayerControl.LocalPlayer);
                        if (role?.ExtraButtons.Count > 0)
                        {
                            role.ExtraButtons[0].enabled = true;
                            role.ExtraButtons[0].graphic.color = Palette.EnabledColor;
                            role.ExtraButtons[0].graphic.material.SetFloat("_Desat", 0f);
                        }
                    }

                    tickStunDictionary.Remove(stunPlayer.PlayerId);
                    PlayerControl.LocalPlayer.myTasks.Remove(stunText);
                    yield break;
                }

                yield return null;
            }
        }
    }
}