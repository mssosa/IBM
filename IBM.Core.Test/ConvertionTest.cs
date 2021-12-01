using IBM.Core.Entities;
using IBM.Core.Interfaces;
using IBM.Core.ObjectValues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace IBM.Core.Test
{
    public class ConvertionTest
    {
        private readonly List<Rate> rates;
        public ConvertionTest()
        {
            rates = new List<Rate>()
            {
                new Rate()
                 {
                    from= "EUR",
                    to= "AUD",
                    rate= "1.01"
                  },
                new Rate(){
                    from= "AUD",
                    to= "EUR",
                    rate= "0.99"
                },
                new Rate(){
                    from= "EUR",
                    to= "CAD",
                    rate= "1.14"
                },
                new Rate(){
                    from= "CAD",
                    to= "EUR",
                    rate= "0.88"
                },
                new Rate(){
                    from= "AUD",
                    to= "USD",
                    rate= "0.88"
                },
                new Rate(){
                    from= "USD",
                    to= "AUD",
                    rate= "1.14"
                }
            };

        }



    }
}
