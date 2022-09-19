using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace 센서값따로받기
{
    public partial class Chartsetting : Form
    {
        SerialPort sPort;
        private double xCount = 200;

        public Chartsetting()
        {
            chartsetting();
           
        }

        private void chartsetting()
        {
            chart1.ChartAreas.Clear();
            chart1.ChartAreas.Add("draw");
            chart1.ChartAreas["draw"].AxisX.Minimum = 0;
            chart1.ChartAreas["draw"].AxisX.Maximum = xCount;
            chart1.ChartAreas["draw"].AxisX.Interval = 20;
            chart1.ChartAreas["draw"].AxisX.MajorGrid.LineColor = Color.White;
            chart1.ChartAreas["draw"].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dash;

            chart1.ChartAreas["draw"].AxisY.Minimum = 0;
            chart1.ChartAreas["draw"].AxisY.Maximum = 1050;
            chart1.ChartAreas["draw"].AxisY.Interval = 150;
            chart1.ChartAreas["draw"].AxisY.MajorGrid.LineColor = Color.White;  
            chart1.ChartAreas["draw"].AxisY.MajorGrid.LineDashStyle= ChartDashStyle.Dash;

            chart1.ChartAreas["draw"].BackColor = Color.Navy;

            chart1.ChartAreas["draw"].CursorX.AutoScroll = true;
            chart1.ChartAreas["draw"].AxisX.ScrollBar.ButtonStyle = ScrollBarButtonStyles.SmallScroll;
            chart1.ChartAreas["draw"].AxisX.ScrollBar.ButtonColor = Color.LightSteelBlue;

            chart1.Series.Clear();
            chart1.Series.Add("s[1]");
            chart1.Series["s[1]"].ChartType = SeriesChartType.Line;
            chart1.Series["s[1]"].Color = Color.Red;
            chart1.Series["s[1]"].BorderWidth = 3;
            if (chart1.Legends.Count > 0)
                chart1.Legends.RemoveAt(0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            sPort = new SerialPort(cb.SelectedItem.ToString());
            sPort.Open();
            sPort.DataReceived += sPort_DataReceived;
           
        }

        private void sPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            byte[] buffer8 = new byte[8];
            int totalRead = 0;
            int readCnt = 0;
            while (totalRead < 8)
            {
                readCnt = sPort.Read(buffer8, totalRead, 8-totalRead);
                totalRead += readCnt;
            }

            Int32[] soil = new Int32[2];
            soil[0] = BitConverter.ToInt32(buffer8, 0);
            soil[1] = BitConverter.ToInt32(buffer8, 4);
            



           
        }

      

        private void 도깨비고비_Load(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }
    }
}
