using System;

namespace Insurance.Api.Model
{
    [Flags]
    public enum Systems
    {
        All = 0x1,
        SystemA = 0x2,
        SystemB = 0x4,
        SystemC = 0x8
    }
}
