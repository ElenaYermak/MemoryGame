using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MemoryGame
{
    public partial class Form1 : Form
    {
        Random random = new Random();
        List<string> icons = new List<string>()
        {
            "C", "C", "S", "S", "M", "M", "E", "E", "T", "T", "P", "P", "J", "J", "R", "R"
        };

        Label clickedFirst, clickedSecond; 

        public Form1()
        {
            InitializeComponent();
            IconsToSquares();
        }

        private void label_Click(object sender, EventArgs e)
        {
            if (clickedFirst != null && clickedSecond != null) //in order not to click more than two icons
            {
                return;
            }

            Label clickedLabel = sender as Label; //trying to convert the sender into a label

            if (clickedLabel == null) // when clickedLabel is null, not convert into a label 
            {
                return;
            }
            
            if (clickedLabel.ForeColor == Color.Black) //check for clicking the same icon second time 
            {
                return;
            }

            if (clickedFirst == null)
            {
                clickedFirst = clickedLabel;
                clickedFirst.ForeColor = Color.Black; //to see that it is pressed
                return;
            }

            clickedSecond = clickedLabel;
            clickedSecond.ForeColor = Color.Black; 

            Winner();

            if (clickedFirst.Text == clickedSecond.Text)
            {
                clickedFirst = null;
                clickedSecond = null; 
            }
            else
                timer1.Start();
        }

        private void Winner()
        {
            Label label;
            for (int i = 0; i < tableLayoutPanel1.Controls.Count; i++)
            {
                label = tableLayoutPanel1.Controls[i] as Label; 
                if (label != null && label.ForeColor == label.BackColor) // it's mean that icon is invisible 
                {
                    return;
                }
            }

            MessageBox.Show("Congratulation!");
            Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();

            clickedFirst.ForeColor = clickedFirst.BackColor;
            clickedSecond.ForeColor = clickedSecond.BackColor;

            clickedFirst = null;
            clickedSecond = null;
        }

        private void IconsToSquares() 
        {
            Label label; //local variable, current label 
            int randomNumber; //random index in list icons
            for (int i = 0; i < tableLayoutPanel1.Controls.Count; i++) //iterating through every label  
            {
                if (tableLayoutPanel1.Controls[i] is Label)
                {
                    label = (Label)tableLayoutPanel1.Controls[i];
                }
                else
                    continue;

                //give to square a random icon
                randomNumber = random.Next(0, icons.Count); 
                label.Text = icons[randomNumber];

                icons.RemoveAt(randomNumber);  //to avoid two identical random number 
            }
        }
    }
}
