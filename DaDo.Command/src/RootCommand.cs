using DaDo.Command;
using DaDo.Command.Common;
using Microsoft.Extensions.CommandLineUtils;

namespace DaDo
{
  public class RootCommand : ICommand
	{
		private readonly CommandLineApplication _app;
		//---------------------------------------------------------------------
		public RootCommand(CommandLineApplication app) => _app = app;
		//---------------------------------------------------------------------
		public int Do()
		{
			_app.ShowHint();
			return 1;
		}
	}
}