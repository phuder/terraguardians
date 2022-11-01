using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.UI;
using Terraria.UI.Chat;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace terraguardians
{
    public partial class Dialogue
    {
        public static ReLogic.Graphics.DynamicSpriteFont GetDialogueFont => 
            FontAssets.MouseText.Value;

        public static bool InDialogue = false;
        private static bool HideJoinLeaveMessage = false;
        public static Companion Speaker;
        private static Companion DialogueStarterSpeaker;
        public static List<Companion> DialogueParticipants = new List<Companion>();
        public static List<TextSnippet[]> Message = null;
        public static MessageBase CurrentMessage { get; private set;}
        public static DialogueOption[] Options { get; private set; }
        private static DialogueOption[] DefaultClose = new DialogueOption[]{ new DialogueOption("Close", EndDialogue) };
        public static DialogueOption[] GetDefaultCloseDialogue { get{ return DefaultClose; } }
        public static float DistancingLeft = 0, DistancingRight = 0;

        public static void Unload()
        {
            Message = null;
            CurrentMessage = null;
            Options = null;
            Speaker = null;
            DialogueStarterSpeaker = null;
            DialogueParticipants.Clear();
            DialogueParticipants = null;
            DefaultClose = null;
            UnloadDefaultDialogues();
        }

        public static void Update()
        {
            DistancingLeft = DistancingRight = 0;
        }

        public static bool IsParticipatingDialogue(Companion companion)
        {
            return DialogueParticipants.Contains(companion);
        }

        public static void StartDialogue(Companion Target)
        {
            PlayerMod.PlayerTalkWith(Main.LocalPlayer, Target);
            DialogueStarterSpeaker = Speaker = Target;
            DialogueParticipants.Clear();
            DialogueParticipants.Add(Target);
            ChangeDialogueMessage("");
            ChangeOptions(DefaultClose);
            Main.playerInventory = false;
            InDialogue = true;
            HideJoinLeaveMessage = false;
            LobbyDialogue();
        }

        public static void AddParticipant(Companion Participant)
        {
            DialogueParticipants.Add(Participant);
        }

        //Always call after setting up message infos.
        public static void ChangeMessage(MessageBase NewMessage)
        {
            CurrentMessage = NewMessage;
            NewMessage.OnDialogueTrigger();
        }

        public static void ChangeDialogueMessage(string NewDialogue)
        {
            if(NewDialogue == "")
            {
                if(Message == null)
                    Message = new List<TextSnippet[]>();
                else
                    Message.Clear();
                return;
            }
            //MessageContainer.SetContents(NewDialogue, Color.White, CompanionDialogueInterface.DialogueWidth - 16);
            //Message = Utils.WordwrapString(NewDialogue, FontAssets.MouseText.Value, CompanionDialogueInterface.DialogueWidth, 10, out DialogueLines);
            List<List<TextSnippet>> Parsed = Utils.WordwrapStringSmart(ParseText(NewDialogue), Color.White, GetDialogueFont, CompanionDialogueInterface.DialogueWidth - 16, 10);
            Message = new List<TextSnippet[]>();
            foreach(List<TextSnippet> Text in Parsed)
            {
                Message.Add(Text.ToArray());
            }
        }

        public static void ChangeCurrentSpeaker(Companion NewSpeaker)
        {
            if(NewSpeaker == null)
                Speaker = DialogueStarterSpeaker;
            else
                Speaker = NewSpeaker;
        }

        public static void ChangeOptions(List<DialogueOption> NewOptions)
        {
            ChangeOptions(NewOptions.ToArray());
        }

        public static void ChangeOptions(DialogueOption[] NewOptions)
        {
            foreach(DialogueOption o in NewOptions) o.ParseText();
            Options = NewOptions;
        }

        public static void EndDialogue()
        {
            InDialogue = false;
            PlayerMod pm = Main.LocalPlayer.GetModPlayer<PlayerMod>();
            if(pm.TalkPlayer != null)
            {
                pm.TalkPlayer.GetModPlayer<PlayerMod>().TalkPlayer = null;
            }
            pm.TalkPlayer = null;
            Message.Clear();
            Message = null;
            Options = null;
        }

        public static string ParseText(string Text)
        {
            Text = Text
                .Replace("[name]", Speaker.GetNameColored())
                .Replace("[nickname]", Speaker.GetPlayerNickname(Main.LocalPlayer));
            string FinalMessage = "";
            string CommandType = "", CommandValue = "", CommandValue2 = "";
            string EntireCommand = "";
            byte GettingCommandStep = 0;
            const byte ParsingCommand = 1, GettingValue = 2, GettingValue2 = 3;
            PlayerMod pm = Main.player[Main.myPlayer].GetModPlayer<PlayerMod>();
            for(int i = 0; i < Text.Length; i++)
            {
                char c = Text[i];
                if (c == '[')
                {
                    GettingCommandStep = ParsingCommand;
                    CommandType = "";
                    CommandValue = "";
                    CommandValue2 = "";
                    EntireCommand = c.ToString();
                }
                else if (c == ']')
                {
                    GettingCommandStep = 0;
                    switch(CommandType)
                    {
                        case "gn":
                        case "cn":
                            {
                                uint cid = uint.Parse(CommandValue);
                                string mid = CommandValue2;
                                if(pm.HasCompanion(cid, mid))
                                {
                                    FinalMessage += pm.GetCompanionData(cid, mid).GetNameColored();
                                }
                                else
                                {
                                    FinalMessage += MainMod.GetCompanionBase(cid, mid).GetNameColored();
                                }
                            }
                            break;
                        case "name":
                            {
                                FinalMessage += Speaker.name;
                            }
                            break;
                        default:
                            {
                                FinalMessage += EntireCommand + ']';
                            }
                            break;
                    }
                }
                else if (GettingCommandStep > 0)
                {
                    if (c == ':')
                    {
                        switch(GettingCommandStep)
                        {
                            case ParsingCommand:
                                GettingCommandStep = GettingValue;
                                CommandValue = "";
                                break;
                            case GettingValue:
                                GettingCommandStep = GettingValue2;
                                CommandValue2 = "";
                                break;
                        }
                    }
                    else
                    {
                        switch(GettingCommandStep)
                        {
                            case ParsingCommand:
                                CommandType += c;
                                break;
                            case GettingValue:
                                CommandValue += c;
                                break;
                            case GettingValue2:
                                CommandValue2 += c;
                                break;
                        }
                    }
                    EntireCommand += c;
                }
                else
                {
                    FinalMessage += c;
                }
            }
            return FinalMessage;
        }
    }
}