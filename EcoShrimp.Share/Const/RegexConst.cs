﻿namespace EcoShrimp.Share.Const
{
	/// <summary>
	/// regex const
	/// </summary>
	public class RegexConst
	{
		public const string CONST_CASE_WITH_SPECIAL = @"^[A-Z0-9@#\$%\^&\*\-!]+(_[A-Z0-9@#\$%\^&\*\-!]+)*$";
		public const string CONST_CASE = @"^[A-Z0-9]+(_[A-Z0-9]+)*$";
		public const string KEBAB_CASE_WITH_SPECIAL = @"^[a-z0-9@#\$%\^&\*\!]+(-[a-z0-9@#\$%\^&\*\!]+)*$";
		public const string KEBAB_CASE = @"^[a-z0-9]+(?:-[a-z0-9]+)*$";
		public const string NOT_VIETNAMESE = @"^[\x00-\x7F]*$";
		public const string EMAIL = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
		public const string PHONE_NUMBER = @"^\+?\d{10,15}$";
		public const string INT_REGEX = @"^[0-9]\d*$";
		public const string FLOAT_REGEX = @"^-?\d+([.,]\d+)?$";
		public const string PASSWORD = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}$";
	}
}
