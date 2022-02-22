
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Smokeball.WPF.Presentation.ViewModel
{
    public abstract class BaseViewModel<TInputViewModel> : BaseViewModel, INotifyPropertyChanged
        where TInputViewModel : class, new()
    {
        #region Fields

        private bool isBusy;
        private TInputViewModel currentModel;
        
        #endregion

        #region Properties

        public virtual TInputViewModel CurrentModel
        {
            get => currentModel;
            set => SetField(ref currentModel, value);
        }

        public bool IsBusy
        {
            get => isBusy;
            set => SetField(ref isBusy, value);
        }

        #endregion

        #region Constructor

        protected BaseViewModel()
        {
            CurrentModel = new TInputViewModel();
        }

        #endregion
    }

    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Methods

        private void RaisedPropertyChanged<T>(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void RaisedPropertyChanged<T>(Expression<Func<T>> property)
        {
            var member = property.Body as MemberExpression;

            var pInfo = member.Member as PropertyInfo;

            RaisedPropertyChanged<T>(pInfo.Name);
        }

        protected void SetField<T>(ref T field, T value, Expression<Func<T>> property)
        {
            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                field = value;

                RaisedPropertyChanged(property);
            }
        }

        protected void SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                field = value;

                RaisedPropertyChanged<T>(propertyName);
            }
        }

        #endregion
    }

}
