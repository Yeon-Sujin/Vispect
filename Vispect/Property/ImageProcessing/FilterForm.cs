using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vispect.Property.ImageProcessing
{

    public enum PropertyType
    {
        None,
        Grayscale,
        HSV,
        Flip,
        PyramidDown,
        Resize,
        Binary,
        Blur,
        Rotate,
        Perspective,
        Affine,
        Morphology,
        Canny
    }

    public partial class FilterForm : Form
    {
        public FilterForm()
        {
            InitializeComponent();
        }
    }
}
