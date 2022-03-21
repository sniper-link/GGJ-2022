Shader "Gearswell/SmartMaterial"
{
    Properties
    {
        _Color("Color", Color) = (1,1,1,1)
        _MainTex("Albedo (RGB)", 2D) = "white" {}
        _Glossiness("Smoothness", Range(0,1)) = 0.5
        _Metallic("Metallic", Range(0,1)) = 0.0
        _Scale("UV Scale", Range(0, 1)) = 0.5
    }
        SubShader
        {
            Tags { "RenderType" = "Opaque" }
            LOD 200

            CGPROGRAM
            // Physically based Standard lighting model, and enable shadows on all light types
            #pragma surface surf Standard fullforwardshadows

            // Use shader model 3.0 target, to get nicer looking lighting
            #pragma target 3.0

            struct Input
            {
                float3 worldNormal;
                float3 worldPos;
            };

            sampler2D _MainTex;
            half _Glossiness;
            half _Metallic;
            fixed4 _Color;
            float _Scale;

            // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
            // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
            // #pragma instancing_options assumeuniformscaling
            UNITY_INSTANCING_BUFFER_START(Props)
                // put more per-instance properties here
            UNITY_INSTANCING_BUFFER_END(Props)

            void surf(Input IN, inout SurfaceOutputStandard o)
            {
                float2 UV;
                fixed4 c;
                if (abs(IN.worldNormal.x) > 0.5) {
                    UV = IN.worldPos.y;// + UVOffset.y; // side
                          c = tex2D(_MainTex, (UV * _Scale + float2(1, 1))); // use WALLSIDE texture
                }
                else if (abs(IN.worldNormal.z) > 0.5) {
                    UV = IN.worldPos.xy; // front
                    c = tex2D(_MainTex, UV * _Scale); // use WALL texture
                }
                else {
                    UV = IN.worldPos.xz; // top
                    c = tex2D(_MainTex, (UV * _Scale + float2(0.5, 1))); // use FLR texture
                }

                o.Albedo = c.rgb * _Color;
                o.Metallic = _Metallic;
                //o.Specular = _Metallic;
                o.Smoothness = _Glossiness;
            }
            ENDCG
        }
            FallBack "Diffuse"
}

//Shader "Custom/World UV Test" {
//
//    Properties{
//    _Color("Main Color", Color) = (1,1,1,1)
//    _MainTexWall2("Wall Side Texture (RGB)", 2D) = "surface" {}
//    _MainTexWall("Wall Front Texture (RGB)", 2D) = "surface" {}
//    _MainTexFlr2("Flr Texture", 2D) = "surface" {}
//    _Scale("Texture Scale", Float) = 0.1
//    }
//
//    SubShader{
//
//    Tags { "RenderType" = "Opaque" }
//
//    CGPROGRAM
//    #pragma surface surf Lambert
//
//    struct Input {
//      float3 worldNormal;
//      float3 worldPos;
//    };
//
//    sampler2D _MainTexWall;
//    sampler2D _MainTexWall2;
//    sampler2D _MainTexFlr2;
//    float4 _Color;
//    float _Scale;
//
//    void surf(Input IN, inout SurfaceOutput o) {
//      float2 UV;
//      fixed4 c;
//
//      if (abs(IN.worldNormal.x) > 0.5) {
//          UV = IN.worldPos.yz; // side
//          c = tex2D(_MainTexWall2, UV * _Scale); // use WALLSIDE texture
//      }
//      else if (abs(IN.worldNormal.z) > 0.5) {
//          UV = IN.worldPos.xy; // front
//          c = tex2D(_MainTexWall, UV * _Scale); // use WALL texture
//      }
//      else {
//          UV = IN.worldPos.xz; // top
//          c = tex2D(_MainTexFlr2, UV * _Scale); // use FLR texture
//      }
//
//      o.Albedo = c.rgb * _Color;
//    }
//
//    ENDCG
//    }
//
//        Fallback "VertexLit"
//}
