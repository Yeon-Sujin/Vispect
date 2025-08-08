﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vispect.Core
{
    public enum InspectType
    {
        InspNone = -1,
        InspBinary,
        InspMatch,
        InspFilter,
        InspAIModule,
        InspCount
    }

    public enum InspWindowType
    {
        None = 0,
        Base,
        Body,
        Sub,
        ID
    }

    public enum DecisionType
    {
        None = 0,
        Good,           //양품
        Defect,         //불량
        Info,
        Error,          //오류
        Timeout         //타임아웃
    }

    public enum WorkingState
    {
        NONE = 0,
        INSPECT,
        LIVE,
        ALARM
    }

    public static class Define
    {
        public static readonly string ROI_IMAGE_NAME = "RoiImage.png";

        public static readonly string PROGRAM_NAME = "Vispect";
    }
}
