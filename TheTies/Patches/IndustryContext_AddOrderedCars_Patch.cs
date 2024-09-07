namespace TheTies.Patches;

using HarmonyLib;
using Model.OpsNew;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Track;

[HarmonyPatch]
public static class PrefabInstancerPatch
{

    static readonly Serilog.ILogger logger = Log.ForContext(typeof(PrefabInstancerPatch));

    [HarmonyTranspiler]
    [HarmonyPatch(typeof(PrefabInstancer), "OnEnable")]
    private static IEnumerable<CodeInstruction> OnEnable(IEnumerable<CodeInstruction> instructions)
    {
        var codes = new List<CodeInstruction>(instructions);

        for (int i = 0; i < codes.Count; i++)
        {
            if (codes[i].opcode == OpCodes.Ldc_I4 && codes[i].operand.Equals(32768))
            {
                logger.Information("Found ties limit, changing to {0}", 32768 * 2);
                codes[i].operand = 32768 * 2;
                continue;
            }

            if (codes[i].opcode == OpCodes.Ldc_I4 && codes[i].operand.Equals(50000))
            {
                logger.Information("Found tie plates limit, changing to {0}", 50000 * 2);
                codes[i].operand = 50000 * 2;
                continue;
            }
        }

        return codes.AsEnumerable();
    }
}
