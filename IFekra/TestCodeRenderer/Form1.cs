using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestCodeRenderer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }



        private void button1_Click(object sender, EventArgs e)
        {
            Panel p = new Panel();
            p.Name = "test";
            p.Dock = DockStyle.Fill;
            p.BackColor = Color.Yellow;
            panelMain.Controls.Add(p);

            Panel p1 = new Panel();

            p1.Size = new Size(200, 200);
            p1.BackColor = Color.Violet;
            p.Controls.Add(p1);

            Panel p2 = new Panel();
            p2.Size = new Size(500, 100);
            p2.BackColor = Color.Red;
            //p2.Location = new Point(200, 0);
            Control prev = p.Controls[p.Controls.Count - 1];
            p2.Location = prev.Location + prev.Size - new Size(0, p2.Height);
            p.Controls.Add(p2);

            /*Label l = new Label();
            l.AutoSize = true;
            l.Font = new Font(l.Font.FontFamily,20);
            l.BorderStyle = BorderStyle.Fixed3D;
            Point point = p1.Location + p1.Size - new Size(0, l.Height);
            l.Location = point;
            l.Text = l.Location.ToString();
            //l.Height
            p.Controls.Add(l);*/
            
            
            //p.Controls[p.Controls.].Location.X
            
        }
    }
}
