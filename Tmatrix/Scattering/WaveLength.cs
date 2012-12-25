using System;

namespace TmatArt.Scattering
{
	/// <summary>
	/// Length of the wave in the electromagnetic field
	/// </summary>
	public struct WaveLength : IUnit<WaveLength, WaveLength.Unit>
	{
		/// <summary>
		/// Set of possible units
		/// </summary>
		public enum Unit {NM, MICRON, EV};

		/// <summary>
		/// Default unit
		/// </summary>
		public const Unit Default = Unit.NM;

		/// <summary>
		/// Wavelength value
		/// </summary>
		public double length;

		/// <summary>
		/// Physical unit in whicht the value is defined
		/// </summary>
		public Unit unit;

		/// <summary>
		/// Physical constants
		/// </summary>
		const double H_PLANK     = 6.6261e-34;     // [J-s]
		const double C_VELOCITY  = 2.997924580e8;  // [m/s]
		const double E_CHARGE    = 1.60217733e-19; // [1 ev = 1.602e-19 J]

		/// <summary>
		/// Initialize new WaveLength instance
		/// </summary>
		/// <param name="length">WaveLength value.</param>
		/// <param name="unit">Unit, in which the value is defined.</param>
		public WaveLength(double length, Unit unit = WaveLength.Default)
		{
			this.length = length;
			this.unit   = unit;
		}

		/// <see cref="WaveLength" />
		public static WaveLength Value(double length, Unit unit = WaveLength.Default)
		{
			return new WaveLength(length, unit);
		}

		/// <summary>
		/// Conversion to double
		/// </summary>
		public static implicit operator double(WaveLength wave)
		{
			return wave.length;
		}

		/// <summary>
		/// Recompute wavelength into specified unit
		/// </summary>
		/// <param name="unit">Unit.</param>
		public WaveLength In(WaveLength.Unit unit)
		{
			if (this.unit == unit) {
				return this;
			}
			
			// value [old] -> default
			switch (this.unit) {
			case Unit.MICRON : length *= 1000; break;
			case Unit.EV     : length = WaveLength.C_VELOCITY * WaveLength.H_PLANK / (length * WaveLength.E_CHARGE * 1E-9); break;
			default : break;
			}
			
			// default -> value [new]
			switch (unit) {
			case Unit.MICRON : length /= 1000; break;
			case Unit.EV     : length = WaveLength.C_VELOCITY * WaveLength.H_PLANK / (length * WaveLength.E_CHARGE * 1E-9); break;
			default : break;
			}

			this.unit = unit;
			return this;
		}
	}
}

