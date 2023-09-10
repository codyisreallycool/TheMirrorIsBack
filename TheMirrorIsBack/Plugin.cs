using BepInEx;
using System;
using UnityEngine;
using Utilla;

namespace TheMirrorIsBack
{
    [ModdedGamemode]
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        bool inRoom;
        GameObject mirror;

        void Start()
        {
            Utilla.Events.GameInitialized += OnGameInitialized;
        }

        void OnEnable()
        {
            HarmonyPatches.ApplyHarmonyPatches();
            FindAndActivateMirror();
        }

        void OnDisable()
        {
            HarmonyPatches.RemoveHarmonyPatches();
            DeactivateMirror();
        }

        void OnGameInitialized(object sender, EventArgs e)
        {
            Transform parentTransform = GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/TreeRoomInteractables").transform;
            mirror = FindGameObjectWithNameContains(parentTransform, "mirror");
            if (mirror != null)
            {
                Logger.LogInfo("Hello! I have found the mirror and I am enabling it now! :D");
                mirror.SetActive(true);
            }
            else
            {
                Logger.LogWarning("Could not start, mirror object not found!");
            }
        }

        void Update()
        {
        }

        [ModdedGamemodeJoin]
        public void OnJoin(string gamemode)
        {
            inRoom = true;
        }

        [ModdedGamemodeLeave]
        public void OnLeave(string gamemode)
        {
            inRoom = false;
        }


        void FindAndActivateMirror()
        {
            if (mirror != null)
            {
                mirror.SetActive(true);
            }
            else
            {
                Logger.LogWarning("Could not activate mirror, object not found!");
            }
        }

        void DeactivateMirror()
        {
            if (mirror != null)
            {
                mirror.SetActive(false);
            }
            else
            {
                Logger.LogWarning("Could not deactivate mirror, object not found!");
            }
        }

        GameObject FindGameObjectWithNameContains(Transform parent, string substring)
        {
            foreach (Transform child in parent)
            {
                if (child.name.Contains(substring))
                {
                    return child.gameObject;
                }
            }
            return null;
        }
    }
}
