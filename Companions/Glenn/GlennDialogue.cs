using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using System.Collections.Generic;
using terraguardians.NPCs.Hostiles;

namespace terraguardians.Companions.Glenn
{
    public class GlennDialogues : CompanionDialogueContainer
    {
        public override string GreetMessages(Companion companion)
        {
            List<string> Mes = new List<string>();
            bool SardineFollowing = HasCompanionSummoned(CompanionDB.Sardine);
            bool BreeFollowing = HasCompanionSummoned(CompanionDB.Bree);
            if (SardineFollowing && BreeFollowing)
            {
                Mes.Add("Mom! Dad! I'm glad you are okay, but why didn't you have returned home?");
            }
            else if (SardineFollowing)
            {
                Mes.Add("Hey Dad, glad to see you're safe. Have you seen Mom?");
            }
            else if (BreeFollowing)
            {
                Mes.Add("Hey Mom, you were taking too long to return. Have you found Dad?");
            }
            else
            {
                Mes.Add("Hello. Have you a Black and a White cat?");
            }
            return Mes[Main.rand.Next(Mes.Count)];
        }
        
        public override string NormalMessages(Companion companion)
        {
            List<string> Mes = new List<string>();
            bool SardineMet = HasCompanion(CompanionDB.Sardine);
            bool BreeMet = HasCompanion(CompanionDB.Bree);
            if (companion.IsUsingToilet)
            {
                Mes.Add("Aaahh! Go away! This is my private time!");
                Mes.Add("I can't really concentrate on what I'm doing with you watching me.");
                Mes.Add("I know how to use toilet, [nickname].");
            }
            else
            {
                Mes.Add("I have read too many books, so now I'm having to use glasses to see far away places.");
                Mes.Add("What kind of books do you like, [nickname]?");
                Mes.Add("Do you like games?");
                Mes.Add("[nickname], do you want something?");
                Mes.Add("Yes, [nickname]?");
                if (SardineMet && BreeMet)
                {
                    Mes.Add("I'm so glad that Mom and Dad are around. I wonder when we'll go back home.");
                    Mes.Add("I can't really recall which world we lived on. I've been in many places, and It's too much for my head.");
                    Mes.Add("Has mom and dad been arguing? They generally cooldown after a while.");
                    Mes.Add("I don't get why people consider Mom and Dad like Good and Evil. I don't think either of them are evil.");
                }
                else if (SardineMet)
                {
                    Mes.Add("After I told my dad that mom is out there looking for him, he begun searching for her too.");
                    Mes.Add("I'm glad to have dad around, but I kind of miss mom...");
                    Mes.Add("I wonder where could mom be. Even dad is looking for her.");
                }
                else if (BreeMet)
                {
                    Mes.Add("Mom told me that had no success finding dad. I hope he's fine.");
                    Mes.Add("Mom always stares at the window during the night, with sad look in her eyes. I think she's expecting dad to show up.");
                    Mes.Add("Mom has been looking for dad all around latelly.");
                }
                if (HasCompanionSummoned(CompanionDB.Bree))
                {
                    Mes.Add("Mom is kind of grumpy, buy she's not angry at you.");
                    Mes.Add("Mom seems to expect us returning home any time soon, that's why she didn't unpacked her things.");
                }
                if (HasCompanionSummoned(CompanionDB.Sardine))
                {
                    if (SardineBountyBoard.TalkedAboutBountyBoard)
                    {
                        Mes.Add("Dad is really happy with his bounty business, but It seems quite dangerous.");
                    }
                    Mes.Add("My dad is the strongest warrior I know. Or was.");
                    Mes.Add("Dad promissed mom that he would bring lots of treasures home. I didn't see any trace of that.");
                }
                if (Main.dayTime)
                {
                    if (Main.eclipse)
                    {
                        Mes.Add("(Eating popcorn, while watching the monsters running around.)");
                        Mes.Add("It feels like being in a horror movie!");
                        Mes.Add("Those monsters doesn't look that scary on the tv. Here, otherwise...");
                    }
                    else
                    {
                        if (!Main.raining)
                            Mes.Add("Enjoying the sun, [nickname]?");
                        else
                            Mes.Add("Playing in the rain, [nickname]?");
                        Mes.Add("I tried to stare at the sun once. It was a horrible idea. I don't recommend doing that though, even more if you use glasses.");
                        Mes.Add("Hey [nickname], wanna play a game?");
                    }
                }
                else
                {
                    if (Main.bloodMoon)
                    {
                        Mes.Add("Why are the women in this world so scary this night?");
                        Mes.Add("[nickname], I'm scared.");
                        Mes.Add("This night is waaaaay too scary for me!");
                        Mes.Add("Why is the moon red?");
                    }
                    else
                    {
                        Mes.Add("I'm enjoying the gentle wind of the night.");
                        Mes.Add("I could read a book during this night, or play some game.");
                        if (Main.moonPhase != 4) Mes.Add("The moon is soooooooooo brighty...");
                        else Mes.Add("Where did the moon go?");
                    }
                }
                if (Main.raining)
                {
                    Mes.Add("I can't stand rain. If I spend a few minutes in It, I start to sneeze.");
                    Mes.Add("I like the sounds of rain drops falling around.");
                    Mes.Add("The thunder sounds really scares me out. They make the fur of my entire body rise.");
                }
                if (HasCompanionSummoned(CompanionDB.Rococo))
                {
                    Mes.Add("[gn:" + CompanionDB.Rococo + "] and I like to play in the forest.");
                    Mes.Add("I understand that [gn:" + CompanionDB.Rococo + "] is really bad at hide and seek...");
                }
                if (HasCompanionSummoned(CompanionDB.Blue))
                {
                    Mes.Add("[gn:" + CompanionDB.Blue + "] is actually really nice to me. I wonder why she sometimes shows her teeth when talking to me.");
                    if (HasCompanionSummoned(CompanionDB.Sardine))
                    {
                        Mes.Add("I've seen my dad defeating many kinds of scary creatures when I was a kid, but why does he let [gn:" + CompanionDB.Blue + "] chew him?");
                        Mes.Add("Why does [gn:" + CompanionDB.Blue + "] always keeps chasing my dad? Did he do something to her?");
                        if (HasCompanionSummoned(CompanionDB.Bree))
                        {
                            Mes.Add("It's not unusual seeing mom getting into a discussion with [gn:" + CompanionDB.Blue + "], because of her chewing dad.");
                            Mes.Add("Mom always keeps looking around, whenever dad is around, and [gn:" + CompanionDB.Blue + "] is nearby.");
                        }
                    }
                }
                if (HasCompanionSummoned(CompanionDB.Zacks))
                {
                    Mes.Add("[gn:" + CompanionDB.Zacks + "] is so scary... Even his smile when looking at me freaks me out!");
                    Mes.Add("I think [gn:" + CompanionDB.Zacks + "] follows me around when I'm alone. I can hear him moving, and his groaning. I try locking myself at home when that happens.");
                    Mes.Add("The other day, [gn:" + CompanionDB.Zacks + "] surged from the floor right in front of me. I ran away soooo fast after that happened!");
                    if (HasCompanionSummoned(CompanionDB.Sardine))
                    {
                        Mes.Add("Help! My dad is being chased by [gn:" + CompanionDB.Zacks + "]!!!");
                        Mes.Add("I just saw [gn:" + CompanionDB.Zacks + "] pull my dad using something he pulled from his chest!");
                        Mes.Add("My dad keeps getting chased by [gn:" + CompanionDB.Zacks + "] about everyday. He always returns home fine, but smelly and a bit wounded.");
                        if (HasCompanionSummoned(CompanionDB.Bree))
                        {
                            Mes.Add("I told mom about dad being chased by [gn:" + CompanionDB.Zacks + "], but she looked as spooked as me!");
                        }
                    }
                }
                if (HasCompanionSummoned(CompanionDB.Fluffles))
                {
                    Mes.Add("I sometimes like having [gn:" + CompanionDB.Fluffles + "] company around.");
                    Mes.Add("The other day, I was collecting rocks in the forest, until a Demon Eye appeared. [gn:" + CompanionDB.Fluffles + "] appeared and saved me.");
                    Mes.Add("I don't feel scared when I discover that [gn:" + CompanionDB.Fluffles + "] is on my shoulder.");
                    Mes.Add("Sometimes I read books alongside [gn:" + CompanionDB.Fluffles + "].");
                    if (HasCompanionSummoned(CompanionDB.Sardine))
                    {
                        if (HasCompanionSummoned(CompanionDB.Blue))
                        {
                            Mes.Add("Everytime [gn:" + CompanionDB.Blue + "] begins chasing my dad, [gn:" + CompanionDB.Fluffles + "] joins her. Dad is really out of luck.");
                            Mes.Add("It's really hard for dad to run away from [gn:" + CompanionDB.Fluffles + "] during the day, since barelly can see her.");
                            Mes.Add("Sometimes dad arrives home after being chased, and discover that [gn:" + CompanionDB.Fluffles + "] is on his shoulder when I tell him...");
                        }
                    }
                }
                if (HasCompanionSummoned(CompanionDB.Alex))
                {
                    Mes.Add("I like playing with [gn:" + CompanionDB.Alex + "], but he sometimes asks if I want to play when I'm busy reading or playing...");
                    Mes.Add("I wonder if you play with [gn:" + CompanionDB.Alex + "] too, sometimes.");
                }
                if (HasCompanionSummoned(CompanionDB.Mabel))
                {
                    Mes.Add("Why a lot of people stops whatever they are doing, to stare [gn:" + CompanionDB.Mabel + "] when she passes through?");
                    if (NPC.AnyNPCs(Terraria.ID.NPCID.Angler))
                    {
                        Mes.Add("I don't like [nn:" + Terraria.ID.NPCID.Angler + "], but I only play with him because [gn:" + CompanionDB.Mabel + "] asked.");
                    }
                }
                if (HasCompanionSummoned(CompanionDB.Brutus))
                {
                    Mes.Add("Do you think some day I'll be as big as [gn:" + CompanionDB.Brutus + "]?");
                    Mes.Add("[gn:" + CompanionDB.Brutus + "] told me the other day that I don't need to worry, because he'll protect me. From what I heard, he also said that to many people.");
                }
                if (HasCompanionSummoned(CompanionDB.Vladimir))
                {
                    Mes.Add("At first, I thought [gn:" + CompanionDB.Vladimir + "] was a big and scary guy, until he talked to me. Now we're best friends.");
                    Mes.Add("No matter what's happening around, [gn:" + CompanionDB.Vladimir + "] is always smiling. I wonder what makes him smile so much.");
                    Mes.Add("Whenever I'm sad, I get a hug from [gn:" + CompanionDB.Vladimir + "]. I always feel better later.");
                }
                if (HasCompanionSummoned(CompanionDB.Minerva))
                {
                    Mes.Add("[gn:" + CompanionDB.Minerva + "] told me to eat lots so I can grow up strong like my parents.");
                    Mes.Add("I like the food [gn:" + CompanionDB.Minerva + "] makes, but I really dislike when my food involves broccolis.");
                }
                if (HasCompanionSummoned(CompanionDB.Malisha))
                {
                    Mes.Add("Everyone keeps telling me to stay away from [gn:" + CompanionDB.Malisha + "]'s place, but she doesn't seems like that bad of a person.");
                    Mes.Add("I saw [gn:" + CompanionDB.Malisha + "] the other day by her house, she was offering me candies. I didn't accepted them, because I just lunched.");
                }
                if (HasCompanionSummoned(CompanionDB.Green))
                {
                    Mes.Add("I don't mind visiting [gn:" + CompanionDB.Green + "] when I'm sick. Even though he looks scary, he always give me a lolipop at the end of the visit.");
                }

            }
            return Mes[Main.rand.Next(Mes.Count)];
        }

