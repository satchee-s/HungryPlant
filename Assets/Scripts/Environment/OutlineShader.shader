Shader "Custom/OutlineShader"
{
    Properties
	{
		_MainTex("Main Texture (RBG)", 2D) = "white" {}
		_Color("Color", Color) = (1,1,1,1)

		_OutlineTex("Outline Texture", 2D) = "white" {}
		_OutlineColor("Outline Color", Color) = (1,1,1,1)
		_OutlineWidth("Outline Width", Range(1.0,10.0)) = 1.1
		//_Material("Default material", Material) = new Material
	}

	SubShader
	{
		Tags 
		{
			"Queue" = "Transparent"
			//"RenderType" = "Transparent"
		}
		Pass
		{
			ZWrite Off
			//Blend SrcAlpha OneMinusSrcAlpha
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

			v2f vert(appdata v)
			{
				v.vertex.xyz *= _OutlineWidth;
				v2f OUT;
				OUT.pos = UnityObjectToClipPos(v.vertex);
				OUT.uv = v.uv;

				return OUT;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				float4 texColor = tex2D(_OutlineTex, i.uv);
				texColor *= (1, 1, 1, 0);
				return texColor * _OutlineColor;
			}
			ENDCG
		}

		/*Pass
		{
			Tags 
			{ 
				"LightMode" = "UniversalForward" 
				//"Queue" = "Transparent"
				//"RenderType" = "Transparent"
			}
			ZWrite Off
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
				col *= (0, 0, 0, 0);
				return col;
			}  
			ENDCG
		}*/
	}
}