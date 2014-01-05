// Guids.cs
// MUST match guids.h
using System;

namespace RyanConrad.AttachToAny
{
    static class GuidList
    {
        public const string guidAttachToAnyPkgString = "177f0816-76a7-401a-87d5-e793c9def1d1";
        public const string guidAttachToAnyCmdSetString = "245c3215-3d66-40e8-bafe-29fb71a00061";
				//public const string guidAttachToAnyListCmdSetString = "4b8af381-7f27-409e-9498-a177a0eeeb02";

				public static readonly Guid guidAttachToAnyCmdSet = new Guid ( guidAttachToAnyCmdSetString );
				//public static readonly Guid guidAttachToAnyListCmdSet = new Guid ( guidAttachToAnyListCmdSetString );
		};
}