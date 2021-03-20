using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace MaterialDesignThemes.Wpf
{
    /// <summary>
    /// Defines how a data context is sourced for a dialog if a <see cref="FrameworkElement"/>
    /// is passed as the command parameter when using <see cref="DialogHost.OpenDialogCommand"/>.
    /// </summary>
    public enum DialogHostOpenDialogCommandDataContextSource
    {
        /// <summary>
        /// The data context from the sender element (typically a <see cref="Button"/>) 
        /// is applied to the content.
        /// </summary>
        SenderElement,
        /// <summary>
        /// The data context from the <see cref="DialogHost"/> is applied to the content.
        /// </summary>
        DialogHostInstance,
        /// <summary>
        /// The data context is explicitly set to <c>null</c>.
        /// </summary>
        None
    }

    [TemplatePart(Name = PopupPartName, Type = typeof(Popup))]
    [TemplatePart(Name = PopupPartName, Type = typeof(ContentControl))]
    [TemplatePart(Name = ContentCoverGridName, Type = typeof(Grid))]
    [TemplateVisualState(GroupName = "PopupStates", Name = OpenStateName)]
    [TemplateVisualState(GroupName = "PopupStates", Name = ClosedStateName)]
    public class DialogHost : ContentControl
    {
        public const string PopupPartName = "PART_Popup";
        public const string PopupContentPartName = "PART_PopupContentElement";
        public const string ContentCoverGridName = "PART_ContentCoverGrid";
        public const string OpenStateName = "Open";
        public const string ClosedStateName = "Closed";

        /// <summary>
        /// Routed command to be used somewhere inside an instance to trigger showing of the dialog. Content can be passed to the dialog via a <see cref="Button.CommandParameter"/>.
        /// </summary>
        public static readonly RoutedCommand OpenDialogCommand = new();
        /// <summary>
        /// Routed command to be used inside dialog content to close a dialog. Use a <see cref="Button.CommandParameter"/> to indicate the result of the parameter.
        /// </summary>
        public static readonly RoutedCommand CloseDialogCommand = new();

        private static readonly HashSet<DialogHost> LoadedInstances = new();

        private DialogOpenedEventHandler? _asyncShowOpenedEventHandler;
        private DialogClosingEventHandler? _asyncShowClosingEventHandler;
        private TaskCompletionSource<object?>? _dialogTaskCompletionSource;

        private Popup? _popup;
        private ContentControl? _popupContentControl;
        private Grid? _contentCoverGrid;
        private DialogOpenedEventHandler? _attachedDialogOpenedEventHandler;
        private DialogClosingEventHandler? _attachedDialogClosingEventHandler;
        private IInputElement? _restoreFocusDialogClose;
        private Action? _currentSnackbarMessageQueueUnPauseAction;

        static DialogHost()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DialogHost), new FrameworkPropertyMetadata(typeof(DialogHost)));
        }

        #region Show overloads

        /// <summary>
        /// Shows a modal dialog. To use, a <see cref="DialogHost"/> instance must be in a visual tree (typically this may be specified towards the root of a Window's XAML).
        /// </summary>
        /// <param name="content">Content to show (can be a control or view model).</param>
        /// <returns>Task result is the parameter used to close the dialog, typically what is passed to the <see cref="CloseDialogCommand"/> command.</returns>
        public static async Task<object?> Show(object content)
            => await Show(content, null, null);

        /// <summary>
        /// Shows a modal dialog. To use, a <see cref="DialogHost"/> instance must be in a visual tree (typically this may be specified towards the root of a Window's XAML).
        /// </summary>
        /// <param name="content">Content to show (can be a control or view model).</param>        
        /// <param name="openedEventHandler">Allows access to opened event which would otherwise have been subscribed to on a instance.</param>        
        /// <returns>Task result is the parameter used to close the dialog, typically what is passed to the <see cref="CloseDialogCommand"/> command.</returns>
        public static async Task<object?> Show(object content, DialogOpenedEventHandler openedEventHandler)
            => await Show(content, null, openedEventHandler, null);

        /// <summary>
        /// Shows a modal dialog. To use, a <see cref="DialogHost"/> instance must be in a visual tree (typically this may be specified towards the root of a Window's XAML).
        /// </summary>
        /// <param name="content">Content to show (can be a control or view model).</param>
        /// <param name="closingEventHandler">Allows access to closing event which would otherwise have been subscribed to on a instance.</param>
        /// <returns>Task result is the parameter used to close the dialog, typically what is passed to the <see cref="CloseDialogCommand"/> command.</returns>
        public static async Task<object?> Show(object content, DialogClosingEventHandler closingEventHandler)
            => await Show(content, null, null, closingEventHandler);

        /// <summary>
        /// Shows a modal dialog. To use, a <see cref="DialogHost"/> instance must be in a visual tree (typically this may be specified towards the root of a Window's XAML).
        /// </summary>
        /// <param name="content">Content to show (can be a control or view model).</param>        
        /// <param name="openedEventHandler">Allows access to opened event which would otherwise have been subscribed to on a instance.</param>
        /// <param name="closingEventHandler">Allows access to closing event which would otherwise have been subscribed to on a instance.</param>
        /// <returns>Task result is the parameter used to close the dialog, typically what is passed to the <see cref="CloseDialogCommand"/> command.</returns>
        public static async Task<object?> Show(object content, DialogOpenedEventHandler? openedEventHandler, DialogClosingEventHandler? closingEventHandler)
            => await Show(content, null, openedEventHandler, closingEventHandler);

        /// <summary>
        /// Shows a modal dialog. To use, a <see cref="DialogHost"/> instance must be in a visual tree (typically this may be specified towards the root of a Window's XAML).
        /// </summary>
        /// <param name="content">Content to show (can be a control or view model).</param>
        /// <param name="dialogIdentifier"><see cref="Identifier"/> of the instance where the dialog should be shown. Typically this will match an identifer set in XAML. <c>null</c> is allowed.</param>
        /// <returns>Task result is the parameter used to close the dialog, typically what is passed to the <see cref="CloseDialogCommand"/> command.</returns>
        public static async Task<object?> Show(object content, object dialogIdentifier)
            => await Show(content, dialogIdentifier, null, null);

        /// <summary>
        /// Shows a modal dialog. To use, a <see cref="DialogHost"/> instance must be in a visual tree (typically this may be specified towards the root of a Window's XAML).
        /// </summary>
        /// <param name="content">Content to show (can be a control or view model).</param>
        /// <param name="dialogIdentifier"><see cref="Identifier"/> of the instance where the dialog should be shown. Typically this will match an identifer set in XAML. <c>null</c> is allowed.</param>
        /// <param name="openedEventHandler">Allows access to opened event which would otherwise have been subscribed to on a instance.</param>
        /// <returns>Task result is the parameter used to close the dialog, typically what is passed to the <see cref="CloseDialogCommand"/> command.</returns>
        public static Task<object?> Show(object content, object dialogIdentifier, DialogOpenedEventHandler openedEventHandler)
            => Show(content, dialogIdentifier, openedEventHandler, null);

        /// <summary>
        /// Shows a modal dialog. To use, a <see cref="DialogHost"/> instance must be in a visual tree (typically this may be specified towards the root of a Window's XAML).
        /// </summary>
        /// <param name="content">Content to show (can be a control or view model).</param>
        /// <param name="dialogIdentifier"><see cref="Identifier"/> of the instance where the dialog should be shown. Typically this will match an identifer set in XAML. <c>null</c> is allowed.</param>        
        /// <param name="closingEventHandler">Allows access to closing event which would otherwise have been subscribed to on a instance.</param>
        /// <returns>Task result is the parameter used to close the dialog, typically what is passed to the <see cref="CloseDialogCommand"/> command.</returns>
        public static Task<object?> Show(object content, object dialogIdentifier, DialogClosingEventHandler closingEventHandler)
            => Show(content, dialogIdentifier, null, closingEventHandler);

        /// <summary>
        /// Shows a modal dialog. To use, a <see cref="DialogHost"/> instance must be in a visual tree (typically this may be specified towards the root of a Window's XAML).
        /// </summary>
        /// <param name="content">Content to show (can be a control or view model).</param>
        /// <param name="dialogIdentifier"><see cref="Identifier"/> of the instance where the dialog should be shown. Typically this will match an identifer set in XAML. <c>null</c> is allowed.</param>
        /// <param name="openedEventHandler">Allows access to opened event which would otherwise have been subscribed to on a instance.</param>
        /// <param name="closingEventHandler">Allows access to closing event which would otherwise have been subscribed to on a instance.</param>
        /// <returns>Task result is the parameter used to close the dialog, typically what is passed to the <see cref="CloseDialogCommand"/> command.</returns>
        public static async Task<object?> Show(object content, object? dialogIdentifier, DialogOpenedEventHandler? openedEventHandler, DialogClosingEventHandler? closingEventHandler)
        {
            if (content is null) throw new ArgumentNullException(nameof(content));
            return await GetInstance(dialogIdentifier).ShowInternal(content, openedEventHandler, closingEventHandler);
        }

        /// <summary>
        ///  Close a modal dialog.
        /// </summary>
        /// <param name="dialogIdentifier"> of the instance where the dialog should be closed. Typically this will match an identifer set in XAML. </param>
        public static void Close(object dialogIdentifier)
            => Close(dialogIdentifier, null);

        /// <summary>
        ///  Close a modal dialog.
        /// </summary>
        /// <param name="dialogIdentifier"> of the instance where the dialog should be closed. Typically this will match an identifer set in XAML. </param>
        /// <param name="parameter"> to provide to close handler</param>
        public static void Close(object? dialogIdentifier, object? parameter)
        {
            DialogHost dialogHost = GetInstance(dialogIdentifier);
            if (dialogHost.CurrentSession is { } currentSession)
            {
                currentSession.Close(parameter);
                return;
            }
            throw new InvalidOperationException("DialogHost is not open.");
        }

        /// <summary>
        /// Retrieve the current dialog session for a DialogHost
        /// </summary>
        /// <param name="dialogIdentifier">The identifier to use to retrieve the DialogHost</param>
        /// <returns>The DialogSession if one is in process, or null</returns>
        public static DialogSession? GetDialogSession(object dialogIdentifier)
        {
            DialogHost dialogHost = GetInstance(dialogIdentifier);
            return dialogHost.CurrentSession;
        }

        /// <summary>
        /// dialog instance exists
        /// </summary>
        /// <param name="dialogIdentifier">of the instance where the dialog should be closed. Typically this will match an identifer set in XAML.</param>
        /// <returns></returns>
        public static bool IsDialogOpen(object dialogIdentifier) => GetDialogSession(dialogIdentifier)?.IsEnded == false;

        private static DialogHost GetInstance(object? dialogIdentifier)
        {
            if (LoadedInstances.Count == 0)
                throw new InvalidOperationException("No loaded DialogHost instances.");
            LoadedInstances.First().Dispatcher.VerifyAccess();

            var targets = LoadedInstances.Where(dh => dialogIdentifier == null || Equals(dh.Identifier, dialogIdentifier)).ToList();
            if (targets.Count == 0)
                throw new InvalidOperationException($"No loaded DialogHost have an {nameof(Identifier)} property matching {nameof(dialogIdentifier)} ('{dialogIdentifier}') argument.");
            if (targets.Count > 1)
                throw new InvalidOperationException("Multiple viable DialogHosts. Specify a unique Identifier on each DialogHost, especially where multiple Windows are a concern.");

            return targets[0];
        }

        internal async Task<object?> ShowInternal(object content, DialogOpenedEventHandler? openedEventHandler, DialogClosingEventHandler? closingEventHandler)
        {
            if (IsOpen)
                throw new InvalidOperationException("DialogHost is already open.");

            _dialogTaskCompletionSource = new TaskCompletionSource<object?>();

            AssertTargetableContent();

            if (content != null)
                DialogContent = content;

            _asyncShowOpenedEventHandler = openedEventHandler;
            _asyncShowClosingEventHandler = closingEventHandler;
            SetCurrentValue(IsOpenProperty, true);

            object? result = await _dialogTaskCompletionSource.Task;

            _asyncShowOpenedEventHandler = null;
            _asyncShowClosingEventHandler = null;

            return result;
        }

        #endregion

        public DialogHost()
        {
            Loaded += OnLoaded;
            Unloaded += OnUnloaded;

            CommandBindings.Add(new CommandBinding(CloseDialogCommand, CloseDialogHandler, CloseDialogCanExecute));
            CommandBindings.Add(new CommandBinding(OpenDialogCommand, OpenDialogHandler));
        }

        public static readonly DependencyProperty IdentifierProperty = DependencyProperty.Register(
            nameof(Identifier), typeof(object), typeof(DialogHost), new PropertyMetadata(default(object)));

        /// <summary>
        /// Identifier which is used in conjunction with <see cref="Show(object)"/> to determine where a dialog should be shown.
        /// </summary>
        public object? Identifier
        {
            get => GetValue(IdentifierProperty);
            set => SetValue(IdentifierProperty, value);
        }

        public static readonly DependencyProperty IsOpenProperty = DependencyProperty.Register(
            nameof(IsOpen), typeof(bool), typeof(DialogHost), new FrameworkPropertyMetadata(default(bool), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, IsOpenPropertyChangedCallback));

        private static void IsOpenPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var dialogHost = (DialogHost)dependencyObject;

            if (dialogHost._popupContentControl != null)
                ValidationAssist.SetSuppress(dialogHost._popupContentControl, !dialogHost.IsOpen);
            VisualStateManager.GoToState(dialogHost, dialogHost.SelectState(), !TransitionAssist.GetDisableTransitions(dialogHost));

            if (dialogHost.IsOpen)
            {
                dialogHost._currentSnackbarMessageQueueUnPauseAction = dialogHost.SnackbarMessageQueue?.Pause();
            }
            else
            {
                dialogHost._attachedDialogClosingEventHandler = null;
                if (dialogHost._currentSnackbarMessageQueueUnPauseAction != null)
                {
                    dialogHost._currentSnackbarMessageQueueUnPauseAction();
                    dialogHost._currentSnackbarMessageQueueUnPauseAction = null;
                }

                object? closeParameter = null;
                if (dialogHost.CurrentSession is { } session)
                {
                    closeParameter = session.CloseParameter;
                    session.IsEnded = true;
                    dialogHost.CurrentSession = null;
                }

                //NB: _dialogTaskCompletionSource is only set in the case where the dialog is shown with Show
                dialogHost._dialogTaskCompletionSource?.TrySetResult(closeParameter);

                // Don't attempt to Invoke if _restoreFocusDialogClose hasn't been assigned yet. Can occur
                // if the MainWindow has started up minimized. Even when Show() has been called, this doesn't
                // seem to have been set.
                dialogHost.Dispatcher.InvokeAsync(() => dialogHost._restoreFocusDialogClose?.Focus(), DispatcherPriority.Input);

                return;
            }

            dialogHost.CurrentSession = new DialogSession(dialogHost);
            var window = Window.GetWindow(dialogHost);
            dialogHost._restoreFocusDialogClose = window != null ? FocusManager.GetFocusedElement(window) : null;

            //multiple ways of calling back that the dialog has opened:
            // * routed event
            // * the attached property (which should be applied to the button which opened the dialog
            // * straight forward dependency property 
            // * handler provided to the async show method
            var dialogOpenedEventArgs = new DialogOpenedEventArgs(dialogHost.CurrentSession, DialogOpenedEvent);
            dialogHost.OnDialogOpened(dialogOpenedEventArgs);
            dialogHost._attachedDialogOpenedEventHandler?.Invoke(dialogHost, dialogOpenedEventArgs);
            dialogHost.DialogOpenedCallback?.Invoke(dialogHost, dialogOpenedEventArgs);
            dialogHost._asyncShowOpenedEventHandler?.Invoke(dialogHost, dialogOpenedEventArgs);

            dialogHost.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
            {
                CommandManager.InvalidateRequerySuggested();
                UIElement? child = dialogHost.FocusPopup();

                if (child != null)
                {
                    //https://github.com/ButchersBoy/MaterialDesignInXamlToolkit/issues/187
                    //totally not happy about this, but on immediate validation we can get some weird looking stuff...give WPF a kick to refresh...
                    Task.Delay(300).ContinueWith(t => child.Dispatcher.BeginInvoke(new Action(() => child.InvalidateVisual())));
                }
            }));
        }

        /// <summary>
        /// Returns a DialogSession for the currently open dialog for managing it programmatically. If no dialog is open, CurrentSession will return null
        /// </summary>
        public DialogSession? CurrentSession { get; private set; }

        public bool IsOpen
        {
            get => (bool)GetValue(IsOpenProperty);
            set => SetValue(IsOpenProperty, value);
        }

        public static readonly DependencyProperty DialogContentProperty = DependencyProperty.Register(
            nameof(DialogContent), typeof(object), typeof(DialogHost), new PropertyMetadata(default(object)));

        public object? DialogContent
        {
            get => GetValue(DialogContentProperty);
            set => SetValue(DialogContentProperty, value);
        }

        public static readonly DependencyProperty DialogContentTemplateProperty = DependencyProperty.Register(
            nameof(DialogContentTemplate), typeof(DataTemplate), typeof(DialogHost), new PropertyMetadata(default(DataTemplate)));

        public DataTemplate? DialogContentTemplate
        {
            get => (DataTemplate?)GetValue(DialogContentTemplateProperty);
            set => SetValue(DialogContentTemplateProperty, value);
        }

        public static readonly DependencyProperty DialogContentTemplateSelectorProperty = DependencyProperty.Register(
            nameof(DialogContentTemplateSelector), typeof(DataTemplateSelector), typeof(DialogHost), new PropertyMetadata(default(DataTemplateSelector)));

        public DataTemplateSelector? DialogContentTemplateSelector
        {
            get => (DataTemplateSelector?)GetValue(DialogContentTemplateSelectorProperty);
            set => SetValue(DialogContentTemplateSelectorProperty, value);
        }

        public static readonly DependencyProperty DialogContentStringFormatProperty = DependencyProperty.Register(
            nameof(DialogContentStringFormat), typeof(string), typeof(DialogHost), new PropertyMetadata(default(string)));

        public string? DialogContentStringFormat
        {
            get => (string?)GetValue(DialogContentStringFormatProperty);
            set => SetValue(DialogContentStringFormatProperty, value);
        }

        public static readonly DependencyProperty DialogMarginProperty = DependencyProperty.Register(
            "DialogMargin", typeof(Thickness), typeof(DialogHost), new PropertyMetadata(default(Thickness)));

        public Thickness DialogMargin
        {
            get => (Thickness)GetValue(DialogMarginProperty);
            set => SetValue(DialogMarginProperty, value);
        }

        public static readonly DependencyProperty OpenDialogCommandDataContextSourceProperty = DependencyProperty.Register(
            nameof(OpenDialogCommandDataContextSource), typeof(DialogHostOpenDialogCommandDataContextSource), typeof(DialogHost), new PropertyMetadata(default(DialogHostOpenDialogCommandDataContextSource)));

        /// <summary>
        /// Defines how a data context is sourced for a dialog if a <see cref="FrameworkElement"/>
        /// is passed as the command parameter when using <see cref="DialogHost.OpenDialogCommand"/>.
        /// </summary>
        public DialogHostOpenDialogCommandDataContextSource OpenDialogCommandDataContextSource
        {
            get => (DialogHostOpenDialogCommandDataContextSource)GetValue(OpenDialogCommandDataContextSourceProperty);
            set => SetValue(OpenDialogCommandDataContextSourceProperty, value);
        }

        public static readonly DependencyProperty CloseOnClickAwayProperty = DependencyProperty.Register(
            "CloseOnClickAway", typeof(bool), typeof(DialogHost), new PropertyMetadata(default(bool)));

        /// <summary>
        /// Indicates whether the dialog will close if the user clicks off the dialog, on the obscured background.
        /// </summary>
        public bool CloseOnClickAway
        {
            get => (bool)GetValue(CloseOnClickAwayProperty);
            set => SetValue(CloseOnClickAwayProperty, value);
        }

        public static readonly DependencyProperty CloseOnClickAwayParameterProperty = DependencyProperty.Register(
            "CloseOnClickAwayParameter", typeof(object), typeof(DialogHost), new PropertyMetadata(default(object)));

        /// <summary>
        /// Parameter to provide to close handlers if an close due to click away is instigated.
        /// </summary>
        public object? CloseOnClickAwayParameter
        {
            get => GetValue(CloseOnClickAwayParameterProperty);
            set => SetValue(CloseOnClickAwayParameterProperty, value);
        }

        public static readonly DependencyProperty SnackbarMessageQueueProperty = DependencyProperty.Register(
            "SnackbarMessageQueue", typeof(SnackbarMessageQueue), typeof(DialogHost), new PropertyMetadata(default(SnackbarMessageQueue), SnackbarMessageQueuePropertyChangedCallback));

        private static void SnackbarMessageQueuePropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var dialogHost = (DialogHost)dependencyObject;
            if (dialogHost._currentSnackbarMessageQueueUnPauseAction != null)
            {
                dialogHost._currentSnackbarMessageQueueUnPauseAction();
                dialogHost._currentSnackbarMessageQueueUnPauseAction = null;
            }

            if (!dialogHost.IsOpen) return;
            var snackbarMessageQueue = dependencyPropertyChangedEventArgs.NewValue as SnackbarMessageQueue;
            dialogHost._currentSnackbarMessageQueueUnPauseAction = snackbarMessageQueue?.Pause();
        }

        /// <summary>
        /// Allows association of a snackbar, so that notifications can be paused whilst a dialog is being displayed.
        /// </summary>
        public SnackbarMessageQueue? SnackbarMessageQueue
        {
            get => (SnackbarMessageQueue?)GetValue(SnackbarMessageQueueProperty);
            set => SetValue(SnackbarMessageQueueProperty, value);
        }

        public static readonly DependencyProperty DialogThemeProperty =
            DependencyProperty.Register(nameof(DialogTheme), typeof(BaseTheme), typeof(DialogHost), new PropertyMetadata(default(BaseTheme)));

        /// <summary>
        /// Set the theme (light/dark) for the dialog.
        /// </summary>
        public BaseTheme DialogTheme
        {
            get => (BaseTheme)GetValue(DialogThemeProperty);
            set => SetValue(DialogThemeProperty, value);
        }

        public static readonly DependencyProperty PopupStyleProperty = DependencyProperty.Register(
            nameof(PopupStyle), typeof(Style), typeof(DialogHost), new PropertyMetadata(default(Style)));

        public Style? PopupStyle
        {
            get => (Style?)GetValue(PopupStyleProperty);
            set => SetValue(PopupStyleProperty, value);
        }

        public static readonly DependencyProperty OverlayBackgroundProperty = DependencyProperty.Register(
            nameof(OverlayBackground), typeof(Brush), typeof(DialogHost), new PropertyMetadata(Brushes.Black));

        /// <summary>
        /// Represents the overlay brush that is used to dim the background behind the dialog
        /// </summary>
        public Brush? OverlayBackground
        {
            get => (Brush?)GetValue(OverlayBackgroundProperty);
            set => SetValue(OverlayBackgroundProperty, value);
        }

        public static readonly DependencyProperty ContentBackgroundProperty = DependencyProperty.Register(
            nameof(ContentBackground), typeof(Brush), typeof(DialogHost), new PropertyMetadata(Brushes.White));

        /// <summary>
        /// Represents the overlay brush that is used to dim the background behind the dialog
        /// </summary>
        public Brush? ContentBackground
        {
            get => (Brush?)GetValue(ContentBackgroundProperty);
            set => SetValue(ContentBackgroundProperty, value);
        }

        public override void OnApplyTemplate()
        {
            if (_contentCoverGrid != null)
                _contentCoverGrid.MouseLeftButtonUp -= ContentCoverGridOnMouseLeftButtonUp;

            _popup = GetTemplateChild(PopupPartName) as Popup;
            _popupContentControl = GetTemplateChild(PopupContentPartName) as ContentControl;
            _contentCoverGrid = GetTemplateChild(ContentCoverGridName) as Grid;

            if (_contentCoverGrid != null)
                _contentCoverGrid.MouseLeftButtonUp += ContentCoverGridOnMouseLeftButtonUp;

            VisualStateManager.GoToState(this, SelectState(), false);

            base.OnApplyTemplate();
        }

        #region open dialog events/callbacks

        public static readonly RoutedEvent DialogOpenedEvent =
            EventManager.RegisterRoutedEvent(
                "DialogOpened",
                RoutingStrategy.Bubble,
                typeof(DialogOpenedEventHandler),
                typeof(DialogHost));

        /// <summary>
        /// Raised when a dialog is opened.
        /// </summary>
        public event DialogOpenedEventHandler DialogOpened
        {
            add { AddHandler(DialogOpenedEvent, value); }
            remove { RemoveHandler(DialogOpenedEvent, value); }
        }

        /// <summary>
        /// Attached property which can be used on the <see cref="Button"/> which instigated the <see cref="OpenDialogCommand"/> to process the event.
        /// </summary>
        public static readonly DependencyProperty DialogOpenedAttachedProperty = DependencyProperty.RegisterAttached(
            "DialogOpenedAttached", typeof(DialogOpenedEventHandler), typeof(DialogHost), new PropertyMetadata(default(DialogOpenedEventHandler)));

        public static void SetDialogOpenedAttached(DependencyObject element, DialogOpenedEventHandler value)
        {
            element.SetValue(DialogOpenedAttachedProperty, value);
        }

        public static DialogOpenedEventHandler GetDialogOpenedAttached(DependencyObject element)
        {
            return (DialogOpenedEventHandler)element.GetValue(DialogOpenedAttachedProperty);
        }

        public static readonly DependencyProperty DialogOpenedCallbackProperty = DependencyProperty.Register(
            nameof(DialogOpenedCallback), typeof(DialogOpenedEventHandler), typeof(DialogHost), new PropertyMetadata(default(DialogOpenedEventHandler)));

        /// <summary>
        /// Callback fired when the <see cref="DialogOpened"/> event is fired, allowing the event to be processed from a binding/view model.
        /// </summary>
        public DialogOpenedEventHandler? DialogOpenedCallback
        {
            get => (DialogOpenedEventHandler?)GetValue(DialogOpenedCallbackProperty);
            set => SetValue(DialogOpenedCallbackProperty, value);
        }

        protected void OnDialogOpened(DialogOpenedEventArgs eventArgs)
        {
            RaiseEvent(eventArgs);
        }

        #endregion

        #region close dialog events/callbacks

        public static readonly RoutedEvent DialogClosingEvent =
            EventManager.RegisterRoutedEvent(
                "DialogClosing",
                RoutingStrategy.Bubble,
                typeof(DialogClosingEventHandler),
                typeof(DialogHost));

        /// <summary>
        /// Raised just before a dialog is closed.
        /// </summary>
        public event DialogClosingEventHandler DialogClosing
        {
            add { AddHandler(DialogClosingEvent, value); }
            remove { RemoveHandler(DialogClosingEvent, value); }
        }

        /// <summary>
        /// Attached property which can be used on the <see cref="Button"/> which instigated the <see cref="OpenDialogCommand"/> to process the closing event.
        /// </summary>
        public static readonly DependencyProperty DialogClosingAttachedProperty = DependencyProperty.RegisterAttached(
            "DialogClosingAttached", typeof(DialogClosingEventHandler), typeof(DialogHost), new PropertyMetadata(default(DialogClosingEventHandler)));

        public static void SetDialogClosingAttached(DependencyObject element, DialogClosingEventHandler value)
        {
            element.SetValue(DialogClosingAttachedProperty, value);
        }

        public static DialogClosingEventHandler GetDialogClosingAttached(DependencyObject element)
        {
            return (DialogClosingEventHandler)element.GetValue(DialogClosingAttachedProperty);
        }

        public static readonly DependencyProperty DialogClosingCallbackProperty = DependencyProperty.Register(
            nameof(DialogClosingCallback), typeof(DialogClosingEventHandler), typeof(DialogHost), new PropertyMetadata(default(DialogClosingEventHandler)));

        /// <summary>
        /// Callback fired when the <see cref="DialogClosing"/> event is fired, allowing the event to be processed from a binding/view model.
        /// </summary>
        public DialogClosingEventHandler? DialogClosingCallback
        {
            get => (DialogClosingEventHandler?)GetValue(DialogClosingCallbackProperty);
            set => SetValue(DialogClosingCallbackProperty, value);
        }

        protected void OnDialogClosing(DialogClosingEventArgs eventArgs)
        {
            RaiseEvent(eventArgs);
        }

        #endregion

        internal void AssertTargetableContent()
        {
            var existingBinding = BindingOperations.GetBindingExpression(this, DialogContentProperty);
            if (existingBinding != null)
                throw new InvalidOperationException(
                    "Content cannot be passed to a dialog via the OpenDialog if DialogContent already has a binding.");
        }

        internal void InternalClose(object? parameter)
        {
            var currentSession = CurrentSession ?? throw new InvalidOperationException($"{nameof(DialogHost)} does not have a current session");
            var dialogClosingEventArgs = new DialogClosingEventArgs(currentSession, DialogClosingEvent);

            currentSession.CloseParameter = parameter;
            currentSession.IsEnded = true;

            //multiple ways of calling back that the dialog is closing:
            // * routed event
            // * the attached property (which should be applied to the button which opened the dialog
            // * straight forward dependency property 
            // * handler provided to the async show method
            OnDialogClosing(dialogClosingEventArgs);
            _attachedDialogClosingEventHandler?.Invoke(this, dialogClosingEventArgs);
            DialogClosingCallback?.Invoke(this, dialogClosingEventArgs);
            _asyncShowClosingEventHandler?.Invoke(this, dialogClosingEventArgs);

            if (dialogClosingEventArgs.IsCancelled)
            {
                currentSession.IsEnded = false;
                return;
            }

            SetCurrentValue(IsOpenProperty, false);
        }

        /// <summary>
        /// Attempts to focus the content of a popup.
        /// </summary>
        /// <returns>The popup content.</returns>
        internal UIElement? FocusPopup()
        {
            var child = _popup?.Child ?? _popupContentControl;
            if (child is null) return null;

            CommandManager.InvalidateRequerySuggested();
            var focusable = child.VisualDepthFirstTraversal().OfType<UIElement>().FirstOrDefault(ui => ui.Focusable && ui.IsVisible);
            focusable?.Dispatcher.InvokeAsync(() =>
            {
                if (!focusable.Focus()) return;
                focusable.MoveFocus(new TraversalRequest(FocusNavigationDirection.First));
            }, DispatcherPriority.Background);

            return child;
        }

        protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
        {
            var window = Window.GetWindow(this);
            if (window != null && !window.IsActive)
                window.Activate();
            base.OnPreviewMouseDown(e);
        }

        private void ContentCoverGridOnMouseLeftButtonUp(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            if (CloseOnClickAway && CurrentSession != null)
                InternalClose(CloseOnClickAwayParameter);
        }

        private void OpenDialogHandler(object sender, ExecutedRoutedEventArgs executedRoutedEventArgs)
        {
            if (executedRoutedEventArgs.Handled) return;

            if (executedRoutedEventArgs.OriginalSource is DependencyObject dependencyObject)
            {
                _attachedDialogOpenedEventHandler = GetDialogOpenedAttached(dependencyObject);
                _attachedDialogClosingEventHandler = GetDialogClosingAttached(dependencyObject);
            }

            if (executedRoutedEventArgs.Parameter != null)
            {
                AssertTargetableContent();

                if (_popupContentControl != null)
                {
                    switch (OpenDialogCommandDataContextSource)
                    {
                        case DialogHostOpenDialogCommandDataContextSource.SenderElement:
                            _popupContentControl.DataContext =
                                (executedRoutedEventArgs.OriginalSource as FrameworkElement)?.DataContext;
                            break;
                        case DialogHostOpenDialogCommandDataContextSource.DialogHostInstance:
                            _popupContentControl.DataContext = DataContext;
                            break;
                        case DialogHostOpenDialogCommandDataContextSource.None:
                            _popupContentControl.DataContext = null;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }

                DialogContent = executedRoutedEventArgs.Parameter;
            }

            SetCurrentValue(IsOpenProperty, true);

            executedRoutedEventArgs.Handled = true;
        }

        private void CloseDialogCanExecute(object sender, CanExecuteRoutedEventArgs canExecuteRoutedEventArgs)
        {
            canExecuteRoutedEventArgs.CanExecute = CurrentSession != null;
        }

        private void CloseDialogHandler(object sender, ExecutedRoutedEventArgs executedRoutedEventArgs)
        {
            if (executedRoutedEventArgs.Handled) return;

            InternalClose(executedRoutedEventArgs.Parameter);

            executedRoutedEventArgs.Handled = true;
        }

        private string SelectState()
        {
            return IsOpen ? OpenStateName : ClosedStateName;
        }

        private void OnUnloaded(object sender, RoutedEventArgs routedEventArgs)
        {
            LoadedInstances.Remove(this);
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            LoadedInstances.Add(this);
        }

    }
}
