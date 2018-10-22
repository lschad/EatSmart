using System.Threading.Tasks;
using System.Windows.Input;

namespace SchadLucas.EatSmart.Data.Commands
{
    public interface IAsyncCommand : IAsyncCommand<object> { }

    public interface IAsyncCommand<in T> : IRaiseCanExecuteChanged
    {
        ICommand Command { get; }

        bool CanExecute(object parameter);

        Task ExecuteAsync(T parameter);
    }
}