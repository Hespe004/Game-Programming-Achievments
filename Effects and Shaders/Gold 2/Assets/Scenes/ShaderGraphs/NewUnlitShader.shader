Shader "Unlit/NewUnlitShader"
{
    Properties
    {
        _Color ("Main Color", Color) = (1,0,0,1)
        _Color2 ("Second Color", Color) = (0,1,0,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag

            #include "UnityCG.cginc"

            fixed4 _Color;
            fixed4 _Color2;

            fixed4 frag (v2f_img i) : SV_Target
            {
                float delta = (sin(_Time.y)+1/2);
                //place more code i dont know here
                return _Color;
            }
            ENDCG
        }
    }
}
