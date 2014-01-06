// Guids.cs
// MUST match guids.h
using System;

namespace RyanConrad.AttachToAny {
	static class ATAGuids {
		public const string guidAttachToAnyPkgString = "177f0816-76a7-401a-87d5-e793c9def1d1";

		public const string guidAttachToAnyCmdGroupString = "245c3215-3d66-40e8-bafe-29fb71a00061";
		public const string guidAttachToAnySettingsGroupString = "fe5c3b97-e53f-480d-9ad0-c41238c05731";

		public static readonly Guid guidAttachToAnyCmdGroup = new Guid ( guidAttachToAnyCmdGroupString );
		public static readonly Guid guidAttachToAnySettingsGroup = new Guid ( guidAttachToAnySettingsGroupString );
	};
}