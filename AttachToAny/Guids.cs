// Guids.cs
// MUST match guids.h
using System;

namespace RyanConrad.AttachToAny {
	static class GuidList {
		public const string guidAttachToAnyPkgString = "177f0816-76a7-401a-87d5-e793c9def1d1";
		public const string guidAttachToAnyCmdSetString = "245c3215-3d66-40e8-bafe-29fb71a00061";

		public static readonly Guid guidAttachToAnyCmdSet = new Guid ( guidAttachToAnyCmdSetString );
	};
}