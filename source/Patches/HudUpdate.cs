using HarmonyLib;
using Hazel;
using Il2CppSystem.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using UnityEngine;
using System;
using System.IO;
using System.Text;
using Reactor.Utilities;
using Object = UnityEngine.Object;
using UColor = UnityEngine.Color;
using UnityEngine.SceneManagement;
using TMPro;
using AmongUs.GameOptions;
using TownOfUsFusion.Roles;
using Cpp2IL.Core.Extensions;

namespace TownOfUsFusion.Patches
{

    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
public static class HudUpdate
{
    private static GameObject ZoomButton;
    public static bool Zooming;
    public static bool OpenedPhone;
    public static string CurTab = "Crew";
    private static Vector3 Pos;
    
    private static SpriteRenderer Phone;
    private static SpriteRenderer GuideOverlay;

    private static GameObject GreyedOptionCrew;
    private static GameObject GreyedOptionNeut;
    private static GameObject GreyedOptionImp;
    private static GameObject GreyedOptionModify;
    private static SpriteRenderer CrewTab;
    private static SpriteRenderer NeutTab;
    private static SpriteRenderer ImpTab;
    private static SpriteRenderer ModifyTab;

    private static GameObject GuideCrewB;
    private static GameObject GuideNeutB;
    private static GameObject GuideImpB;
    private static GameObject GuideModifyB;

    private static TextMeshPro PhoneText;

    private static GameObject GuideButton;
    private static bool GuideActive;
    private static bool RoleCardActive;
    private static Vector3 Pos2;
    private static PassiveButton KILLYOURSELF;
    private static PassiveButton ToTheGuide;
    private static PassiveButton NextButton;
    private static PassiveButton BackButton;
    private static PassiveButton YourStatus;
    public static readonly Dictionary<int, List<PassiveButton>> Buttons = new();
//    private static readonly Dictionary<int, KeyValuePair<string, Info>> Sorted = new();
    public static int Page;
    private static int ResultPage;
    private static int MaxPage;
    private static bool PagesSet;
//    private static Info Selected;
    private static bool LoreActive;
    private static bool SelectionActive;
//    private static readonly List<string> Entry = new();
    
    private static Vector3 MapPos;

