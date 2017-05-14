namespace DaDo.Command.Common
{
    public interface ICommandLineOptions
    {
        ICommand Command { get; set; }
        GlobalOptions Globals {get;}
    }
}