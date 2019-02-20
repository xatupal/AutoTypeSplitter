using System.Collections.Generic;
using System.Windows.Forms;
using KeePass.Plugins;
using KeePass.Util;
using KeePassLib;

namespace AutoTypeSplitter
{
	public class SequenceVisitor
	{
		private readonly IPluginHost _host;

		private bool _isCanceled;
		private int _sequenceIndex = -1;
		private SequencePart[] _sequenceParts;

		public SequenceVisitor(IPluginHost host)
		{
			_host = host;
		}

		public void Visit(object sender, AutoTypeEventArgs e)
		{
			if (_sequenceParts == null)
			{
				_isCanceled = false;

				var newSequenceParts = SequencePart.Create(e.Sequence);
				if (newSequenceParts.Length == 1)
					return;

				_sequenceParts = newSequenceParts;
				_sequenceIndex = _sequenceParts.Length;
			}

			var sequencePart = _sequenceParts[--_sequenceIndex];
			e.Sequence = sequencePart.Sequence;
			if (_sequenceIndex == 0)
			{
				_sequenceParts = null;
			}
			else
			{
				AutoType.PerformGlobal(new List<PwDatabase> {e.Database}, _host.MainWindow.ClientIcons);
				if (sequencePart.Message != null && !_isCanceled)
				{
					var dialogResult = MessageBox.Show(sequencePart.Message, _host.MainWindow.Text, MessageBoxButtons.OKCancel);
					if (dialogResult == DialogResult.Cancel)
						_isCanceled = true;
				}

				if (_isCanceled)
					e.Sequence = string.Empty;
			}
		}
	}
}