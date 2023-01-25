using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;

namespace GooseGooseDuck
{
    public partial class Form1 : Form
    {
        GetMemory gm = new GetMemory();
        float old_position_x = 0, old_position_y = 0;
        ulong XYhookDump;

        string xarr = "01ACA7C0";
        string xarroffset1 = "48";
        string xarroffset2 = "370";
        string xarroffset3 = "10";
        string xarroffset4 = "60";
        string xarroffset5 = "2C";


        float current_position_x, current_position_y;

        [DllImport("user32.dll")]
        private static extern int RegisterHotKey(int hwnd, int id, int fsModifiers, int vk);

        //ÇÖÅ°Á¦°Å
        [DllImport("user32.dll")]
        private static extern int UnregisterHotKey(int hwnd, int id);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            gm.get_handel();
            XYhookDump = gm.get_memory_ulong(gm.UnityPlayer + Convert.ToUInt64(xarr, 16), 8);
            XYhookDump = gm.get_memory_ulong(XYhookDump + Convert.ToUInt64(xarroffset1, 16), 8);
            XYhookDump = gm.get_memory_ulong(XYhookDump + Convert.ToUInt64(xarroffset2, 16), 8);
            XYhookDump = gm.get_memory_ulong(XYhookDump + Convert.ToUInt64(xarroffset3, 16), 8);
            XYhookDump = gm.get_memory_ulong(XYhookDump + Convert.ToUInt64(xarroffset4, 16), 8);
            current_position_x = gm.get_memory_float(XYhookDump + Convert.ToUInt64(xarroffset5, 16), 4);
            current_position_y = gm.get_memory_float(XYhookDump + Convert.ToUInt64(xarroffset5, 16) + 4, 4);

            label1.Text = current_position_x.ToString();
            label2.Text = current_position_y.ToString();
            label3.Text = "old_X = " + current_position_x.ToString();
            label4.Text = "old_Y = " + current_position_y.ToString();

            old_position_x = current_position_x;
            old_position_y = current_position_y;

            timer1.Enabled = true;

            label5.Text = (trackBar1.Value * 0.01).ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            current_position_x = gm.get_memory_float(XYhookDump + Convert.ToUInt64(xarroffset5, 16), 4);
            current_position_y = gm.get_memory_float(XYhookDump + Convert.ToUInt64(xarroffset5, 16) + 4, 4);

            label1.Text = current_position_x.ToString();
            label2.Text = current_position_y.ToString();
            label3.Text = "old_X = " + old_position_x.ToString();
            label4.Text = "old_Y = " + old_position_y.ToString();
            if (checkBox1.Checked == true)
            {
                if (current_position_x != old_position_x)
                {
                    if (current_position_x - old_position_x > 0)
                    {
                        gm.put_memory(XYhookDump + Convert.ToUInt64(xarroffset5, 16), BitConverter.GetBytes(Convert.ToSingle(current_position_x + (Convert.ToSingle(label5.Text)))));
                    }
                    else if (current_position_x - old_position_x < 0)
                    {
                        gm.put_memory(XYhookDump + Convert.ToUInt64(xarroffset5, 16), BitConverter.GetBytes(Convert.ToSingle(current_position_x - (Convert.ToSingle(label5.Text)))));
                    }

                }
                if (current_position_y != old_position_y)
                {
                    if (current_position_y - old_position_y > 0)
                    {
                        gm.put_memory(XYhookDump + Convert.ToUInt64(xarroffset5, 16) + 4, BitConverter.GetBytes(Convert.ToSingle(current_position_y + (Convert.ToSingle(label5.Text)))));
                    }
                    else if (current_position_y - old_position_y < 0)
                    {
                        gm.put_memory(XYhookDump + Convert.ToUInt64(xarroffset5, 16) + 4, BitConverter.GetBytes(Convert.ToSingle(current_position_y - (Convert.ToSingle(label5.Text)))));
                    }

                }
            }
            current_position_x = gm.get_memory_float(XYhookDump + Convert.ToUInt64(xarroffset5, 16), 4);
            current_position_y = gm.get_memory_float(XYhookDump + Convert.ToUInt64(xarroffset5, 16) + 4, 4);
            old_position_x = current_position_x;
            old_position_y = current_position_y;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            gm.put_memory(XYhookDump + Convert.ToUInt64(xarroffset5, 16), BitConverter.GetBytes(Convert.ToSingle(textBox1.Text)));
            gm.put_memory(XYhookDump + Convert.ToUInt64(xarroffset5, 16) + 4 , BitConverter.GetBytes(Convert.ToSingle(textBox2.Text)));
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void timer2_Tick_1(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label5.Text = (trackBar1.Value * 0.01).ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = label1.Text;
            textBox2.Text = label2.Text;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}