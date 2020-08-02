using System.ComponentModel;
using System.Runtime.Serialization;
using Terraria;
using Terraria.ModLoader.Config;

namespace ShakyMouse{
	public class ShakyConfig : ModConfig{
		public override ConfigScope Mode => ConfigScope.ClientSide;

		/// <summary>
		/// Controls how shaky the mouse will be
		/// </summary>
		[Range(0f, 5f)]
		[Increment(0.1f)]
		[DefaultValue(1f)]
		[Label("Mouse Vibeness")]
		[Tooltip("How much the mouse should vibrate compared to the default vibing")]
		public float ShakeFactor;

		[DefaultValue(false)]
		[Label("Disable Vibing")]
		[Tooltip("Prevent or enable mouse vibing")]
		public bool DisableShake;

		[OnDeserialized]
		internal void OnDeserializedEvent(StreamingContext context){
			ShakeFactor = Utils.Clamp(ShakeFactor, 0f, 5f);
		}
	}
}
