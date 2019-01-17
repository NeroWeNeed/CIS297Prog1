using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FederalIncomeTaxCalculator
{

     class TaxBracket
     {
          public float Percent;
          public double Minimum;
          public double Maximum;
          public TaxBracket(float percent,double minimum,double maximum)
          {
               Percent = percent;
               Minimum = minimum;
               Maximum = maximum;
          }



     }
     class Program
     {
          static void Main(string[] args)
          {
               List<double> incomes = new List<double>();
               List<double> deductions = new List<double>();
               List<TaxBracket> taxBrackets = new List<TaxBracket>();
               //Init Tax Brackets
               taxBrackets.Add(new TaxBracket(0.1f, 0.0, 9525.0));
               taxBrackets.Add(new TaxBracket(0.12f, 9525.0, 38700.0));
               taxBrackets.Add(new TaxBracket(0.22f, 38700.0, 82500.0));
               taxBrackets.Add(new TaxBracket(0.24f, 82500.0, 157500.0));
               taxBrackets.Add(new TaxBracket(0.32f, 157500.0, 200000.0));
               taxBrackets.Add(new TaxBracket(0.35f, 200000.0, 500000.0));
               taxBrackets.Add(new TaxBracket(0.37f, 500000.0, double.PositiveInfinity));
               //Single Filers
               double minimumDeduction = 12000.0;
               double input;
               Console.WriteLine("Please enter an income from your W2");
               do {
                    input = double.Parse(Console.ReadLine());
                    if (input != 0.0)
                    {
                         incomes.Add(input);
                         Console.WriteLine("Please enter an income from your W2 or 0 when you have entered all of your W2s");
                    }
               } while (input != 0.0);
               do
               {
                    Console.WriteLine("Please enter an itemized deduction or 0 to end entering itemized deductions");
                    input = double.Parse(Console.ReadLine());
                    if (input != 0.0)
                    {
                         deductions.Add(input);
                    }
               } while (input != 0.0);
               double grossIncome = incomes.Sum();
               double totalDeductions = deductions.Sum();
               if (totalDeductions < minimumDeduction)
                    totalDeductions = minimumDeduction;
               double adjustedGrossIncome = grossIncome - totalDeductions;
               Console.WriteLine("Gross income: $" + grossIncome);
               Console.WriteLine("Adjusted Gross Income: $" + adjustedGrossIncome);
               double previous = 0;
               double lastDollar;
               double totalOwed = 0;
               double owed;
               foreach (TaxBracket taxBracket in taxBrackets) {
                    lastDollar = Math.Min(taxBracket.Maximum, adjustedGrossIncome);
                    owed = Math.Round(Math.Max(((lastDollar - previous) * taxBracket.Percent), 0),2);
                    totalOwed += owed;
                    Console.WriteLine("Taxes owed at " + (taxBracket.Percent * 100) + "%: $" + owed);
                    previous = lastDollar;
               }
               Console.WriteLine("Taxes as percentage of adjusted gross income: " + Math.Round(totalOwed / adjustedGrossIncome * 100,2) + "%");
               Console.WriteLine("Taxes as percentage of income:  " + Math.Round(totalOwed / grossIncome * 100,2) + "%");
               Console.ReadKey();
          }
     }
}
