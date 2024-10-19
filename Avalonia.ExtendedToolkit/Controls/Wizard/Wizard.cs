using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.ExtendedToolkit.Extensions;
using Avalonia.Interactivity;
using DynamicData.Kernel;

namespace Avalonia.ExtendedToolkit.Controls
{
    //ported from https://github.com/xceedsoftware/wpftoolkit

    public partial class Wizard : ItemsControl
    {
        /// <summary>
        /// initialise items collection
        /// add some changed listener
        /// </summary>
        public Wizard()
        {
            //ItemsSource = new AvaloniaList<WizardPage>();

            CurrentPageProperty.Changed.AddClassHandler<Wizard>((Action<Wizard, AvaloniaPropertyChangedEventArgs>)((o, e) => OnCurrentPageChanged(o, e)));
            ItemsPanelProperty.Changed.AddClassHandler<Wizard>((Action<Wizard, AvaloniaPropertyChangedEventArgs>)((o,  e) => OnItemChanged(o, e)));
            //.edited
            ItemsSourceProperty.Changed.AddClassHandler<Wizard>((Action<Wizard, AvaloniaPropertyChangedEventArgs>)((o,       e) => OnItemSourceChanged(o, e)));

            Initialized += (o, e) =>
            {
               if (Items.OfType<object>().Any() && CurrentPage == null)
                    CurrentPage = Items.OfType<WizardPage>().FirstOrDefault();
            };
        }

        private void OnCurrentPageChanged(Wizard o, AvaloniaPropertyChangedEventArgs e)
        {
            OnCurrentPageChanged(e.OldValue as WizardPage, e.NewValue as WizardPage);
        }

        private void OnCurrentPageChanged(WizardPage oldValue, WizardPage newValue)
        {
            RaiseEvent(new RoutedEventArgs
            {
                RoutedEvent = Wizard.PageChangedEvent,
                Source = this
            });
        }

        private void OnItemSourceChanged(Wizard wizard, AvaloniaPropertyChangedEventArgs e)
        {
            if (Items == null)
                ItemsSource = new AvaloniaList<WizardPage>();

            if (e.NewValue == null)
                return;

            var list = e.NewValue as IEnumerable<IWizardPage>;

            if (list != null)
            {
                foreach (IWizardPage wizardPageVM in list)
                {
                    IWizardPage vm = wizardPageVM as IWizardPage;
                    WizardPage wizardPage = new WizardPage();
                    wizardPage.DataContext = vm;
                    wizardPage.CanCancel = CanCancel;
                    //.edited
                    //(ItemsSource as AvaloniaList<WizardPage>).Add(wizardPage);
                }
            }
        }

        private void OnItemChanged(Wizard o, AvaloniaPropertyChangedEventArgs e)
        {
            //.edited
            if (Items == null)
            {
                ItemsSource = new AvaloniaList<WizardPage>();
            }

            if (e.NewValue == null)
                return;

            if (Items.OfType<WizardPage>().Any() && CurrentPage == null)
                CurrentPage = Items.OfType<WizardPage>().FirstOrDefault();
        }

        /// <summary>
        /// fires can execute on the commands
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (
                    e.Property.Name == nameof(CanSelectNextPage)
                    || (e.Property.Name == nameof(CanHelp))
                    || (e.Property.Name == nameof(CanFinish))
                    || (e.Property.Name == nameof(CanCancel))
                    || (e.Property.Name == nameof(CanSelectPreviousPage))
                    || (e.Property.Name == nameof(CurrentPage))
                )
            {
                CancelCommand?.CanExecute(CanCancel);
                FinishCommand?.CanExecute(CanFinish);
                PreviousPageCommand?.CanExecute(CanSelectPreviousPage);
                NextPageCommand?.CanExecute(CanSelectNextPage);
                HelpCommand?.CanExecute(CanHelp);
                UpdateButtonState();
            }
        }

