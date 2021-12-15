using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : Controller
    {

        [HttpPost("test1")]
        public  IActionResult ParentClass(JObject json)
        {
            var data = new JObject();

            var numbers = json.GetValue("data").ToString().Split(",");
            var listNumbers = new List<int>();
            foreach (var item in numbers)
            {
                listNumbers.Add(Convert.ToInt32(item));
            }

            var ret = listNumbers;

            if (json.GetValue("pilihan").ToString() == "even")
            {
                 ret = Even(listNumbers);
            }

            if (json.GetValue("pilihan").ToString() == "odd")
            {
                 ret = Odd(listNumbers);
            }

            if (json.GetValue("pilihan").ToString() == "prime")
            {
                 ret = Prime(listNumbers);
            }
            var newRet = string.Join(",", ret);
            data.Add("ret", newRet);

            return Content(data.ToString(), "application/json");
        }

        public List<int> Even(List<int> numbers)
        {
            var EvenNumbers = new List<int>();

            foreach (var item in numbers)
            {
                if (item % 2 == 0)
                {
                    EvenNumbers.Add(item);
                }
            }

            return EvenNumbers;
        }

        public List<int> Odd(List<int> numbers)
        {
            var OddNumbers = new List<int>();

            foreach (var item in numbers)
            {
                if (item % 2 != 0)
                {
                    OddNumbers.Add(item);
                }
            }

            return OddNumbers;
        }

        private List<int> Prime(List<int> numbers)
        {
            var PrimeNumbers = new List<int>();

            foreach (var number in numbers)
            {
                int i;
                var flag = 0;
                for (i = 2; i <= number - 1; i++)
                {
                    if (number % i == 0)
                    {
                        flag = 1;
                    }
                }
                if (flag == 0)
                {
                    PrimeNumbers.Add(number);
                }
            }

            return PrimeNumbers;

        }

        [HttpPost("test2")]
        public IActionResult ReverseWord(JObject json)
        {
            var data = new JObject();
            var word = json.GetValue("data").ToString();
            string reverse = new string(word.Reverse().ToArray());

            data.Add("ret", reverse);
            return Content(data.ToString(), "application/json");
        }

        [HttpPost("test3")]
        public IActionResult FindWord(JObject json)
        {
            var data = new JObject();

            var sentence = json.GetValue("data").ToString().Replace(" ", "");
            int stringLength = sentence.Length;
            List<AlphValue> AlphValues = new List<AlphValue>();
            var n = 0;
            var j = 1;
            for (int i = 1; i <= stringLength; i++)
            {

                var nowalphabet = sentence.Substring(n, 1);

                if (AlphValues.Select(x => x.alphabet).Contains(nowalphabet))
                {
                  var alph =  AlphValues.SingleOrDefault(x => x.alphabet == nowalphabet);
                    alph.value = alph.value + 1;

                }
                else
                {
                    AlphValue item = new AlphValue();
                    item.alphabet = nowalphabet;
                    item.value = 1;

                    AlphValues.Add(item);
                }

                n++;
                //j++;
            }

            //var newRet = string.Join(",", AlphValues);

            foreach (var item in AlphValues)
            {
                data.Add(item.alphabet, item.value);
            }
           // data.Add("ret", newRet);

            return Content(data.ToString(), "application/json");
        }


        public class AlphValue
        {
            public string alphabet { get; set; }
            public int value { get; set; }
        }

    }
}
