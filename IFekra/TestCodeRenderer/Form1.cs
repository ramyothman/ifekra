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

        void GenerateControls(List<CodeRenderer.MarkupStructure.Html> list)
        {
            foreach(CodeRenderer.MarkupStructure.Html s in list )
            {

                
            }

        }


        private void button1_Click(object sender, EventArgs e)
        {
            Panel p = new Panel();
            p.Name = "test";
            p.Dock = DockStyle.Fill;
            p.BackColor = Color.Yellow;
            panelMain.Controls.Add(p);

            Panel p1 = new Panel();

            p1.Size = new Size(200, 300);
            p1.BackColor = Color.Violet;
            p.Controls.Add(p1);
        }
    }
}