        /// <summary>
        /// if CancelButtonClosesWindow is true the parent window is closed
        /// </summary>
        private void ExecuteCancelWizard()
        {
            RaiseRoutedEvent(Wizard.CancelEvent);
            if (CancelCommand?.CanExecute(CanCancel) == true)
            {
                CancelCommand?.Execute(CanCancel);
            }
            if (CancelButtonClosesWindow)
                CloseParentWindow(false);
        }

        private bool CanExecuteCancelWizard()
        {
            bool result = false;
            if (CurrentPage != null)
            {
                if (CurrentPage.CanCancel.HasValue)
                    result = CurrentPage.CanCancel.Value;
                else
                    result = CanCancel;
            }
            return result;
        }

        private void ExecuteFinishWizard()
        {
            if (FinishCommand?.CanExecute(CanFinish) == true)
            {
                FinishCommand?.Execute(CanFinish);
            }

            var eventArgs = new RoutedEventArgs(Wizard.FinishEvent);
            RaiseEvent(eventArgs);
            //if (eventArgs.Cancel)
            //    return;

            if (FinishButtonClosesWindow)
                CloseParentWindow(true);
        }

        private bool CanExecuteFinishWizard()
        {
            bool result = false;
            if (CurrentPage != null)
            {
                if (CurrentPage.CanFinish.HasValue)
                    result = CurrentPage.CanFinish.Value;
                else
                    result = CanFinish;
            }

            return result;
        }

        private void ExecuteRequestHelp()
        {
            RaiseRoutedEvent(Wizard.HelpEvent);
            if (HelpCommand?.CanExecute(CanHelp) == true)
            {
                HelpCommand?.Execute(CanHelp);
            }
        }

        private bool CanExecuteRequestHelp()
        {
            bool result = false;
            if (CurrentPage != null)
            {
                if (CurrentPage.CanHelp.HasValue)
                    result = CurrentPage.CanHelp.Value;
                else
                    result = CanHelp;
            }
            return result;
        }

        private void ExecuteSelectNextPage()
        {
            WizardPage nextPage = null;

            if (NextPageCommand?.CanExecute(CanSelectNextPage) == true)
            {
                NextPageCommand?.Execute(CanSelectNextPage);
            }

            if (CurrentPage != null)
            {
                var eventArgs = new RoutedEventArgs(NextEvent);
                RaiseEvent(eventArgs);
                //if (eventArgs.Cancel)
                //    return;

                //check next page
                if (CurrentPage.NextPage != null)
                    nextPage = CurrentPage.NextPage;
                else
                {
                    //no next page defined use index
                    var currentIndex = Items.OfType<WizardPage>().ToList().IndexOf(CurrentPage);
                    var nextPageIndex = currentIndex + 1;
                    if (nextPageIndex < Items.OfType<WizardPage>().Count())
                        nextPage = Items.OfType<WizardPage>().ToList()[nextPageIndex];
                }
            }

            CurrentPage = nextPage;
        }

        private bool CanExecuteSelectNextPage()
        {
            bool result = false;
            if (CurrentPage != null)
            {
                if (CurrentPage.CanSelectNextPage.HasValue) //check to see if page has overriden default behavior
                {
                    if (CurrentPage.CanSelectNextPage.Value)
                        result = NextPageExists();
                }
                else if (CanSelectNextPage)
                    result = NextPageExists();
            }
            return result;
        }

        private void ExecuteSelectPreviousPage()
        {
            WizardPage previousPage = null;

            if (PreviousPageCommand?.CanExecute(CanSelectPreviousPage) == true)
            {
                PreviousPageCommand?.Execute(CanSelectPreviousPage);
            }

            if (CurrentPage != null)
            {
                var eventArgs = new RoutedEventArgs(PreviousEvent);
                RaiseEvent(eventArgs);
                //if (eventArgs.Cancel)
                //    return;

                //check previous page
                if (CurrentPage.PreviousPage != null)
                    previousPage = CurrentPage.PreviousPage;
                else
                {
                    //no previous page defined so use index
                    var currentIndex = Items.OfType<WizardPage>().ToList().IndexOf(CurrentPage);
                    var previousPageIndex = currentIndex - 1;
                    if (previousPageIndex >= 0 && previousPageIndex < Items.OfType<WizardPage>().Count())
                        previousPage = Items.OfType<WizardPage>().ToList()[previousPageIndex];
                }
            }

            CurrentPage = previousPage;
        }

