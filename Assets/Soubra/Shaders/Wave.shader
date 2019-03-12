Shader "Soubra/Wave"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_Softness("Softness", Range(0, 5.0)) = 0.2
		TextureColor("Color: ", Color) = (1,1,1,1)
		HighColor("High Color: ", Color) = (1,1,1,1)
		LowColor("Low Color: ", Color) = (1,1,1,1)
		Speed("Wave Speed", Range(0.0, 5.0)) = 1.0
		Opacity("Opacity: ", Range(0.0, 1.0)) = 1.0
		Factor("Factor: ", Range(0.0, 20.0)) = 0.0
	}
		SubShader
		{
			Tags { "RenderType" = "Opaque" }
			LOD 100

			Pass
			{
				Blend SrcAlpha OneMinusSrcAlpha

				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag

				#include "UnityCG.cginc"

				struct appdata
				{
					float4 vertex : POSITION;
					float2 uv : TEXCOORD0;

				};

				struct v2f
				{
					float2 uv : TEXCOORD0;
					float4 vertex : SV_POSITION;
					float4 color : COLOR;
				};

				sampler2D _MainTex;
				float4 _MainTex_ST;
				float _Softness;
				float4 TextureColor;
				float4 HighColor;
				float4 LowColor;
				float Factor;
				float Speed;
				float Opacity;

				v2f vert(appdata v)
				{
					v2f o;



					float4 wpos = mul(unity_ObjectToWorld, v.vertex);

					float offset = sin(wpos.x + _Time.w * Speed)  * Factor;
					offset += sin(wpos.y + _Time.w * Speed)  * Factor;
					offset += sin(wpos.z + _Time.w * Speed) * Factor;

					wpos.y -= offset;

					v.vertex = mul(unity_WorldToObject, wpos);
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.color = lerp(LowColor, HighColor, v.vertex.y * _Softness);
					o.uv = TRANSFORM_TEX(v.uv, _MainTex);
					return o;
				}

				fixed4 frag(v2f i) : SV_Target
				{
					fixed4 color = tex2D(_MainTex, i.uv);

					fixed4 col = fixed4(color.rgb, color.a) * (float4(TextureColor.r,TextureColor.g,TextureColor.b, Opacity));
					return col * i.color;
				}
				ENDCG
			}
		}
}
