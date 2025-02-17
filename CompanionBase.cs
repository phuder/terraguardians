using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria.DataStructures;

namespace terraguardians
{
    public class CompanionBase
    {
        #region Internal Variables
        private float _playersizescale = -1;
        public float GetPlayerSizeScale { get { if (_playersizescale < 0) _playersizescale = (float)42 / Height; return _playersizescale; } }
        #endregion
        #region Companion Infos
        private BitsByte Flags0 = 0;
        private bool InvalidCompanion
        {
            get { return Flags0[0]; }
            set { Flags0[0] = true;}
        }
        private bool AnimationsLoaded
        {
            get { return Flags0[1]; }
            set { Flags0[1] = true;}
        }
        private bool AnimationPositionsLoaded
        {
            get { return Flags0[2]; }
            set { Flags0[2] = true;}
        }
        private bool SubAttacksLoaded
        {
            get { return Flags0[3]; }
            set { Flags0[3] = true;}
        }
        internal CompanionBase SetInvalid() { InvalidCompanion = true; return this; }
        public bool IsInvalidCompanion { get{ return InvalidCompanion; }}
        public virtual string Name { get { return ""; } }
        public virtual string FullName { get { return Name; } }
        public virtual string WikiName { get { return Name; } }
        public virtual string ContributorName { get { return ""; } }
        public virtual string Description { get { return ""; } }
        public virtual string CompanionContentFolderName { get { return Name; } }
        public virtual int Age { get { return 18; } }
        private int Birthday = -1;
        public virtual BirthdayCalculator SetBirthday { get { return new BirthdayCalculator(); } }
        public int GetBirthday {
            get
            {
                if (Birthday == -1)
                    Birthday = SetBirthday.GetResult;
                return Birthday;
            }
        }
        public virtual Sizes Size { get { return Sizes.Medium; } }
        public virtual Genders Gender { get { return Genders.Male; } }
        public virtual bool CanChangeGenders { get { return false; } }
        public virtual bool RandomGenderOnSpawn { get { return true; } }
        public virtual CompanionTypes CompanionType { get { return CompanionTypes.TerraGuardian ;} }
        public virtual MountStyles MountStyle { get { return MountStyles.PlayerMountsOnCompanion; } }
        public virtual PartDrawOrdering MountedDrawOrdering { get { return PartDrawOrdering.Behind; } }
        public virtual bool CanCrouch { get { return false; } }
        public virtual int Width { get { return 32; } }
        public virtual int Height { get { return 82; } }
        public virtual float Scale { get { return 1f; } }
        public virtual int CrouchingHeight { get { return 52; } }
        public virtual int SpriteWidth { get { return 96 ; } }
        public virtual int SpriteHeight { get { return 96 ; } }
        public virtual int FramesInRow { get { return 20; } }
        public virtual bool CanUseHeavyItem { get { return false; } }
        public virtual Rectangle GetHeadDrawFrame(Texture2D HeadTexture)
        {
            return new Rectangle(0, 0, HeadTexture.Width, HeadTexture.Height);
        }
        public virtual SoundStyle HurtSound {get { return Terraria.ID.SoundID.NPCHit1; }}
        public virtual SoundStyle DeathSound{ get{ return Terraria.ID.SoundID.NPCDeath1; }}
        public virtual void InitialInventory(out InitialItemDefinition[] InitialInventoryItems, ref InitialItemDefinition[] InitialEquipments)
        {
            InitialInventoryItems = new InitialItemDefinition[] { new InitialItemDefinition(ItemID.WoodenSword), new InitialItemDefinition(ItemID.LesserHealingPotion, 5) };
        }
        public virtual bool IsNocturnal { get { return false; } }
        public virtual bool SleepsWhenOnBed { get { return true; } }
        public virtual bool DrawBehindWhenSharingChair { get { return false; } }
        public virtual bool DrawBehindWhenSharingThrone { get { return false; } }
        public virtual bool DrawBehindWhenSharingBed { get { return false; } }
        public virtual bool CanBeAppointedAsBuddy { get { return true; } }
        protected virtual FriendshipLevelUnlocks SetFriendshipUnlocks => new FriendshipLevelUnlocks();
        private FriendshipLevelUnlocks? _unlocks = null;
        public FriendshipLevelUnlocks GetFriendshipUnlocks
        {
            get
            {
                if (!_unlocks.HasValue)
                    _unlocks = SetFriendshipUnlocks;
                return _unlocks.Value;
            }
        }
        public virtual PersonalityBase GetPersonality(Companion c)
        {
            return PersonalityDB.Neutral;
        }
        public virtual CompanionData CreateCompanionData => new CompanionData();
        private TerrarianCompanionInfo terrariancompanioninfo = null;
        public TerrarianCompanionInfo GetTerrarianCompanionInfo
        {
            get
            {
                if(terrariancompanioninfo == null)
                    terrariancompanioninfo = SetTerrarianCompanionInfo;
                return terrariancompanioninfo;
            }
        }
        public virtual CombatTactics DefaultCombatTactic { get { return CombatTactics.MidRange; } }
        public virtual CompanionGroup GetCompanionGroup { get { return MainMod.GetTerrariansGroup; } }
        protected virtual TerrarianCompanionInfo SetTerrarianCompanionInfo { get { return new TerrarianCompanionInfo(); } }
        public virtual bool CanSpawnNpc()
        {
            return true;
        }
        #endregion
        #region Base Status
        public virtual int InitialMaxHealth { get { return 100; } }
        public virtual int HealthPerLifeCrystal{ get { return 20; } }
        public virtual int HealthPerLifeFruit { get { return 5; } }
        public virtual int InitialMaxMana { get { return 20; } }
        public virtual int ManaPerManaCrystal { get { return 20; } }
        public float HealthScale { get { return (float)(InitialMaxHealth + HealthPerLifeCrystal * 15 + HealthPerLifeFruit * 20) / 500;}}
        public float ManaScale { get { return (float)(InitialMaxMana + ManaPerManaCrystal * 9) / 200; } }
        public virtual void UpdateAttributes(Companion companion)
        {

        }
        public virtual float AccuracyPercent { get { return .5f; } }
        public virtual float AgilityPercent { get { return .5f; } }
        public virtual byte TriggerPercent { get { return 50; } }
        #endregion
        #region Default Permissions
        public virtual bool AllowSharingChairWithPlayer { get { return true; } }
        public virtual bool AllowSharingBedWithPlayer { get { return true; } }
        #endregion
        #region Mobility Status
        public virtual float MaxFallSpeed { get { return 10f; }}
        public virtual float Gravity { get { return 0.4f; }}
        public virtual int JumpHeight { get{ return 15;}}
        public virtual float JumpSpeed { get{ return 5.01f; }}
        public virtual float MaxRunSpeed { get{ return 3f; } }
        public virtual float RunAcceleration { get { return 0.08f; }}
        public virtual float RunDeceleration { get{ return 0.2f; }}
        public virtual int FallHeightTolerance { get { return 25; }}
        #endregion
        #region Sub Attacks Setup
        private List<SubAttackBase> _SubAttacks = new List<SubAttackBase>();
        public IReadOnlyList<SubAttackBase> GetSubAttackBases => _SubAttacks;
        protected virtual SubAttackBase[] GetDefaultSubAttacks()
        {
            return new SubAttackBase[0];
        }
        internal void InitializeSubAttackLoading()
        {
            if (SubAttacksLoaded) return;
            _SubAttacks.AddRange(GetDefaultSubAttacks());
            SubAttacksLoaded = true;
        }
        #endregion
        #region Behavior Scripts
        public virtual BehaviorBase DefaultIdleBehavior { get { return new IdleBehavior(); } }
        public virtual BehaviorBase DefaultCombatBehavior { get { return new CombatBehavior(); } }
        public virtual BehaviorBase DefaultFollowLeaderBehavior { get { return new FollowLeaderBehavior(); } }
        public virtual BehaviorBase PreRecruitmentBehavior { get { return new PreRecruitBehavior(); } }
        public virtual ReviveBehavior ReviveBehavior { get { return new ReviveBehavior(); } }
        #endregion
        #region Animations
        private Animation _StandingFrame, _WalkingFrames, _JumpingFrames, 
        _HeavySwingFrames, _ItemUseFrames, _CrouchingFrames, _CrouchingSwingFrames,
        _SittingItemUseFrames, _SittingFrames, _ChairSittingFrames, _ThroneSittingFrames, 
        _SittingMountPlayerFrames, _BedSleepingFrames, _RevivingFrames, _DownedFrames, _PetrifiedFrames, 
        _PlayerMountedArmFrame, _BackwardStandingFrames, _BackwardsRevivingFrames;
        private AnimationFrameReplacer _BodyFrontFrameReplacers;
        private AnimationFrameReplacer[] _ArmFrontFrameReplacers;
        public Animation GetAnimation(AnimationTypes anim)
        {
            if (!AnimationsLoaded) InitializeAnimations();
            switch(anim)
            {
                case AnimationTypes.StandingFrame:
                    return _StandingFrame;
                case AnimationTypes.WalkingFrames:
                    return _WalkingFrames;
                case AnimationTypes.JumpingFrames:
                    return _JumpingFrames;
                case AnimationTypes.HeavySwingFrames:
                    return _HeavySwingFrames;
                case AnimationTypes.ItemUseFrames:
                    return _ItemUseFrames;
                case AnimationTypes.CrouchingFrames:
                    return _CrouchingFrames;
                case AnimationTypes.CrouchingSwingFrames:
                    return _CrouchingSwingFrames;
                case AnimationTypes.SittingItemUseFrames:
                    return _SittingItemUseFrames;
                case AnimationTypes.SittingFrames:
                    return _SittingFrames;
                case AnimationTypes.ChairSittingFrames:
                    return _ChairSittingFrames;
                case AnimationTypes.SittingMountPlayerFrames:
                    return _SittingMountPlayerFrames;
                case AnimationTypes.ThroneSittingFrames:
                    return _ThroneSittingFrames;
                case AnimationTypes.BedSleepingFrames:
                    return _BedSleepingFrames;
                case AnimationTypes.RevivingFrames:
                    return _RevivingFrames;
                case AnimationTypes.DownedFrames:
                    return _DownedFrames;
                case AnimationTypes.PetrifiedFrames:
                    return _PetrifiedFrames;
                case AnimationTypes.PlayerMountedArmFrame:
                    return _PlayerMountedArmFrame;
                case AnimationTypes.BackwardStandingFrames:
                    return _BackwardStandingFrames;
                case AnimationTypes.BackwardsRevivingFrames:
                    return _BackwardsRevivingFrames;
            }
            return null;
        }
        public AnimationFrameReplacer GetAnimationFrameReplacer(AnimationFrameReplacerTypes anim)
        {
            if (!AnimationsLoaded) InitializeAnimations();
            switch(anim)
            {
                case AnimationFrameReplacerTypes.BodyFront:
                    return _BodyFrontFrameReplacers;
            }
            return null;
        }
        public AnimationFrameReplacer GetArmFrontAnimationFrame(byte Arm)
        {
            if (!AnimationsLoaded) InitializeAnimations();
            if (Arm >= _ArmFrontFrameReplacers.Length)
                return _ArmFrontFrameReplacers[0];
            return _ArmFrontFrameReplacers[Arm];
        }
        protected virtual Animation SetStandingFrames { get { return new Animation(0); } }
        protected virtual Animation SetWalkingFrames { get { return new Animation(0); } }
        protected virtual Animation SetJumpingFrames { get { return new Animation(0); } }
        protected virtual Animation SetHeavySwingFrames { get { return new Animation(0); } }
        protected virtual Animation SetItemUseFrames { get { return new Animation(0); } }
        protected virtual Animation SetCrouchingFrames { get { return new Animation(0); } }
        protected virtual Animation SetCrouchingSwingFrames { get { return new Animation(0); } }
        protected virtual Animation SetSittingItemUseFrames { get { return new Animation(); } }
        protected virtual Animation SetSittingFrames { get { return new Animation(); } }
        protected virtual Animation SetChairSittingFrames { get { return new Animation(); } }
        protected virtual Animation SetThroneSittingFrames { get { return new Animation(); } }
        protected virtual Animation SetBedSleepingFrames { get { return new Animation(); } }
        protected virtual Animation SetRevivingFrames { get { return new Animation(); } }
        protected virtual Animation SetDownedFrames { get { return new Animation(); } }
        protected virtual Animation SetPetrifiedFrames { get { return new Animation(); } }
        protected virtual Animation SetPlayerMountedArmFrame { get { return new Animation(); } }
        protected virtual Animation SetBackwardStandingFrames { get { return new Animation(); } }
        protected virtual Animation SetBackwardReviveFrames { get { return new Animation(); } }
        protected virtual AnimationFrameReplacer SetBodyFrontFrameReplacers { get { return new AnimationFrameReplacer(); } }
        protected virtual AnimationFrameReplacer[] SetArmFrontFrameReplacers { get { return new AnimationFrameReplacer[]{ new AnimationFrameReplacer(), new AnimationFrameReplacer() }; } }
        internal void InitializeAnimations()
        {
            _StandingFrame = SetStandingFrames;
            _WalkingFrames = SetWalkingFrames;
            _JumpingFrames = SetJumpingFrames; 
            _HeavySwingFrames = SetHeavySwingFrames;
            _ItemUseFrames = SetItemUseFrames;
            _CrouchingFrames = SetCrouchingFrames;
            _CrouchingSwingFrames = SetCrouchingSwingFrames;
            _SittingItemUseFrames = SetItemUseFrames;
            _SittingFrames = SetSittingFrames;
            _ChairSittingFrames = SetChairSittingFrames;
            _ThroneSittingFrames = SetThroneSittingFrames;
            _BedSleepingFrames = SetBedSleepingFrames;
            _RevivingFrames = SetRevivingFrames;
            _DownedFrames = SetDownedFrames;
            _PetrifiedFrames = SetPetrifiedFrames;
            _PlayerMountedArmFrame = SetPlayerMountedArmFrame;
            _BackwardStandingFrames = SetBackwardStandingFrames;
            _BackwardsRevivingFrames = SetBackwardReviveFrames;
            //
            _BodyFrontFrameReplacers = SetBodyFrontFrameReplacers;
            _ArmFrontFrameReplacers = SetArmFrontFrameReplacers;
            //
            AnimationsLoaded = true;
        }
        #endregion
        #region Animation Positions
        private AnimationPositionCollection[] _HandPositions, _ArmOffsetPositions;
        private AnimationPositionCollection _MountShoulderPosition, 
            _HeadVanityPosition, _WingPosition, _SittingPosition, 
            _SleepingOffset, _PlayerSittingOffset, _PlayerSleepingOffset, _PlayerSleepingCompanionOffset, _BodyOffsetPositions;

