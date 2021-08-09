using System;

namespace WpfApp1
{
    /// <summary>
    /// モデルがビューモデルに公開するインタフェース
    /// </summary>
    public interface IAppLogic
    {
        public string CurrentModelName { get; }
        public string CurrentVersion { get; }
        public double TransferredRatio { get; }
        public void Execute();
        public event EventHandler SyncCompleted;
        public event EventHandler EraseCompleted;
        public event EventHandler DeviceCheckCompleted;
        public event EventHandler<int> ErrorDetected;
    }
}
