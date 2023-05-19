Shader "Custom/NewSurfaceShader" {
    Properties {
        _Color ("Color", Color) = (1,1,1,1)
        _Color1 ("Color (1)", Color) = (1,1,1,1)
    }
    SubShader {
        Tags { "RenderType"="Opaque" "Queue"="Geometry" }
        LOD 100

        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata {
                float4 vertex : POSITION;
            };

            struct v2f {
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                return o;
            }

            float3 Hash3(float3 p) { return frac(sin(dot(p, float3(127.1, 311.7, 74.7))) * 43758.5453); }

            float3 Hash3v(float3 p) { return Hash3(p) * 2.0 - 1.0; }

            float Voronoi(float3 x, out float3 id) {
                float3 p = floor(x);
                float3 f = frac(x);
                float md = 8.0;
                float3 m = float3(0.0, 0.0, 0.0);
                for (int k = -1; k <= 1; k++) {
                    for (int j = -1; j <= 1; j++) {
                        for (int i = -1; i <= 1; i++) {
                            float3 b = float3(float(i), float(j), float(k));
                            float3 r = b - f + Hash3(p + b);
                            float d = dot(r, r);
                            if (d < md) {
                                md = d;
                                m = p + b;
                            }
                        }
                    }
                }
                id = m;
                return md;
            }

            float3 GetColor(float3 uvw) {
                float3 id;
                float f = Voronoi(uvw, id);
                float3 c = 1.0 - f;
                return c;
            }

            float4 _Color;
            float4 _Color1;
            
            float4 frag (v2f i) : SV_Target {
                float2 uv = i.vertex.xy / _ScreenParams.xy;
                float time = _Time.y * 0.5;
                float3 uvw = float3(uv, time);
                float3 col = GetColor(uvw);
                float3 col1 = GetColor(uvw + float3(0.0, 0.0, 10.0));
                float c = pow(col1.x, 3.0);
                float3 finalColor = lerp(_Color.rgb, _Color1.rgb, c);
                return float4(finalColor, 0.0);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
