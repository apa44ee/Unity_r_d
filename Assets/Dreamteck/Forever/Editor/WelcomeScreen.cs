// Заглушка WelcomeScreen.cs, отключающая сетевые запросы Dreamteck
using UnityEditor;
using UnityEngine;

namespace Dreamteck.Splines.Editor
{
    [InitializeOnLoad]
    public static class PluginInfo
    {
        static PluginInfo()
        {
            // Отключаем FetchBanners и другие запросы
            Debug.Log("Dreamteck banners disabled.");
        }
    }
}