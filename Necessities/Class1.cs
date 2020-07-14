using System.Collections.Generic;
using Oxide.Core.Libraries.Covalence;

namespace Oxide.Plugins
{
    [Info("Necessities", "Orf1", "1.0.0")]
    [Description("Provides many essential commands for server admins.")]
    class Necessities : CovalencePlugin
    {
        List<IPlayer> bypassing = new List<IPlayer>();
        
        private void Init()
        {
            var version = "1.0.0";
            
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
            if (args.Length != 0)
            {
                IPlayer target = players.FindPlayer(args[0]);
                if (target != null)
                {
                    if (!target.IsBanned)
                    {
                        if (args.Length > 1)
                        {
                            target.Ban(args[1]);
                            player.Message("Banned player " + target.Name + "from the server for reason: " + args[1]);
                        }
                        else
                        {
                            target.Ban("You are banned from the server!");
                            player.Message("Banned player " + target.Name + "from the server with no reason specified.");
                        }
                    }
                    else
                    {
                        player.Message("Player " + target.Name + " is already banned from the server.");
                    }
                }
                else
                {
                    player.Message("Player " + args[0] + " not found.");
                }
            }
            else
            {
                player.Message("Invalid usage! /ban [player] [time] (reason)");
            }
        }
        [Command("bypass"), Permission("necessities.bypass")]
        private void BypassCommand(IPlayer IPlayer, string command, string[] args)
        {
            BasePlayer player = (BasePlayer) IPlayer;
            if (!bypassing.Contains(IPlayer))
            {
                AntiHack.ShouldIgnore(player);
                bypassing.Add(IPlayer);
                IPlayer.Message("You are now bypassing anticheat!");
            }
            else
            {
                bypassing.Remove(IPlayer);
                //Add reset anti hack here. TODO
                IPlayer.Message("You are no longer bypassing anticheat!");
            }
        }
    }
}