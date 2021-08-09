using System;

namespace WpfApp1
{
    /// <summary>
    /// モデル層の本番コード
    /// </summary>
    public class AppLogic : BindableBase, IAppLogic
    {
        /// <summary>
        /// コマンド実行の実装
        /// </summary>
        public void Execute()
        {
            throw new NotImplementedException($"Exception thrown in {nameof(AppLogic)}.{nameof(Execute)}");
        }

        public double TransferredRatio => throw new NotImplementedException();

        public string CurrentModelName => throw new NotImplementedException();

        public string CurrentVersion => throw new NotImplementedException();

        public event EventHandler SyncCompleted;
        public event EventHandler EraseCompleted;
        public event EventHandler DeviceCheckCompleted;
        public event EventHandler<int> ErrorDetected;
    }
}
