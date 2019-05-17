Shader "pokoro/cartoon"
{
    Properties
    {
        [HDR]
        _AmbientColor("Ambient Color", Color) = (0.4, 0.4, 0.4, 1)
        _Color("Color", Color) = (1, 0.5, 0, 1)
        // _LightBands("LightBands", Int) = 2;     //Default to light and dark
        
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags 
        { 
            "RenderType"="Opaque" 
            "LightMode" = "ForwardBase"
            "PassFlags" = "OnlyDirectional"
        }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"
            #include "Lighting.cginc"

            struct appdata
            {
                //Stuff in here are populated automatically
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                //Stuff in here must be manually populated in the vertex shader
                float4 vertex : SV_POSITION;
                float3 worldNormal : NORMAL;
                
                UNITY_FOG_COORDS(1)
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                
                //Transform the normal from object space to world space
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                
                return o;
            }
            
            //QUESTION do these have to be here?
            float4 _AmbientColor;
            float4 _Color;
            // int _LightBands;

            fixed4 frag (v2f i) : SV_Target
            {
                float3 normal = normalize(i.worldNormal);
                float NdotL = dot(_WorldSpaceLightPos0, normal);    //dot product of normal and light position
                
                //Two lighting bands
                float lightIntensity = NdotL > 0 ? 1 : 0;
                    //Multiple lighting bands [Extra task: What if we wanted more than two discrete bands of shading?]
                            
                //Include light color
                float4 light = lightIntensity * _LightColor0;   //LightColor0 is the color of the main directional light
                    
                // sample the texture
                fixed4 sample = tex2D(_MainTex, i.uv);
                                
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                
                return _Color * sample * (_AmbientColor + light);
            }
            ENDCG
        }
    }
}
