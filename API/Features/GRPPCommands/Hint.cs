namespace GRPP.API.Features.GRPPCommands;

using System;
using System.Collections.Generic;
using System.Linq;
using CommandSystem;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using GRPP.API.Attributes;
using GRPP.Extensions;
using UnityEngine;
using VoiceChat;
using VoiceChat.Networking;

[CommandHandler(typeof(RemoteAdminCommandHandler))]
public class HintWrapper : ICommand
{
    public string Command => "ghint";
    public string[] Aliases => ["grpphint", "hint", "showhint"];
    public string Description => "A simple wrapper for hints.";

    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        if (!sender.CheckRemoteAdmin(out response))
            return false;

        if (!ExPlayer.TryGet(arguments.At(0), out var player))
        {
            response = $"Could not find player '{arguments.At(0)}'.";
            return false;
        }

        if (arguments.Count >= 1)
        {
            player = ExPlayer.Get(arguments.At(0));
            player.ShowHint(arguments.Skip(0).ToString());
        }
        
        return true;
    }
}