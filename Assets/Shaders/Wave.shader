Shader "Soubra/Wave"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		Speed ("Wave Speed", Range(0.0, 5.0)) = 1.0
		TextureColor ("Color: ", Color) = (1,1,1,1)
		Opacity ("Opacity: ", Range(0.0, 1.0)) = 1.0
		Factor("Factor: ", Range(0.0, 20.0)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
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
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
			float4 TextureColor;
			float Speed;
			float Opacity;
			float Factor;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);

				float3 worldPosition = mul(unity_ObjectToWorld, v.vertex).xyz;

				o.vertex.y += sin(worldPosition.x + _Time.w * Speed) * cos(worldPosition.x + _Time.w * Speed) * Factor;
				o.vertex.y += sin(worldPosition.z + _Time.w * Speed) * cos(worldPosition.z + _Time.w * Speed) * Factor;
				o.vertex.y += sin(worldPosition.y + _Time.w * Speed) * cos(worldPosition.y + _Time.w * Speed) * Factor;

                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv) * float4(TextureColor.r, TextureColor.g, TextureColor.b, Opacity);
                return col;
            }
            ENDCG
        }
    }
}
