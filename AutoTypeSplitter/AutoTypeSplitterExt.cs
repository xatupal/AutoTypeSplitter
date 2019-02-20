using System.Drawing;
using AutoTypeSplitter.Properties;
using KeePass.Plugins;
using KeePass.Util;
using KeePass.Util.Spr;

namespace AutoTypeSplitter
{
	public sealed class AutoTypeSplitterExt : Plugin
	{
		private SequenceVisitor _sequenceVisitor;

		public override bool Initialize(IPluginHost host)
		{
			if (host == null)
				return false;

			_sequenceVisitor = new SequenceVisitor(host);

			SprEngine.FilterPlaceholderHints.Add(SequencePart.Placeholder);
			AutoType.FilterCompilePre += HandleAutoTypeFilterCompilePre;

			return true;
		}

		public override void Terminate()
		{
			AutoType.FilterCompilePre -= HandleAutoTypeFilterCompilePre;
			SprEngine.FilterPlaceholderHints.Remove(SequencePart.Placeholder);
		}

		private void HandleAutoTypeFilterCompilePre(object sender, AutoTypeEventArgs e)
		{
			_sequenceVisitor.Visit(sender, e);
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
