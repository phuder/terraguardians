using System.Collections.Generic;
using Terraria;
using Terraria.UI;
using terraguardians.Interfaces;
using terraguardians.Behaviors.Orders;

namespace terraguardians.Interfaces.Orders
{
    public class MountOrders : CompanionOrderInterface.CompanionOrderStep
    {
        public override string Text => "Mount/Dismount Companion";

        public override void OnActivate()
        {
            CompanionOrderInterface.OpenCompanionSelectionStep(false);
        }

        public override void FinallyDo(List<Companion> Companions)
        {
            if (Companions.Count > 0)
            {
                Companion c = Companions[0];
                if (c.ToggleMount(MainMod.GetLocalPlayer))
                {
                    if (c.IsMountedOnSomething)
                        c.SaySomething(c.GetDialogues.MountCompanionMessage(c, MountCompanionContext.Success));
                    else
                        c.SaySomething(c.GetDialogues.DismountCompanionMessage(c, DismountCompanionContext.SuccessMount));
                }
                else
                {
                    if (c.IsMountedOnSomething)
                        c.SaySomething(c.GetDialogues.MountCompanionMessage(c, MountCompanionContext.Fail));
                    else
                        c.SaySomething(c.GetDialogues.DismountCompanionMessage(c, DismountCompanionContext.Fail));
                }
            }
        }
    }
    
    public class PlayerControlOrders : CompanionOrderInterface.CompanionOrderStep
    {
        public override string Text => "Control Companion";

        public override void OnActivate()
        {
            CompanionOrderInterface.SelectedCompanion = 0;
            CompanionOrderInterface.ExecuteOrders();
        }

        public override void FinallyDo(List<Companion> Companions)
        {
            if (Companions.Count > 0)
            {
                Companion c = Companions[0];
                if (c.TogglePlayerControl(MainMod.GetLocalPlayer))
                {
                    if (c.IsBeingControlledBySomeone)
                    {
                        c.SaySomething(c.GetDialogues.ControlMessage(c, ControlContext.SuccessTakeControl));
                    }
                    else
                    {
                        c.SaySomething(c.GetDialogues.ControlMessage(c, ControlContext.SuccessReleaseControl));
                    }
                }
                else
                {
                    if (c.IsBeingControlledBySomeone)
                    {
                        c.SaySomething(c.GetDialogues.ControlMessage(c, ControlContext.FailTakeControl));
                    }
                    else
                    {
                        c.SaySomething(c.GetDialogues.ControlMessage(c, ControlContext.FailReleaseControl));
                    }
                }
            }
        }
    }
    public class SetLeaderOrders : CompanionOrderInterface.CompanionOrderStep
    {
        public override string Text => "Set as Leader";

        public override void OnActivate()
        {
            CompanionOrderInterface.OpenCompanionSelectionStep(false);
        }

        public override void FinallyDo(List<Companion> Companions)
        {
            if (Companions.Count > 0)
            {
                Companion c = Companions[0];
                if (PlayerMod.PlayerChangeLeaderCompanion(MainMod.GetLocalPlayer, Companions[0]))
                {
                    c.SaySomething(c.GetDialogues.ChangeLeaderMessage(c, ChangeLeaderContext.Success));
                }
                else
                {
                    c.SaySomething(c.GetDialogues.ChangeLeaderMessage(c, ChangeLeaderContext.Failed));
                }
            }
        }
    }
}