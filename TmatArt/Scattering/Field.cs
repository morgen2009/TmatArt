using System;

namespace TmatArt.Scattering
{
	public abstract class Field<Medium>
	{
/*		Medium medium;
		
		// Coordinate coord
		public Field (Medium medium)
		{
			this.medium = medium;
		}
		
		public abstract Field shift(double x, double y, double z);
		public abstract Field shift(Vector r);
		public abstract Field rotate(double alpha, double beta, double gamma);
		public abstract Field rotate(Euler angle);
		public abstract Field reflect(Medium medium, double z);
		public abstract Field transmit(Medium medium, double z);
		
		public abstract Vector near(Vector r);
		public abstract Vector far(double phi, double theta);*/
	}
}

/*
 * FieldExpension<ExpensionBasis> : Field
 * IncommingExpension : FieldExpension<IncommingSVWF>
 * OutcommingExpension : FieldExpension<OutcommingSVWF> 
 * PlaneWave : IncommingExpension
 * ReflectedWave : PlaneWave
 * TransmittedWave : PlaneWave
 * ReflectedExpension: IncommingExpension
 * TransmittedExpension: OutcommingExpension
 * ...
 * AggregateWave : Expension
 * 
 * 
 * PlaneWave
 * 	convert to IExpension ???
 * 
 * 
 * Field f;
 * 
 * Field f2 = f.clone();
 * 
 * f2.rotate(10,10,20).reflect(10).rotate(-20,-10,-10);
 * 
 * 
 */

