using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Threading;

namespace WpfApp1
{
    public class FakeAppLogic2 : BindableBase, IAppLogic
    {
        public string CurrentModelName { get; }
        public string CurrentVersion { get; }
        public double TransferredRatio { get; }

        public void Execute()
        {
            // ワーカスレッドから new FixedPage() するとInvalidOperationExceptionが発生するのでSTAスレッドで実行させる例：
            // 　　InvalidOperationException: 呼び出しスレッドは、多数の UI コンポーネントが必要としているため、STA である必要があります。
            //
            // vvvvvv STAで走らせる例 vvvvvv
            //var th = new Thread(() =>
            //{
            //    var page = new FixedPage();
            //});
            //th.SetApartmentState(ApartmentState.STA);
            //th.Start();
            //th.Join();
            // ^^^^^^ STAで走らせる例 ^^^^^^
            //
            // 上記方法で例外回避できるが、この手法では非同期処理を行う各所に同じようなコードを入れないといけない。
            // なので、コマンド呼出し元でメインスレッドから呼び出されるようにDispatcherを使う方がよさそう。
            var page = new FixedPage();

            Debug.WriteLine("FixedPage object constructed.");
        }

        public event EventHandler SyncCompleted;
        public event EventHandler EraseCompleted;
        public event EventHandler DeviceCheckCompleted;
        public event EventHandler<int> ErrorDetected;
    }
}
