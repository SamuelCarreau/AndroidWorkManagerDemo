using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using WorkManagerDemo.Interfaces;
using WorkManagerDemo.Messages;
using Xamarin.Forms;

namespace WorkManagerDemo.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private IDisposable _taxResultSubcribtion;

        private double _taxResult;
        public double TaxResult
        {
            get { return _taxResult; }
            set { SetProperty(ref _taxResult, value); }
        }

        private DateTime _calculationTime;
        public DateTime CalculationTime
        {
            get { return _calculationTime; }
            set { SetProperty(ref _calculationTime, value); }
        }

        private DateTime _nextCalculationTime;
        public DateTime NextCalculationTime
        {
            get { return _nextCalculationTime; }
            set { SetProperty(ref _nextCalculationTime, value); }
        }

        public MainPageViewModel(INavigationService navigationService,
            ITaxCalculator taxCalculator)
            : base(navigationService)
        {
            Title = "Main Page";
            _taxResultSubcribtion = Observable.FromEventPattern<EventHandler<double>, double>(
                h => taxCalculator.TaxesCalculated += h,
                h => taxCalculator.TaxesCalculated -= h)
                .Select(e => e.EventArgs)
                .Subscribe(taxCalculation => 
                { 
                    TaxResult = taxCalculation;
                    CalculationTime = DateTime.Now;
                    NextCalculationTime = DateTime.Now.AddMinutes(15);
                });
        }

        public override void Destroy()
        {
            _taxResultSubcribtion.Dispose();
        }

        private DelegateCommand _subscribeCommand;
        public DelegateCommand SubscribeCommand =>
            _subscribeCommand ?? (_subscribeCommand = new DelegateCommand(() => 
            MessagingCenter.Send(new CalculatorWorkerRequested(), nameof(CalculatorWorkerRequested))));
    }
}
