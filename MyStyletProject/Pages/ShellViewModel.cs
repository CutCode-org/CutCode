using Stylet;
using System;
using System.Diagnostics;

namespace MyStyletProject.Pages
{
    public class ShellViewModel : Screen
    {
        private readonly ITest _test;
        public ShellViewModel(ITest test)
        {

            text = "Hello world";
            _test = test;
            _test.Changed += Changed;
        }

        private void Changed(object sender, EventArgs e)
        {
            after = _test.data;
            Trace.WriteLine($"Interface updated and the data is {_test.data}");
        }

        public void ClickedCommand()
        {
            _test.data = text;
        }

        private string _text;
        public string text
        {
            get => _text;
            set
            {
                SetAndNotify(ref _text, value);
            }
        }

        private string _after = "";
        public string after
        {
            get => _after;
            set
            {
                SetAndNotify(ref _after, value);
            }
        }
    }
}

