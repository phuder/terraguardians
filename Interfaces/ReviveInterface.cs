using System;
using Terraria;
using Terraria.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace terraguardians
{
    public class ReviveInterface : LegacyGameInterfaceLayer
    {
        private static bool KnockedOutColdAlpha = false;
        internal static int ReviveBarStyle = 0;

        public ReviveInterface() : base("TerraGuardians: Revive Interface", DrawInterface, InterfaceScaleType.UI)
        {
            
        }

        public static bool DrawInterface()
        {
            Player ReviveCharacter = MainMod.GetLocalPlayer;
            Companion controlled = PlayerMod.PlayerGetControlledCompanion(ReviveCharacter);
            if (controlled != null) ReviveCharacter = controlled;
            KnockoutStates state = PlayerMod.GetPlayerKnockoutState(ReviveCharacter);
            if(state == KnockoutStates.Awake)
            {
                KnockedOutColdAlpha = false;
                return true;
            }
            if (state == KnockoutStates.KnockedOutCold) KnockedOutColdAlpha = true;
            float Percentage = Math.Clamp((float)ReviveCharacter.statLife / ReviveCharacter.statLifeMax2, 0f, 1f);
            float RescueBarTime = state == KnockoutStates.KnockedOutCold ? (float)ReviveCharacter.GetModPlayer<PlayerMod>().GetRescueStack / PlayerMod.MaxRescueStack : 0;
            DrawVerticalBars(state, Percentage, RescueBarTime);
            DrawHealthBar(state, Percentage, ReviveCharacter);
            return true;
        }

        private static void DrawHealthBar(KnockoutStates state, float Percentage, Player player)
        {
            Vector2 BarPosition = new Vector2(Main.screenWidth * 0.5f - 80, Main.screenHeight * 0.7f - 12);
            if (Percentage > 0)
            {
                Rectangle DrawDimension = new Rectangle(0, 0, 160, 24);
                Main.spriteBatch.Draw(MainMod.ReviveHealthBarTexture.Value, BarPosition, DrawDimension, Color.White);
                BarPosition.X += 4;
                BarPosition.Y += 4;
                DrawDimension.X += 4;
                DrawDimension.Y += 28;
                DrawDimension.Width = (int)(152 * Percentage);
                DrawDimension.Height -= 8;
                Main.spriteBatch.Draw(MainMod.ReviveHealthBarTexture.Value, BarPosition, DrawDimension, Color.White);
                BarPosition.X += 80;
                BarPosition.Y += 52;
                Utils.DrawBorderStringBig(Main.spriteBatch, player.GetModPlayer<PlayerMod>().GetReviveBoost > 0 ? "Being Revived" : player.GetModPlayer<PlayerMod>().GetReviveStack > 0 ? "Regaining Consciousness" : (state == KnockoutStates.KnockedOut ? "Bleeding out" : "Incapacitated"), BarPosition, Color.White, 1, 0.5f, 0.5f);
            }
            else
            {
                if (!player.dead)
                {
                    BarPosition.X += 80;
                    BarPosition.Y += 32;
                    Utils.DrawBorderStringBig(Main.spriteBatch, "Incapacitated", BarPosition, Color.White, 1, 0.5f, 0.5f);
                }
            }
            if (state == KnockoutStates.KnockedOutCold && MainMod.PlayerKnockoutColdEnable)
            {
                    BarPosition.Y += 50;
                    Utils.DrawBorderStringBig(Main.spriteBatch, player.GetModPlayer<PlayerMod>().GetRescueStack >= PlayerMod.MaxRescueStack / 2 ? "Rescued by someone." : player.controlHook ? "Calling for help." : "Hold Quick Hook key to be rescued.", BarPosition, Color.White, 1, 0.5f, 0.5f);
            }
        }

        private static void DrawVerticalBars(KnockoutStates state, float Percentage, float RescueBarTime)
        {
            Rectangle DrawFrame = new Rectangle(ReviveBarStyle * 640, 0, 640, 480);
            Vector2 Scale = new Vector2((float)Main.screenWidth / 640, (float)Main.screenHeight / 480);
            Vector2 Position = new Vector2(0, (int)(Percentage * (Main.screenHeight * 0.6f)));
            float Opacity = 1;
            if (state == KnockoutStates.KnockedOutCold)
                Opacity = System.Math.Min(1, 0.5f + RescueBarTime);
            else
                Opacity = KnockedOutColdAlpha ? (1f - Percentage) : 0.5f * (1f - Percentage);
            Color color = Color.White * Opacity;
            Main.spriteBatch.Draw(MainMod.ReviveBarsEffectTexture.Value, Position, DrawFrame, color, 0, Vector2.Zero, Scale, SpriteEffects.None, 0);
            Position = new Vector2(0, (int)(Percentage * (-Main.screenHeight * 0.6f)));
            DrawFrame.Y += DrawFrame.Height;
            Main.spriteBatch.Draw(MainMod.ReviveBarsEffectTexture.Value, Position, DrawFrame, color, 0, Vector2.Zero, Scale, SpriteEffects.None, 0);
        }
    }
}