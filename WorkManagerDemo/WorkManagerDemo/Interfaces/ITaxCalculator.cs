using System;
using System.Collections.Generic;
using System.Text;

namespace WorkManagerDemo.Interfaces
{
    public interface ITaxCalculator
    {
        event EventHandler<double> TaxesCalculated;

        double CalculateTaxes(int salt);
    }
}
