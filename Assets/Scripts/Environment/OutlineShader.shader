Shader "Unlit/OutlineShader"
{
    Properties{
		_MainTex ("Texture", 2D) = "white" {}
		_OutColor ("Tint", Color) = (1, 1, 1, 1)
		_OutValue ("Outline value", Range(0.0, 0.2)) = 0.1
	}

	SubShader{

		Pass{
			Tags{ "Queue"="Transparent" }
			Blend SrcAlpha OneMinusSrcAlpha
			//ZWrite Off

			CGPROGRAM

			#include "UnityCG.cginc"

			#pragma vertex vert
			#pragma fragment frag

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float4 _OutColor;
			float _OutValue;

			fixed4 _Color;

			struct appdata{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f {
				float4 position : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			float4 outline (float4 vertexPos, float outValue)
			{
				float4x4 scale = float4x4
				(
					1 + outValue, 0, 0, 0,
					0, 1 + outValue, 0, 0,
					0, 0, 1 + outValue, 0,
					0, 0, 0,  1 + outValue
				);
				return mul(scale, vertexPos);
			}

			v2f vert(appdata v){
				v2f o;
				float4 vertexPos = outline(v.vertex, _OutValue);
				o.position = UnityObjectToClipPos(vertexPos);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}

			fixed3 frag(v2f i) : SV_TARGET{
				fixed4 col = tex2D(_MainTex, i.uv);
				
				return float4(_OutColor.r, _OutColor.g, _OutColor.b, col.a);
			}
			
			ENDCG
		}

		//texture

		Pass{
			Tags{ "Queue"="Transparent+1" }
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM

			#include "UnityCG.cginc"

			#pragma vertex vert
			#pragma fragment frag

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float4 _OutColor;
			float _OutValue;

			fixed4 _Color;

			struct appdata{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f {
				float4 position : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			v2f vert(appdata v){
				v2f o;
				
				o.position = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}

			fixed3 frag(v2f i) : SV_TARGET{
				fixed4 col = tex2D(_MainTex, i.uv);
				return col;
			}
			
			ENDCG
		}
	}
}