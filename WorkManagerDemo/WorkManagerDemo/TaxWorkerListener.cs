using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;
using WorkManagerDemo.Interfaces;

namespace WorkManagerDemo
{
    public class TaxWorkerListener : IDisposable
    {
        private IDisposable _subcribtion;

        public TaxWorkerListener(ITaxCalculator taxCalculator,INotificationService notificationService)
        {
            _subcribtion = Observable.FromEventPattern<EventHandler<double>, double>(
                h => taxCalculator.TaxesCalculated += h,
                h => taxCalculator.TaxesCalculated -= h)
                .Select(e => e.EventArgs)
                .Subscribe(taxInfo => notificationService.CreateNotification("New tax Info", $"Your Tax Return is:{taxInfo}"));
        }

        public void Dispose()
        {
            _subcribtion.Dispose();
        }
    }
}
