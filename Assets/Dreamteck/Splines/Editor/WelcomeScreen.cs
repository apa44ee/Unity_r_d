using UnityEditor;
using UnityEngine;

namespace Dreamteck.Splines.Editor // или Dreamteck.Forever.Editor — см. путь
{
    [InitializeOnLoad]
    public static class PluginInfo
    {
        static PluginInfo()
        {
            Debug.Log("🔇 Dreamteck banners disabled.");
        }
    }
}