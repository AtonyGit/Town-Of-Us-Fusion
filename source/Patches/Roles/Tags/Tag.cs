using System;
using System.Collections.Generic;
using System.Linq;
using Reactor.Utilities.Extensions;
using TownOfUsFusion.Extensions;
using UnityEngine;

namespace TownOfUsFusion.Roles.Tags
{
    public abstract class Tag
{
    public static readonly Dictionary<byte, Tag> TagDictionary = new Dictionary<byte, Tag>();
    public Func<string> TaskText;

    protected Tag(PlayerControl player)
    {
        Player = player;
        TagDictionary.Add(player.PlayerId, this);
    }

    public static IEnumerable<Tag> AllTags => TagDictionary.Values.ToList();
    protected internal string Name { get; set; }
    public bool Hidden { get; set; }

    public string PlayerName { get; set; }
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
    protected internal TagEnum TagType { get; set; }
    protected internal Color Color { get; set; }
    public string ColorString => "<color=#" + Color.ToHtmlStringRGBA() + ">";
    private bool Equals(Tag other)
    {
        return Equals(Player, other.Player) && TagType == other.TagType;
    }

    internal virtual bool TagWin(LogicGameFlowNormal __instance)
    {
        return true;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != typeof(Tag)) return false;
        return Equals((Tag)obj);
    }


    public override int GetHashCode()
    {
        return HashCode.Combine(Player, (int)TagType);
    }


    public static bool operator ==(Tag a, Tag b)
    {
        if (a is null && b is null) return true;
        if (a is null || b is null) return false;
        return a.TagType == b.TagType && a.Player.PlayerId == b.Player.PlayerId;
    }

    public static bool operator !=(Tag a, Tag b)
    {
        return !(a == b);
    }

    public static Tag GetTag(PlayerControl player)
    {
        return (from entry in TagDictionary where entry.Key == player.PlayerId select entry.Value)
            .FirstOrDefault();
    }

    public static IEnumerable<Tag> GetTags(TagEnum Tagtype)
    {
        return AllTags.Where(x => x.TagType == Tagtype);
    }

    public virtual List<PlayerControl> GetTeammates()
    {
        var team = new List<PlayerControl>();
        return team;
    }

    public static T GetTag<T>(PlayerControl player) where T : Tag
    {
        return GetTag(player) as T;
    }

    public static Tag GetTag(PlayerVoteArea area)
    {
        var player = PlayerControl.AllPlayerControls.ToArray()
            .FirstOrDefault(x => x.PlayerId == area.TargetPlayerId);
        return player == null ? null : GetTag(player);
    }
}
}