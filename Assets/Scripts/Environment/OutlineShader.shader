Shader "Custom/OutlineShader"
{
    Properties
	{
		_MainTex("Main Texture (RBG)", 2D) = "white" {}
		_Color("Color", Color) = (1,1,1,1)

		_OutlineTex("Outline Texture", 2D) = "white" {}
		_OutlineColor("Outline Color", Color) = (1,1,1,1)
		_OutlineWidth("Outline Width", Range(1.0,10.0)) = 1.1
		
		[Enum(UnityEngine.Rendering.CullMode)] _Cull("Cull", Float) = 2
	}

	SubShader
	{
		Tags 
		{
			"Queue" = "Transparent"
		}
		//Cull[_Cull]

		Pass
		{
			ZWrite Off
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
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			float _OutlineWidth;
			float4 _OutlineColor;
			sampler2D _OutlineTex;

			v2f vert(appdata IN)
			{
				IN.vertex.xyz *= _OutlineWidth;
				v2f OUT;

				OUT.pos = UnityObjectToClipPos(IN.vertex);
				OUT.uv = IN.uv;

				return OUT;
			}

			fixed4 frag(v2f IN) : SV_Target
			{
				float4 texColor = tex2D(_OutlineTex, IN.uv);
				return texColor * _OutlineColor;
			}
			ENDCG
		}

		Pass
		{
			Tags { "LightMode" = "UniversalForward" }
			ZWrite On
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

            struct v2f {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            float4 _Color;
            float4 _MainTex_ST;

			v2f vert (appdata v){
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}
			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);
				//fixed4 col1 = col;
				//clip(col.r - 1);

				fixed4 colf = col * _Color;

				return colf;
			}  
			ENDCG
		}
	}
}