using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Widget;
using Android.OS;
using Plugin.BLE;
using Plugin.BLE.Abstractions;

namespace BLEConnectLoopFail
{
    [Activity(Label = "BLEConnectLoopFail", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            DoBluetoothConnectLoop();

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
        }

        public async void DoBluetoothConnectLoop()
        {
            var manager = CrossBluetoothLE.Current;
            var adapter = manager.Adapter;

            var sw = Stopwatch.StartNew();
            var pairedDeviceId = Guid.Parse("00000000-0000-0000-0000-a0e6f888c512");
            while (true)
            {
                try
                {
                    Console.WriteLine($"$> Attempt connect (last attempt {sw.ElapsedMilliseconds}ms ago)");
                    var cts = new CancellationTokenSource(1000);
                    await adapter.ConnectToKnownDeviceAsync(pairedDeviceId, new ConnectParameters(true), cts.Token);
                }
                catch (TaskCanceledException)
                {
                }
            }
        }
    }
}

