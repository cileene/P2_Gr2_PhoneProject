// Assets/Editor/XCodeSettings.cs
#if UNITY_IOS
using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;

namespace Editor
{
    public static class XCodeSettings
    {
        [PostProcessBuild]
        public static void OnPostProcessBuild(BuildTarget target, string path)
        {
            if (target != BuildTarget.iOS) return;

            // ----- Info.plist edits -----
            var plistPath = Path.Combine(path, "Info.plist");
            var plist = new PlistDocument();
            plist.ReadFromFile(plistPath);
            var root = plist.root;

            // 1) Enable Files.app sharing
            root.SetBoolean("UIFileSharingEnabled", true);
            root.SetBoolean("LSSupportsOpeningDocumentsInPlace", true);

            // 1a) Allow the app to query the 'shareddocuments://' scheme (Files app deep link)
            const string scheme = "shareddocuments";
            PlistElementArray schemesArray;
            if (root.values.ContainsKey("LSApplicationQueriesSchemes"))
            {
                schemesArray = root["LSApplicationQueriesSchemes"].AsArray();
            }
            else
            {
                schemesArray = root.CreateArray("LSApplicationQueriesSchemes");
            }

            bool hasScheme = false;
            foreach (var val in schemesArray.values)
            {
                if (val.AsString() == scheme) { hasScheme = true; break; }
            }
            if (!hasScheme)
                schemesArray.AddString(scheme);

            // 2) Remove any "remote-notification" background mode
            if (root.values.ContainsKey("UIBackgroundModes"))
            {
                var array = root["UIBackgroundModes"].AsArray();
                for (int i = array.values.Count - 1; i >= 0; i--)
                {
                    if (array.values[i].AsString() == "remote-notification")
                        array.values.RemoveAt(i);
                }
                if (array.values.Count == 0)
                    root.values.Remove("UIBackgroundModes");
            }

            // 3) Strip any Push-Notification entitlement so Xcode won’t add it
            var entPlistPath = Path.Combine(path, PlayerSettings.applicationIdentifier + ".entitlements");
            if (File.Exists(entPlistPath))
            {
                var ent = new PlistDocument();
                ent.ReadFromFile(entPlistPath);
                if (ent.root.values.ContainsKey("aps-environment"))
                    ent.root.values.Remove("aps-environment");
                ent.WriteToFile(entPlistPath);
            }

            // Save changes
            plist.WriteToFile(plistPath);
        }
    }
}
#endif