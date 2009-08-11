using System.Xml;
using RuffnTumble.Asset;
using ArcEngine;

namespace RuffnTumble.Interface
{
	/// <summary>
	/// Interface pour la gestion des entite par scripting
	/// </summary>
	public interface IEntity
	{
		/// <summary>
		/// Initializes the entity 
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		bool Init(Entity entity);


		/// <summary>
		/// Updates the entity every frame
		/// </summary>
		/// <param name="entity"></param>
		void Update(Entity entity, GameTime time);


		/// <summary>
		/// Saves the entity parameters to the bank
		/// </summary>
		/// <param name="entity"></param>
		/// <param name="xml"></param>
		/// <returns></returns>
		bool Save(Entity entity, XmlWriter xml);


		/// <summary>
		/// Loads entity parameters from a bank
		/// </summary>
		/// <param name="entity"></param>
		/// <param name="xml"></param>
		/// <returns></returns>
		bool Load(Entity entity, XmlNode xml);

	}

}