        public int GetHands
        {
            get
            { 
                if(!AnimationPositionsLoaded)
                {
                    InitializeAnimationPositions();
                }
                return _HandPositions.Length;
            }
        }

        internal void InitializeAnimationPositions()
        {
            _HandPositions = SetHandPositions;
            if(_HandPositions.Length == 0)
            {
                _HandPositions = new AnimationPositionCollection[]{ new AnimationPositionCollection() };
            }
            _ArmOffsetPositions = SetArmOffsetPosition;
            if (_ArmOffsetPositions.Length < _HandPositions.Length)
            {
                AnimationPositionCollection[] Last = _ArmOffsetPositions;
                _ArmOffsetPositions = new AnimationPositionCollection[_HandPositions.Length];
                for (int i = 0; i < _ArmOffsetPositions.Length; i++)
                {
                    if ( i < Last.Length)
                        _ArmOffsetPositions[i] = Last[i];
                    else
                        _ArmOffsetPositions[i] = new AnimationPositionCollection();
                }
                Last = null;
            }
            _MountShoulderPosition = SetMountShoulderPosition;
            _HeadVanityPosition = SetHeadVanityPosition;
            _WingPosition = SetWingPosition;
            _SittingPosition = SetSittingPosition;
            _SleepingOffset = SetSleepingOffset;
            _PlayerSittingOffset = SetPlayerSittingOffset;
            _PlayerSleepingOffset = SetPlayerSleepingOffset;
            _PlayerSleepingCompanionOffset = SetPlayerSleepingCompanionOffset;
            _BodyOffsetPositions = SetBodyOffsetPosition;
            AnimationPositionsLoaded = true;
        }
        protected virtual AnimationPositionCollection[] SetHandPositions { get { return new AnimationPositionCollection[]{
            new AnimationPositionCollection(),
            new AnimationPositionCollection()
        }; } }
        protected virtual AnimationPositionCollection SetMountShoulderPosition { get { return new AnimationPositionCollection(); }}
        protected virtual AnimationPositionCollection SetHeadVanityPosition { get { return new AnimationPositionCollection(); }}
        protected virtual AnimationPositionCollection SetWingPosition { get { return new AnimationPositionCollection(); }}
        protected virtual AnimationPositionCollection SetSittingPosition { get { return new AnimationPositionCollection(); }}
        protected virtual AnimationPositionCollection SetSleepingOffset { get { return new AnimationPositionCollection(); }}
        protected virtual AnimationPositionCollection SetPlayerSittingOffset { get { return new AnimationPositionCollection(); } }
        protected virtual AnimationPositionCollection SetPlayerSleepingOffset { get { return new AnimationPositionCollection(); } }
        protected virtual AnimationPositionCollection SetPlayerSleepingCompanionOffset { get { return new AnimationPositionCollection(); } }
        protected virtual AnimationPositionCollection SetBodyOffsetPosition { get { return new AnimationPositionCollection(); } }
        protected virtual AnimationPositionCollection[] SetArmOffsetPosition { get { return new AnimationPositionCollection[]{ new AnimationPositionCollection(), new AnimationPositionCollection() }; } }
        public AnimationPositionCollection GetAnimationPosition(AnimationPositions Position, byte MultipleAnimationsIndex = 0)
        {
            if(!AnimationPositionsLoaded)
            {
                InitializeAnimationPositions();
            }
            switch(Position)
            {
                case AnimationPositions.HandPosition:
                    if(MultipleAnimationsIndex < _HandPositions.Length)
                        return _HandPositions[MultipleAnimationsIndex];
                    return _HandPositions[0];
                case AnimationPositions.MountShoulderPositions:
                    return _MountShoulderPosition;
                case AnimationPositions.HeadVanityPosition:
                    return _HeadVanityPosition;
                case AnimationPositions.SittingPosition:
                    return _SittingPosition;
                case AnimationPositions.SleepingOffset:
                    return _SleepingOffset;
                case AnimationPositions.WingPositions:
                    return _WingPosition;
                case AnimationPositions.PlayerSittingOffset:
                    return _PlayerSittingOffset;
                case AnimationPositions.PlayerSleepingOffset:
                    return _PlayerSleepingOffset;
                case AnimationPositions.PlayerSleepingCompanionOffset:
                    return _PlayerSleepingCompanionOffset;
                case AnimationPositions.ArmPositionOffset:
                    if(MultipleAnimationsIndex < _ArmOffsetPositions.Length)
                        return _ArmOffsetPositions[MultipleAnimationsIndex];
                    return _ArmOffsetPositions[0];
                case AnimationPositions.BodyPositionOffset:
                    return _BodyOffsetPositions;
            }
            return null;
        }
        #endregion
        #region Spritesheet Loading Trick
        public virtual CompanionSpritesContainer SetSpritesContainer(CompanionBase cb, Mod mod)
        {
            return new CompanionSpritesContainer(cb, mod);
        }
        public CompanionSpritesContainer GetSpriteContainer { get{
            if (_spritecontainer == null)
            {
                _spritecontainer = SetSpritesContainer(this, ReferedMod);
                SetupSpritesContainer(_spritecontainer);
                _spritecontainer.LoadContent();
            }
            foreach(SubAttackBase s in GetSubAttackBases)
            {
                s.LoadIcon();
            }
            return _spritecontainer;
        } }
        public virtual void SetupSpritesContainer(CompanionSpritesContainer container)
        {
            
        }
        private CompanionSpritesContainer _spritecontainer;
        private Mod ReferedMod;
        internal void DefineMod(Mod mod)
        {
            ReferedMod = mod;
        }
        #endregion
        #region Dialogue Container
        public CompanionDialogueContainer GetDialogues
        {
            get
            {
                LoadDialogue();
                return _dialogueContainer;
            }
        }
        private CompanionDialogueContainer _dialogueContainer = null;
        protected virtual CompanionDialogueContainer GetDialogueContainer { get { return new CompanionDialogueContainer(); } }
        private void LoadDialogue()
        {
            if (_dialogueContainer == null)
            {
                _dialogueContainer = GetDialogueContainer;
                _dialogueContainer.SetOwnerCompanion(this);
            }
        }
        #endregion
        #region Other Hooks
        public virtual void PreDrawCompanions(ref PlayerDrawSet drawSet, ref TgDrawInfoHolder Holder)
        {
            
        }

