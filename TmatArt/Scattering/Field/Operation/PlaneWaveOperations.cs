using System;
using TmatArt.Geometry.Region;

namespace TmatArt.Scattering.Field.Operation
{
	public class PlaneWaveOperations: IReflectOperation, IExpansionOperation
	{
		/* begin of singleton pattern */
		private static PlaneWaveOperations instance;
		private PlaneWaveOperations () { }
		public static PlaneWaveOperations Instance()
		{
			if (PlaneWaveOperations.instance == null) {
				PlaneWaveOperations.instance = new PlaneWaveOperations();
			}

			return PlaneWaveOperations.instance;
		}
		/* end of singleton pattern */

		public IFieldOperation Select(Field field)
		{
			//this.field = field;
			throw new System.NotImplementedException ();
		}

		public Field Reflect(Halfspace region, Medium.Isotrop mediumExt)
		{
			throw new System.NotImplementedException ();
		}

		public Field Transmit(Halfspace region, Medium.Isotrop mediumExt)
		{
			throw new System.NotImplementedException ();
		}

		public Expansion Expansion(int nrank, int mrank)
		{
			throw new System.NotImplementedException ();
		}
	}
}

