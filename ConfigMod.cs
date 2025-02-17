using Terraria.ModLoader.Config;
using System.ComponentModel;
using Microsoft.Xna.Framework;

namespace terraguardians
{
    public class ClientConfiguration : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [DefaultValue(true)]
        public bool UsePathFinding;

        [DefaultValue(0)]
        public ReviveBarStyles ReviveBar;

        [DefaultValue(PlayerIndex.Two)]
        public PlayerIndex Index; //For 2P mode.

        [DefaultValue(MainMod.CompanionMaxDistanceFromPlayer.Normal)]
        public MainMod.CompanionMaxDistanceFromPlayer MaxDistanceFromPlayer;

        public override void OnChanged()
        {
            MainMod.UsePathfinding = UsePathFinding;
            ReviveInterface.ReviveBarStyle = (int)ReviveBar;
            MainMod.SecondPlayerPort = Index;
            MainMod.MaxDistanceFromPlayer = MaxDistanceFromPlayer;
        }

        public enum ReviveBarStyles : int
        {
            Jaws = 0,
            Bars = 1
        }
    }
    public class ServerConfiguration : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        [DefaultValue(false)]
        public bool DebugMode;

        [DefaultValue(true)]
        public bool AllowModCompanions;

        [DefaultValue(false)]
        public bool PlayerKnockoutEnable;

        [DefaultValue(false)]
        public bool PlayerKnockoutColdEnable;

        [DefaultValue(true)]
        public bool CompanionKnockoutEnable;

        [DefaultValue(false)]
        public bool CompanionKnockoutColdEnable;

        [DefaultValue(true)]
        public bool SkillsEnabled;

        [DefaultValue(0.1f)]
        public float DamageNerfByCompanionCount;

        public override void OnChanged()
        {
            MainMod.DebugMode = DebugMode;
            MainMod.PlayerKnockoutEnable = PlayerKnockoutEnable;
            MainMod.PlayerKnockoutColdEnable = PlayerKnockoutColdEnable;
            MainMod.CompanionKnockoutEnable = CompanionKnockoutEnable;
            MainMod.CompanionKnockoutColdEnable = CompanionKnockoutColdEnable;
            MainMod.DamageNerfByCompanionCount = DamageNerfByCompanionCount;
            MainMod.SkillsEnabled = SkillsEnabled;
            if (MainMod.DisableModCompanions == AllowModCompanions)
            {
                if(Terraria.Main.gameMenu)
                {
                    MainMod.DisableModCompanions = !AllowModCompanions;
                }
                else
                {
                    Terraria.Main.NewText("You can't change this while in-game.");
                }
            }
        }
    }
}