        public virtual void CompanionDrawLayerSetup(bool IsDrawingFrontLayer, PlayerDrawSet drawSet, ref TgDrawInfoHolder Holder, ref List<DrawData> DrawDatas)
        {

        }

        public virtual void ModifyAnimation(Companion companion)
        {

        }

        public virtual void PostUpdateAnimation(Companion companion)
        {

        }

        public virtual void UpdateCompanion(Companion companion)
        {

        }

        public virtual void UpdateBehavior(Companion companion)
        {

        }

        public virtual void OnAttackedByPlayer(Companion companion, Player attacker, int Damage, bool Critical)
        {
            
        }

        public virtual void OnAttackedByNpc(Companion companion, NPC attacker, int Damage, bool Critical)
        {
            
        }

        public virtual void OnAttackedByProjectile(Companion companion, Projectile proj, int Damage, bool Critical)
        {
            
        }

        public virtual void ModifyMountedCharacterPosition(Companion companion, Player MountedCharacter, ref Vector2 Position)
        {
            
        }

        public virtual bool CompanionRoomRequirements(bool IsRoomEvil, out string RequirementFailMessage)
        {
            return WorldMod.Housing_CheckBasicHousingRoomNeeds(IsRoomEvil, out RequirementFailMessage);
        }
        #endregion

