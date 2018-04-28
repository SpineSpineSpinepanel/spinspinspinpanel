Shader "Custom/shForCards" {
	Properties
	{
		_MainTex ("Base (RGB), Alpha (A)", 2D) = "white" {}
	}
	
	SubShader
	{
		LOD 200

		Tags
		{
			"Queue" = "Transparent+200"
			"IgnoreProjector" = "True"
		}
		
		Pass
		{
			Cull Off
			Lighting Off
			Alphatest Off
			ZWrite Off
			Fog { Mode Off }
			Offset -1, -1
			Blend SrcAlpha OneMinusSrcAlpha
			ColorMaterial AmbientAndDiffuse
			
			SetTexture [_MainTex]
			{
				Combine Texture * Primary
			}
		}
	}
}
