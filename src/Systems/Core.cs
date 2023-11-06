using Newtonsoft.Json.Linq;
using System.Linq;
using Vintagestory.API.Common;
using Vintagestory.API.Common.Entities;
using Vintagestory.API.Datastructures;

[assembly: ModInfo(name: "Harvestable Inventory Size", modID: "harvestableinventorysize")]

namespace HarvestableInventorySize;

public class Core : ModSystem
{
    public override void Start(ICoreAPI api)
    {
        base.Start(api);

        api.World.Logger.Event("started '{0}' mod", Mod.Info.Name);
    }

    public override void AssetsFinalize(ICoreAPI api)
    {
        foreach (EntityProperties entityType in api.World.EntityTypes)
        {
            if (api.Side.IsServer())
            {
                foreach (JsonObject behavior in entityType.Server.BehaviorsAsJsonObj.Where(behavior => behavior.ToString().Contains("harvestable")))
                {
                    behavior.Token["quantitySlots"] = JToken.FromObject(16);
                }
            }
            if (api.Side.IsClient())
            {
                foreach (JsonObject behavior in entityType.Client.BehaviorsAsJsonObj.Where(behavior => behavior.ToString().Contains("harvestable")))
                {
                    behavior.Token["quantitySlots"] = JToken.FromObject(16);
                }
            }
        }
    }
}