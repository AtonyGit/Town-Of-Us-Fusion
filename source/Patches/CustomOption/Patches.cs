using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using Reactor.Utilities.Extensions;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using UnityEngine;

namespace TownOfUsFusion.CustomOption
{
    public static class Patches
{

    static string[] Menus = { "Map", "Game", "Crew", "Neutral", "Imposter", "Modifier", "Alliance" };

    public static Export ExportButton;
    public static Import ImportButton;
    public static List<OptionBehaviour> DefaultOptions;
    public static float LobbyTextRowHeight { get; set; } = 0.081F;


    private static List<OptionBehaviour> CreateOptions(GameOptionsMenu __instance, MultiMenu type)
    {
        var options = new List<OptionBehaviour>();

        var togglePrefab = Object.FindObjectOfType<ToggleOption>();
        var numberPrefab = Object.FindObjectOfType<NumberOption>();
        var stringPrefab = Object.FindObjectOfType<StringOption>();

        if (type == MultiMenu.main)
        {
            if (ExportButton.Setting != null)
            {
                ExportButton.Setting.gameObject.SetActive(true);
                options.Add(ExportButton.Setting);
            }
            else
            {
                var toggle = Object.Instantiate(togglePrefab, togglePrefab.transform.parent);
                toggle.transform.GetChild(2).gameObject.SetActive(false);
                toggle.transform.GetChild(0).localPosition += new Vector3(1f, 0f, 0f);
                toggle.transform.GetChild(1).localScale = new(1.6f, 1f, 1f);
                //toggle.transform.GetChild(1).localPosition += new Vector3(1.2f, 0f, 0f);
                //toggle.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new(1.6f, 0.26f);

                ExportButton.Setting = toggle;
                ExportButton.OptionCreated();
                options.Add(toggle);
            }

            if (ImportButton.Setting != null)
            {
                ImportButton.Setting.gameObject.SetActive(true);
                options.Add(ImportButton.Setting);
            }
            else
            {
                var toggle = Object.Instantiate(togglePrefab, togglePrefab.transform.parent);
                toggle.transform.GetChild(2).gameObject.SetActive(false);
                toggle.transform.GetChild(0).localPosition += new Vector3(1f, 0f, 0f);
                toggle.transform.GetChild(1).localScale = new(1.6f, 1f, 1f);
                //toggle.transform.GetChild(1).localPosition += new Vector3(1.2f, 0f, 0f);
                //toggle.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new(1.6f, 0.26f);

                ImportButton.Setting = toggle;
                ImportButton.OptionCreated();
                options.Add(toggle);
            }
        }


        DefaultOptions = __instance.Children.ToList();
        foreach (var defaultOption in __instance.Children) options.Add(defaultOption);

        foreach (var option in CustomOption.AllOptions.Where(x => x.Menu == type))
        {
            if (option.Setting != null)
            {
                option.Setting.gameObject.SetActive(true);
                options.Add(option.Setting);
                continue;
            }

            switch (option.Type)
            {
                case CustomOptionType.Header:
                    var toggle = Object.Instantiate(togglePrefab, togglePrefab.transform.parent);
                    toggle.transform.GetChild(1).gameObject.SetActive(false);
                    toggle.transform.GetChild(2).gameObject.SetActive(false);
                    
                        toggle.transform.GetChild(0).localPosition = new(-1.05f, 0f, 0f);
                        toggle.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new(5.5f, 0.37f);
                        toggle.transform.GetChild(1).localScale = new(1.6f, 1f, 1f);
                        toggle.transform.GetChild(2).localPosition = new(2.72f, 0f, 0f);
                        toggle.gameObject.GetComponent<BoxCollider2D>().size = new(option is CustomHeaderOption ? 0f : 7.91f, 0.45f);

                    option.Setting = toggle;
                    options.Add(toggle);
                    break;
                case CustomOptionType.Toggle:
                    var toggle2 = Object.Instantiate(togglePrefab, togglePrefab.transform.parent);
                    
                        toggle2.transform.GetChild(0).localPosition = new(-1.05f, 0f, 0f);
                        toggle2.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new(5.5f, 0.37f);
                        toggle2.transform.GetChild(1).localScale = new(1.6f, 1f, 1f);
                        toggle2.transform.GetChild(2).localPosition = new(2.72f, 0f, 0f);
                        toggle2.gameObject.GetComponent<BoxCollider2D>().size = new(option is CustomHeaderOption ? 0f : 7.91f, 0.45f);
                    
                    option.Setting = toggle2;
                    options.Add(toggle2);
                    break;
                case CustomOptionType.Number: //Background = 0, + = 1, - = 2, Title = 3, Val = 4
                    var number = Object.Instantiate(numberPrefab, numberPrefab.transform.parent);

                    number.transform.GetChild(0).localScale = new(1.6f, 1f, 1f);
                    number.transform.GetChild(1).localPosition += new Vector3(1.4f, 0f, 0f);
                    number.transform.GetChild(2).localPosition += new Vector3(1, 0f, 0f);
                    number.transform.GetChild(3).localPosition = new(-1.05f, 0f, 0f);
                    number.transform.GetChild(3).GetComponent<RectTransform>().sizeDelta = new(5.5f, 0.37f);
                    number.transform.GetChild(4).localPosition += new Vector3(1.2f, 0f, 0f);
                    number.transform.GetChild(4).GetComponent<RectTransform>().sizeDelta = new(1.6f, 0.26f);

                    option.Setting = number;
                    options.Add(number);
                    break;
                case CustomOptionType.String: //Background = 0, + = 1, - = 2, Title = 3, Val = 4
                                                //+ = 0, - = 1, Title = 2, Val = 3, Background = 4
                    var str = Object.Instantiate(stringPrefab, stringPrefab.transform.parent);

                    str.transform.GetChild(4).localScale = new(1.6f, 1f, 1f);
                    str.transform.GetChild(0).localPosition += new Vector3(1.4f, 0f, 0f);
                    str.transform.GetChild(1).localPosition += new Vector3(1, 0f, 0f);
                    str.transform.GetChild(2).localPosition = new(-1.05f, 0f, 0f);
                    str.transform.GetChild(2).GetComponent<RectTransform>().sizeDelta = new(5.5f, 0.37f);
                    str.transform.GetChild(3).localPosition += new Vector3(1.2f, 0f, 0f);
                    str.transform.GetChild(3).GetComponent<RectTransform>().sizeDelta = new(1.6f, 0.26f);
/*
                    str.transform.GetChild(1).localScale = new(1.6f, 1f, 1f);
                    str.transform.GetChild(2).localPosition += new Vector3(1.4f, 0f, 0f);
                    str.transform.GetChild(3).localPosition += new Vector3(1, 0f, 0f);

                    str.transform.GetChild(2).GetComponent<RectTransform>().sizeDelta = new(5.5f, 0.37f);
                    str.transform.GetChild(2).localPosition = new(-1.05f, 0f, 0f);
                    str.transform.GetChild(4).localPosition += new Vector3(1.2f, 0f, 0f);
                    str.transform.GetChild(3).GetComponent<RectTransform>().sizeDelta = new(1.6f, 0.26f);
*/
                    option.Setting = str;
                    options.Add(str);
                    break;
            }

            option.OptionCreated();
        }

        return options;
    }


