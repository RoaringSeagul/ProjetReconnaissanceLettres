using System;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace TPARCHIPERCEPTRON.Présentation
{
    /// <summary>
    /// Auteur: Jonathan Koch-Roy
    /// Date: 
    /// Description: 
    /// </summary>
    public class TextBoxStreamWriter : TextWriter
    {
        TextBox _output = null;

        public TextBoxStreamWriter(TextBox output)
        {
            _output = output;
        }

        public override void Write(char value)
        {
            base.Write(value);
            _output.AppendText(value.ToString());

        }

        public override Encoding Encoding
        {
            get { return System.Text.Encoding.UTF8; }
        }
    }
}
