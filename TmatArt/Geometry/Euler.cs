using System;
using TmatArt.Numeric.Mathematics;

namespace TmatArt.Geometry
{
	/// <summary>
	/// Euler rotation angles
	/// </summary>
	public struct Euler : IGroupOperations<Euler>
	{
		/// <summary>
		/// Euler anglea &alpha;, &beta;, &gamma
		/// </summary>
		public double alpha, beta, gamma;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="alpha">Alpha angle</param>
		/// <param name="beta">Beta angle</param>
		/// <param name="gamma">Gamma angle</param>
		public Euler (double alpha, double beta, double gamma)
		{
			this.alpha = alpha;
			this.beta  = beta;
			this.gamma = gamma;
		}

		/* implementation of IEqualityComparer */
		public bool Equals(Euler a, Euler b)
		{
			return a.alpha.Equals(b.alpha) && a.beta.Equals(b.beta) && a.gamma.Equals(b.gamma);
		}

		public int GetHashCode(Euler a)
		{
			return a.alpha.GetHashCode() + a.beta.GetHashCode() + a.gamma.GetHashCode();
		}

		/* implementation of IGroupOperations */
		public Euler Add(Euler a)
		{
			Vector3d e3 = new Vector3d(0, 0, 1);
			Vector3d e3t = e3.Rotate(a.Negate()).Rotate(this.Negate());

			Vector3d e2 = new Vector3d(0, 1, 0);
			Vector3d e2t = e2.Rotate(a.Negate()).Rotate(this.Negate());

			// beta
			double beta = System.Math.Acos(e3t.z);

			// alpha
			double alpha = System.Math.Acos(e3t.x / System.Math.Sqrt(1-e3t.z*e3t.z));
			if (double.IsNaN(alpha)) {
				alpha = 0;
			} else {
				if (e3t.y < 0) {
					alpha = 2 * System.Math.PI - alpha;
				}
			}

			// gamma
			e2 = e2.RotateZ(-alpha);
			double gamma = System.Math.Acos(e2 * e2t);
			if ((e2 ^ e3t) * e2t > 0) {
				gamma = 2 * System.Math.PI - gamma;
			}

			return new Euler(alpha, beta, gamma);
		}

		public Euler Subtract(Euler a)
		{
			return this.Add(a.Negate());
		}

		public Euler Negate()
		{
			return new Euler(-this.gamma, -this.beta, -this.alpha).Normalize();
		}

		public Euler Zero()
		{
			return new Euler(0, 0, 0);
		}

		/* static operations */
		public static Euler operator + (Euler a, Euler b) { return a.Add(b); }
		public static Euler operator - (Euler a, Euler b) { return a.Subtract(b); }
		public static Euler operator - (Euler a) { return a.Negate(); }

		/// <summary>
		/// Normalize Euler angles that
		/// <list type="list">
		/// <item>0 &le; &alpha; 2*&pi;</item>
		/// <item>0 &le; &beta; &pi;</item>
		/// <item>0 &le; &gamma; 2*&pi;</item>
		/// </list>
		/// </summary>
		public Euler Normalize()
		{
			Euler res = this;

			Func<double, double> limit_0_2PI = delegate (double angle ) {
				while (angle < 0) {
					angle += System.Math.PI * 2;
				}
				
				while (angle >= System.Math.PI * 2) {
					angle -= System.Math.PI * 2;
				}

				return angle;
			};

			res.beta  = limit_0_2PI(res.beta);
			if (res.beta > System.Math.PI) {
				res.alpha = System.Math.PI + res.alpha;
				res.beta  = System.Math.PI * 2 - res.beta;
				res.gamma = System.Math.PI + res.gamma;
			}

			res.alpha = limit_0_2PI(res.alpha);
			res.gamma = limit_0_2PI(res.gamma);

			return res;
		}
	}
}