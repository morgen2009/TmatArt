using System;
using TmatArt.Geometry;
using TmatArt.Numeric.Mathematics;

namespace TmatArt.Scattering.Field
{
	public interface IFieldContainer: IField
	{
		IField getField();
		void setField(IField field);
	}
}

