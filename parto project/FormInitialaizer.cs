using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace parto_project
{
    partial class Form1:Form
    {
        private List<RichTextBox> textBoxes = new List<RichTextBox>();
        private void Initialiver(int number , string[] colors)
        {
            this.btnstart.Location = new Point(125, 300 * number + 21);
            this.btnStop.Location = new Point(180, 300 * number + 21);
            for (int i =0; i < number; i++)
            {
                this.textBoxes.Add(new RichTextBox());
                this.textBoxes[i].Name = $"txtbox{i}";
                this.textBoxes[i].Size = new Size(400,300);
                this.textBoxes[i].Location = new Point(0, 300 * i + 20);
                this.textBoxes[i].BackColor = Color.FromName(colors[i]);
                this.textBoxes[i].TabIndex = i;
                this.textBoxes[i].ReadOnly = true;
            }
            if(number != 0)
            {
                this.Size = new Size(400, 300 * number);
            }

            foreach(RichTextBox textbox in this.textBoxes)
            {
                this.Controls.Add(textbox);
            }
        }
    }
}
