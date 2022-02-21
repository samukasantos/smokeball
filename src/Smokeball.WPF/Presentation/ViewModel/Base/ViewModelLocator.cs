

using System;
using System.Globalization;
using System.Reflection;
using System.Windows;

namespace Smokeball.WPF.Presentation.ViewModel.Base
{
    public static class ViewModelLocator
    {
        #region Bindable Properties

        public static DependencyProperty AutoWireViewModelProperty = DependencyProperty.RegisterAttached("AutoWireViewModel", typeof(bool),
            typeof(ViewModelLocator), new PropertyMetadata(false, AutoWireViewModelChanged));

        #endregion

        #region Methods

        public static bool GetAutoWireViewModel(UIElement element)
        {
            return (bool)element.GetValue(AutoWireViewModelProperty);
        }

        public static void SetAutoWireViewModel(UIElement element, bool value)
        {
            element.SetValue(AutoWireViewModelProperty, value);
        }

        private static void AutoWireViewModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue)
            {
                Bind(d);
            }
        }

        private static void Bind(DependencyObject view)
        {
            if (view is FrameworkElement frameworkElement)
            {
                var viewModelType = FindViewModel(frameworkElement.GetType());

                var serviceProvider = (IServiceProvider)App.Current
                                .GetType()
                                .GetField("serviceProvider", BindingFlags.NonPublic | BindingFlags.Instance)
                                .GetValue(App.Current);

                frameworkElement.DataContext = serviceProvider.GetService(viewModelType);
            }
        }

        private static Type FindViewModel(Type viewType)
        {
            var viewName = viewType.FullName.Replace("View", "ViewModel");
            var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
            var viewModelName = string.Format(CultureInfo.InvariantCulture, "{0}, {1}", viewName, viewAssemblyName);

            return Type.GetType(viewModelName);
        }

        #endregion
    }
}
