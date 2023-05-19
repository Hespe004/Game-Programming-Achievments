Shader "Custom/NewSurfaceShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BumpMap ("Normal Map", 2D) = "bump" {}
        _Color ("Color", Color) = (1,1,1,1)
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
        sampler2D _BumpMap;
        fixed4 _Color;
        float _Glossiness;
        float _Metallic;

        struct Input
        {
            float2 uv_MainTex;
            float3 worldPos;
            float3 worldNormal;
            INTERNAL_DATA
            #ifdef _WORLDNORMALTANGENT
                float3 worldTangent;
                float3 worldBiTangent;
            #endif
        };


        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        // Standard lighting function
        void surf (Input IN, inout SurfaceOutputStandard o) {
            // Albedo
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;

            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;

            // Normal mapping
            o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_MainTex));

            // Reflection
            float3 worldRefl = reflect(normalize(IN.worldPos.xyz - _WorldSpaceCameraPos.xyz), IN.worldNormal.xyz);
            o.Emission = c.rgb * pow(max(0.0, dot(worldRefl, IN.worldNormal.xyz)), 10.0);
        }
        ENDCG
    }
    FallBack "Diffuse"
}
