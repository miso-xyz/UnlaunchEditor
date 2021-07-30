using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UnlaunchEditor
{
    public partial class Actions : Form
    {
        public Actions()
        {
            InitializeComponent();
        }

        private void listView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Hide();
            }
        }

        private void Actions_Paint(object sender, PaintEventArgs e)
        {
            listView1.Columns[0].Width = listView1.Width;
            if (listView1.Items.Count == 0)
            {
                listView1.Hide();
                e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
                e.Graphics.DrawString("No actions has been made", new Font(DefaultFont.FontFamily, 12, FontStyle.Regular), Brushes.Black, new PointF(10, 10));
            }
            else
            {
                listView1.Show();
            }
        }

        private void Actions_FormClosing(object sender, FormClosingEventArgs e) { e.Cancel = true; Hide(); }
    }
}
