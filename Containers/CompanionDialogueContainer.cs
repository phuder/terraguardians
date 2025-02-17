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
    public class CompanionDialogueContainer
    {
        private CompanionBase OwnerCompanion;
        public CompanionBase GetCompanionBase { get { return OwnerCompanion; } }
        internal void SetOwnerCompanion(CompanionBase Base)
        {
            OwnerCompanion = Base;
        }

        public virtual string GreetMessages(Companion companion)
        {
            string s = companion.GetPersonality.GetDialogues.GreetMessages(companion);
            if (s != "") return s;
            return "*[name] liked to meet you.*";
        }

        public virtual string NormalMessages(Companion companion)
        {
            string s = companion.GetPersonality.GetDialogues.NormalMessages(companion);
            if (s != "") return s;
            return "*[name] stares at you, waiting for you to say something.*";
        }

        public virtual string TalkAboutOtherTopicsMessage(Companion companion, TalkAboutOtherTopicsContext context)
        {
            string s = companion.GetPersonality.GetDialogues.TalkAboutOtherTopicsMessage(companion, context);
            if (s != "") return s;
            switch(context)
            {
                case TalkAboutOtherTopicsContext.FirstTimeInThisDialogue:
                    return "*[name] is asking you what else you want to talk about.*";
                case TalkAboutOtherTopicsContext.AfterFirstTime:
                    return "*[name] asks what else you want to talk about.*";
                case TalkAboutOtherTopicsContext.Nevermind:
                    return "*[name] asks if you want to talk about something else.*";
            }
            return "**";
        }

        public virtual string TalkMessages(Companion companion)
        {
            string s = companion.GetPersonality.GetDialogues.TalkMessages(companion);
            if (s != "") return s;
            return "*[name] told you something.*";
        }

        public virtual string RequestMessages(Companion companion, RequestContext context)
        {
            string s = companion.GetPersonality.GetDialogues.RequestMessages(companion, context);
            if (s != "") return s;
            switch(context)
            {
                case RequestContext.NoRequest:
                    return "*[name] seems to have nothing to ask you for.*";
                case RequestContext.HasRequest:
                    return "*[name] wants you to [objective].*";
                case RequestContext.Completed:
                    return "*[name] thanked you deeply.*";
                case RequestContext.Accepted:
                    return "*[name] tells you that he will wait for your return.*";
                case RequestContext.Rejected:
                    return "*[name] is sad that you rejected their request.*";
                case RequestContext.Failed:
                    return "*[name] seems disappointed at you failing the request.*";
                case RequestContext.TooManyRequests:
                    return "*[name] told you that you have too many requests active.*";
                case RequestContext.PostponeRequest:
                    return "*[name] said that you can return later to check their request.*";
                case RequestContext.AskIfRequestIsCompleted:
                    return "*[name] asked if you completed their request.*";
                case RequestContext.RemindObjective:
                    return "*[name] told you that you need to [objective].*";
                case RequestContext.CancelRequestAskIfSure:
                    return "*[name] asks if you're sure that you want to cancel the request.*";
                case RequestContext.CancelRequestYes:
                    return "*[name] is disappointed at you, and cancels the request they gave you.*";
                case RequestContext.CancelRequestNo:
                    return "*[name] says that their request will still be active then.*";
            }
            return "**";
        }

        public virtual string JoinGroupMessages(Companion companion, JoinMessageContext context)
        {
            string s = companion.GetPersonality.GetDialogues.JoinGroupMessages(companion, context);
            if (s != "") return s;
            switch(context)
            {
                case JoinMessageContext.Success:
                    return "([name] join your adventure.)";
                case JoinMessageContext.Fail:
                    return "([name] refused.)";
                case JoinMessageContext.FullParty:
                    return "(There is no space for [name] in the group.)";
            }
            return "";
        }

        public virtual string LeaveGroupMessages(Companion companion, LeaveMessageContext context)
        {
            string s = companion.GetPersonality.GetDialogues.LeaveGroupMessages(companion, context);
            if (s != "") return s;
            switch(context)
            {
                case LeaveMessageContext.Success:
                case LeaveMessageContext.DangerousPlaceYesAnswer:
                    return "([name] left your group.)";
                case LeaveMessageContext.Fail:
                    return "([name] refuses to leave your group.)";
                case LeaveMessageContext.AskIfSure:
                    return "([name] asks if you're sure you want "+companion.GetPronoun()+" to leave your group.)";
                case LeaveMessageContext.DangerousPlaceNoAnswer:
                    return "([name] stays on your group.)";
            }
            return "";
        }
        public virtual string MountCompanionMessage(Companion companion, MountCompanionContext context)
        {
            string s = companion.GetPersonality.GetDialogues.MountCompanionMessage(companion, context);
            if (s != "") return s;
            switch(context)
            {
                case MountCompanionContext.Success:
                    return "*[name] let you mount on their shoulder.*";
                case MountCompanionContext.SuccessMountedOnPlayer:
                    return "*[name] climbed your back and is mounted on your shoulder.*";
                case MountCompanionContext.Fail:
                    return "*[name] refused.*";
                case MountCompanionContext.NotFriendsEnough:
                    return "*[name] said you're not friends enough for that.*";
                case MountCompanionContext.SuccessCompanionMount:
                    return "*[name] says that will let [target] ride them.*";
                case MountCompanionContext.AskWhoToCarryMount:
                    return "*[name] asks who should they carry.*";
            }
            return "";
        }

        public virtual string DismountCompanionMessage(Companion companion, DismountCompanionContext context)
        {
            string s = companion.GetPersonality.GetDialogues.DismountCompanionMessage(companion, context);
            if (s != "") return s;
            switch (context)
            {
                case DismountCompanionContext.SuccessMount:
                    return "*[name] placed you on the ground.*";
                case DismountCompanionContext.SuccessMountOnPlayer:
                    return "*[name] got off your shoulder.*";
                case DismountCompanionContext.Fail:
                    return "*[name] doesn't think it's a good moment for that.*";
            }
            return "";
        }

        public virtual string AskCompanionToMoveInMessage(Companion companion, MoveInContext context)
        {
            string s = companion.GetPersonality.GetDialogues.AskCompanionToMoveInMessage(companion, context);
            if (s != "") return s;
            switch(context)
            {
                case MoveInContext.Success:
                    return "*[name] happily accepts moving into your world.*";
                case MoveInContext.Fail:
                    return "*[name] doesn't seems to be wanting to move in right now.*";
                case MoveInContext.NotFriendsEnough:
                    return "*[name] doesn't know you well enough to move in closer.*";
            }
            return "";
        }

        public virtual string AskCompanionToMoveOutMessage(Companion companion, MoveOutContext context)
        {
            string s = companion.GetPersonality.GetDialogues.AskCompanionToMoveOutMessage(companion, context);
            if (s != "") return s;
            switch(context)
            {
                case MoveOutContext.Success:
                    return "*[name] says that will begin packing its things.*";
                case MoveOutContext.Fail:
                    return "*[name] tells you that is not leaving now.*";
                case MoveOutContext.NoAuthorityTo:
                    return "*[name] tells you that is not going to listen to you.*";
            }
            return "";
        }

        public virtual string OnToggleShareChairMessage(Companion companion, bool Share)
        {
            string s = companion.GetPersonality.GetDialogues.OnToggleShareChairMessage(companion, Share);
            if (s != "") return s;
            if(Share) return "*[name] doesn't mind letting you sit on their lap.*";
            return "*[name] tells you that will seek another chair next time.*";
        }

        public virtual string OnToggleShareBedsMessage(Companion companion, bool Share)
        {
            string s = companion.GetPersonality.GetDialogues.OnToggleShareBedsMessage(companion, Share);
            if (s != "") return s;
            if(Share) return "*[name] doesn't mind sharing their bed with you.*";
            return "*[name] hopes there's another bed for them.*";
        }

        public virtual string TacticChangeMessage(Companion companion, TacticsChangeContext context)
        {
            string s = companion.GetPersonality.GetDialogues.TacticChangeMessage(companion, context);
            if (s != "") return s;
            switch(context)
            {
                case TacticsChangeContext.OnAskToChangeTactic:
                    return "*[name] asks how they should act in combat.*";
                case TacticsChangeContext.ChangeToCloseRange:
                    return "*[name] acknowledges, saying that will take on foes they face.*";
                case TacticsChangeContext.ChangeToMidRanged:
                    return "*[name] acknowledges, saying that will avoid contact with their foes.*";
                case TacticsChangeContext.ChangeToLongRanged:
                    return "*[name] acknowledges, saying that will attack their foes by distance.*";
                case TacticsChangeContext.Nevermind:
                    return "*[name] asks if there is anything else you need.*";
                case TacticsChangeContext.FollowAhead:
                    return "*[name] acknowledges going on ahead.*";
                case TacticsChangeContext.FollowBehind:
                    return "*[name] acknowledges following from behind.*";
                case TacticsChangeContext.AvoidCombat:
                    return "*[name] says that will avoid getting in combat.*";
                case TacticsChangeContext.PartakeInCombat:
                    return "*[name] says that will take on combat again.*";
            }
            return "";
        }

        public virtual string ControlMessage(Companion companion, ControlContext context)
        {
            string s = companion.GetPersonality.GetDialogues.ControlMessage(companion, context);
            if (s != "") return s;
            switch(context)
            {
                case ControlContext.SuccessTakeControl:
                    return "*[name] links bodies.*";
                case ControlContext.SuccessReleaseControl:
                    return "*[name] released the body link.*";
                case ControlContext.FailTakeControl:
                    return "*[name] refused.*";
                case ControlContext.FailReleaseControl:
                    return "*[name] doesn't want to unlink both of you right now.*";
                case ControlContext.NotFriendsEnough:
                    return "*[name] doesn't trust you enough for that.*";
                case ControlContext.ControlChatter:
                    return "*[name] reminds you they're still in there.*";
                case ControlContext.GiveCompanionControl:
                    return "*[name] tells you that will be taking control of their actions, and that you may let them know if you want it back.*";
                case ControlContext.TakeCompanionControl:
                    return "*[name] returns control to you, and stays on standby.*";
            }
            return "";
        }

        public virtual string UnlockAlertMessages(Companion companion, UnlockAlertMessageContext context)
        {
            string s = companion.GetPersonality.GetDialogues.UnlockAlertMessages(companion, context);
            if (s != "") return s;
            switch(context)
            {
                case UnlockAlertMessageContext.MoveInUnlock:
                    return "*[name] seems to be interested in living in this world.*";
                case UnlockAlertMessageContext.FollowUnlock:
                    return "*[name] tells you that they may follow you on your quest.*";
                case UnlockAlertMessageContext.MountUnlock:
                    if (companion.MountStyle == MountStyles.CompanionRidesPlayer)
                        return "*[name] tells you that they may mount on your shoulder.*";
                    return "*[name] lets you know that you can mount on their shoulder.*";
                case UnlockAlertMessageContext.ControlUnlock:
                    return "*[name] tells you that they may try linking with you.*";
                case UnlockAlertMessageContext.RequestsUnlock:
                    return "*[name] says that you might be able to help them with their requests.*";
                case UnlockAlertMessageContext.BuddiesModeUnlock:
                    return "*[name] lets you know that you may want to be their buddy, if you want.*";
                case UnlockAlertMessageContext.BuddiesModeBenefitsMessage:
                    return "*[name] lets you know that since you two are buddies for life, they will be willing to do mostly anything you ask of them.*";
            }
            return "";
        }

        public virtual string SleepingMessage(Companion companion, SleepingMessageContext context)
        {
            string s = companion.GetPersonality.GetDialogues.SleepingMessage(companion, context);
            if (s != "") return s;
            switch(context)
            {
                case SleepingMessageContext.WhenSleeping:
                    return "*Seems to be sleeping soundly.*";
                case SleepingMessageContext.OnWokeUp:
                    return "*[name] asks why you woke "+companion.GetPronoun()+" up.*";
                case SleepingMessageContext.OnWokeUpWithRequestActive:
                    return "*[name] asks if you woke "+companion.GetPronoun()+" up because completed their request.*";
            }
            return "";
        }

        public virtual string InteractionMessages(Companion companion, InteractionMessageContext context)
        {
            string s = companion.GetPersonality.GetDialogues.InteractionMessages(companion, context);
            if (s != "") return s;
            switch(context)
            {
                case InteractionMessageContext.OnAskForFavor:
                    return "*[name] asks what you need of them.*";
                case InteractionMessageContext.Accepts:
                    return "*[name] says that will do it.*";
                case InteractionMessageContext.Rejects:
                    return "*[name] rejects.*";
                case InteractionMessageContext.Nevermind:
                    return "*[name] asks if you need something else.*";
            }
            return "";
        }

        public virtual string ChangeLeaderMessage(Companion companion, ChangeLeaderContext context)
        {
            string s = companion.GetPersonality.GetDialogues.ChangeLeaderMessage(companion, context);
            if (s != "") return s;
            switch(context)
            {
                case ChangeLeaderContext.Success:
                    return "*[name] tells you that will lead the group.*";
                case ChangeLeaderContext.Failed:
                    return "*[name] rejected leading the group.*";
            }
            return "";
        }

        public virtual string BuddiesModeMessage(Companion companion, BuddiesModeContext context)
        {
            string s = companion.GetPersonality.GetDialogues.BuddiesModeMessage(companion, context);
            if (s != "") return s;
            switch(context)
            {
                case BuddiesModeContext.AskIfPlayerIsSure:
                    return "*[name] asks if you're sure that you want to make them your buddy.*";
                case BuddiesModeContext.PlayerSaysYes:
                    return "*[name] agrees to be your buddy.*";
                case BuddiesModeContext.PlayerSaysNo:
                    return "*[name] didn't liked that.*";
                case BuddiesModeContext.NotFriendsEnough:
                    return "*[name] tells you that doesn't know you enough for that.*";
                case BuddiesModeContext.Failed:
                    return "*[name] rejects being your buddy.*";
                case BuddiesModeContext.AlreadyHasBuddy:
                    return "*[name] says that you're already someone else's buddy.*";
            }
            return "";
        }

        public virtual string InviteMessages(Companion companion, InviteContext context)
        {
            string s = companion.GetPersonality.GetDialogues.InviteMessages(companion, context);
            if (s != "") return s;
            switch(context)
            {
                case InviteContext.Success:
                    return "*[name] said that will be visiting you soon.*";
                case InviteContext.SuccessNotInTime:
                    return "*[name] said that will be visiting you next "+(companion.Base.IsNocturnal ? "night" : "day")+".*";
                case InviteContext.Failed:
                    return "*[name] refused.*";
                case InviteContext.CancelInvite:
                    return "*[name] cancels the visit.*";
                case InviteContext.ArrivalMessage:
                    return "*[name] tells you they've arrived.*";
            }
            return "";
        }

        public virtual string ReviveMessages(Companion companion, Player target, ReviveContext context)
        {
            string s = companion.GetPersonality.GetDialogues.ReviveMessages(companion, target, context);
            if (s != "") return s;
            switch(context)
            {
                case ReviveContext.HelpCallReceived:
                    return "*Someone tells you that they're coming.*";
                case ReviveContext.RevivingMessage:
                    return "*[name] is says they'll be fine.*";
                case ReviveContext.OnComingForFallenAllyNearbyMessage:
                    return "*[name] says that they're coming to help.*";
                case ReviveContext.ReachedFallenAllyMessage:
                    return "*[name] says that you'll be fine, just hang on.*";
                case ReviveContext.RevivedByItself:
                    return "*[name] says that managed to recover conscousness.*";
                case ReviveContext.ReviveWithOthersHelp:
                    return "*[name] thanks everyone for the help.*";
            }
            return "";
        }

        public virtual string GetOtherMessage(Companion companion, string Context)
        {
            string s = companion.GetPersonality.GetDialogues.GetOtherMessage(companion, Context);
            if (s != "") return s;
            return "";
        }

        public virtual void ManageOtherTopicsDialogue(Companion companion, MessageDialogue dialogue)
        {
            
        }

        public virtual void ManageLobbyTopicsDialogue(Companion companion, MessageDialogue dialogue)
        {
            
        }

        public virtual MessageBase MessageDialogueOverride(Companion companion)
        {
            return null;
        }

        #region Handy Methods
        public static bool CanTalkAboutCompanion(CompanionID ID)
        {
            return CanTalkAboutCompanion(ID.ID, ID.ModID);
        }

        public static bool CanTalkAboutCompanion(uint ID, string ModID = "")
        {
            return !PlayerMod.PlayerHasCompanionSummoned(MainMod.GetLocalPlayer, ID, ModID) && WorldMod.HasCompanionNPCSpawned(ID, ModID) && WorldMod.HasMetCompanion(ID, ModID);
        }

        public static bool HasCompanionSummoned(CompanionID ID, bool ControlledToo = true)
        {
            return HasCompanionSummoned(ID.ID, ID.ModID, ControlledToo);
        }

        public static bool HasCompanionSummoned(uint ID, string ModID = "", bool ControlledToo = false)
        {
            return PlayerMod.PlayerHasCompanionSummoned(MainMod.GetLocalPlayer, ID, ModID) && (ControlledToo || !IsControllingCompanion(ID, ModID));
        }

        public static bool HasCompanion(CompanionID ID)
        {
            return HasCompanion(ID.ID, ID.ModID);
        }

        public static bool HasCompanion(uint ID, string ModID = "")
        {
            return PlayerMod.PlayerHasCompanion(MainMod.GetLocalPlayer, ID, ModID) && !PlayerMod.PlayerHasCompanionSummoned(MainMod.GetLocalPlayer, ID, ModID);
        }

        public static bool IsControllingCompanion(CompanionID ID)
        {
            return IsControllingCompanion(ID.ID, ID.ModID);
        }

        public static bool IsControllingCompanion(uint ID, string ModID = "")
        {
            return PlayerMod.IsPlayerControllingCompanion(MainMod.GetLocalPlayer, ID, ModID);
        }

        public static bool IsPlayerRoomMate(CompanionID? ID = null)
        {
            if (!ID.HasValue)
                ID = Dialogue.Speaker.GetCompanionID;
            return PlayerMod.IsPlayerCompanionRoomMate(MainMod.GetLocalPlayer, ID.Value);
        }
        #endregion
    }

    public class MessageIDs
    {
        public const string LeopoldMessage1 = "leopoldmes1";
        public const string LeopoldMessage2 = "leopoldmes2";
        public const string LeopoldMessage3 = "leopoldmes3";
        public const string LeopoldEscapedMessage = "leopoldfleemess";
        public const string VladimirRecruitPlayerGetsHugged = "vladimirhugcomment";
    }

    [System.Flags]
    public enum UnlockAlertMessageContext : byte
    {
        None = 0,
        MoveInUnlock = 1,
        FollowUnlock = 2,
        MountUnlock = 4,
        ControlUnlock = 8,
        RequestsUnlock = 16,
        BuddiesModeUnlock = 32,
        BuddiesModeBenefitsMessage = 64
    }

    public enum ReviveContext : byte
    {
        HelpCallReceived = 0,
        RevivingMessage = 1,
        OnComingForFallenAllyNearbyMessage = 2,
        ReachedFallenAllyMessage = 3,
        RevivedByItself = 4,
        ReviveWithOthersHelp = 5
    }

    public enum InviteContext : byte
    {
        Success = 0,
        SuccessNotInTime = 1,
        Failed = 2,
        CancelInvite = 3,
        ArrivalMessage = 4
    }

    public enum MountCompanionContext : byte
    {
        Success = 0,
        SuccessMountedOnPlayer = 1,
        Fail = 2,
        NotFriendsEnough = 3,
        SuccessCompanionMount = 4,
        AskWhoToCarryMount = 5
    }

    public enum DismountCompanionContext : byte
    {
        SuccessMount = 0,
        SuccessMountOnPlayer = 1,
        Fail = 2
    }

    public enum BuddiesModeContext : byte
    {
        AskIfPlayerIsSure = 0,
        PlayerSaysYes = 1,
        PlayerSaysNo = 2,
        NotFriendsEnough = 3,
        Failed = 4,
        AlreadyHasBuddy = 5
    }

    public enum ChangeLeaderContext : byte
    {
        Success = 0,
        Failed = 1
    }

    public enum InteractionMessageContext : byte
    {
        OnAskForFavor,
        Accepts,
        Rejects,
        Nevermind
    }

    public enum SleepingMessageContext : byte
    {
        WhenSleeping,
        OnWokeUp,
        OnWokeUpWithRequestActive
    }

    public enum TacticsChangeContext : byte
    {
        OnAskToChangeTactic,
        ChangeToCloseRange,
        ChangeToMidRanged,
        ChangeToLongRanged,
        Nevermind,
        FollowAhead,
        FollowBehind,
        AvoidCombat,
        PartakeInCombat,
        AllowSubattackUsage,
        UnallowSubattackUsage
    }

    public enum TalkAboutOtherTopicsContext : byte
    {
        FirstTimeInThisDialogue,
        AfterFirstTime,
        Nevermind
    }
    
    public enum JoinMessageContext : byte
    {
        Success,
        Fail,
        FullParty
    }

    public enum LeaveMessageContext : byte
    {
        Success,
        Fail,
        AskIfSure,
        DangerousPlaceYesAnswer,
        DangerousPlaceNoAnswer
    }

    public enum MoveInContext : byte
    {
        Success,
        Fail,
        NotFriendsEnough
    }

    public enum MoveOutContext : byte
    {
        Success,
        Fail,
        NoAuthorityTo
    }

    public enum RequestContext : byte
    {
        NoRequest,
        HasRequest,
        Accepted,
        Rejected,
        Completed,
        Failed,
        TooManyRequests,
        PostponeRequest,
        AskIfRequestIsCompleted,
        RemindObjective,
        CancelRequestAskIfSure,
        CancelRequestYes,
        CancelRequestNo
    }

    public enum ControlContext : byte
    {
        SuccessTakeControl,
        SuccessReleaseControl,
        FailTakeControl,
        FailReleaseControl,
        NotFriendsEnough,
        ControlChatter,
        GiveCompanionControl,
        TakeCompanionControl
    }
}