    private static bool OnEnable(OptionBehaviour opt)
    {
        if (opt == ExportButton.Setting)
        {
            ExportButton.OptionCreated();
            return false;
        }

        if (opt == ImportButton.Setting)
        {
            ImportButton.OptionCreated();
            return false;
        }


        var customOption =
            CustomOption.AllOptions.FirstOrDefault(option =>
                option.Setting == opt); // Works but may need to change to gameObject.name check

        if (customOption == null)
        {
            customOption = ExportButton.SlotButtons.FirstOrDefault(option => option.Setting == opt);
            if (customOption == null)
            {
                customOption = ImportButton.SlotButtons.FirstOrDefault(option => option.Setting == opt);
                if (customOption == null) return true;
            }
        }

        customOption.OptionCreated();

        return false;
    }

    [HarmonyPatch(typeof(GameSettingMenu), nameof(GameSettingMenu.Start))]
    private class OptionsMenuBehaviour_Start
    {
        public static void Postfix(GameSettingMenu __instance)
        {
            var obj = __instance.RolesSettingsHightlight.gameObject.transform.parent.parent;
            var diff = 0.9f * Menus.Length - 2;
            obj.transform.localPosition = new Vector3(obj.transform.localPosition.x - diff, obj.transform.localPosition.y, obj.transform.localPosition.z);
            __instance.GameSettingsHightlight.gameObject.transform.parent.localPosition = new Vector3(obj.transform.localPosition.x, obj.transform.localPosition.y, obj.transform.localPosition.z);
            List<GameObject> menug = new List<GameObject>();
            List<SpriteRenderer> menugs = new List<SpriteRenderer>();
            for (int index = 0; index < Menus.Length; index++)
            {
                var touSettings = Object.Instantiate(__instance.RegularGameSettings, __instance.RegularGameSettings.transform.parent);
                touSettings.SetActive(false);
                touSettings.name = "TOUSettings" + Menus[index];

            //Derived this from Town Of Host Edited
            touSettings.transform.FindChild("BackPanel").transform.localScale = new(1.6f, 1f, 1f);
            touSettings.transform.FindChild("Bottom Gradient").transform.localScale = new(1.6f, 1f, 1f);
            touSettings.transform.FindChild("BackPanel").transform.localPosition += new Vector3(0.2f, 0f, 0f);
            touSettings.transform.FindChild("Bottom Gradient").transform.localPosition += new Vector3(0.2f, 0f, 0f);
            touSettings.transform.FindChild("Background").transform.localScale = new(1.8f, 1f, 1f);
            touSettings.transform.FindChild("UI_Scrollbar").transform.localPosition += new Vector3(1.4f, 0f, 0f);
            touSettings.transform.FindChild("UI_ScrollbarTrack").transform.localPosition += new Vector3(1.4f, 0f, 0f);
            touSettings.transform.FindChild("GameGroup/SliderInner").transform.localPosition += new Vector3(-0.3f, 0f, 0f);

                var gameGroup = touSettings.transform.FindChild("GameGroup");
                var title = gameGroup?.FindChild("Text");

                if (title != null)
                {
                    title.GetComponent<TextTranslatorTMP>().Destroy();
                    title.GetComponent<TMPro.TextMeshPro>().m_text = $"Town Of Us {Menus[index]} Settings";
                }
                var sliderInner = gameGroup?.FindChild("SliderInner");
                if (sliderInner != null)
                    sliderInner.GetComponent<GameOptionsMenu>().name = $"Tou{Menus[index]}OptionsMenu";

                var ourSettingsButton = Object.Instantiate(obj.gameObject, obj.transform.parent);
                ourSettingsButton.transform.localPosition = new Vector3(obj.localPosition.x + (1f * (index + 1)), obj.localPosition.y, obj.localPosition.z);
                ourSettingsButton.name = $"TOU{Menus[index]}tab";
                var hatButton = ourSettingsButton.transform.GetChild(0); //TODO:  change to FindChild I guess to be sure
                var hatIcon = hatButton.GetChild(0);
                var tabBackground = hatButton.GetChild(1);

                var renderer = hatIcon.GetComponent<SpriteRenderer>();
                renderer.sprite = GetSettingSprite(index);
                var touSettingsHighlight = tabBackground.GetComponent<SpriteRenderer>();
                menug.Add(touSettings);
                menugs.Add(touSettingsHighlight);

                PassiveButton passiveButton = tabBackground.GetComponent<PassiveButton>();
                passiveButton.OnClick = new UnityEngine.UI.Button.ButtonClickedEvent();
                passiveButton.OnClick.AddListener(ToggleButton(__instance, menug, menugs, index + 2));

                //fix for scrollbar (bug in among us)
                touSettings.GetComponentInChildren<Scrollbar>().parent = touSettings.GetComponentInChildren<Scroller>();
            }
            PassiveButton passiveButton2 = __instance.GameSettingsHightlight.GetComponent<PassiveButton>();
            passiveButton2.OnClick = new UnityEngine.UI.Button.ButtonClickedEvent();
            passiveButton2.OnClick.AddListener(ToggleButton(__instance, menug, menugs, 0));
            passiveButton2 = __instance.RolesSettingsHightlight.GetComponent<PassiveButton>();
            passiveButton2.OnClick = new UnityEngine.UI.Button.ButtonClickedEvent();
            passiveButton2.OnClick.AddListener(ToggleButton(__instance, menug, menugs, 1));

            __instance.RegularGameSettings.GetComponentInChildren<Scrollbar>().parent = __instance.RegularGameSettings.GetComponentInChildren<Scroller>();
            try
            {
                __instance.RolesSettings.GetComponentInChildren<Scrollbar>().parent = __instance.RolesSettings.GetComponentInChildren<Scroller>();
            }
            catch
            {
                var vanillarolebutton = __instance.Tabs.transform.FindChild("RoleTab");
                __instance.Tabs.transform.FindChild("GameTab").localPosition = new(0.17f, 0, -5);
                vanillarolebutton.gameObject.SetActive(false);
            }
        }

