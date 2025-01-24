using System;
using System.Collections.Generic;
using System.Linq;
using Reactor.Utilities.Extensions;
using TownOfUsFusion.Extensions;
using UnityEngine;

namespace TownOfUsFusion.Roles.Alliances
{
    public abstract class Alliance
{
    public static readonly Dictionary<byte, Alliance> AllianceDictionary = new Dictionary<byte, Alliance>();
    public Func<string> TaskText;

    protected Alliance(PlayerControl player)
    {
        Player = player;
        AllianceDictionary.Add(player.PlayerId, this);
    }

    public static IEnumerable<Alliance> AllAlliances => AllianceDictionary.Values.ToList();
    protected internal string Name { get; set; }
    protected internal string SymbolName { get; set; }

    protected internal string GetColoredSymbol()
    {
        if (SymbolName == null) return null;

        return $"{ColorString}{SymbolName}</color>";
    }

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
    protected internal Color Color { get; set; }
    protected internal AllianceEnum AllianceType { get; set; }
    public string ColorString => "<color=#" + Color.ToHtmlStringRGBA() + ">";

    private bool Equals(Alliance other)
    {
        return Equals(Player, other.Player) && AllianceType == other.AllianceType;
    }

    internal virtual bool AllianceWin(LogicGameFlowNormal __instance)
    {
        return true;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != typeof(Alliance)) return false;
        return Equals((Alliance)obj);
    }


    public override int GetHashCode()
    {
        return HashCode.Combine(Player, (int)AllianceType);
    }


    public static bool operator ==(Alliance a, Alliance b)
    {
        if (a is null && b is null) return true;
        if (a is null || b is null) return false;
        return a.AllianceType == b.AllianceType && a.Player.PlayerId == b.Player.PlayerId;
    }

    public static bool operator !=(Alliance a, Alliance b)
    {
        return !(a == b);
    }

    public static Alliance GetAlliance(PlayerControl player)
    {
        return (from entry in AllianceDictionary where entry.Key == player.PlayerId select entry.Value)
            .FirstOrDefault();
    }

    public static IEnumerable<Alliance> GetAlliances(AllianceEnum Alliancetype)
    {
        return AllAlliances.Where(x => x.AllianceType == Alliancetype);
    }

    public virtual List<PlayerControl> GetTeammates()
    {
        var team = new List<PlayerControl>();
        return team;
    }

    public static T GetAlliance<T>(PlayerControl player) where T : Alliance
    {
        return GetAlliance(player) as T;
    }

    public static Alliance GetAlliance(PlayerVoteArea area)
    {
        var player = PlayerControl.AllPlayerControls.ToArray()
            .FirstOrDefault(x => x.PlayerId == area.TargetPlayerId);
        return player == null ? null : GetAlliance(player);
    }
}
}