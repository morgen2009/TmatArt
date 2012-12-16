using System;
using TmatArt.Geometry.Region;

namespace TmatArt.Scattering.Field.Operation
{
	public class PlaneWaveService: IReflectOperation, IExpansionOperation, IFieldOperation<PlaneWave>
	{
		/* begin of singleton pattern */
		private static PlaneWaveService instance;
		private PlaneWaveService () { }
		public static PlaneWaveService Instance()
		{
			if (PlaneWaveService.instance == null) {
				PlaneWaveService.instance = new PlaneWaveService();
			}

			return PlaneWaveService.instance;
		}
		/* end of singleton pattern */

		public void SetField(PlaneWave field)
		{
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