    public static void Postfix(HudManager __instance)
    {
        if (!ZoomButton)
        {
            ZoomButton = Object.Instantiate(__instance.MapButton.gameObject, __instance.MapButton.transform.parent);
            ZoomButton.GetComponent<PassiveButton>().OnClick = new();
            ZoomButton.GetComponent<PassiveButton>().OnClick.AddListener(new Action(Zoom));
        }

        Pos = __instance.MapButton.transform.localPosition + new Vector3(0.02f, -0.66f, 0f);
        var dead = false;
        if (Utils.ShowDeadBodies)
        {
            if (PlayerControl.LocalPlayer.Is(RoleEnum.Haunter))
            {
                var haunter = Role.GetRole<Haunter>(PlayerControl.LocalPlayer);
                if (haunter.Caught) dead = true;
            }
            else if (PlayerControl.LocalPlayer.Is(RoleEnum.Phantom))
            {
                var phantom = Role.GetRole<Phantom>(PlayerControl.LocalPlayer);
                if (phantom.Caught) dead = true;
            }
            else dead = true;
        }

            MapPos = __instance.SettingsButton.transform.localPosition + new Vector3(0, -0.66f, -__instance.SettingsButton.transform.localPosition.z - 51f);


            if (!GuideButton)
            {
            GuideButton = Object.Instantiate(__instance.MapButton.gameObject, __instance.MapButton.transform.parent);
            GuideButton.GetComponent<PassiveButton>().OnClick = new();
            GuideButton.GetComponent<PassiveButton>().OnClick.AddListener(new Action(Open));
            }
            GuideButton.SetActive(!IntroCutscene.Instance);
            GuideButton.transform.localPosition = MapPos;
        GuideButton.GetComponent<SpriteRenderer>().sprite = TownOfUsFusion.GuideButtonSprite;

            ResetButtonPos();

            Pos = MapPos + new Vector3(0, -0.66f, 0f);
            __instance.MapButton.transform.localPosition = Pos;

            if (Patches.SubmergedCompatibility.isSubmerged())
            {
                var floorButton = __instance.MapButton.transform.parent.Find(__instance.MapButton.name + "(Clone)");

                if (floorButton && floorButton.gameObject.active)
                {
                    Pos += new Vector3(0, -0.66f, 0f);
                    floorButton.localPosition = Pos;
                }
            }
            if (PhoneText)
            {/*
                if (RoleCardActive)
                    PhoneText.text = CustomPlayer.Local.RoleCardInfo();
                else if ((SelectionActive) && Entry.Count > 1)
                {
                    if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.mouseScrollDelta.y > 0f)
                        ResultPage = Utils.CycleInt(Entry.Count - 1, 0, ResultPage, false);
                    else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.mouseScrollDelta.y < 0f)
                        ResultPage = Utils.CycleInt(Entry.Count - 1, 0, ResultPage, true);

                    PhoneText.text = Entry[ResultPage];
                }*/
            }

        ZoomButton.SetActive(!MeetingHud.Instance && dead && AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started
            && GameOptionsManager.Instance.CurrentGameOptions.GameMode == GameModes.Normal);
        ZoomButton.transform.localPosition = Pos;
        ZoomButton.GetComponent<SpriteRenderer>().sprite = Zooming ? TownOfUsFusion.ZoomPlusButton : TownOfUsFusion.ZoomMinusButton;

            if (!GreyedOptionCrew)
            {
            GreyedOptionCrew = Object.Instantiate(__instance.MapButton.gameObject, __instance.MapButton.transform.parent);
            GreyedOptionCrew.GetComponent<PassiveButton>().OnClick = new();
            GreyedOptionCrew.GetComponent<PassiveButton>().OnClick.AddListener(new Action(OpenCrewTab));
            }
            GreyedOptionCrew.transform.localPosition = new Vector3(-3.563f, 1.06f, -11f);
        GreyedOptionCrew.GetComponent<SpriteRenderer>().sprite = TownOfUsFusion.GreyedOption;

            if (!GreyedOptionNeut)
            {
            GreyedOptionNeut = Object.Instantiate(__instance.MapButton.gameObject, __instance.MapButton.transform.parent);
            GreyedOptionNeut.GetComponent<PassiveButton>().OnClick = new();
            GreyedOptionNeut.GetComponent<PassiveButton>().OnClick.AddListener(new Action(OpenNeutTab));
            }
            GreyedOptionNeut.transform.localPosition = new Vector3(-3.563f, 0.12f, -11f);
        GreyedOptionNeut.GetComponent<SpriteRenderer>().sprite = TownOfUsFusion.GreyedOption;

            if (!GreyedOptionImp)
            {
            GreyedOptionImp = Object.Instantiate(__instance.MapButton.gameObject, __instance.MapButton.transform.parent);
            GreyedOptionImp.GetComponent<PassiveButton>().OnClick = new();
            GreyedOptionImp.GetComponent<PassiveButton>().OnClick.AddListener(new Action(OpenImpTab));
            }
            GreyedOptionImp.transform.localPosition = new Vector3(-3.563f, -0.79f, -11f);
        GreyedOptionImp.GetComponent<SpriteRenderer>().sprite = TownOfUsFusion.GreyedOption;

            if (!GreyedOptionModify)
            {
            GreyedOptionModify = Object.Instantiate(__instance.MapButton.gameObject, __instance.MapButton.transform.parent);
            GreyedOptionModify.GetComponent<PassiveButton>().OnClick = new();
            GreyedOptionModify.GetComponent<PassiveButton>().OnClick.AddListener(new Action(OpenModifyTab));
            }
            GreyedOptionModify.transform.localPosition = new Vector3(-3.563f, -1.71f, -11f);
        GreyedOptionModify.GetComponent<SpriteRenderer>().sprite = TownOfUsFusion.GreyedOption;

            GreyedOptionCrew.SetActive(!IntroCutscene.Instance && CurTab != "Crew" && OpenedPhone);
            GreyedOptionNeut.SetActive(!IntroCutscene.Instance && CurTab != "Neut" && OpenedPhone);
            GreyedOptionImp.SetActive(!IntroCutscene.Instance && CurTab != "Imp" && OpenedPhone);
            GreyedOptionModify.SetActive(!IntroCutscene.Instance && CurTab != "Modify" && OpenedPhone);
            
        if (!GuideCrewB)
        {
            GuideCrewB = Object.Instantiate(__instance.MapButton.gameObject, __instance.MapButton.transform.parent);
            GuideCrewB.GetComponent<PassiveButton>().OnClick = new();
            GuideCrewB.GetComponent<PassiveButton>().OnClick.AddListener((Action)(() => Application.OpenURL("https://github.com/AtonyGit/Town-Of-Us-Fusion?tab=readme-ov-file#crewmate-roles")));
        }
            GuideCrewB.transform.localPosition = new(0f, 0f, -20f);
            GuideCrewB.GetComponent<SpriteRenderer>().sprite = TownOfUsFusion.GuidePlaceholderSprite;
        if (!GuideNeutB)
        {
            GuideNeutB = Object.Instantiate(__instance.MapButton.gameObject, __instance.MapButton.transform.parent);
            GuideNeutB.GetComponent<PassiveButton>().OnClick = new();
            GuideNeutB.GetComponent<PassiveButton>().OnClick.AddListener((Action)(() => Application.OpenURL("https://github.com/AtonyGit/Town-Of-Us-Fusion?tab=readme-ov-file#neutral-roles")));
        }
            GuideNeutB.transform.localPosition = new(0f, 0f, -20f);
            GuideNeutB.GetComponent<SpriteRenderer>().sprite = TownOfUsFusion.GuidePlaceholderSprite;
        if (!GuideImpB)
        {
            GuideImpB = Object.Instantiate(__instance.MapButton.gameObject, __instance.MapButton.transform.parent);
            GuideImpB.GetComponent<PassiveButton>().OnClick = new();
            GuideImpB.GetComponent<PassiveButton>().OnClick.AddListener((Action)(() => Application.OpenURL("https://github.com/AtonyGit/Town-Of-Us-Fusion?tab=readme-ov-file#impostor-roles")));
        }
            GuideImpB.transform.localPosition = new(0f, 0f, -20f);
            GuideImpB.GetComponent<SpriteRenderer>().sprite = TownOfUsFusion.GuidePlaceholderSprite;
        if (!GuideModifyB)
        {
            GuideModifyB = Object.Instantiate(__instance.MapButton.gameObject, __instance.MapButton.transform.parent);
            GuideModifyB.GetComponent<PassiveButton>().OnClick = new();
            GuideModifyB.GetComponent<PassiveButton>().OnClick.AddListener((Action)(() => Application.OpenURL("https://github.com/AtonyGit/Town-Of-Us-Fusion?tab=readme-ov-file#modifiers")));
        }
            GuideModifyB.transform.localPosition = new(0f, 0f, -20f);
            GuideModifyB.GetComponent<SpriteRenderer>().sprite = TownOfUsFusion.GuidePlaceholderSprite;

        GuideCrewB.gameObject.SetActive(!IntroCutscene.Instance && CurTab == "Crew" && OpenedPhone);
        GuideNeutB.gameObject.SetActive(!IntroCutscene.Instance && CurTab == "Neut" && OpenedPhone);
        GuideImpB.gameObject.SetActive(!IntroCutscene.Instance && CurTab == "Imp" && OpenedPhone);
        GuideModifyB.gameObject.SetActive(!IntroCutscene.Instance && CurTab == "Modify" && OpenedPhone);
    }
    
