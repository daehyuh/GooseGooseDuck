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


        ulong SpeedDump;
        string speedarr = "03C22000";
        string Speedoffset1 = "80";
        string Speedoffset2 = "80";
        string Speedoffset3 = "20";
        string Speedoffset4 = "B8";
        string Speedoffset5 = "270";

        ulong spaceDump;
        string spacearr = "03C814B0";
        string spaceoffset1 = "40";
        string spaceoffset2 = "B8";
        string spaceoffset3 = "0";
        string spaceoffset4 = "20";
        string spaceoffset5 = "1B0";

        ulong killDump;
        string killarr = "03C814B0";
        string killoffset1 = "40";
        string killoffset2 = "B8";
        string killoffset3 = "0";
        string killoffset4 = "20";
        string killoffset5 = "250";


        float current_position_x, current_position_y;


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            gm.get_handel();
            xyset();
            speedset();
            spaceset();
            killset();

            label5.Text = trackBar1.Value.ToString();
            timer1.Enabled = true;
        }


        private void speedset() {
            SpeedDump = gm.get_memory_ulong(gm.GameAssembly + Convert.ToUInt64(speedarr, 16), 8);
            SpeedDump = gm.get_memory_ulong(SpeedDump + Convert.ToUInt64(Speedoffset1, 16), 8);
            SpeedDump = gm.get_memory_ulong(SpeedDump + Convert.ToUInt64(Speedoffset2, 16), 8);
            SpeedDump = gm.get_memory_ulong(SpeedDump + Convert.ToUInt64(Speedoffset3, 16), 8);
            SpeedDump = gm.get_memory_ulong(SpeedDump + Convert.ToUInt64(Speedoffset4, 16), 8);
            SpeedDump = SpeedDump + Convert.ToUInt64(Speedoffset5, 16);
        }

        private void xyset() {
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
        }

        private void spaceset() {
            spaceDump = gm.get_memory_ulong(gm.GameAssembly + Convert.ToUInt64(spacearr, 16), 8);
            spaceDump = gm.get_memory_ulong(spaceDump + Convert.ToUInt64(spaceoffset1, 16), 8);
            spaceDump = gm.get_memory_ulong(spaceDump + Convert.ToUInt64(spaceoffset2, 16), 8);
            spaceDump = gm.get_memory_ulong(spaceDump + Convert.ToUInt64(spaceoffset3, 16), 8);
            spaceDump = gm.get_memory_ulong(spaceDump + Convert.ToUInt64(spaceoffset4, 16), 8);
            spaceDump = spaceDump + Convert.ToUInt64(spaceoffset5, 16);
        }

        private void killset()
        {
            killDump = gm.get_memory_ulong(gm.GameAssembly + Convert.ToUInt64(killarr, 16), 8);
            killDump = gm.get_memory_ulong(killDump + Convert.ToUInt64(killoffset1, 16), 8);
            killDump = gm.get_memory_ulong(killDump + Convert.ToUInt64(killoffset2, 16), 8);
            killDump = gm.get_memory_ulong(killDump + Convert.ToUInt64(killoffset3, 16), 8);
            killDump = gm.get_memory_ulong(killDump + Convert.ToUInt64(killoffset4, 16), 8);
            killDump = killDump + Convert.ToUInt64(killoffset5, 16);
        }

        private void move() {
            if (current_position_x != old_position_x)
            {
                if (current_position_x - old_position_x > 0)
                {
                    gm.put_memory(XYhookDump + Convert.ToUInt64(xarroffset5, 16), BitConverter.GetBytes(Convert.ToSingle(current_position_x + 1.5)));
                }
                else if (current_position_x - old_position_x < 0)
                {
                    gm.put_memory(XYhookDump + Convert.ToUInt64(xarroffset5, 16), BitConverter.GetBytes(Convert.ToSingle(current_position_x - 1.5)));
                }

            }
            if (current_position_y != old_position_y)
            {
                if (current_position_y - old_position_y > 0)
                {
                    gm.put_memory(XYhookDump + Convert.ToUInt64(xarroffset5, 16) + 4, BitConverter.GetBytes(Convert.ToSingle(current_position_y + 1.5)));
                }
                else if (current_position_y - old_position_y < 0)
                {
                    gm.put_memory(XYhookDump + Convert.ToUInt64(xarroffset5, 16) + 4, BitConverter.GetBytes(Convert.ToSingle(current_position_y - 1.5)));
                }

            }
        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            gm.get_handel();
            xyset();
            speedset();
            spaceset();
            timer1.Enabled = true;
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            current_position_x = gm.get_memory_float(XYhookDump + Convert.ToUInt64(xarroffset5, 16), 4);
            current_position_y = gm.get_memory_float(XYhookDump + Convert.ToUInt64(xarroffset5, 16) + 4, 4);

            label1.Text = "X = " + current_position_x.ToString();
            label2.Text = "Y = " + current_position_y.ToString();
            label3.Text = "old_X = " + old_position_x.ToString();
            label4.Text = "old_Y = " + old_position_y.ToString();

            if (checkBox1.Checked == true) 
            {
                gm.put_memory(SpeedDump, BitConverter.GetBytes(Convert.ToSingle(trackBar1.Value)));
            }

            if (checkBox2.Checked == true) {
                move();
            }

            if (checkBox3.Checked == true) {
                gm.put_memory(spaceDump, BitConverter.GetBytes(Convert.ToSingle(0)));
            }
            if (checkBox4.Checked == true)
            {
                gm.put_memory(killDump, BitConverter.GetBytes(Convert.ToSingle(0)));
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
            label5.Text = trackBar1.Value.ToString();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            gm.get_handel();
            xyset();
            speedset();
            spaceset();
            killset();
            timer1.Enabled = true;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = current_position_x.ToString();
            textBox2.Text = current_position_y.ToString();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}