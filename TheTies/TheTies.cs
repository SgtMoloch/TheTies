namespace TheTies;

using HarmonyLib;
using Railloader;
using Serilog;

public class TheTies : SingletonPluginBase<TheTies>
{
    ILogger logger = Log.ForContext<TheTies>();

    public TheTies()
    {
        new Harmony("Moloch.TheTies").PatchAll(GetType().Assembly);
    }

}

