using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace Entrada.Editor
{
    public partial class AutoCompleteTextBox : UserControl
    {
        private bool suspend = false;
        private Func<string, Task<IEnumerable<string>>> get_suggestions;
        private Control not_found_label;

		public event EventHandler ItemSelected;
        
        public AutoCompleteTextBox ()
        {
            InitializeComponent ();

            textBox2.TextChanged += textBox2_TextChanged;
            textBox2.PreviewKeyDown += textBox2_PreviewKeyDown;
            textBox2.KeyDown += textBox2_KeyDown;
            textBox2.KeyPress += textBox2_KeyPress;
            listBox1.MouseClick += listBox1_MouseClick;
        }

        public void SetSuggestionFunction (Func<string, Task<IEnumerable<string>>> getSuggestionsFunc)
        {
            get_suggestions = getSuggestionsFunc;
        }

        public void SetNotFoundLabel (Control label)
        {
            not_found_label = label;
        }

        public new string Text {
            get { return textBox2.Text; }
            set {
                    suspend = true;
                    textBox2.Text = value;
                    suspend = false;
                }
        }

        private void listBox1_MouseClick (object sender, MouseEventArgs e)
        {
            SelectItem ();
        }

        private void textBox2_KeyPress (object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar) {
                case (char)10:
                case (char)13:
                case '\t':
                    if (listBox1.Visible)
                        e.Handled = true;

                    break;
            }
        }

        private void textBox2_KeyDown (object sender, KeyEventArgs e)
        {
            switch (e.KeyCode) {
                case Keys.Down:
                    MoveListBoxDown ();
                    e.Handled = true;
                    break;
                case Keys.Up:
                    MoveListBoxUp ();
                    e.Handled = true;
                    break;
                case Keys.Tab:
                case Keys.Enter:
                    e.Handled = SelectItem ();
                    break;
				case Keys.Escape:
					HideDropDown ();
					e.Handled = true;
					break;
            }
        }

        private void textBox2_PreviewKeyDown (object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
                if (listBox1.Visible)
                    e.IsInputKey = true;
        }

        private async void textBox2_TextChanged (object sender, EventArgs e)
        {
            if (suspend)
                return;

            if (textBox2.Text.Length > 2) {

                var suggestions = await get_suggestions (textBox2.Text);

                listBox1.Items.Clear ();

                if (suggestions != null && suggestions.Count () > 0) {
                    if (not_found_label != null)
                        not_found_label.Visible = false;

                    listBox1.Items.AddRange (suggestions.Select (p => p.ToString ()).ToArray ());
                    ShowDropDown ();
                    return;
                }

                if (not_found_label != null)
                    not_found_label.Visible = true;
            }

            HideDropDown ();
        }

        private void MoveListBoxDown ()
        {
            var index = listBox1.SelectedIndex + 1;

            if (index < listBox1.Items.Count)
                listBox1.SelectedIndex = index;
        }

        private void MoveListBoxUp ()
        {
            var index = listBox1.SelectedIndex - 1;

            if (index >= 0)
                listBox1.SelectedIndex = index;
        }

        private bool SelectItem ()
        {
            if (listBox1.Visible && listBox1.SelectedIndex >= 0) {
                suspend = true;
                textBox2.Text = listBox1.SelectedItem.ToString ();
				textBox2.SelectionStart = textBox2.Text.Length;
                suspend = false;
                HideDropDown ();

				if (ItemSelected != null)
					ItemSelected (null, EventArgs.Empty);

                return true;
            }

            return false;
        }

        private void ShowDropDown ()
        {
            if (listBox1.Items.Count > 0) {
                listBox1.Visible = true;
                listBox1.Height = Math.Min (listBox1.Items.Count, 7) * listBox1.ItemHeight + 2;
                listBox1.Width = Width;
                Height = listBox1.Bottom;
            }
        }

        private void HideDropDown ()
        {
            listBox1.Visible = false;
            Height = textBox2.Bottom;
        }
    }
}