        private static Sprite GetSettingSprite(int index)
        {
            switch (index)
            {
                default:
                    return TownOfUsFusion.SettingsButtonSprite;
                case 0:
                    return TownOfUsFusion.MapSettingsButtonSprite;
                case 1:
                    return TownOfUsFusion.SettingsButtonSprite;
                case 2:
                    return TownOfUsFusion.CrewSettingsButtonSprite;
                case 3:
                    return TownOfUsFusion.NeutralSettingsButtonSprite;
                case 4:
                    return TownOfUsFusion.ImposterSettingsButtonSprite;
                case 5:
                    return TownOfUsFusion.ModifierSettingsButtonSprite;
                case 6:
                    return TownOfUsFusion.AllianceSettingsButtonSprite;
            }
        }
    }

    public static System.Action ToggleButton(GameSettingMenu settingMenu, List<GameObject> TouSettings, List<SpriteRenderer> highlight, int id)
    {
        return new System.Action(() =>
        {
            settingMenu.RegularGameSettings.SetActive(id == 0);
            settingMenu.GameSettingsHightlight.enabled = id == 0;
            settingMenu.RolesSettings.gameObject.SetActive(id == 1);
            settingMenu.RolesSettingsHightlight.enabled = id == 1;
            foreach (GameObject g in TouSettings)
            {
                g.SetActive(id == TouSettings.IndexOf(g) + 2);
                highlight[TouSettings.IndexOf(g)].enabled = id == TouSettings.IndexOf(g) + 2;
            }
        });
    }

