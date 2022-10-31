using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.Audio;

namespace terraguardians.Companions
{
    public class RococoBase : CompanionBase
    {
        public override string Name => "Rococo";
        public override string Description => "";
        public override int Age => 15;
        public override int SpriteWidth => 96;
        public override int SpriteHeight => 96;
        public override bool CanCrouch => true;
        public override int Width => 28;
        public override int Height => 86;
        public override float Scale => 94f / 86;
        public override int InitialMaxHealth => 200;
        public override int HealthPerLifeCrystal => 40;
        public override int HealthPerLifeFruit => 10;
        //public override float Gravity => 0.5f;
        public override float MaxRunSpeed => 5.2f;
        public override float RunAcceleration => 0.18f;
        public override float RunDeceleration => 0.47f;
        public override int JumpHeight => 15;
        public override float JumpSpeed => 7.08f;
        public override CompanionTypes CompanionType => CompanionTypes.TerraGuardian;
        public override SoundStyle HurtSound => Terraria.ID.SoundID.DD2_KoboldHurt;
        public override SoundStyle DeathSound => Terraria.ID.SoundID.DD2_KoboldDeath;
        #region  Animations
        protected override Animation SetWalkingFrames {
            get
            {
                Animation anim = new Animation();
                for(short i = 1; i <= 8; i++)
                    anim.AddFrame(i, 24); //8
                return anim;
            }
        }
        protected override Animation SetJumpingFrames => new Animation(9);
        protected override Animation SetItemUseFrames 
        {
            get
            {
                Animation anim = new Animation();
                for(short i = 16; i <= 19; i++)
                    anim.AddFrame(i, 1);
                return anim;
            }
        }
        protected override Animation SetHeavySwingFrames
        {
            get
            {
                Animation anim = new Animation();
                for(short i = 10; i <= 12; i++) anim.AddFrame(i, 1);
                return anim;
            }
        }
        protected override Animation SetCrouchingFrames => new Animation(20);
        protected override Animation SetCrouchingSwingFrames
        {
            get
            {
                Animation anim = new Animation();
                anim.AddFrame(21, 1);
                anim.AddFrame(22, 1);
                anim.AddFrame(12, 1);
                return anim;
            }
        }
        protected override Animation SetSittingFrames => new Animation(23);
        protected override Animation SetPlayerMountedArmFrame => new Animation(9);
        protected override Animation SetThroneSittingFrames => new Animation(24);
        protected override Animation SetBedSleepingFrames => new Animation(25);
        protected override Animation SetRevivingFrames => new Animation(26);
        protected override Animation SetDownedFrames => new Animation(27);
        protected override Animation SetPetrifiedFrames => new Animation(28);
        protected override Animation SetBackwardStandingFrames => new Animation(29);
        protected override Animation SetBackwardReviveFrames => new Animation(30);
        #endregion
        #region Animation Positions
        protected override AnimationPositionCollection[] SetHandPositions
        {
            get
            {
                AnimationPositionCollection[] Hands = new AnimationPositionCollection[]
                {
                    new AnimationPositionCollection(new Vector2(18, 31), true), 
                    new AnimationPositionCollection(new Vector2(30, 31), true)
                };
                //Left Arm
                Hands[0].AddFramePoint2X(10, 8, 10);
                Hands[0].AddFramePoint2X(11, 32, 9);
                Hands[0].AddFramePoint2X(12, 44, 37);
                
                Hands[0].AddFramePoint2X(16, 15, 4);
                Hands[0].AddFramePoint2X(17, 35, 7);
                Hands[0].AddFramePoint2X(18, 40, 19);
                Hands[0].AddFramePoint2X(19, 35, 31);
                
                Hands[0].AddFramePoint2X(21, 34, 14);
                Hands[0].AddFramePoint2X(22, 44, 29);
                
                Hands[0].AddFramePoint2X(26, 34, 41);
                
                //Right Arm
                Hands[1].AddFramePoint2X(10, 8, 10);
                Hands[1].AddFramePoint2X(11, 32, 9);
                Hands[1].AddFramePoint2X(12, 44, 37);
                
                Hands[1].AddFramePoint2X(16, 15, 4);
                Hands[1].AddFramePoint2X(17, 35, 7);
                Hands[1].AddFramePoint2X(18, 40, 19);
                Hands[1].AddFramePoint2X(19, 36, 31);
                
                Hands[1].AddFramePoint2X(21, 36, 16);
                Hands[1].AddFramePoint2X(22, 44, 29);
                return Hands;
            }
        }
        protected override AnimationPositionCollection SetSleepingOffset => new AnimationPositionCollection(Vector2.UnitX * 16);
        #endregion
        #region Dialogue
        
        #endregion
    }
}