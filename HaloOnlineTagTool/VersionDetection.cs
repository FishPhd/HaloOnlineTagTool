﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaloOnlineTagTool
{
	/// <summary>
	/// ElDorado engine version constants.
	/// Listed in order from oldest to newest.
	/// </summary>
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public enum EngineVersion
	{
		Unknown = -1,
		V1_106708_cert_ms23,
		V1_235640_cert_ms25,
		V0_0_1_301003_cert_MS26_new,
		V0_4_1_327043_cert_MS26_new,
		V8_1_372731_Live,
		V0_0_416097_Live,
		V10_1_430475_Live,
		V10_1_454665_Live,
		V10_1_449175_Live, // NOTE: This actually seems to be newer than 454665 despite the lower build number
		V11_1_498295_Live,
		V11_1_530605_Live,
		V11_1_532911_Live,
		V11_1_554482_Live,
		V11_1_571627_Live,
	}

	public static class VersionDetection
	{
		/// <summary>
		/// Detects the engine that a tags.dat was built for.
		/// </summary>
		/// <param name="cache">The cache file.</param>
		/// <param name="closestGuess">On return, the closest guess for the engine's version.</param>
		/// <returns>The engine version if it is known for sure, otherwise <see cref="EngineVersion.Unknown"/>.</returns>
		public static EngineVersion DetectVersion(TagCache cache, out EngineVersion closestGuess)
		{
			return DetectVersionFromTimestamp(cache.Timestamp, out closestGuess);
		}

		/// <summary>
		/// Gets the version string corresponding to an <see cref="EngineVersion"/> value.
		/// </summary>
		/// <param name="version">The version.</param>
		/// <returns>The version string.</returns>
		public static string GetVersionString(EngineVersion version)
		{
			switch (version)
			{
				case EngineVersion.V1_106708_cert_ms23:
					return "1.106708 cert_ms23";
				case EngineVersion.V1_235640_cert_ms25:
					return "1.235640 cert_ms23";
				case EngineVersion.V0_0_1_301003_cert_MS26_new:
					return "0.0.1.301003 cert_MS26_new";
				case EngineVersion.V0_4_1_327043_cert_MS26_new:
					return "0.4.1.327043 cert_MS26_new";
				case EngineVersion.V8_1_372731_Live:
					return "8.1.372731 Live";
				case EngineVersion.V0_0_416097_Live:
					return "0.0.416097 Live";
				case EngineVersion.V10_1_430475_Live:
					return "10.1.430475 Live";
				case EngineVersion.V10_1_454665_Live:
					return "10.1.454665 Live";
				case EngineVersion.V10_1_449175_Live:
					return "10.1.449175 Live";
				case EngineVersion.V11_1_498295_Live:
					return "11.1.498295 Live";
				case EngineVersion.V11_1_530605_Live:
					return "11.1.530605 Live";
				case EngineVersion.V11_1_532911_Live:
					return "11.1.532911 Live";
				case EngineVersion.V11_1_554482_Live:
					return "11.1.554482 Live";
				case EngineVersion.V11_1_571627_Live:
					return "11.1.571627 Live";
				default:
					return version.ToString();
			}
		}

		/// <summary>
		/// Compares two version numbers.
		/// </summary>
		/// <param name="lhs">The left-hand version number.</param>
		/// <param name="rhs">The right-hand version number.</param>
		/// <returns>A positive value if the left version is newer, a negative value if the right version is newer, and 0 if the versions are equivalent.</returns>
		public static int Compare(EngineVersion lhs, EngineVersion rhs)
		{
			// Assume the enum values are in order by release date
			return (int)lhs - (int)rhs;
		}

		/// <summary>
		/// Detects the engine that a tags.dat was built for based on its timestamp.
		/// </summary>
		/// <param name="timestamp">The timestamp.</param>
		/// <param name="closestGuess">On return, the closest guess for the engine's version.</param>
		/// <returns>The engine version if the timestamp matched directly, otherwise <see cref="EngineVersion.Unknown"/>.</returns>
		private static EngineVersion DetectVersionFromTimestamp(long timestamp, out EngineVersion closestGuess)
		{
			var index = Array.BinarySearch(VersionTimestamps, timestamp);
			if (index >= 0)
			{
				// Version matches a timestamp directly
				closestGuess = (EngineVersion)index;
				return closestGuess;
			}

			// Match the closest timestamp
			index = Math.Max(0, Math.Min(~index - 1, VersionTimestamps.Length - 1));
			closestGuess = (EngineVersion)index;
			return EngineVersion.Unknown;
		}

		/// <summary>
		/// tags.dat timestamps for each game version.
		/// Timestamps in here should correspond directly to <see cref="EngineVersion"/> enum values (excluding <see cref="EngineVersion.Unknown"/>).
		/// </summary>
		private static readonly long[] VersionTimestamps =
		{
			130713360239499012, // V1_106708_cert_ms23
			130772932862346058, // V1_235640_cert_ms25
			130785901486445524, // V0_0_1_301003_cert_MS26_new
			130800445160458507, // V0_4_1_327043_cert_MS26_new
			130814318396118255, // V8_1_372731_Live
			130829123589114103, // V0_0_416097_Live
			130834294034159845, // V10_1_430475_Live
			130844512316254660, // V10_1_454665_Live
			130851642645809862, // V10_1_449175_Live
			130858473716879375, // V11_1_498295_Live
			130868891945946004, // V11_1_530605_Live
			130869644198634503, // V11_1_532911_Live
			130879952719550501, // V11_1_554482_Live
			130881889330693956, // V11_1_571627_Live
		};
	}
}
