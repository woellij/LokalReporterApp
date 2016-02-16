using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LokalReporter.Client.Dummy;

namespace scheißfuck
{
    class Program
    {
        static void Main(string[] args)
        {
            new XmlArticlesService().GetArticlesAsync(null, CancellationToken.None);
            Console.ReadLine();
        }
    }
}