    public static void Zoom()
    {
        Zooming = !Zooming;
        var size = Zooming ? 12f : 3f;
        Camera.main.orthographicSize = size;

        foreach (var cam in Camera.allCameras)
        {
            if (cam?.gameObject.name == "UI Camera")
                cam.orthographicSize = size;
        }

        ResolutionManager.ResolutionChanged.Invoke((float)Screen.width / Screen.height, Screen.width, Screen.height, Screen.fullScreen);
    }

    public static void ZoomStart()
    {
        var size = Zooming ? 12f : 3f;
        Camera.main.orthographicSize = size;

        foreach (var cam in Camera.allCameras)
        {
            if (cam?.gameObject.name == "UI Camera")
                cam.orthographicSize = size;
        }

        ResolutionManager.ResolutionChanged.Invoke((float)Screen.width / Screen.height, Screen.width, Screen.height, Screen.fullScreen);
    }
    public static void Open()
    {
        PluginSingleton<TownOfUsFusion>.Instance.Log.LogMessage("Guide was opened via button!");
        OpenedPhone = !OpenedPhone;
        if (!Phone)
        {
            Phone = new GameObject("Phone") { layer = 5 }.AddComponent<SpriteRenderer>();
            Phone.GetComponent<SpriteRenderer>().sprite = TownOfUsFusion.Phone;
            Phone.transform.SetParent(HudManager.Instance.transform);
            Phone.transform.localPosition = new(0, 0, -10f);
        //    Phone.transform.localScale *= .75f;
        }
        if (!GuideOverlay)
        {
            GuideOverlay = new GameObject("GuideOverlay") { layer = 5 }.AddComponent<SpriteRenderer>();
            GuideOverlay.GetComponent<SpriteRenderer>().sprite = TownOfUsFusion.GuideOverlay;
            GuideOverlay.transform.SetParent(HudManager.Instance.transform);
            GuideOverlay.transform.localPosition = new(0, 0, -11f);
        //    GuideOverlay.transform.localScale = .75f;
        }
        Phone.gameObject.SetActive(OpenedPhone);
        GuideOverlay.gameObject.SetActive(OpenedPhone);
        if (!CrewTab)
        {
            CrewTab = new GameObject("CrewTab") { layer = 5 }.AddComponent<SpriteRenderer>();
            CrewTab.GetComponent<SpriteRenderer>().sprite = TownOfUsFusion.CrewGuideTab;
            CrewTab.transform.SetParent(HudManager.Instance.transform);
            CrewTab.transform.localPosition = new(-3.563f, 1.06f, -12f);
        }
        if (!NeutTab)
        {
            NeutTab = new GameObject("NeutTab") { layer = 5 }.AddComponent<SpriteRenderer>();
            NeutTab.GetComponent<SpriteRenderer>().sprite = TownOfUsFusion.NeutralGuideTab;
            NeutTab.transform.SetParent(HudManager.Instance.transform);
            NeutTab.transform.localPosition = new(-3.563f, 0.12f, -12f);
        }
        if (!ImpTab)
        {
            ImpTab = new GameObject("ImpTab") { layer = 5 }.AddComponent<SpriteRenderer>();
            ImpTab.GetComponent<SpriteRenderer>().sprite = TownOfUsFusion.ImpostorGuideTab;
            ImpTab.transform.SetParent(HudManager.Instance.transform);
            ImpTab.transform.localPosition = new(-3.563f, -0.79f, -12f);
        }
        if (!ModifyTab)
        {
            ModifyTab = new GameObject("ModifyTab") { layer = 5 }.AddComponent<SpriteRenderer>();
            ModifyTab.GetComponent<SpriteRenderer>().sprite = TownOfUsFusion.ModifierGuideTab;
            ModifyTab.transform.SetParent(HudManager.Instance.transform);
            ModifyTab.transform.localPosition = new(-3.563f, -1.71f, -12f);
        }
        CrewTab.gameObject.SetActive(OpenedPhone);
        NeutTab.gameObject.SetActive(OpenedPhone);
        ImpTab.gameObject.SetActive(OpenedPhone);
        ModifyTab.gameObject.SetActive(OpenedPhone);

/*        if (IsInGame)
            OpenRoleCard();
        else*/
            OpenCrewTab();
    }
    
