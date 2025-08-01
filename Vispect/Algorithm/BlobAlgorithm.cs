﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace Vispect.Algorithm
{
    public struct BinaryThreshold
    { 
        public int lower { get; set; }
        public int upper { get; set; }
        public bool invert {  get; set; }
    }

    public class BlobAlgorithm : InspAlgorithm
    {
        public BinaryThreshold BinThreshold { get; set; } = new BinaryThreshold();
     
        public BlobAlgorithm()
        {
            InspectType = InspectType.InspBinary;
        }

        public override bool DoInspect()
        {
            ResetResult();

            IsInspected = true;

            return true;
        }

        public override void ResetResult()
        {
            base.ResetResult();
        }
    }
}
