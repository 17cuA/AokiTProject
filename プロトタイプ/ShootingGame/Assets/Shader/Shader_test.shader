Shader "Custom/Shader_test"
{
    Properties
    {
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0
		_Factor("Factor", Range(0, 5)) = 1.0

		_Hue("Hue", Float) = 0
		_Sat("Saturation", Float) = 1
		_Val("Value", Float) = 1 }
    SubShader
    {

        Tags { "RenderType"="Opaque" "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent"}
        
		GrabPass { }

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
		
		GrabPass { }

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
		LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
		{
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        half _Hue, _Sat, _Val;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
        // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)
        
        // RGB->HSV変換
        float3 rgb2hsv(float3 rgb)
        {
            float3 hsv;

            // RGBの三つの値で最大のもの
            float maxValue = max(rgb.r, max(rgb.g, rgb.b));
            // RGBの三つの値で最小のもの
            float minValue = min(rgb.r, min(rgb.g, rgb.b));
            // 最大値と最小値の差
            float delta = maxValue - minValue;
            
            // V（明度）
            // 一番強い色をV値にする
            hsv.z = maxValue;
            
            // S（彩度）
            // 最大値と最小値の差を正規化して求める
            if (maxValue != 0.0)
			{
                hsv.y = delta / maxValue;
            } else {
                hsv.y = 0.0;
            }
            
            // H（色相）
            // RGBのうち最大値と最小値の差から求める
            if (hsv.y > 0.0)
			{
                if (rgb.r == maxValue) 
				{
                    hsv.x = (rgb.g - rgb.b) / delta;
                }
				else if (rgb.g == maxValue)
				{
                    hsv.x = 2 + (rgb.b - rgb.r) / delta;
                }
				else 
				{
                    hsv.x = 4 + (rgb.r - rgb.g) / delta;
                }
                hsv.x /= 6.0;
                if (hsv.x < 0)
                {
                    hsv.x += 1.0;
                }
            }
            
            return hsv;
        }
        
        // HSV->RGB変換
        float3 hsv2rgb(float3 hsv)
        {
            float3 rgb;

            if (hsv.y == 0)
			{
                // S（彩度）が0と等しいならば無色もしくは灰色
                rgb.r = rgb.g = rgb.b = hsv.z;
            } 
			else 
			{
                // 色環のH（色相）の位置とS（彩度）、V（明度）からRGB値を算出する
                hsv.x *= 6.0;
                float i = floor (hsv.x);
                float f = hsv.x - i;
                float aa = hsv.z * (1 - hsv.y);
                float bb = hsv.z * (1 - (hsv.y * f));
                float cc = hsv.z * (1 - (hsv.y * (1 - f)));
                if( i < 1 )
				{
                    rgb.r = hsv.z;
                    rgb.g = cc;
                    rgb.b = aa;
                } 
				else if( i < 2 ) 
				{
                    rgb.r = bb;
                    rgb.g = hsv.z;
                    rgb.b = aa;
                }
				else if( i < 3 ) 
				{
                    rgb.r = aa;
                    rgb.g = hsv.z;
                    rgb.b = cc;
                } 
				else if( i < 4 ) 
				{
                    rgb.r = aa;
                    rgb.g = bb;
                    rgb.b = hsv.z;
                } 
				else if( i < 5 )
				{
                    rgb.r = cc;
                    rgb.g = aa;
                    rgb.b = hsv.z;
                }
				else
				{
                    rgb.r = hsv.z;
                    rgb.g = aa;
                    rgb.b = bb;
                }
            }
            return rgb;
        }
        
        float3 shift_col(float3 rgb, half3 shift)
        {
            // RGB->HSV変換
            float3 hsv = rgb2hsv(rgb);
            
            // HSV操作
            hsv.x += shift.x;
            if (1.0 <= hsv.x)
            {
                hsv.x -= 1.0;
            }
            hsv.y *= shift.y;
            hsv.z *= shift.z;
            
            // HSV->RGB変換
            return hsv2rgb(hsv);
        }

        void surf (Input IN, inout SurfaceOutputStandard o) 
		{
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            half3 shift = half3(_Hue, _Sat, _Val);
            fixed4 shiftColor = fixed4(shift_col(c.rgb, shift), c.a);
            o.Albedo = shiftColor.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = shiftColor.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
