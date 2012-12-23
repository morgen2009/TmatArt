using System;

namespace TmatArt.Scattering
{
	/// <summary>
	/// Length of the wave in the electromagnetic field
	/// </summary>
	public class WaveLength
	{
		/// <summary>
		/// Physical units for the wavelength
		/// </summary>
		public enum Unit {NM, MICRON, EV};

		/// <summary>
		/// Physical constants
		/// </summary>
		const double H_PLANK     = 6.6261e-34;     // [J-s]
		const double C_VELOCITY  = 2.997924580e8;  // [m/s]
		const double E_CHARGE    = 1.60217733e-19; // [1 ev = 1.602e-19 J]

		public double value;
		public Unit unit;

		/// <summary>
		/// Initializes a new instance of the <see cref="TmatArt.Scattering.WaveLength"/> class.
		/// </summary>
		/// <param name="value">Wavelength value.</param>
		/// <param name="unit">Units.</param>
		public WaveLength (double value, Unit unit = Unit.NM)
		{
			this.value = value;
			this.unit  = unit;
		}

		/// <summary>
		/// Return wavelength value
		/// </summary>
		public double Length()
		{
			return this.value;
		}
		
		/// <summary>
		/// Return wavelength value in the specified units
		/// </summary>
		public double Length(Unit unit)
		{
			return this.Rescale(this.value, this.unit, unit);
		}
		
		/// <summary>
		/// Recompute wavelength value into given units
		/// </summary>
		private double Rescale(double value, Unit unitOld, Unit unitNew)
		{
			if (unitOld == unitNew) {
				return value;
			}

			// value [old] -> value [nm]
			switch (unitOld) {
			case Unit.MICRON : value *= 1000; break;
			case Unit.EV     : value = WaveLength.C_VELOCITY * WaveLength.H_PLANK / (value * WaveLength.E_CHARGE * 1E-9); break;
			default : break;
			}
			
			// value [nm] -> value [new]
			switch (unitNew) {
			case Unit.MICRON : value /= 1000; break;
			case Unit.EV     : value = WaveLength.C_VELOCITY * WaveLength.H_PLANK / (value * WaveLength.E_CHARGE * 1E-9); break;
			default : break;
			}
			
			return value;
		}
	}
}

