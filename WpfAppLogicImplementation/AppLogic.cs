using System;

namespace WpfApp1
{
    /// <summary>
    /// ���f���w�̖{�ԃR�[�h
    /// </summary>
    public class AppLogic : BindableBase, IAppLogic
    {
        /// <summary>
        /// �R�}���h���s�̎���
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
