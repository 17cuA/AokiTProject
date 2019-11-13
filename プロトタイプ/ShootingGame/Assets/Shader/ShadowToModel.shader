
Shader "Custom/Enemy"
{
	Properties
	{
		_MainTex("MainTex",2D) = "black"{}

		_Hue("Hue", Range(0.0,1.0)) = 0
		_Sat("Saturation", Range(0.0,1.0)) = 0
		_Val("Value", Range(0.0,1.0)) = 0

		  _Factor("Factor", Range(0, 5)) = 1.0
	}

		SubShader
		{


			Pass
			{
				Tags
				{
					"RenderType" = "Opaque"
					"Ligting" = "ForwardBase"
				}


				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag

				#include "UnityCG.cginc"
				#include "AutoLight.cginc"

				sampler2D _MainTex;
				fixed4 _MainTex_ST;

				half _Hue;
				half _Sat;
				half _Val;

				//RGBからHSVに変換
				float3 RGBToHSV(float3 rgb)
				{
					float4 K = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
					float4 p = rgb.g < rgb.b ? float4(rgb.bg, K.wz) : float4(rgb.gb, K.xy);
					float4 q = rgb.r < p.x ? float4(p.xyw, rgb.r) : float4(rgb.r, p.yzx);

					float d = q.x - min(q.w, q.y);
					float e = 1.0e-10;
					return float3(abs(q.z + (q.w - q.y) / (6.0 * d + e)), d / (q.x + e), q.x);
				}
				//RGBからHSVに変換
				float3 HSVToRGB(float3 hsv)
				{
					float4 K = float4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
					float3 p = abs(frac(hsv.xxx + K.xyz) * 6.0 - K.www);
					return hsv.z * lerp(K.xxx, saturate(p - K.xxx), hsv.y);
				}

				struct VertexInput
				{
					float4 vertex : POSITION;
					float2 uv : TEXCOORD0;
				};

				struct VertexOutput
				{
					float4 vertex : POSITION;
					float2 uv : TEXCOORD0;
				};

				VertexOutput vert(VertexInput v)
				{
					VertexOutput o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.uv = v.uv;
					return o;
				}

				float4 frag(VertexOutput i) : SV_Target
				{
					float3 color = tex2D(_MainTex , TRANSFORM_TEX(i.uv, _MainTex));
					float3 hsv = RGBToHSV(color);
					hsv.x += _Hue;
					hsv.y += _Sat;
					hsv.z += _Val;

					color = HSVToRGB(hsv);

					return float4(color, 1);
				}

			ENDCG
			}

			Pass
				{
				Tags { "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
					
		
					CGPROGRAM
					#pragma vertex vert
					#pragma fragment frag
					#pragma fragmentoption ARB_precision_hint_fastest

					#include "UnityCG.cginc"

					struct appdata
					{
						float4 vertex : POSITION;
						float2 uv : TEXCOORD0;
					};

					struct v2f
					{
						float4 pos : SV_POSITION;
						float4 uv : TEXCOORD0;
					};

					v2f vert(appdata v)
					{
						v2f o;
						o.pos = UnityObjectToClipPos(v.vertex);
						o.uv = ComputeGrabScreenPos(o.pos);
						return o;
					}

					sampler2D _GrabTexture;
					float4 _GrabTexture_TexelSize;
					float _Factor;

					half4 frag(v2f i) : SV_Target
					{

						half4 pixelCol = half4(0, 0, 0, 0);

						#define ADDPIXEL(weight,kernelX) tex2Dproj(_GrabTexture, UNITY_PROJ_COORD(float4(i.uv.x + _GrabTexture_TexelSize.x * kernelX * _Factor, i.uv.y, i.uv.z, i.uv.w))) * weight

						pixelCol += ADDPIXEL(0.05, 4.0);
						pixelCol += ADDPIXEL(0.09, 3.0);
						pixelCol += ADDPIXEL(0.12, 2.0);
						pixelCol += ADDPIXEL(0.15, 1.0);
						pixelCol += ADDPIXEL(0.18, 0.0);
						pixelCol += ADDPIXEL(0.15, -1.0);
						pixelCol += ADDPIXEL(0.12, -2.0);
						pixelCol += ADDPIXEL(0.09, -3.0);
						pixelCol += ADDPIXEL(0.05, -4.0);
						return pixelCol;
					}
					ENDCG
				}

					GrabPass{ }

						Pass
					{
						CGPROGRAM
						#pragma vertex vert
						#pragma fragment frag
						#pragma fragmentoption ARB_precision_hint_fastest

						#include "UnityCG.cginc"

						struct appdata
						{
							float4 vertex : POSITION;
							float2 uv : TEXCOORD0;
						};

						struct v2f
						{
							float4 pos : SV_POSITION;
							float4 uv : TEXCOORD0;
						};

						v2f vert(appdata v)
						{
							v2f o;
							o.pos = UnityObjectToClipPos(v.vertex);
							o.uv = ComputeGrabScreenPos(o.pos);
							return o;
						}

						sampler2D _GrabTexture;
						float4 _GrabTexture_TexelSize;
						float _Factor;

						fixed4 frag(v2f i) : SV_Target
						{

							fixed4 pixelCol = fixed4(0, 0, 0, 0);

							#define ADDPIXEL(weight,kernelY) tex2Dproj(_GrabTexture, UNITY_PROJ_COORD(float4(i.uv.x, i.uv.y + _GrabTexture_TexelSize.y * kernelY * _Factor, i.uv.z, i.uv.w))) * weight

							pixelCol += ADDPIXEL(0.05, 4.0);
							pixelCol += ADDPIXEL(0.09, 3.0);
							pixelCol += ADDPIXEL(0.12, 2.0);
							pixelCol += ADDPIXEL(0.15, 1.0);
							pixelCol += ADDPIXEL(0.18, 0.0);
							pixelCol += ADDPIXEL(0.15, -1.0);
							pixelCol += ADDPIXEL(0.12, -2.0);
							pixelCol += ADDPIXEL(0.09, -3.0);
							pixelCol += ADDPIXEL(0.05, -4.0);
							return pixelCol;
						}
						ENDCG
					}
	}
}
