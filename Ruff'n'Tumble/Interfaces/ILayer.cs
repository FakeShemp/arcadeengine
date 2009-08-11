
using RuffnTumble.Asset;



namespace RuffnTumble.Interface
{
	/// <summary>
	/// Interface pour la gestion des layers
	/// </summary>
	public interface ILayer
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="layer"></param>
		/// <returns></returns>
		bool Init(Layer layer);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="layer"></param>
		void Update(Layer layer);


	}

}
