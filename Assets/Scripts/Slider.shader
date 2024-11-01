Shader "Experiment3/Slider"
{
    Properties
    {
        [NoScaleOffset]_MainTex ("Texture", 2D) = "white" {}
        _SliderValue("Slider Value", Range(0,1)) = 1
        _BorderSize("Boder Size", Range(0,0.5)) = 0.11
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        Pass
        {
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha //Alpha blending

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
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _SliderValue;
            float _BorderSize;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }
            float InverseLerp(float a, float b, float v){
                return (v-a)/(b-a);
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 coord =  i.uv;
                coord.x *= 8;
                float2 pointOnLineSeg = float2(clamp(coord.x, 0.5, 7.5),0.5);
                float sdf = distance(coord, pointOnLineSeg) * 2 -1;
                clip(-sdf);

                float borderSDF = sdf + _BorderSize;
                float pd = fwidth(borderSDF);
                float borderMask = 1-saturate(borderSDF/pd);

                float sliderMask = _SliderValue > i.uv.x;  
                float3 sliderColor= tex2D(_MainTex, float2(_SliderValue, i.uv.y));

                if(_SliderValue< 0.2){
                    float flash = cos(_Time.y * 4) * 0.4 + 1;
                    sliderColor *= flash; 
                }

                return float4(sliderColor *sliderMask* borderMask,sliderMask);
            }
            ENDCG
        }
    }
}