        public override string TalkMessages(Companion companion)
        {
            bool SardineMet = HasCompanionSummoned(CompanionDB.Sardine);
            bool BreeMet = HasCompanionSummoned(CompanionDB.Bree);
            List<string> Mes = new List<string>();
            Mes.Add("There's so many games I play. Which ones do you play?");
            Mes.Add("I like reading fantasy books: It feels like life.");
            Mes.Add("What kind of places have you visited? I discovered some as I travelled to here.");
            if (SardineMet && BreeMet)
            {
                Mes.Add("I fear when my parents argue, since I don't like thinking about them breaking up.");
            }
            if (SardineMet)
            {
                Mes.Add("Do you think I'll be a great adventurer, like my father?");
            }
            return Mes[Main.rand.Next(Mes.Count)];
        }

        public override string RequestMessages(Companion companion, RequestContext context)
        {
            switch(context)
            {
                case RequestContext.NoRequest:
                    return "Not yet.";
                case RequestContext.HasRequest: //[objective] tag useable for listing objective
                    return "There is something I need done, but I can't really do It right now. Could you help me with It? It's to [objective].";
                case RequestContext.Completed:
                    return "Thanks [nickname]! That was amazing!";
                case RequestContext.Accepted:
                    return "Amazing!";
                case RequestContext.TooManyRequests:
                    return "Mom and dad teached me not to get myself overloaded with many stuffs to do, maybe you shouldn't either.";
                case RequestContext.Rejected:
                    return "Aww man....";
                case RequestContext.PostponeRequest:
                    return "Not now? Oh... Fine...";
                case RequestContext.Failed:
                    return "You couldn't do It...?";
                case RequestContext.AskIfRequestIsCompleted:
                    return "Have you done my request?";
                case RequestContext.RemindObjective: //[objective] tag useable for listing objective
                    return "I asked you to [objective], remember?"; 
                case RequestContext.CancelRequestAskIfSure:
                    return "You can't do what I asked? Are you serious?!";
                case RequestContext.CancelRequestYes:
                    return "Aww... Now I will have to do It...";
                case RequestContext.CancelRequestNo:
                    return "Phew.... Then try completting It.";
            }
            return base.RequestMessages(companion, context);
        }

