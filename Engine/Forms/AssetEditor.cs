using ArcEngine.Asset;
using WeifenLuo.WinFormsUI.Docking;

namespace ArcEngine.Forms
{

	/// <summary>
	/// Base classe for asset editor
	/// </summary>
	public class AssetEditor : DockContent
	{

		/// <summary>
		/// Save the asset
		/// </summary>
		public virtual void Save()
		{
			throw new System.NotImplementedException("Asset");
		}


		#region Properties


		/// <summary>
		/// Gets the edited asset 
		/// </summary>
		public virtual IAsset Asset
		{
			get
			{
				throw new System.NotImplementedException("Asset");
			}
		}

		#endregion
	}
}
