using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using OpenCvSharp;
using Vispect.ImagaeProcessing;
using Vispect.Core;

namespace Vispect
{
    public partial class CameraForm : DockContent
    {
        public CameraForm()
        {
            InitializeComponent();
        }
    
        public void LoadImage(string filePath)
        {
            if (File.Exists(filePath) == false)
                return;

            //picMainview.Image = Image.FromFile(filePath);

            Image bitmap = Image.FromFile(filePath);
            imageViewer.LoadBitmap((Bitmap)bitmap);


        }

        private void CameraForm_Resize(object sender, EventArgs e)
        {
            int margin = 0;
            imageViewer.Width = this.Width - margin * 2;
            imageViewer.Height = this.Height - margin * 2;

            imageViewer.Location = new System.Drawing.Point(margin, margin);
        }

        public void UpdateDisplay(Bitmap bitmap = null)
        {
            if (bitmap == null)
            {
                bitmap = Global.Inst.InspStage.GetBitmap(0);
                if (bitmap == null)
                    return;
            }

            if (imageViewer != null)
                imageViewer.LoadBitmap(bitmap);
        }

        public Bitmap GetDisplayImage()
        { 
            Bitmap curImage = null;

            if (imageViewer != null)
                curImage = imageViewer.GetCurBitmap();

            return curImage;
        }
    }
}
