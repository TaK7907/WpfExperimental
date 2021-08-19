using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace WpfApp1
{
    /// <summary>
    /// MainWindowのビューモデル層
    /// </summary>
    internal class MainWindowViewModel : BindableBase
    {
        public DelegateCommand ExecCommand { get; init; }

        private readonly IAppLogic _logic;

        private string _progressReport;
        public string ProgressReport
        {
            get => _progressReport;
            set => SetProperty(ref _progressReport, value);
        }

        private bool _isRunning;
        public bool IsRunning
        {
            get => _isRunning;
            set
            {
                SetProperty(ref _isRunning, value);
                ExecCommand?.RaiseCanExecuteChanged();
            }
        }

        private string _newModelName;
        public string NewModelName
        {
            get => _newModelName;
            private set => SetProperty(ref _newModelName, value);
        }

        private string _newVersion;
        public string NewVersion
        {
            get => _newVersion;
            private set => SetProperty(ref _newVersion, value);
        }

        /// <summary>
        /// モデル層のProperty変化をビューに転送する
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void PropertyChangedEventHandler(object s, PropertyChangedEventArgs e)
        {

            switch (e.PropertyName)
            {
                case nameof(_logic.TransferredRatio):
                    if (_logic.TransferredRatio > 0)
                        ReportProgress($"Transferred {_logic.TransferredRatio:n1}%");
                    break;
                case nameof(_logic.CurrentModelName):
                    if (_logic.CurrentModelName != default)
                        ReportProgress($"Current model: \"{_logic.CurrentModelName}\"");
                    break;
                case nameof(_logic.CurrentVersion):
                    if (_logic.CurrentVersion != default)
                        ReportProgress($"Current version: {_logic.CurrentVersion}");
                    break;
                default:
                    break;
            }
        }

        private void ReportProgress(string report) =>
            ProgressReport += $"{DateTime.Now.ToTimeStamp()} | {report}\n";

        public MainWindowViewModel()
        {
            _dispatcher = Dispatcher.CurrentDispatcher;

            /// TODO: 本番コードに差し替え
#if false //Production
            _logic = new AppLogic();
#else
            _logic = new FakeAppLogic2();
#endif
            IsRunning = false;

            ExecCommand = new(
                async () => await ExecuteAsync(),
                _ => !IsRunning);

            (_logic as BindableBase).PropertyChanged += PropertyChangedEventHandler;
            _logic.SyncCompleted += SyncCompletedEventHandler;
            _logic.EraseCompleted += EraseCompletedEventHandler;
            _logic.DeviceCheckCompleted += DeviceCheckCompletedEventHandler;

            ProgressReport = default;
        }

        private void DeviceCheckCompletedEventHandler(object sender, EventArgs e)
        {
            ReportProgress("Device check done.");
        }

        private void EraseCompletedEventHandler(object sender, EventArgs e)
        {
            ReportProgress("Erase done.");
        }

        private void SyncCompletedEventHandler(object sender, EventArgs e)
        {
            ReportProgress("Synchronization done.");
        }

        private readonly Dispatcher _dispatcher;

        private async Task ExecuteAsync()
        {
            IsRunning = true;
            ProgressReport = default;

            try
            {
                ReportProgress("Started.");

#if true
                // FakeAppLogic2に仕込んだInvalidOperationExceptionを回避するため
                // Dispather経由で非同期処理実体を呼び出す。
                // (See also: FakeAppLogic2.Execute() 内のコメント)
                await _dispatcher.InvokeAsync(_logic.Execute());
#else
                await Task.Run(() => _logic.Execute());
#endif
            }
            finally
            {
                ReportProgress("Finished.");
                IsRunning = false;
            }
        }
    }
}
