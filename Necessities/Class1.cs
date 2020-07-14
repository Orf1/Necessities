using System;
using Oxide.Core.Libraries.Covalence;

namespace Oxide.Plugins
{
    [Info("Necessities", "Orf1", "1.0.0")]
    [Description("Provides many essential commands for server admins.")]
    class Necessities : CovalencePlugin
    {
        private void Init()
        {
            String version = "1.0.0";
            permission.RegisterPermission("necessities.admin", this);
            permission.RegisterPermission("necessities.ping", this);
            Puts("Necessities loaded. Version: " + version);
            
        }

        [Command("ping"), Permission("necessities.ping")]
        private void PingCommand(IPlayer player, string command, string[] args)
        {
            player.Reply("Pong!");
        }
        [Command("broadcast"), Permission("necessities.broadcast")]
        private void BroadcastCommand(IPlayer player, string command, string[] args)
        {
            if (args.Length != 0) {
                foreach (var p in players.Connected)
                {
                    p.Message("[Broadcast] " + args);
                    
                }
            }
            else
            {
                player.Message("Invalid usage! /broadcast [message]");
            }
        }
        [Command("ban"), Permission("necessities.ban")]
        private void BanCommand(IPlayer player, string command, string[] args)
        {
            if (args.Length == 2)
            {
            }
            else
            {
                player.Message("Invalid usage! /ban [player] [time] [reason]");
            }
        }
    }
}