    [HarmonyPatch(typeof(GameOptionsMenu), nameof(GameOptionsMenu.Start))]
    private class GameOptionsMenu_Start
    {
        public static bool Prefix(GameOptionsMenu __instance)
        {
            for (int index = 0; index < Menus.Length; index++)
            {
                if (__instance.name == $"Tou{Menus[index]}OptionsMenu")
                {
                    __instance.Children = new Il2CppReferenceArray<OptionBehaviour>(new OptionBehaviour[0]);
                    var childeren = new Transform[__instance.gameObject.transform.childCount];
                    for (int k = 0; k < childeren.Length; k++)
                    {
                        childeren[k] = __instance.gameObject.transform.GetChild(k); //TODO: Make a better fix for this for example caching the options or creating it ourself.
                    }
                    var startOption = __instance.gameObject.transform.GetChild(0);
                    var customOptions = CreateOptions(__instance, (MultiMenu)index);
                    var y = startOption.localPosition.y;
                    var x = startOption.localPosition.x;
                    var z = startOption.localPosition.z;
                    for (int k = 0; k < childeren.Length; k++)
                    {
                        childeren[k].gameObject.Destroy();
                    }

                    var i = 0;
                    foreach (var option in customOptions)
                        option.transform.localPosition = new Vector3(x, y - i++ * 0.5f, z);

                    __instance.Children = new Il2CppReferenceArray<OptionBehaviour>(customOptions.ToArray());
                    return false;
                }
            }
            return true;
        }
    }

