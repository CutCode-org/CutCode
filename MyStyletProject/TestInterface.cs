using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStyletProject
{
    public interface ITest
    {
        event EventHandler Changed;
        string data { get; set; }
    }

    public class Test : ITest
    {
        private string _data;
        public string data
        {
            get => _data;
            set
            {
                _data = value;
                Changed?.Invoke(this, EventArgs.Empty);
            }
        }

        public event EventHandler Changed;

    }
}