        private bool CanExecuteSelectPreviousPage()
        {
            bool result = false;
            if (CurrentPage != null)
            {
                if (CurrentPage.CanSelectPreviousPage.HasValue) //check to see if page has overriden default behavior
                {
                    if (CurrentPage.CanSelectPreviousPage.Value)
                        result = PreviousPageExists();
                }
                else if (CanSelectPreviousPage)
                    result = PreviousPageExists();
            }
            return result;
        }

        private void CloseParentWindow(bool dialogResult)
        {
            Window window = this.TryFindParent<Window>();
            if (window != null)
            {
                window.Close();
            }
        }

        private bool NextPageExists()
        {
            bool exists = false;

            if (CurrentPage.NextPage != null) //check to see if a next page has been specified
                exists = true;
            else
            {
                //lets use an index to find the next page
                var currentIndex = Items.OfType<WizardPage>().ToList().IndexOf(CurrentPage);
                var nextPageIndex = currentIndex + 1;
                if (nextPageIndex < Items.OfType<WizardPage>().Count())
                    exists = true;
            }

            return exists;
        }

        private bool PreviousPageExists()
        {
            bool exists = false;

            if (CurrentPage.PreviousPage != null) //check to see if a previous page has been specified
                exists = true;
            else
            {
                //lets use an index to find the next page
                var currentIndex = Items.OfType<WizardPage>().ToList().IndexOf(CurrentPage);
                var previousPageIndex = currentIndex - 1;
                if (previousPageIndex >= 0 && previousPageIndex < Items.OfType<WizardPage>().Count())
                    exists = true;
            }

            return exists;
        }

        private void RaiseRoutedEvent(RoutedEvent routedEvent)
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(routedEvent, this);
            base.RaiseEvent(newEventArgs);
        }

        /// <summary>
        /// gets the controls from the style
        /// </summary>
        /// <param name="e"></param>
        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            btnHelp = e.NameScope.Find<Button>(ButtonName_Help);
            btnBack = e.NameScope.Find<Button>(ButtonName_Back);
            btnNext = e.NameScope.Find<Button>(ButtonName_Next);
            btnFinish = e.NameScope.Find<Button>(ButtonName_Finish);
            btnCancel = e.NameScope.Find<Button>(ButtonName_Cancel);

            btnHelp.Click += BtnHelp_Click;
            btnBack.Click += BtnBack_Click;
            btnNext.Click += BtnNext_Click;
            btnFinish.Click += BtnFinish_Click;
            btnCancel.Click += BtnCancel_Click;
            UpdateButtonState();
            base.OnApplyTemplate(e);
        }

        private void UpdateButtonState()
        {
            if (btnHelp != null)
                btnHelp.IsEnabled = CanExecuteRequestHelp();
            if (btnBack != null)
                btnBack.IsEnabled = CanExecuteSelectPreviousPage();
            if (btnNext != null)
                btnNext.IsEnabled = CanExecuteSelectNextPage();
            if (btnFinish != null)
                btnFinish.IsEnabled = CanExecuteFinishWizard();
            if (btnCancel != null)
                btnCancel.IsEnabled = CanExecuteCancelWizard();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (CanExecuteCancelWizard())
            {
                ExecuteCancelWizard();
            }
        }

        private void BtnFinish_Click(object sender, RoutedEventArgs e)
        {
            if (CanExecuteFinishWizard())
            {
                ExecuteFinishWizard();
            }
        }

        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            if (CanExecuteSelectNextPage())
            {
                ExecuteSelectNextPage();
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            if (CanExecuteSelectPreviousPage())
            {
                ExecuteSelectPreviousPage();
            }
        }

        private void BtnHelp_Click(object sender, RoutedEventArgs e)
        {
            if (CanExecuteRequestHelp())
            {
                ExecuteRequestHelp();
            }
        }
    }
}
