﻿Shader "Unlit/Test"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" { }
    }

    SubShader
    {
        Tags
        {
            "RenderType" = "Opaque"
        }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            #include "UnityCustomRenderTexture.cginc"

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
            float4x4 _FixedMVP;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                float4 screenPos = ComputeScreenPos(mul(_FixedMVP, v.vertex));
				o.uv = float2(screenPos.xy / screenPos.w);
                return o;
            }

            fixed4 frag(v2f i) : COLOR
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                return col;
            }
            ENDCG
        }
    }
}