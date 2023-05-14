Shader "Unlit/ToonShader"
{
    Properties
    {
        // Toon effect
        _Brightness("Brightness", Range(0,1)) = 0.3
        _Strength("Strength", Range(0,1)) = 0.5
        _Color("Color", COLOR) = (1,1,1,1)
        _Detail("Detail", Range(0,1)) = 0.3
        // Look
        _UseTexture("Use Texture", Range(0,1)) = 0
        _MainTex ("Texture", 2D) = "white" {}
        _MaterialColor("Material Color", COLOR) = (1,1,1,1)
        // Outline
        _OutlineColor("Outline Color", COLOR) = (0, 0, 0, 1)
        _OutlineWidth("Outline Width", Range(0, 1)) = 0.1
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        // Toon shader pass
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                half3 worldNormal: NORMAL;
            };

            float _Brightness;
            float _Strength;
            float4 _Color;
            float _Detail;
            float4 _MaterialColor;
            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _UseTexture;
            // Outline
            float4 _OutlineColor;
            float _OutlineWidth;

            float Toon(float3 normal, float3 lightDir)
            {
                float NdotL = max(0.0, dot(normalize(normal), normalize(lightDir)));
                return floor(NdotL / _Detail);
            }

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col;
                if (_UseTexture > 0)
                {
                    col = tex2D(_MainTex, i.uv);
                }
                else
                {
                    col = _MaterialColor;
                }
                
                col *= Toon(i.worldNormal, _WorldSpaceLightPos0.xyz) * _Strength * _Color + _Brightness;
                return col;
            }
            ENDCG
        }
    }
}

