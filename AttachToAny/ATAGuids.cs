// Guids.cs
// MUST match guids.h
using System;

namespace RyanConrad.AttachToAny {
	static class ATAGuids {
		public const string guidAttachToAnyPkgString = "177f0816-76a7-401a-87d5-e793c9def1d1";
		public const string guidAttachToAnyCmdSetString = "245c3215-3d66-40e8-bafe-29fb71a00061";
		public const string guidAttachToAnyMainCmdSetString = "ec15dc63-29e0-4ee1-86a4-a52a89e046b2";
		public const string guidAttachToAnySettingsCmdSetString = "5ecb76f9-a70a-4618-a751-3a68444b3ff8";


		public static readonly Guid guidAttachToAnyCmdSet = new Guid ( guidAttachToAnyCmdSetString );
		public static readonly Guid guidAttachToAnyMainCmdSet = new Guid ( guidAttachToAnyMainCmdSetString );
		public static readonly Guid guidAttachToAnySettingsCmdSet = new Guid ( guidAttachToAnySettingsCmdSetString );
	};
}