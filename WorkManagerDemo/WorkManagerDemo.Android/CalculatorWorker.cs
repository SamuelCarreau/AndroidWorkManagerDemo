using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.Work;
using Prism;
using WorkManagerDemo.Interfaces;

namespace WorkManagerDemo.Droid
{
    public class CalculatorWorker : Worker
    {
        static int _instanceCount;
        private readonly PrismApplicationBase _app;

        static CalculatorWorker()
        {
            _instanceCount = 0;
        }
        public CalculatorWorker(Context context, WorkerParameters workerParams) : base(context, workerParams)
        {
            _instanceCount++;
            //Worker should be able to work if the app is not alive. That why he create is own app instance if needed.
            _app = App.Current ?? new App(new AndroidInitializer());
            Log("In Worker Constructor");
        }

        public override Result DoWork()
        {
            ITaxCalculator taxCalculator = _app.Container.Resolve(typeof(ITaxCalculator)) as ITaxCalculator;
            double taxReturn = taxCalculator.CalculateTaxes(_instanceCount);
            Log($"Your Tax Return is: {taxReturn}");
            return Result.InvokeSuccess();
        }

        private void Log(string message)
        {
            Android.Util.Log.Debug(nameof(CalculatorWorker), $"[Worker_{_instanceCount}] {message}");
        }
    }
}