    [HarmonyPatch(typeof(GameOptionsMenu), nameof(GameOptionsMenu.Update))]
    private class GameOptionsMenu_Update
    {
        public static void Postfix(GameOptionsMenu __instance)
        {
            if (__instance.Children == null || __instance.Children.Length == 0)
                return;
            var y = __instance.GetComponentsInChildren<OptionBehaviour>()
                .Max(option => option.transform.localPosition.y);
            float x, z;
            if (__instance.Children.Length == 1)
            {
                x = __instance.Children[0].transform.localPosition.x;
                z = __instance.Children[0].transform.localPosition.z;
            }
            else
            {
                x = __instance.Children[1].transform.localPosition.x;
                z = __instance.Children[1].transform.localPosition.z;
            }

            var i = 0;
            foreach (var option in __instance.Children)
                option.transform.localPosition = new Vector3(x, y - i++ * 0.5f, z);

            try
            {
                var commonTasks = __instance.Children.FirstOrDefault(x => x.name == "NumCommonTasks").TryCast<NumberOption>();
                if (commonTasks != null) commonTasks.ValidRange = new FloatRange(0f, 4f);

                var shortTasks = __instance.Children.FirstOrDefault(x => x.name == "NumShortTasks").TryCast<NumberOption>();
                if (shortTasks != null) shortTasks.ValidRange = new FloatRange(0f, 26f);

                var longTasks = __instance.Children.FirstOrDefault(x => x.name == "NumLongTasks").TryCast<NumberOption>();
                if (longTasks != null) longTasks.ValidRange = new FloatRange(0f, 15f);
            }
            catch
            {

            }
        }
    }

    [HarmonyPatch(typeof(ToggleOption), nameof(ToggleOption.OnEnable))]
    private static class ToggleOption_OnEnable
    {
        private static bool Prefix(ToggleOption __instance)
        {
            return OnEnable(__instance);
        }
    }

    [HarmonyPatch(typeof(NumberOption), nameof(NumberOption.OnEnable))]
    private static class NumberOption_OnEnable
    {
        private static bool Prefix(NumberOption __instance)
        {
            return OnEnable(__instance);
        }
    }

    [HarmonyPatch(typeof(StringOption), nameof(StringOption.OnEnable))]
    private static class StringOption_OnEnable
    {
        private static bool Prefix(StringOption __instance)
        {
            return OnEnable(__instance);
        }
    }


    [HarmonyPatch(typeof(ToggleOption), nameof(ToggleOption.Toggle))]
    private class ToggleButtonPatch
    {
        public static bool Prefix(ToggleOption __instance)
        {
            var option =
                CustomOption.AllOptions.FirstOrDefault(option =>
                    option.Setting == __instance); // Works but may need to change to gameObject.name check
            if (option is CustomToggleOption toggle)
            {
                toggle.Toggle();
                return false;
            }

            if (__instance == ExportButton.Setting)
            {
                if (!AmongUsClient.Instance.AmHost) return false;
                ExportButton.Do();
                return false;
            }

            if (__instance == ImportButton.Setting)
            {
                if (!AmongUsClient.Instance.AmHost) return false;
                ImportButton.Do();
                return false;
            }


            if (option is CustomHeaderOption) return false;

            CustomOption option2 = ExportButton.SlotButtons.FirstOrDefault(option => option.Setting == __instance);
            if (option2 is CustomButtonOption button)
            {
                if (!AmongUsClient.Instance.AmHost) return false;
                button.Do();
                return false;
            }

            CustomOption option3 = ImportButton.SlotButtons.FirstOrDefault(option => option.Setting == __instance);
            if (option3 is CustomButtonOption button2)
            {
                if (!AmongUsClient.Instance.AmHost) return false;
                button2.Do();
                return false;
            }

            return true;
        }
    }

    [HarmonyPatch(typeof(NumberOption), nameof(NumberOption.Increase))]
    private class NumberOptionPatchIncrease
    {
        public static bool Prefix(NumberOption __instance)
        {
            var option =
                CustomOption.AllOptions.FirstOrDefault(option =>
                    option.Setting == __instance); // Works but may need to change to gameObject.name check
            if (option is CustomNumberOption number)
            {
                number.Increase();
                return false;
            }

            return true;
        }
    }

    [HarmonyPatch(typeof(NumberOption), nameof(NumberOption.Decrease))]
    private class NumberOptionPatchDecrease
    {
        public static bool Prefix(NumberOption __instance)
        {
            var option =
                CustomOption.AllOptions.FirstOrDefault(option =>
                    option.Setting == __instance); // Works but may need to change to gameObject.name check
            if (option is CustomNumberOption number)
            {
                number.Decrease();
                return false;
            }

            return true;
        }
    }