        public override string AskCompanionToMoveInMessage(Companion companion, MoveInContext context)
        {
            switch(context)
            {
                case MoveInContext.Success:
                    return "";
                case MoveInContext.Fail:
                    return "";
                case MoveInContext.NotFriendsEnough:
                    return "";
            }
            return base.AskCompanionToMoveInMessage(companion, context);
        }

        public override string AskCompanionToMoveOutMessage(Companion companion, MoveOutContext context)
        {
            switch(context)
            {
                case MoveOutContext.Success:
                    return "";
                case MoveOutContext.Fail:
                    return "";
                case MoveOutContext.NoAuthorityTo:
                    return "";
            }
            return base.AskCompanionToMoveOutMessage(companion, context);
        }

        public override string JoinGroupMessages(Companion companion, JoinMessageContext context)
        {
            switch(context)
            {
                case JoinMessageContext.Success:
                    return "You're calling me to go on an adventure? Yay! Let's go!";
                case JoinMessageContext.Fail:
                    return "... My parents taught me not to follow strangers..";
                case JoinMessageContext.FullParty:
                    return "There's way too many people in your group, I can't seem to fit in It.";
            }
            return base.JoinGroupMessages(companion, context);
        }

        public override string LeaveGroupMessages(Companion companion, LeaveMessageContext context)
        {
            switch(context)
            {
                case LeaveMessageContext.Success:
                    return "Awww... But It was so fun...";
                case LeaveMessageContext.Fail:
                    return "Oh, okay. Then let's continue the adventure.";
                case LeaveMessageContext.AskIfSure:
                    return "Here?! This place is dangerous! How can I get to home from here?";
                case LeaveMessageContext.DangerousPlaceYesAnswer:
                    return "W-what?! Uh... I guess.. I should try to survive my way home, then.";
                case LeaveMessageContext.DangerousPlaceNoAnswer:
                    return "";
            }
            return base.LeaveGroupMessages(companion, context);
        }