    public static void OpenCrewTab()
    {
        PluginSingleton<TownOfUsFusion>.Instance.Log.LogMessage("Crewmate Guide tab was opened!");
        CurTab = "Crew";
    }
    public static void OpenNeutTab()
    {
        PluginSingleton<TownOfUsFusion>.Instance.Log.LogMessage("Neutral Guide tab was opened!");
        CurTab = "Neut";
    }
    public static void OpenImpTab()
    {
        PluginSingleton<TownOfUsFusion>.Instance.Log.LogMessage("Impostor Guide tab was opened!");
        CurTab = "Imp";
            /*KILLYOURSELF = CreateButton("kys", "GO KILL YOURSELF VIC", () =>
            {
            });*/
    }
    public static void OpenModifyTab()
    {
        PluginSingleton<TownOfUsFusion>.Instance.Log.LogMessage("Modifiers Guide tab was opened!");
        CurTab = "Modify";
    }
    public static void OpenRoleCard()
    {
    }
/*
    private static void OpenRoleCard()
    {
        CloseMenus(SkipEnum.RoleCard);

        if (LocalBlocked)
            return;

        if (PhoneText)
            PhoneText.gameObject.Destroy();

        PhoneText = Object.Instantiate(HUD.KillButton.cooldownTimerText, Phone.transform);
        PhoneText.enableWordWrapping = false;
        PhoneText.transform.localScale = Vector3.one * 0.4f;
        PhoneText.transform.localPosition = new(0, 0, -50f);
        PhoneText.gameObject.layer = 7;
        PhoneText.alignment = TextAlignmentOptions.Center;
        PhoneText.name = "PhoneText";

        if (ToTheGuide == null)
        {
            ToTheGuide = CreateButton("ToTheGuide", "Mod Guide", () =>
            {
                OpenRoleCard();
                OpenGuide();
            });
        }

        RoleCardActive = !RoleCardActive;
        PhoneText.text = CustomPlayer.Local.RoleCardInfo();
        PhoneText.gameObject.SetActive(RoleCardActive);
        Phone.gameObject.SetActive(RoleCardActive);
        GuideOverlay.gameObject.SetActive(RoleCardActive);
        ToTheGuide.gameObject.SetActive(RoleCardActive && IsNormal && IsInGame);
    }*/

/*    private static void OpenGuide()
    {
        CloseMenus(SkipEnum.Guide);

        if (LocalBlocked)
            return;

        if (!PagesSet)
        {
            var clone = Info.AllInfo.Clone();
            clone.RemoveAll(x => x.Name is "Invalid" or "None" || x.Type == InfoType.Lore);
            clone.Reverse();
            clone = clone.Distinct().ToList();
            var i = 0;
            var j = 0;
            var k = 0;

            foreach (var pair in clone)
            {
                Sorted.Add(j, new(pair is SymbolInfo symbol ? symbol.Symbol : pair.Name, pair));
                j++;
                k++;

                if (k >= 28)
                {
                    i++;
                    k = 0;
                }
            }

            MaxPage = i;
            PagesSet = true;
        }

        if (NextButton == null)
        {
            NextButton = CreateButton("GuideNextButton", "Next Page", () =>
            {
                Page = Utils.CycleInt(MaxPage, 0, Page, true);
                ResetButtonPos();
            });
        }

        if (BackButton == null)
        {
            BackButton = CreateButton("GuideBack", "Previous Page", () =>
            {
                if (Selected == null)
                    Page = Utils.CycleInt(MaxPage, 0, Page, false);
                else if (LoreActive)
                {
                    PhoneText.gameObject.SetActive(false);
                    AddInfo();
                    LoreActive = false;
                }
                else
                {
                    Selected = null;
                    SelectionActive = false;
                    LoreButton.gameObject.SetActive(false);
                    NextButton.gameObject.SetActive(true);
                    NextButton.transform.localPosition = new(2.5f, 1.6f, 0f);
                    PhoneText.gameObject.SetActive(false);
                    Entry.Clear();
                }

                ResetButtonPos();
            });
        }

        if (YourStatus == null)
        {
            YourStatus = CreateButton("YourStatus", "Your Status", () =>
            {
                OpenGuide();
                OpenRoleCard();
            });
        }

        if (LoreButton == null)
        {
            LoreButton = CreateButton("GuideLore", "Lore", () =>
            {
                LoreActive = !LoreActive;
                SetEntryText(Info.ColorIt(WrapText(LayerInfo.AllLore.Find(x => x.Name == Selected.Name || x.Short == Selected.Short).Description)));
                PhoneText.text = Entry[0];
                PhoneText.transform.localPosition = new(-2.6f, 0.45f, -5f);
                SelectionActive = true;
            });
        }

        if (Buttons.Count == 0)
        {
            var i = 0;
            var j = 0;

            for (var k = 0; k < Sorted.Count; k++)
            {
                if (!Buttons.ContainsKey(i))
                    Buttons.Add(i, new());

                var cache = Sorted[k].Value;
                var cache2 = Sorted[k].Key;
                var button = CreateButton($"{cache2}Info", cache2, () =>
                {
                    foreach (var buttons in Buttons.Values)
                    {
                        if (buttons.Count > 0)
                            buttons.ForEach(x => x?.gameObject?.SetActive(false));
                    }

                    Selected = cache;
                    NextButton.gameObject.SetActive(false);
                    AddInfo();
                }, cache.Color);

                Buttons[i].Add(button);
                j++;

                if (j >= 28)
                {
                    i++;
                    j = 0;
                }
            }
        }

        GuideActive = !GuideActive;
        Phone.gameObject.SetActive(GuideActive);
        GuideOverlay.gameObject.SetActive(GuideActive);
        NextButton.gameObject.SetActive(GuideActive);
        BackButton.gameObject.SetActive(GuideActive);
        YourStatus.gameObject.SetActive(GuideActive && IsNormal && IsInGame);
        ResetButtonPos();
        Selected = null;

        if (!GuideActive && PhoneText)
            PhoneText.gameObject.SetActive(false);
    }
*/
    public static void ResetButtonPos()
    {/*
        if (BackButton != null)
            BackButton.transform.localPosition = new(-2.6f, 1.6f, 0f);

        if (NextButton != null)
            NextButton.transform.localPosition = new(2.5f, 1.6f, 0f);

        if (YourStatus != null)
            YourStatus.transform.localPosition = new(0f, 1.6f, 0f);

        if (ToTheGuide != null)
            ToTheGuide.transform.localPosition = new(-2.6f, 1.6f, 0f);

        if (Selected != null)
        {
            if (LayerInfo.AllLore.Any(x => x.Name == Selected.Name || x.Short == Selected.Short) && LoreButton != null)
            {
                LoreButton.gameObject.SetActive(!LoreActive);
                LoreButton.transform.localPosition = new(0f, -1.71f, 0f);
            }

            return;
        }

        var m = 0;

        foreach (var pair in Buttons)
        {
            foreach (var button in pair.Value)
            {
                if (button == null)
                    continue;

                var row = m / 4;
                var col = m % 4;
                button.transform.localPosition = new(-2.6f + (1.7f * col), 1f - (0.45f * row), -1f);
                button.gameObject.SetActive(Page == pair.Key && GuideActive);
                m++;

                if (m >= 28)
                    m -= 28;
            }
        }*/
    }
/*
    public static void AddInfo()
    {
        if (PhoneText)
            PhoneText.gameObject.Destroy();

        Selected.GuideEntry(out var result);
        SetEntryText(result);
        PhoneText = Object.Instantiate(HUD.TaskPanel.taskText, Phone.transform);
        PhoneText.color = UColor.white;
        PhoneText.text = Entry[0];
        PhoneText.enableWordWrapping = false;
        PhoneText.transform.localScale = Vector3.one * 0.75f;
        PhoneText.transform.localPosition = new(-2.6f, 0.45f, -5f);
        PhoneText.alignment = TextAlignmentOptions.TopLeft;
        PhoneText.fontStyle = FontStyles.Bold;
        PhoneText.gameObject.SetActive(true);
        PhoneText.name = "PhoneText";
        SelectionActive = true;
    }*/
 /*   public static PassiveButton CreateButton(string name, string labelText, Action onClick, UColor? textColor = null)
    {
            GreyedOptionModify = Object.Instantiate(__instance.MapButton.gameObject, __instance.MapButton.transform.parent);
            GreyedOptionModify.GetComponent<PassiveButton>().OnClick = new();
            GreyedOptionModify.GetComponent<PassiveButton>().OnClick.AddListener(new Action(OpenModifyTab));
            }
            GreyedOptionModify.transform.localPosition = new Vector3(-3.563f, -1.71f, -11f);
        GreyedOptionModify.GetComponent<SpriteRenderer>().sprite = TownOfUsFusion.GreyedOption;

            GreyedOptionCrew.SetActive(!IntroCutscene.Instance && CurTab != "Crew" && OpenedPhone);
            
        var button = Object.Instantiate(HudManager.MapButton.gameObject, HudManager.Instance.transform);
        button.name = $"{name}Button";
        //button.GetComponent<PassiveButton>().HoverSound = SoundEffects["Hover"];
        button.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
        button.GetComponent<BoxCollider2D>().size = new(2.5f, 0.55f);

        var label = Object.Instantiate(HudManager.TaskPanel.taskText.gameObject, button.transform);
        label.color = textColor ?? UColor.white;
        label.text = labelText;
        label.enableWordWrapping = false;
        label.transform.localPosition = new Vector3(0f, 0f, label.transform.localPosition.z);
        label.transform.localScale *= 1.55f;
        label.alignment = TextAlignmentOptions.Center;
        label.fontStyle = FontStyles.Bold;
        label.name = $"{name}Text";
        var rend = button.GetComponent<SpriteRenderer>();
        rend.GetComponent<SpriteRenderer>().sprite = TownOfUsFusion.Plate;
        rend.color = UColor.white;
        button.OnClick = new();
        button.OnClick.AddListener(onClick);
        button.OnMouseOver = new();
        button.OnMouseOver.AddListener((Action)(() => rend.color = UColor.yellow));
        button.OnMouseOut = new();
        button.OnMouseOut.AddListener((Action)(() => rend.color = UColor.white));
        return button;
    }*/
/*
    public static void CloseMenus(SkipEnum skip = SkipEnum.None)
    {
        if (GuideActive && skip != SkipEnum.Guide)
            OpenGuide();

        if (RoleCardActive && skip != SkipEnum.RoleCard)
            OpenRoleCard();

        if (Zooming && skip != SkipEnum.Zooming)
            ClickZoom();

        if (SettingsActive && skip != SkipEnum.Settings)
            OpenSettings();

        if (MapPatch.MapActive && Map && skip != SkipEnum.Map)
            Map.Close();

        if (ActiveTask && ActiveTask && skip != SkipEnum.Task)
            ActiveTask.Close();

        if (GameSettingMenu.Instance && skip != SkipEnum.Client)
            GameSettingMenu.Instance.Close();
    }

    private static void SetEntryText(string result)
    {
        Entry.Clear();
        ResultPage = 0;
        var texts = result.Split('\n');
        var pos = 0;
        var result2 = "";

        foreach (var text in texts)
        {
            result2 += $"{text}\n";
            pos++;

            if (pos >= 19)
            {
                Entry.Add(result2);
                result2 = "";
                pos -= 19;
            }
        }

        if (!IsNullEmptyOrWhiteSpace(result2))
            Entry.Add(result2);
    }*/
}
}