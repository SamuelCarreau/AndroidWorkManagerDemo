using System;
using System.Collections.Generic;
using System.Text;
using WorkManagerDemo.Interfaces;

namespace WorkManagerDemo.Services
{
    public class TaxCalculator : ITaxCalculator
    {
        public event EventHandler<double> TaxesCalculated;
        public double CalculateTaxes(int salt) 
        {
            double result = 200 + 200 * (1/(double)salt);
            TaxesCalculated.Invoke(this, result);
            return result;
        }
    }

}