        public override string MountCompanionMessage(Companion companion, MountCompanionContext context)
        {
            switch(context)
            {
                case MountCompanionContext.Success:
                    return "";
                case MountCompanionContext.SuccessMountedOnPlayer:
                    return "";
                case MountCompanionContext.Fail:
                    return "";
                case MountCompanionContext.NotFriendsEnough:
                    return "";
                case MountCompanionContext.SuccessCompanionMount:
                    return "";
                case MountCompanionContext.AskWhoToCarryMount:
                    return "";
            }
            return base.MountCompanionMessage(companion, context);
        }

        public override string DismountCompanionMessage(Companion companion, DismountCompanionContext context)
        {
            switch(context)
            {
                case DismountCompanionContext.SuccessMount:
                    return "";
                case DismountCompanionContext.SuccessMountOnPlayer:
                    return "";
                case DismountCompanionContext.Fail:
                    return "";
            }
            return base.DismountCompanionMessage(companion, context);
        }

        public override string OnToggleShareBedsMessage(Companion companion, bool Share)
        {
            if (Share)
                return "";
            return "";
        }

        public override string OnToggleShareChairMessage(Companion companion, bool Share)
        {
            if (Share)
                return "";
            return "";
        }

        public override string SleepingMessage(Companion companion, SleepingMessageContext context)
        {
            switch(context)
            {
                case SleepingMessageContext.WhenSleeping:
                    return "";
                case SleepingMessageContext.OnWokeUp:
                    return "[nickname].. It's too late for me to stay awaken.";
                case SleepingMessageContext.OnWokeUpWithRequestActive:
                    return "..Oh... Did you do my request... Or what?";
            }
            return base.SleepingMessage(companion, context);
        }

