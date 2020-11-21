using System;

namespace Solid.DataTypes
{
    [Flags]
    public enum RangeInclusion
    {
        IncludeStart = 1 << 0,
        IncludeEnd = 1 << 1,

        Inclusive = IncludeStart | IncludeEnd,
        Exclusive = 0
    }
}