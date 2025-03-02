using MiraAPI.Networking;
using MiraAPI.Utilities.Assets;

namespace TownOfUsFusion.Roles;

public class DeputyRole : CrewmateRole, ICustomRole
{
    public string RoleName => "Deputy";
    public string RoleDescription => "Camp Crewmates To Catch Their Killer";
    public string RoleLongDescription => "Camp crewmates then shoot their killer";
    public Color RoleColor => Colors.Deputy;
    public PlayerControl Killer = null;
    public bool HasCamped = false;
    public Dictionary<byte, GameObject> Buttons { get; set; } = new();
    public ModdedRoleTeams Team => ModdedRoleTeams.Crewmate;
    public CustomRoleConfiguration Configuration => new CustomRoleConfiguration(this)
    {
        OptionsScreenshot = ToufAssets.Banner,
        CanModifyChance = true,
        DefaultChance = 0,
        DefaultRoleCount = 0,
    };
}
[HarmonyPatch(typeof(MeetingHud), nameof(MeetingHud.Start))]
public class AddButton
{
    public static LoadableAsset<Sprite> DepSprite => ToufAssets.ShootButton;
    private static bool IsExempt(PlayerVoteArea voteArea)
        {
            if (voteArea.AmDead) return true;
            var player = Utils.PlayerById(voteArea.TargetPlayerId);
            if (
                player == null ||
                player.Data.IsDead ||
                player.Data.Disconnected
            ) return true;
            return player.Data.Role == null;
        }
        public static void GenButton(DeputyRole role, PlayerVoteArea voteArea)
        {
            var targetId = voteArea.TargetPlayerId;
            if (IsExempt(voteArea))
            {
                role.Buttons[targetId] = null;
                return;
            }

            var confirmButton = voteArea.Buttons.transform.GetChild(0).gameObject;

            var newButton = UObject.Instantiate(confirmButton, voteArea.transform);
            var renderer = newButton.GetComponent<SpriteRenderer>();
            var passive = newButton.GetComponent<PassiveButton>();

            renderer.sprite = DepSprite.LoadAsset();
            newButton.transform.position = confirmButton.transform.position - new Vector3(0.75f, 0f, 0f);
            newButton.transform.localScale *= 0.8f;
            newButton.layer = 5;
            newButton.transform.parent = confirmButton.transform.parent.parent;

            passive.OnClick = new UnityEngine.UI.Button.ButtonClickedEvent();
            passive.OnClick.AddListener(Shoot(role, voteArea));
            role.Buttons[targetId] = newButton;
        }


        private static Action Shoot(DeputyRole role, PlayerVoteArea voteArea)
        {
            void Listener()
            {
                var target = Utils.PlayerById(voteArea.TargetPlayerId);
                Logger<TownOfUsFusion>.Info($"The Targeted Player is {target}, the killer is {role.Killer}");
                if (target == role.Killer)
                {
                    Shoot(role, target);
                    PlayerControl.LocalPlayer.RpcCustomMurder(target, createDeadBody: false, teleportMurderer: false);
                }
                else DestroyableSingleton<HudManager>.Instance.Chat.AddChat(PlayerControl.LocalPlayer, "You missed your shot! They are either not the killer or are invincible.");
                role.Killer = null;
                role.HasCamped = false;
                foreach (var (_, button) in role.Buttons)
                {
                    if (button == null) continue;
                    button.SetActive(false);
                    button.GetComponent<PassiveButton>().OnClick = new UnityEngine.UI.Button.ButtonClickedEvent();
                }
                role.Buttons.Clear();
            }

            return Listener;
        }

        public static void Shoot(DeputyRole deputy, PlayerControl player)
        {
            PlayerVoteArea voteArea = MeetingHud.Instance.playerStates.First(
                x => x.TargetPlayerId == player.PlayerId
            );

            var hudManager = DestroyableSingleton<HudManager>.Instance;
            var amOwner = player.AmOwner;
            if (amOwner)
            {
                hudManager.ShadowQuad.gameObject.SetActive(false);
                player.nameText().GetComponent<MeshRenderer>().material.SetInt("_Mask", 0);
                player.RpcSetScanner(false);
                ImportantTextTask importantTextTask = new GameObject("_Player").AddComponent<ImportantTextTask>();
                importantTextTask.transform.SetParent(AmongUsClient.Instance.transform, false);
            }

            if (voteArea == null) return;
            if (voteArea.DidVote) voteArea.UnsetVote();
            voteArea.AmDead = true;
            voteArea.Overlay.gameObject.SetActive(true);
            voteArea.Overlay.color = Color.white;
            voteArea.XMark.gameObject.SetActive(true);
            voteArea.XMark.transform.localScale = Vector3.one;

            var meetingHud = MeetingHud.Instance;
            if (amOwner)
            {
                meetingHud.SetForegroundForDead();
            }

            foreach (var playerVoteArea in meetingHud.playerStates)
            {
                if (playerVoteArea.VotedFor != player.PlayerId) continue;
                playerVoteArea.UnsetVote();
                var voteAreaPlayer = Utils.PlayerById(playerVoteArea.TargetPlayerId);
                if (!voteAreaPlayer.AmOwner) continue;
                meetingHud.ClearVote();
            }

            if (AmongUsClient.Instance.AmHost) meetingHud.CheckForEndVoting();
        }
            public static void Postfix(MeetingHud __instance)
            {
                foreach (var player in PlayerControl.AllPlayerControls)
                {
                    if (player.Data.Role is DeputyRole)
                    {
                        var dep = player.Data.Role as DeputyRole;
                        dep.Buttons.Clear();
                    }
                }

                if (PlayerControl.LocalPlayer.Data.IsDead) return;
                if (!(PlayerControl.LocalPlayer.Data.Role is DeputyRole)) return;
                var deputyrole = PlayerControl.LocalPlayer.Data.Role as DeputyRole;

                if (deputyrole.Killer == null) return;
                foreach (var voteArea in __instance.playerStates)
                {
                    GenButton(deputyrole, voteArea);
                }
            }
    }