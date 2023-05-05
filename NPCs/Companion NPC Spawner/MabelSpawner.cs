using Terraria.ModLoader;

namespace terraguardians.NPCs.CompanionNPCSpawner
{
    public class MabelSpawner : CompanionNpcSpawner
    {
        public override CompanionID ToSpawnID => new CompanionID(CompanionDB.Mabel);

        /*public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (Terraria.Main.dayTime && !Terraria.Main.eclipse && spawnInfo.PlayerInTown && CanSpawnCompanionNpc() && TargetIsPlayer(spawnInfo.Player) && PlayerMod.PlayerGetTerraGuardianCompanionsMet(spawnInfo.Player) > 0)
                return 1f / 10;
            return 0;
        }*/
    }
}