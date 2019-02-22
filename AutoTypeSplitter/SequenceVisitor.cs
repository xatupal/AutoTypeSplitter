using System.Windows.Forms;
using KeePass.Plugins;
using KeePass.Util;

namespace AutoTypeSplitter
{
	public class SequenceVisitor
	{
		private readonly IPluginHost _host;

		private SequencePart[] _sequenceParts;

		public SequenceVisitor(IPluginHost host)
		{
			_host = host;
		}

		public void Visit(object sender, AutoTypeEventArgs e)
		{
			if (_sequenceParts != null)
				return;

			_sequenceParts = SequencePart.Create(e.Sequence);
			if (_sequenceParts.Length == 1)
			{
				_sequenceParts = null;
				return;
			}

			foreach (var sequencePart in _sequenceParts)
			{
				if (sequencePart.Message != null)
				{
					var dialogResult = MessageBox.Show(sequencePart.Message, _host.MainWindow.Text, 
						MessageBoxButtons.OKCancel, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, 
						MessageBoxOptions.DefaultDesktopOnly);

					if (dialogResult == DialogResult.Cancel)
						break;
				}

				var success = AutoType.PerformIntoCurrentWindow(e.Entry, e.Database, sequencePart.Sequence);
				if (!success)
					break;
			}

			_sequenceParts = null;
			e.Sequence = string.Empty;
		}
	}
}