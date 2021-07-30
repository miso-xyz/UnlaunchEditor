using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace UnlaunchEditor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            if (actions.listView1.Items.Count != 0)
            {
                switch (MessageBox.Show("Changes have been made, are you sure you want to open a different file?", "Save file?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning))
                {
                    case DialogResult.No:
                    case DialogResult.Cancel:
                        return;
                }
            }
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                GeneralFunctions.UnlaunchFile = File.ReadAllBytes(ofd.FileName);
                GeneralFunctions.InitializeUnlaunch();
                UpdateBackground();
            }
        }

        private void UpdateBackground() { pictureBox1.BackgroundImage = Image.FromStream(new MemoryStream(GeneralFunctions.Background)); }

        private void HighlightOnClick(object sender, EventArgs e)
        {
            if (GeneralFunctions.Background == null) { return; }
            Size s_ = new Size(0, 0);
            if (sender is PictureBox) { s_ = new Size(1, 1); }
            HighlightSelection((Control)sender, ((Control)sender).Location, ((Control)sender).Size, Pens.Red, s_);
        }
        private void pictureBox1_Paint(object sender, PaintEventArgs e) { if (highlightedControl == (Control)sender && drawRect) { e.Graphics.DrawRectangle(rectPen, rect); } }
        private void pictureBox2_Paint(object sender, PaintEventArgs e) { if (highlightedControl == (Control)sender && drawRect) { e.Graphics.DrawRectangle(rectPen, rect); } }

        private void HighlightSelection(Control sender, Point position, Size size, Pen pen, Size minus = new Size())
        {
            drawRect = false;
            if (highlightedControl != null) { highlightedControl.Invalidate(); }
            drawRect = true;
            rect = new Rectangle(position, size - minus);
            rectPen = pen;
            sender.Invalidate();
            highlightedControl = sender;
        }

        Control highlightedControl = null;
        Pen rectPen;
        bool drawRect = false;
        Rectangle rect = new Rectangle();

        private void menuItem4_Click(object sender, EventArgs e) { Application.Exit(); }

        private void menuItem6_Click(object sender, EventArgs e)
        {
            Console.WriteLine();
            Console.WriteLine("- Please select your 'UNLAUNCH.DSI' file to edit via File->Open (Ctrl+O)");
            Console.WriteLine("- To see what you're able to edit, just right click or drag the file");
        }

        private void menuItem5_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "DSI File|*.dsi";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                using (MemoryStream ms = new MemoryStream(GeneralFunctions.UnlaunchFile))
                {
                    List<byte> bck = new List<byte>();
                    byte[] zeroBytes = new byte[0x3c70-GeneralFunctions.Background.Length];
                    bck.AddRange(GeneralFunctions.Background);
                    bck.AddRange(zeroBytes);
                    ms.Position = 0x48F0;
                    ms.Write(bck.ToArray(), 0, 0x3c70);
                    File.WriteAllBytes(sfd.FileName, ms.ToArray());
                }
                AddActions("File Saved!", "File Saved!", Color.LightGreen, new Color());
                MessageBox.Show("File Saved!", "Unlaunch.dsi Editor", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && GeneralFunctions.Background != null)
            {
                contextMenu1.MenuItems.Clear();
                if (highlightedControl is PictureBox)
                {
                    contextMenu1.MenuItems.Add(new MenuItem("Replace Background", ReplaceBackground));
                    contextMenu1.MenuItems.Add(new MenuItem("Export Background", ExportBackground));
                }
                contextMenu1.Show(this, PointToClient(Cursor.Position));
            }
        }

        private void ExportBackground(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "GIF File|*.gif";
            if (sfd.ShowDialog() == DialogResult.OK) { File.WriteAllBytes(sfd.FileName, GeneralFunctions.Background); }
        }

        private void ReplaceBackground(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "GIF File|*.gif";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                AddActions("Replacing background with " + ofd.FileName + "...", "");
                byte[] newBck = File.ReadAllBytes(ofd.FileName);
                if (!GeneralFunctions.isValidHeader(newBck))
                {
                    AddActions("|- Invalid Header Found!, not replacing background", "File has an invalid header!", Color.IndianRed);
                    MessageBox.Show("Invalid GIF File! Make sure the file you selected is valid", "Error - Invalid Header", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (!GeneralFunctions.DoesBackgroundFit(newBck))
                {
                    AddActions("|- File is too big!, not replacing background", "File is too big!", Color.IndianRed);
                    MessageBox.Show("File is too big, make sure its less than 15472 bytes!", "Error - File is too big", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                GeneralFunctions.Background = newBck;
                AddActions("Background Successfully replaced", "Background Successfully replaced", Color.LightGreen);
                UpdateBackground();
            }
        }

        Actions actions = new Actions();

        private void AddActions(string action, string statusBarText, Color actionBackColor = new Color(), Color actionForeColor = new Color())
        {
            ListViewItem lvi = new ListViewItem(action);
            if (actionBackColor == new Color()) { actionBackColor = actions.listView1.BackColor; }
            if (actionForeColor == new Color()) { actionForeColor = actions.listView1.ForeColor; }
            lvi.BackColor = actionBackColor;
            lvi.ForeColor = actionForeColor;
            statusBar1.Text = action;
            actions.Invalidate();
            actions.listView1.Items.Add(lvi);
        }

        private void Form1_Move(object sender, EventArgs e) { actions.Location = new Point(Location.X + Size.Width, Location.Y);}
        void Form1_DragEnter(object sender, DragEventArgs e) { if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy; }

        void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            GeneralFunctions.UnlaunchFile = File.ReadAllBytes(files[0]);
            GeneralFunctions.InitializeUnlaunch();
            UpdateBackground();
        }

        private void menuItem9_Click(object sender, EventArgs e) { actions.Show(); actions.Location = new Point(Location.X + Size.Width, Location.Y); }
    }
}