        public virtual Companion GetCompanionObject
        {
            get
            {
                return new Companion();
            }
        }

        internal void Unload()
        {
            OnUnload();
            if(_spritecontainer != null)
            {
                _spritecontainer.Unload();
                _spritecontainer = null;
            }
            foreach(SubAttackBase s in GetSubAttackBases)
            {
                s.OnUnload();
            }
            _SubAttacks .Clear();
            _SubAttacks = null;
            ReferedMod = null;
        }

        public virtual void OnUnload()
        {

        }

        public string GetNameColored(CompanionData data = null)
        {
            string Name = data != null ? data.GetName : this.Name;
            MainMod.SetGenderColoring(Gender, ref Name);
            return Name;
        }
    }

    public enum AnimationPositions : byte
    {
        HandPosition,
        MountShoulderPositions,
        HeadVanityPosition,
        WingPositions,
        SittingPosition,
        SleepingOffset,
        PlayerSittingOffset,
        PlayerSleepingOffset,
        PlayerSleepingCompanionOffset,
        BodyPositionOffset,
        ArmPositionOffset
    }

    public enum AnimationTypes : byte
    {
        StandingFrame, 
        WalkingFrames, 
        JumpingFrames, 
        HeavySwingFrames, 
        ItemUseFrames, 
        CrouchingFrames, 
        CrouchingSwingFrames,
        SittingItemUseFrames, 
        SittingFrames, 
        ChairSittingFrames, 
        SittingMountPlayerFrames,
        ThroneSittingFrames,
        BedSleepingFrames, 
        RevivingFrames, 
        DownedFrames, 
        PetrifiedFrames, 
        PlayerMountedArmFrame,
        BackwardStandingFrames, 
        BackwardsRevivingFrames
    }

