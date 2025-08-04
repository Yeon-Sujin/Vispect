﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;

namespace Vispect.Algorithm
{
    public enum InspectType
    { 
        InspNone = -1,
        InspBinary,
        InspCount
    }

    public abstract class InspAlgorithm
    {
        public InspectType InspectType { get; set; } = InspectType.InspNone;
        public bool IsUse { get; set; } = true;
        public bool IsInspected { get; set; } = false;
        public Rect TeachRect { get; set; }
        public Rect InspRect { get; set; }

        protected Mat _srcImage = null;
        public List<string> ResultString { get; set; } = new List<string>();
        public bool IsDefect {  get; set; }
        public virtual void SetInspData(Mat srcImage)
        {
            _srcImage = srcImage;
        }
        public abstract bool DoInspect();
        public virtual void ResetResult()
        { 
            IsInspected = false;
            IsDefect = false;
            ResultString.Clear();
        }

        public virtual int GetResultRect(out List<DrawInspectInfo> resultArea)
        {
            resultArea = null;
            return 0;
        }
    }
}
