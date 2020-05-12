using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TimeTracker
{
    public partial class StringInputForm : Form
    {
        /// <summary>
        /// The default constructor supplies a default prompt if none
        /// is provided.
        /// </summary>
        public StringInputForm()
        {
            InitializeComponent();

            this.Text = "Please enter a string";
        }

        /// <summary>
        /// An overloaded constructor allows the caller to specify the prompt
        /// for the string.
        /// </summary>
        /// <param name="title">The prompt to use to request the string</param>
        public StringInputForm(string title)
        {
            InitializeComponent();

            this.Text = title;
        }

        /// <summary>
        /// An overloaded constructor allows the caller to specify the prompt
        /// for the string and a default value.
        /// </summary>
        /// <param name="title">The prompt to use to request the string</param>
        public StringInputForm(string title, string defaultValue)
        {
            InitializeComponent();

            this.Text = title;
            this.Response = defaultValue;
        }

        /// <summary>
        /// An overloaded constructor allows the caller to specify the prompt
        /// for the string and a default value.
        /// </summary>
        /// <param name="title">The prompt to use to request the string</param>
        public StringInputForm(string title, string defaultValue, string mask, Type validatingType)
        {
            InitializeComponent();

            this.Text = title;
            this.Response = defaultValue;
            responseTextBox.Mask = mask;
            responseTextBox.ValidatingType = validatingType;
        }

        /// <summary>
        /// Rather than exposing the TextBox directly, create a new
        /// property, Response to return the entered string.
        /// </summary>
        public string Response
        {
            get
            {
                return responseTextBox.Text;
            }
            set
            {
                responseTextBox.Text = value;
            }
        }

        private void StringInputForm_Shown(object sender, EventArgs e)
        {
            responseTextBox.Focus();
        }

    }
}