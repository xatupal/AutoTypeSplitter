using System;
using System.Collections.Generic;
using System.Drawing;
using AutoTypeSplitter.Properties;
using KeePass.Plugins;
using KeePass.Util;
using KeePass.Util.Spr;
using KeePassLib;

namespace AutoTypeSplitter
{
	public sealed class AutoTypeSplitterExt : Plugin
	{
		private const string Placeholder = "{SPLIT}";

		private int _sequenceIndex = -1;
		private string[] _sequenceParts;

		public override bool Initialize(IPluginHost host)
		{
			if (host == null)
				return false;

			SprEngine.FilterPlaceholderHints.Add(Placeholder);
			AutoType.FilterCompilePre += HandleAutoTypeFilterCompilePre;

			return true;
		}

		public override void Terminate()
		{
			AutoType.FilterCompilePre -= HandleAutoTypeFilterCompilePre;
			SprEngine.FilterPlaceholderHints.Remove(Placeholder);
		}

		private void HandleAutoTypeFilterCompilePre(object sender, AutoTypeEventArgs e)
		{
			if (_sequenceParts == null)
			{
				var newSequenceParts = e.Sequence.Split(new[] { Placeholder }, StringSplitOptions.RemoveEmptyEntries);
				if (newSequenceParts.Length == 1)
					return;

				_sequenceParts = newSequenceParts;
				_sequenceIndex = _sequenceParts.Length;
				e.Sequence = _sequenceParts[--_sequenceIndex];
				AutoType.PerformGlobal(new List<PwDatabase> { e.Database }, null);
			}
			else
			{
				e.Sequence = _sequenceParts[--_sequenceIndex];
				if (_sequenceIndex == 0)
				{
					_sequenceParts = null;
				}
				else
				{
					AutoType.PerformGlobal(new List<PwDatabase> { e.Database }, null);
				}
			}
		}

		public override string UpdateUrl
		{
			get { return "https://nibiru.pl/keepass/plugins.php?name=AutoTypeSplitter"; }
		}

		public override Image SmallIcon
		{
			get { return KeePassLib.Utility.GfxUtil.ScaleImage(Resource.Splitter, 16, 16); }
		}
	}
}