        public override string TacticChangeMessage(Companion companion, TacticsChangeContext context)
        {
            switch(context)
            {
                case TacticsChangeContext.OnAskToChangeTactic:
                    return "";
                case TacticsChangeContext.ChangeToCloseRange:
                    return "";
                case TacticsChangeContext.ChangeToMidRanged:
                    return "";
                case TacticsChangeContext.ChangeToLongRanged:
                    return "";
                case TacticsChangeContext.Nevermind:
                    return "";
                case TacticsChangeContext.FollowAhead:
                    return "";
                case TacticsChangeContext.FollowBehind:
                    return "";
                case TacticsChangeContext.AvoidCombat:
                    return "";
                case TacticsChangeContext.PartakeInCombat:
                    return "";
            }
            return base.TacticChangeMessage(companion, context);
        }

        public override string TalkAboutOtherTopicsMessage(Companion companion, TalkAboutOtherTopicsContext context)
        {
            switch(context)
            {
                case TalkAboutOtherTopicsContext.FirstTimeInThisDialogue:
                    return "";
                case TalkAboutOtherTopicsContext.AfterFirstTime:
                    return "";
                case TalkAboutOtherTopicsContext.Nevermind:
                    return "";
            }
            return base.TalkAboutOtherTopicsMessage(companion, context);
        }

