﻿using System;
using System.Threading.Tasks;
using Impostor.Api.Events;
using Impostor.Api.Events.Net;

namespace Impostor.Plugins.Example.Handlers
{
    public class PlayerEventListener : IEventListener
    {
        [EventListener]
        public void OnPlayerSpawned(PlayerSpawnedEvent e)
        {
            Console.WriteLine(e.PlayerControl.PlayerInfo.PlayerName + " spawned");
        }

        [EventListener]
        public void OnPlayerDestroyed(PlayerDestroyedEvent e)
        {
            Console.WriteLine(e.PlayerControl.PlayerInfo.PlayerName + " destroyed");
        }

        [EventListener]
        public async ValueTask OnPlayerChat(PlayerChatEvent e)
        {
            Console.WriteLine(e.PlayerControl.PlayerInfo.PlayerName + " said " + e.Message);

            if (e.Message == "test")
            {
                e.Game.Options.KillCooldown = 0;
                e.Game.Options.NumImpostors = 2;
                e.Game.Options.PlayerSpeedMod = 5;

                await e.Game.SyncSettingsAsync();
            }

            await e.PlayerControl.SetNameAsync(e.Message);
            await e.PlayerControl.SendChatAsync(e.Message);
        }
    }
}