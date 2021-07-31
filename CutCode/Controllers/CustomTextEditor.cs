using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace CutCode
{
    public class CustomTextEditor : TextBox
    {
        private int TabSize = 4;
        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                var tab = new string("    ");
                int caretPosition = base.CaretIndex;
                base.Text = base.Text.Insert(caretPosition, tab);
                base.CaretIndex = caretPosition + TabSize + 1;
                e.Handled = true;
            }
            base.OnPreviewKeyDown(e);
        }

        private int oldText = 0;
        private List<char> uniqueChars = new List<char>()
        {
            "'"[0], '"', '(', '[', '{', '<'
        };

        private List<char> uniqueExtChars = new List<char>()
        {
            "'"[0], '"', ')', ']', '}', '>'
        };
        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            if(Text.Length - oldText == 1) 
            {
                int caretPosition = base.CaretIndex;
                var newChar = Text[caretPosition-1];
                
                if (uniqueChars.Contains(newChar))
                {
                    var t = new string($"{uniqueExtChars[uniqueChars.IndexOf(newChar)]}");
                    base.Text = base.Text.Insert(caretPosition, t);
                    base.CaretIndex = caretPosition;
                }
            }
            oldText = Text.Length;
            base.OnTextChanged(e);
        }
    }
}
