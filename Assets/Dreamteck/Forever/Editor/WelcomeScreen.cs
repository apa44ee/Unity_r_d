// Assets/Dreamteck/Splines/Editor/WelcomeScreen.cs

using UnityEditor;
using UnityEngine;

namespace Dreamteck.Splines.Editor
{
    [InitializeOnLoad]
    public static class PluginInfo
    {
        static PluginInfo()
        {
            // Загрузка баннеров отключена
            Debug.Log("🔇 Dreamteck Splines: загрузка баннеров отключена (патч WelcomeScreen.cs)");
        }
    }
}