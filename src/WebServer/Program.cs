// Copyright (c) Aura development team - Licensed under GNU GPL
// For more information, see license file in the main folder

using Elania.Shared.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elania.Web
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				WebServer.Instance.Run();
			}
			catch (Exception ex)
			{
				Log.Error("Error on startup: {0}", ex.Message);
				CliUtil.Exit(1, true);
			}
		}
	}
}
