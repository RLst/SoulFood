Shader "pokoro/cartoon"
{
    Properties
    {
        [HDR]
        _AmbientColor("Ambient", Color) = (0.4, 0.4, 0.4, 1)
        _DiffuseColor("Diffuse", Color) = (1, 0.5, 0, 1)
        [HDR]
        _SpecularColor("Specular", Color) = (0.9, 0.9, 0.9, 1)
        [HDR]
        _RimColor("Rim", Color) = (1,1,1,1)
        
        [Space]
        _Glossiness("Glossiness", Float) = 32
        _RimAmount("Rim Amount", Range(0,1)) = 0.716
        _RimThreshold("Rim Threshold", Range(0,1)) = 0.1
        
        [Space]
        _LightBands("LightBands [Not Implemented Yet]", Int) = 2      //Default to light and dark
        _EdgeSmooth("Edge Smoothing", float) = 0.01
        _SpecularEdgeSmooth("Specular Edge Smoothing", float) = 0.01
        _RimEdgeSmoothing("Rim Edge Smoothing", float) = 0.01
        
        [Space]
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
                float3 viewDir : TEXCOORD1;
                
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
                o.viewDir = WorldSpaceViewDir(v.vertex);
                
                return o;
            }
            
            //Matching variables
            //QUESTION do these have to be here?
            float4 _AmbientColor;
            float4 _DiffuseColor;
            float4 _SpecularColor;
            float4 _RimColor;
            
            float _Glossiness;
            float _RimAmount;
            float _RimThreshold;
            
            int _LightBands;
            float _EdgeSmooth;
            float _SpecularEdgeSmooth;
            float _RimEdgeSmoothing;
            
            
            fixed4 frag (v2f i) : SV_Target
            {
                float3 normal = normalize(i.worldNormal);
                float NdotL = dot(_WorldSpaceLightPos0, normal);    //dot product of normal and light position
                
                //Calculate lighting intensity [Extra task: What if we wanted more than two discrete bands of shading?]
                float lightIntensity = smoothstep(0, _EdgeSmooth, NdotL);
                float4 light = lightIntensity * _LightColor0;   //LightColor0 is the color of the main directional light
                    
                //Calculate specular intensity
                float3 viewDir = normalize(i.viewDir);
                float3 halfVector = normalize(_WorldSpaceLightPos0 + viewDir);
                float NdotH = dot(normal, halfVector);
                float specularIntensity = pow(NdotH * lightIntensity, _Glossiness * _Glossiness);   //Glossiness is multiplied by itself to allow smaller values int he material editor to have a larger effect, and make it easier to work with the shader
                float specularIntensitySmooth = smoothstep(0.005, _SpecularEdgeSmooth, specularIntensity);
                float4 specular = specularIntensitySmooth * _SpecularColor;
                
                //Calculate rim lighting
                float4 rimDot = 1 - dot(viewDir, normal);   //Calculate the rim by taking the dot product of the normal and the view direction and inverting it
				float rimIntensity = rimDot * pow(NdotL, _RimThreshold);
				rimIntensity = smoothstep(_RimAmount - _RimEdgeSmoothing, _RimAmount + _RimEdgeSmoothing, rimIntensity);
				float4 rim = rimIntensity * _RimColor;
				                    
                // sample the texture
                fixed4 sample = tex2D(_MainTex, i.uv);
                                
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                
                return _DiffuseColor * sample * (_AmbientColor + light + specular + rim);
            }
            ENDCG
        }
    }
}
