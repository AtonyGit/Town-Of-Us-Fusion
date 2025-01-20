using HarmonyLib;

namespace TownOfUsFusion
{
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
    public class CamouflageUnCamouflage
    {
        public static bool CommsEnabled;

        public static bool IsCamoed => CommsEnabled;

        public static void Postfix(HudManager __instance)
        {
            if (CustomGameOptions.ColourblindComms)
            {
                if (ShipStatus.Instance != null)
                    switch (GameOptionsManager.Instance.currentNormalGameOptions.MapId)
                    {
                        default:
                        case 0:
                        case 2:
                        case 3:
                        case 4:
<<<<<<< Updated upstream
=======
                        case 7:
>>>>>>> Stashed changes
                        case 6:
                            var comms1 = ShipStatus.Instance.Systems[SystemTypes.Comms].Cast<HudOverrideSystemType>();
                            if (comms1.IsActive)
                            {
                                CommsEnabled = true;
<<<<<<< Updated upstream
                                Utils.Camouflage();
=======
                                Utils.GroupCamouflage();
>>>>>>> Stashed changes
                                return;
                            }

                            break;
                        case 1:
                        case 5:
                            var comms2 = ShipStatus.Instance.Systems[SystemTypes.Comms].Cast<HqHudSystemType>();
                            if (comms2.IsActive)
                            {
                                CommsEnabled = true;
<<<<<<< Updated upstream
                                Utils.Camouflage();
=======
                                Utils.GroupCamouflage();
>>>>>>> Stashed changes
                                return;
                            }

                            break;
                    }

                if (CommsEnabled)
                {
                    CommsEnabled = false;
                    Utils.UnCamouflage();
                }
            }
        }
    }
}