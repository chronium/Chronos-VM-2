using System;
using System.Drawing;
using System.Windows.Forms;
namespace Virtual_Machine {
	public partial class Screen : Form {
		private Bitmap buffer;
		private Timer refreshTimer;

		public Screen()
		{
			this.Text = "ChronsVM";
			this.DoubleBuffered = true;
			this.Height = 400;
			this.Width = 640;
			this.buffer = new Bitmap(640, 400);
			this.refreshTimer = new Timer();
			this.refreshTimer.Interval = 20;
			this.refreshTimer.Tick += refreshScreen;
			this.refreshTimer.Start();
			for (int x = 0; x < 640; x++)
				for(int y = 0; y < 400; y++)
					this.buffer.SetPixel(x, y, Color.Black);
		}

		private void refreshScreen (object sender, EventArgs e)
		{
			this.Refresh();
		}
		
		public void SetPixel(int x, int y, int color)
		{
			lock(this.buffer)
			{
				buffer.SetPixel(x, y, Color.FromArgb(color));
			}
		}
		
		public int GetPixel(int x, int y)
		{
			return buffer.GetPixel(x, y).ToArgb();
		}
		
		protected override void OnPaint (PaintEventArgs e)
		{
			lock(this.buffer)
			{
				e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
				e.Graphics.DrawImage(this.buffer, new Rectangle(0, 0, this.Width, this.Height), new Rectangle(Point.Empty, buffer.Size),  GraphicsUnit.Pixel);
			}
		}
	}
}
