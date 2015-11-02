using System;

namespace Assets.MVC.HexAlgorithmsEventArgs
{
    public class TextEventArgs : EventArgs
    {
        public string Text { get; set; }

        public TextEventArgs(string text)
        {
            Text = text;
        }
    }
}

