using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace TownOfUsFusion.Roles
{
    public interface IGuesser
    {
        public Dictionary<byte, (GameObject, GameObject, GameObject, TMP_Text)> Buttons { get; set; }
    }
}