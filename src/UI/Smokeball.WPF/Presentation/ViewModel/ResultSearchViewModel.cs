namespace Smokeball.WPF.Presentation.ViewModel
{
    public class ResultSearchViewModel : BaseViewModel
    {
        #region Fields

        private string title;

        #endregion

        #region Properties
        public string Title 
        {
            get => title;
            set => SetField(ref title, value);
        }

        #endregion
    }
}
