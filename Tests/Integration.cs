using System;
using NUnit.Framework;
using System.Linq;

namespace TmatArt.Numeric.Integration
{
	[TestFixture()]
	public class MeshTest
	{
		internal class TestCaseData
		{
			public Func<double, double> func;
			public int left = 0, right = 1;
			public double result;
			public double epsilon = 1E-5;
		}
		
		private TestCaseData[] getTestCaseSource ()
		{
			return new []
			{
				new TestCaseData { func=delegate(double x) { return x*x; }, result=1.0/3 },
				new TestCaseData { func=delegate(double x) { return 1/(x+1); }, result=System.Math.Log(2) },
				new TestCaseData { func=delegate(double x) { return x*x*x; }, result=1.0/4 }
			};
			
		}
		
		[Test()]
		public void RectMesh ()
		{
			int count = 1003;
			TestCaseData [] source = this.getTestCaseSource();
			MeshRect.IntegralType type = MeshRect.IntegralType.Simpson38;
			
			foreach (TestCaseData data in source)
			{
				MeshRect mesh = new MeshRect(count, data.left, data.right, type);
				double res = 0;
				foreach (MeshNode node in mesh.nodes())
				{
					Console.WriteLine(String.Format("{0} {1} {2}", node.point, node.weight, data.func(node.point)));
					res += data.func(node.point) * node.weight;
				}
				Console.WriteLine(String.Format("Norm {0}", mesh.norm));
				res = res * mesh.norm;
				
				Assert.AreEqual(data.result, res, data.epsilon);
			}
		}

		[Test()]
		public void GaussMesh ()
		{
			int count = 10;
			TestCaseData [] source = this.getTestCaseSource();
			
			foreach (TestCaseData data in source)
			{
				Numeric.Polynomial.Legendre pol = new Numeric.Polynomial.Legendre();
				MeshGauss mesh = new MeshGauss(count, pol, data.left, data.right);
				double res = 0;
				foreach (MeshNode node in mesh.nodes())
				{
					Console.WriteLine(String.Format("{0} {1} {2}", node.point, node.weight, data.func(node.point)));
					res += data.func(node.point) * node.weight;
				}
				Console.WriteLine(String.Format("Norm {0}", mesh.norm));
				res = res * mesh.norm;
				
				Assert.AreEqual(data.result, res, data.epsilon);
				//Assert.Fail();
			}
		}

		[Test()]
		public void AbitraryMesh ()
		{
			int count = 3;
			TestCaseData [] source = this.getTestCaseSource();
			
			foreach (TestCaseData data in source)
			{
				Numeric.Polynomial.Arbitrary pol = new Numeric.Polynomial.Arbitrary(data.left, data.right, count, data.func, 100);
				//Console.WriteLine(pol.roots(count).Count());
				//foreach (double x in pol.roots(count)) Console.WriteLine(x);
				MeshGauss mesh = new MeshGauss(count, pol, data.left, data.right);
				double res = 0;
				foreach (MeshNode node in mesh.nodes())
				{
					double val = 1;
					Console.WriteLine(String.Format("{0} {1} {2}", node.point, node.weight, val));
					res += val * node.weight;
				}
				//Console.WriteLine(String.Format("Norm {0}", mesh.norm));
				res = res * mesh.norm;
				//Console.WriteLine(String.Format("{0} {1}", res, data.result));
				Assert.AreEqual(data.result, res, data.epsilon);
			}
			//Assert.Fail();
		}
	}
}

