using System;
using UnityEngine;

namespace TownOfUsFusion.RainbowMod
{
    public class RainbowBehaviour : MonoBehaviour
    {
        public Renderer Renderer;
        public int Id;

        public void AddRend(Renderer rend, int id)
        {
            Renderer = rend;
            Id = id;
        }

        public void Update()
        {
            if (Renderer == null) return;

            if (RainbowUtils.IsGradient(Id))
            {
                if (RainbowUtils.IsRainbow(Id)) RainbowUtils.SetRainbow(Renderer);
                if (RainbowUtils.IsGalaxy(Id)) RainbowUtils.SetGalaxy(Renderer);
                if (RainbowUtils.IsFire(Id)) RainbowUtils.SetFire(Renderer);
                if (RainbowUtils.IsAcid(Id)) RainbowUtils.SetAcid(Renderer);
                if (RainbowUtils.IsMonochrome(Id)) RainbowUtils.SetMonochrome(Renderer);
            }
        }

        public RainbowBehaviour(IntPtr ptr) : base(ptr) { }
    }
}
