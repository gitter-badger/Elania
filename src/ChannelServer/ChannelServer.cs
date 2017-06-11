﻿// Copyright (c) Aura development team - Licensed under GNU GPL
// For more information, see license file in the main folder

using Elania.Channel.Database;
using Elania.Channel.Network;
using Elania.Channel.Scripting;
using Elania.Channel.Util;
using Elania.Channel.World;
using Elania.Shared;
using Elania.Shared.Database;
using Elania.Shared.Network;
using Elania.Shared.Util;
using Elania.Shared.Util.Commands;
using Elania.Shared.Util.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elania.Channel
{
	public class ChannelServer : Server
	{
		public static readonly ChannelServer Instance = new ChannelServer();

		/// <summary>
		/// Login server's database.
		/// </summary>
		public ChannelDb Database { get; private set; }

		/// <summary>
		/// Channel's console commands.
		/// </summary>
		public ConsoleCommands ConsoleCommands { get; private set; }

		/// <summary>
		/// GM commands.
		/// </summary>
		public GmCommands GmCommands { get; private set; }

		/// <summary>
		/// The world~
		/// </summary>
		public WorldManager World { get; private set; }

		/// <summary>
		/// Channel's script manager.
		/// </summary>
		public ScriptManager ScriptManager { get; private set; }

		/// <summary>
		/// Connection acceptor and manager.
		/// </summary>
		public ConnectionManager<ChannelConnection> ConnectionManager { get; private set; }

		/// <summary>
		/// Creates new channel server.
		/// </summary>
		private ChannelServer()
		{
			this.World = new WorldManager();
			this.ScriptManager = new ScriptManager();
		}

		/// <summary>
		/// Starts the server.
		/// </summary>
		public override void Run()
		{
			base.Run();

			CliUtil.WriteHeader("Channel", ConsoleColor.DarkGreen);
			CliUtil.LoadingTitle();

			// Conf
			this.LoadConf();

			// Database
			this.Database = new ChannelDb();
			this.InitDatabase(this.Database);

			// Data
			this.LoadData(DataToLoad.All, true);

			// GM Commands
			this.GmCommands = new GmCommands();

			// Packet handlers
			ChannelPacketHandler.Instance.RegisterMethods();

			// World
			Log.Info("Initializing world...");
			this.World.Initialize();
			Log.Info("  done loading {0} maps.", this.World.Count);

			// Script manager
			this.ScriptManager.Initialize();
			this.ScriptManager.Load();

			// Get channel data
			var serverId = 1;
			var serverData = this.Data.ServerDb.FindChannel(serverId);
			if (serverData == null)
			{
				Log.Error("Server data not found. ({0})", serverId);
				CliUtil.Exit(1);
			}

			// Server
			this.ConnectionManager = new ConnectionManager<ChannelConnection>(serverData.Port);
			this.ConnectionManager.Start();

			// Ready
			CliUtil.RunningTitle();
			Log.Status("Server ready, listening on {0}.", this.ConnectionManager.Address);

			// Commands
			this.ConsoleCommands = new ConsoleCommands();
			this.ConsoleCommands.Wait();
		}
	}
}
