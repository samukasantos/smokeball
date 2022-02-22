
using System.Threading.Tasks;
using System.Windows.Input;

namespace Smokeball.WPF.Presentation.ViewTemplate.Commands.Interfaces
{
    public interface IAsyncCommand : ICommand
    {
        Task ExecuteAsync();
        bool CanExecute();
    }

    public interface IAsyncCommand<TViewModel> : ICommand
    {
        Task ExecuteAsync(TViewModel parameter);
        bool CanExecute(TViewModel parameter);
    }
}
