using System;
using UnityEngine;

namespace TownOfUs.RainbowMod
{
    public class RainbowBehaviour : MonoBehaviour
    {
        public Renderer Renderer;
        public int Id;

        public void AddRend(Renderer rend, int id)
<<<<<<< Updated upstream
=======
        {
            Renderer = rend;
            Id = id;
        }

        public void Update()
        {
            if (Renderer == null) return;

        if (RainbowUtils.IsGradient(Id))
>>>>>>> Stashed changes
        {
            Renderer = rend;
            Id = id;
        }
<<<<<<< Updated upstream

        public void Update()
        {
            if (Renderer == null) return;

            if (RainbowUtils.IsRainbow(Id))
            {
                RainbowUtils.SetRainbow(Renderer);
            }
        }

=======
        }

>>>>>>> Stashed changes
        public RainbowBehaviour(IntPtr ptr) : base(ptr) { }
    }
}
