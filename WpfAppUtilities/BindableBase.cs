using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfApp1
{
    /// <summary>
    /// プロパティ変更通知用のベースクラス
    /// </summary>
    public class BindableBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// プロパティの値を変更してから、PropertyChangedイベントを発火させる。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storage">プロパティのバッキングストレージ</param>
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
