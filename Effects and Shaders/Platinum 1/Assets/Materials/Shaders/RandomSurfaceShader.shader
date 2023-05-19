Shader "Custom/FireSurfaceShader" {
    Properties {
        _FlameColor ("Flame Color", Color) = (1,1,1,1)
        _FlameShape ("Flame Shape", 2D) = "white" {}
        _FlameDirection ("Flame Direction", Vector) = (1,1,0,0)
        _DistortionAmount ("Distortion Amount", Range(0, 10)) = 1
        _FadeControl ("Fade Control", Vector) = (1,1,0,0)
        _FadePower ("Fade Power", Range(0, 10)) = 1
        _FadeScale ("Fade Scale", Range(0, 10)) = 1
    }

    SubShader {
        Tags {"Queue"="Transparent" "RenderType"="Opaque"}
        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            
            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };
            
            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };
            
            sampler2D _FlameShape;
            float2 _FlameDirection;
            float _DistortionAmount;
            float2 _FadeControl;
            float _FadePower;
            float _FadeScale;
            float4 _FlameColor;
            
            float2 voronoi(float2 x) {
                float2 i = floor(x);
                float2 f = frac(x);
                float m = 8.0;
                float2 res;
                for (int a = -1; a <= 1; a++) {
                    for (int b = -1; b <= 1; b++) {
                        float2 p = i + float2(a, b);
                        float2 r = frac(p) - 0.5;
                        float d = dot(r, r);
                        if (d < m) {
                            m = d;
                            res = p;
                        }
                    }
                }
                return res + f;
            }
            
            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target {
                float2 UV = i.uv;
                float2 offset = (_FlameDirection * _Time.y) * _DistortionAmount;
                UV += offset;
                UV *= _FadeControl;
                float4 noise = tex2D(_FlameShape, UV);
                float4 c = lerp(noise, _FlameColor, noise.a);
                
                // Path 2
                float2 uv2 = i.uv;
                uv2 *= _FadeScale;
                uv2 += (_FadeControl * _Time.y) * _DistortionAmount;
                float noise2 = voronoi(uv2 * 10.0);
                noise2 = pow(noise2, _FadePower);
                noise2 *= tex2D(_FlameShape, i.uv).a;
                c *= noise2;
                return c;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
