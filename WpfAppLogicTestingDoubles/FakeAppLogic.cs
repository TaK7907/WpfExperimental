using System;
using System.Threading;
using System.Threading.Tasks;

namespace WpfApp1
{
    /// <summary>
    /// コマンド実行のフェイク実装
    /// </summary>
    public class FakeAppLogic : BindableBase, IAppLogic
    {
        private readonly AutoResetEvent _evSync;

        public FakeAppLogic()
        {
            SetPropertyDefault();

            _evSync = new (false);
        }

        private void SetPropertyDefault()
        {
            TransferredRatio = default;
            CurrentModelName = default;
            CurrentVersion = default;
        }

        public void Execute()
        {
            SetPropertyDefault();

            _evSync.Reset();
            Task.Run(() =>
            {
                Thread.Sleep(3000);
                _evSync.Set();
            });
            _evSync.WaitOne();
            SyncCompleted?.Invoke(this, EventArgs.Empty);

            Task.Delay(1000).Wait();
            CurrentModelName = "MODEL NAME      ";

            Task.Delay(1000).Wait();
            CurrentVersion = "11.22.33";

            Task.Delay(1000).Wait();
            EraseCompleted?.Invoke(this, EventArgs.Empty);

            var r = 0.0;
            const double transferMax = 100.0;
            const double transferSizeRatio = 18.8;
            do
            {
                Task.Delay(500).Wait();
                r += transferSizeRatio;
                TransferredRatio = Math.Min(r, transferMax);
            } while (r <= transferMax);

            Task.Delay(1000).Wait();
            DeviceCheckCompleted?.Invoke(this, EventArgs.Empty);
        }

        private double _transferredRatio;
        public double TransferredRatio
        {
            get => _transferredRatio;
            private set => SetProperty(ref _transferredRatio, value);
        }

        private string _currentModelName;
        public string CurrentModelName {
            get => _currentModelName;
            private set => SetProperty(ref _currentModelName, value);
        }

        private string _currentVersion;
        public string CurrentVersion
        {
            get => _currentVersion;
            private set => SetProperty(ref _currentVersion, value);
        }

        public event EventHandler SyncCompleted;
        public event EventHandler EraseCompleted;
        public event EventHandler DeviceCheckCompleted;
        public event EventHandler<int> ErrorDetected;
    }
}