        public override string ControlMessage(Companion companion, ControlContext context)
        {
            switch(context)
            {
                case ControlContext.SuccessTakeControl:
                    return "";
                case ControlContext.SuccessReleaseControl:
                    return "";
                case ControlContext.FailTakeControl:
                    return "";
                case ControlContext.FailReleaseControl:
                    return "";
                case ControlContext.NotFriendsEnough:
                    return "";
                case ControlContext.ControlChatter:
                    return "";
                case ControlContext.GiveCompanionControl:
                    return "";
                case ControlContext.TakeCompanionControl:
                    return "";
            }
            return base.ControlMessage(companion, context);
        }

        public override string UnlockAlertMessages(Companion companion, UnlockAlertMessageContext context)
        {
            switch(context)
            {
                case UnlockAlertMessageContext.MoveInUnlock:
                    return "";
                case UnlockAlertMessageContext.ControlUnlock:
                    return "";
                case UnlockAlertMessageContext.FollowUnlock:
                    return "";
                case UnlockAlertMessageContext.MountUnlock:
                    return "";
                case UnlockAlertMessageContext.RequestsUnlock:
                    return "";
                case UnlockAlertMessageContext.BuddiesModeUnlock:
                    return "";
                case UnlockAlertMessageContext.BuddiesModeBenefitsMessage:
                    return "";
            }
            return base.UnlockAlertMessages(companion, context);
        }

        public override string InteractionMessages(Companion companion, InteractionMessageContext context)
        {
            switch(context)
            {
                case InteractionMessageContext.OnAskForFavor:
                    return "";
                case InteractionMessageContext.Accepts:
                    return "";
                case InteractionMessageContext.Rejects:
                    return "";
                case InteractionMessageContext.Nevermind:
                    return "";
            }
            return base.InteractionMessages(companion, context);
        }

        public override string ChangeLeaderMessage(Companion companion, ChangeLeaderContext context)
        {
            switch(context)
            {
                case ChangeLeaderContext.Success:
                    return "";
                case ChangeLeaderContext.Failed:
                    return "";
            }
            return "";
        }

        public override string BuddiesModeMessage(Companion companion, BuddiesModeContext context)
        {
            switch(context)
            {
                case BuddiesModeContext.AskIfPlayerIsSure:
                    return "";
                case BuddiesModeContext.PlayerSaysYes:
                    return "";
                case BuddiesModeContext.PlayerSaysNo:
                    return "";
                case BuddiesModeContext.NotFriendsEnough:
                    return "";
                case BuddiesModeContext.Failed:
                    return "";
                case BuddiesModeContext.AlreadyHasBuddy:
                    return "";
            }
            return "";
        }

        public override string InviteMessages(Companion companion, InviteContext context)
        {
            switch(context)
            {
                case InviteContext.Success:
                    return "";
                case InviteContext.SuccessNotInTime:
                    return "";
                case InviteContext.Failed:
                    return "";
                case InviteContext.CancelInvite:
                    return "";
                case InviteContext.ArrivalMessage:
                    return "";
            }
            return "";
        }

        public override string GetOtherMessage(Companion companion, string Context)
        {
            switch(Context)
            {
                case MessageIDs.LeopoldEscapedMessage:
                    return "";
                case MessageIDs.VladimirRecruitPlayerGetsHugged:
                    return "";
            }
            return base.GetOtherMessage(companion, Context);
        }

        public override string ReviveMessages(Companion companion, Player target, ReviveContext context)
        {
            switch(context)
            {
                case ReviveContext.HelpCallReceived:
                    return "I... I'll help you!";
                case ReviveContext.RevivingMessage:
                    {
                        return "Is this how I do that?";
                    }
                case ReviveContext.OnComingForFallenAllyNearbyMessage:
                    return "";
                case ReviveContext.ReachedFallenAllyMessage:
                    return "Don't worry.. I'll try to keep you safe.";
                case ReviveContext.RevivedByItself:
                    return "Ouch... Everything, from head to feet, hurts...";
                case ReviveContext.ReviveWithOthersHelp:
                    return "That was so horrible! I'm glad you guys helped me out.";
            }
            return base.ReviveMessages(companion, target, context);
        }
    }
}