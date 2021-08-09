using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfApp1
{
    /// <summary>
    /// �v���p�e�B�ύX�ʒm�p�̃x�[�X�N���X
    /// </summary>
    public class BindableBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// �v���p�e�B�̒l��ύX���Ă���APropertyChanged�C�x���g�𔭉΂�����B
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storage">�v���p�e�B�̃o�b�L���O�X�g���[�W</param>
        /// <param name="newValue"></param>
        /// <param name="propertyName"></param>
        protected void SetProperty<T>(ref T storage, T newValue, [CallerMemberName] string propertyName = "")
        {
            if (Equals(storage, newValue))
            {
                return;
            }

            storage = newValue;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
