using PolyWare.AssetManagement;
using UnityEngine;

namespace Alpaca.Collections {
	[CreateAssetMenu(menuName = "PolyWare/Collections/ColorCollection", fileName = "New ColorCollection")]
	public class ColorCollection : StringCollection<Color> { }
}