    public enum AnimationFrameReplacerTypes : byte
    {
        BodyFront
    }

    public enum MountStyles : byte
    {
        CantMount = 0,
        PlayerMountsOnCompanion = 1,
        CompanionRidesPlayer = 2
    }

    public struct BirthdayCalculator
    {
        int Result;

        internal BirthdayCalculator(byte Season, byte Day)
        {
            if (Day == 0) Day++;
            Result = (Season % 4) * 30 + ((Day - 1) % 30);
        }

        public BirthdayCalculator(Seasons Season, byte Day)
        {
            if (Day == 0) Day++;
            Result = ((byte)Season % 4) * 30 + ((Day - 1) % 30);
        }

        public int GetResult { get { return Result; }}
    }

    public struct InitialItemDefinition
    {
        public int ID;
        public int Stack;

        public InitialItemDefinition(int ItemID, int ItemStack = 1)
        {
            ID = ItemID;
            Stack = ItemStack;
        }
    }

    public struct FriendshipLevelUnlocks
    {
        public byte FollowerUnlock;
        public byte VisitUnlock;
        public byte MountUnlock;
        public byte MoveInUnlock;
        public byte ControlUnlock;
        public byte RequestUnlock;
        public byte BuddyUnlock;
        public byte InviteUnlock;

        public FriendshipLevelUnlocks()
        {
            FollowerUnlock = 0;
            InviteUnlock = 0;
            VisitUnlock = 1;
            MoveInUnlock = 3;
            MountUnlock = 5;
            ControlUnlock = 10;
            RequestUnlock = 0;
            BuddyUnlock = 15;
        }
    }

    public enum PartDrawOrdering : byte
    {
        Behind = 0,
        InBetween = 1,
        InFront = 2
    }
}