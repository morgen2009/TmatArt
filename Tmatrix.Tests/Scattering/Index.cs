using System;
using NUnit.Framework;
using TmatArt.Scattering.Indexing.Mappers;
using TmatArt.Geometry;
using System.Linq;

namespace TmatArt.Scattering.Indexing
{
	[TestFixture()]
	public class IndexTest
	{
		public class MapperTest: Mapper {
			public MapperTest (Symmetry symmetry, int nrank, int mrank): base(symmetry, nrank, mrank)
			{
			}
			public new int hashIndex(int n, int m, Index.Type l)
			{
				return base.hashIndex(n,m,l);
			}
		};
		
		[Test()]
		public void testMapper ()
		{
			/* to test Mapper.hashIndex */
			Symmetry sym = Symmetry.getInstance().Rotate(Symmetry.RotateType.AxisZ, 4);
			int nrank = 5;
			int mrank = 5;
			MapperTest map = new MapperTest(sym, nrank, mrank);
			
			Console.WriteLine(String.Format("{0}\t{1}\t{2}", nrank, mrank, map.nmax));
			int m = -5;
			for (int n = Math.Abs(m); n <= nrank; n++)
					Console.WriteLine(String.Format("{0}\t{1}\t{2}", n, m, map.hashIndex(n, m, Index.Type.M)));
			
			//Assert.AreEqual(map.hashIndex(2,2, Index.Type.M), map.hashIndex(2,2, Index.Type.N));			
			//Assert.Fail();
		}
		
		[Test()]
		public void testMapperDoicu ()
		{
			/* to test MapperCase.GenerallDoicu */
			MapperAggregator agg = MapperAggregator.getInstance();
			agg.registerFactory(GeneralDoicuFactory.getInstance());
			
			Symmetry sym = Symmetry.getInstance().Rotate(Symmetry.RotateType.AxisZ, 4);
			int nrank = 5;
			int mrank = 5;
			Mapper map = agg.getMapper(sym, nrank, mrank);
			
			/*for (int i = 0; i < map.count(); i++)
			{
				Index id = map.index(i);
				Console.WriteLine(String.Format("{0}\t{1}\t{2}\t{3}\t{4}", i, id.position, id.n, id.m, id.l));
			}*/
			
			Assert.AreEqual(map.blocks().First().items().Distinct().Count(), map.count());
			Assert.AreEqual(map.blocks().First().items().Where(b => b.position == 0).Count(), 1);
			Assert.AreEqual(map.blocks().First().items().Min( b => b.position ), 0);
			Assert.AreEqual(map.blocks().First().items().Max( b => b.position ), map.count()-1);
			
			//Assert.Fail();
		}
		
		[Test()]
		public void testMapperGeneral ()
		{
			/* to test MapperCase.Generall */			
			Symmetry sym = Symmetry.getInstance().Rotate(Symmetry.RotateType.AxisZ, 4).Reflect(Symmetry.RectectType.PlaneZ);
			int nrank = 5;
			int mrank = 5;
			Mapper map = GeneralFactory.getInstance().createMapper(sym, nrank, mrank);
			
			Assert.AreEqual(map.blocks().SelectMany(x => x.items()).Distinct().Count(), map.count());
			Assert.AreEqual(map.blocks().SelectMany(x => x.items()).Where(b => b.position == 0).Count(), 1);
			Assert.AreEqual(map.blocks().SelectMany(x => x.items()).Min(b => b.position), 0);
			Assert.AreEqual(map.blocks().SelectMany(x => x.items()).Max(b => b.position), map.count()-1);
			
			Console.WriteLine("Blocks");
			foreach (Block b in map.blocks())
			{
				int b2 = b.offset;
				if (b.parent != null) b2 = b.parent.offset;
				Console.WriteLine(String.Format("{0} {1} -> {2}", b.offset, b.length, b2));
			}
			
			Console.WriteLine("Map");
			for (int i = 0; i < map.count(); i++)
			{
				Index ind = map.index(i);
				int block = map.offset(ind.position).block;
				bool flag = map.blocks(block).isMaster();
				Console.WriteLine(String.Format("{0} {1} = {2} {3} {4} {5}", block, ind.position, ind.n, ind.m, ind.l, flag ));
			}			
			//Assert.Fail();
		}
	}
}

