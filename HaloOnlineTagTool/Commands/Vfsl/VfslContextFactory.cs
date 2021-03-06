﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HaloOnlineTagTool.Serialization;
using HaloOnlineTagTool.TagStructures;

namespace HaloOnlineTagTool.Commands.Vfsl
{
	static class VfslContextFactory
	{
		public static CommandContext Create(CommandContext parent, OpenTagCache info, HaloTag tag, VFilesList vfsl)
		{
			var context = new CommandContext(parent, string.Format("{0:X8}.vfsl", tag.Index));
			context.AddCommand(new VfslListCommand(vfsl));
			context.AddCommand(new VfslExtractCommand(vfsl));
			context.AddCommand(new VfslExtractAllCommand(vfsl));
			context.AddCommand(new VfslImportCommand(info, tag, vfsl));
			context.AddCommand(new VfslImportAllCommand(info, tag, vfsl));
			return context;
		}
	}
}
