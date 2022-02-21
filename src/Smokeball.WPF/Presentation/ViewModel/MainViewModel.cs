
using Smokeball.WPF.Application.Service.Interfaces;
using Smokeball.WPF.Presentation.ViewTemplate.Commands;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Smokeball.WPF.Presentation.ViewModel
{
    public class MainViewModel : BaseViewModel<SearchViewModel>
    {
        #region Fields

        private ICommand searchCommand;
        private ICommand closeCommand;

        private bool hasError;
        private string errorMessage;
        private ObservableCollection<ResultSearchViewModel> items;

        #endregion

        #region Properties

        public ISearchApplicationService ApplicationService { get; private set; }
        public ICommand SearchCommand => searchCommand ?? (searchCommand = new AsyncRelayCommand<SearchViewModel>(ExecuteSearchAsync, CanExecute));
        public ICommand CloseCommand => closeCommand ?? (closeCommand = new RelayCommand(Close));

        public bool HasError 
        {
            get => hasError;
            set => SetField(ref hasError, value);
        }

        public string ErrorMessage
        {
            get => errorMessage;
            set => SetField(ref errorMessage, value);
        }

        public ObservableCollection<ResultSearchViewModel> Items
        {
            get => items;
            set => SetField(ref items, value);
        }

        #endregion

        #region Constructor

        public MainViewModel(ISearchApplicationService applicationService)
        {
            this.ApplicationService = applicationService;
        }

        #endregion

        #region Methods

        private bool CanExecute(SearchViewModel viewModel)
        {
            return !IsBusy;
        }

        private async Task ExecuteSearchAsync(SearchViewModel searchViewModel) 
        {
            if (searchViewModel.IsValid()) 
            {
                IsBusy = true;

                var result = await ApplicationService.SearchAsync(searchViewModel);

                if (result.IsValid())
                {
                    Items = result.Data;
                }
                else 
                {
                    ShowError(result.Errors);
                }
            }
            else 
            {
                ShowError(searchViewModel.Errors);
            }

            IsBusy = false;
        }

        private void ShowError(IReadOnlyCollection<string> errors) 
        {
            HasError = true;
            ErrorMessage = errors.First();
        }

        private void Close()
        {
            HasError = false;
            ErrorMessage = string.Empty;
        }


        #endregion
    }
}