    [HarmonyPatch(typeof(StringOption), nameof(StringOption.Increase))]
    private class StringOptionPatchIncrease
    {
        public static bool Prefix(StringOption __instance)
        {
            var option =
                CustomOption.AllOptions.FirstOrDefault(option =>
                    option.Setting == __instance); // Works but may need to change to gameObject.name check
            if (option is CustomStringOption str)
            {
                str.Increase();
                return false;
            }

            return true;
        }
    }

    [HarmonyPatch(typeof(StringOption), nameof(StringOption.Decrease))]
    private class StringOptionPatchDecrease
    {
        public static bool Prefix(StringOption __instance)
        {
            var option =
                CustomOption.AllOptions.FirstOrDefault(option =>
                    option.Setting == __instance); // Works but may need to change to gameObject.name check
            if (option is CustomStringOption str)
            {
                str.Decrease();
                return false;
            }

            return true;
        }
    }

    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.RpcSyncSettings))]
    private class PlayerControlPatch
    {
        public static void Postfix()
        {
            if (PlayerControl.AllPlayerControls.Count < 2 || !AmongUsClient.Instance ||
                !PlayerControl.LocalPlayer || !AmongUsClient.Instance.AmHost) return;

            Rpc.SendRpc();
        }
    }

    [HarmonyPatch(typeof(PlayerPhysics), nameof(PlayerPhysics.CoSpawnPlayer))]
    private class PlayerJoinPatch
    {
        public static void Postfix()
        {
            if (PlayerControl.AllPlayerControls.Count < 2 || !AmongUsClient.Instance ||
                !PlayerControl.LocalPlayer || !AmongUsClient.Instance.AmHost) return;

            Rpc.SendRpc();
        }
    }

    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
    private class HudManagerUpdate
    {
        private const float
            MinX = -5.233334F /*-5.3F*/,
            OriginalY = 2.9F,
            MinY = 3F; // Differs to cause excess options to appear cut off to encourage scrolling

        private static Scroller Scroller;
        private static Vector3 LastPosition = new Vector3(MinX, MinY);

        public static void Prefix(HudManager __instance)
        {
            if (__instance.GameSettings?.transform == null) return;


            // Scroller disabled
            if (!CustomOption.LobbyTextScroller)
            {
                // Remove scroller if disabled late
                if (Scroller != null)
                {
                    __instance.GameSettings.transform.SetParent(Scroller.transform.parent);
                    __instance.GameSettings.transform.localPosition = new Vector3(MinX, OriginalY);

                    Object.Destroy(Scroller);
                }

                return;
            }

            CreateScroller(__instance);

            Scroller.gameObject.SetActive(__instance.GameSettings.gameObject.activeSelf);

            if (!Scroller.gameObject.active) return;

            var rows = __instance.GameSettings.text.Count(c => c == '\n');
            var maxY = Mathf.Max(MinY, rows * LobbyTextRowHeight + (rows - 38) * LobbyTextRowHeight);

            Scroller.ContentYBounds = new FloatRange(MinY, maxY);

            // Prevent scrolling when the player is interacting with a menu
            if (PlayerControl.LocalPlayer?.CanMove != true)
            {
                __instance.GameSettings.transform.localPosition = LastPosition;

                return;
            }

            if (__instance.GameSettings.transform.localPosition.x != MinX ||
                __instance.GameSettings.transform.localPosition.y < MinY) return;

            LastPosition = __instance.GameSettings.transform.localPosition;
        }

        private static void CreateScroller(HudManager __instance)
        {
            if (Scroller != null) return;

            Scroller = new GameObject("SettingsScroller").AddComponent<Scroller>();
            Scroller.transform.SetParent(__instance.GameSettings.transform.parent);
            Scroller.gameObject.layer = 5;

            Scroller.transform.localScale = Vector3.one;
            Scroller.allowX = false;
            Scroller.allowY = true;
            Scroller.active = true;
            Scroller.velocity = new Vector2(0, 0);
            Scroller.ScrollbarYBounds = new FloatRange(0, 0);
            Scroller.ContentXBounds = new FloatRange(MinX, MinX);
            Scroller.enabled = true;

            Scroller.Inner = __instance.GameSettings.transform;
            __instance.GameSettings.transform.SetParent(Scroller.transform);
        }
    }
}
}